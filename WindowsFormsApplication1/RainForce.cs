using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DamBreakModelApplication;

namespace CAFloodModel
{
    public partial class RainForce : Form
    {
        IRasterLayer infilLayer = null;
        float[,] mann = null;
        IRasterLayer rainLayer = null;
        float[,] rain = null;
        float[,] output = null;
        float noDataValue = -9999f;
        int rainTime = 3600;
        ProcessManager pm;
        Thread td;
        public RainForce()
        {
            InitializeComponent();
            this.readDefault();
        }
        private void btnRain_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择雨量文件存放目录";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                tBRainPath.Text = folder.SelectedPath; 
            }
        }

        private void btnInfil_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入曼宁数据";
                openDialog.Filter = "IMAGINE Image(*.img)|*.img";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                   tBInfilPath.Text = openDialog.FileName.ToString();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("数据加载失败，请重新加载！", "添加流域DEM数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择径流深文件存放目录";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                tBSavePath.Text = folder.SelectedPath;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.writeDefault();
            td = new Thread(new ThreadStart(calculateDepth));
            td.Start();
        }
        private void calculateDepth() {
            pm = new ProcessManager();
            this.Invoke(pm.createProcessWindow());
            try
            {
                this.Invoke(pm.addInfomation, new object[] { "正在读取最大下渗量图层..." });
                GISdataManager.readRaster(tBInfilPath.Text, ref infilLayer);
                mann = GISdataManager.Raster2Mat(infilLayer);
                this.Invoke(pm.addInfomation, new object[] { "读取最大下渗量图层成功" });
            }
            catch(Exception e) { }
            output = new float[infilLayer.ColumnCount, infilLayer.RowCount];
            int.TryParse(tBRainTime.Text, out rainTime);
            DirectoryInfo dirInfo = new DirectoryInfo(tBRainPath.Text);
            this.Invoke(pm.setProcess, new object []{ 0, dirInfo.GetFiles("*.img").Length });
            int key = 0;
            foreach(var info in dirInfo.GetFiles("*.img")){
                try
                {
                    this.Invoke(pm.updateProcess, new object[] { String.Format("正在读取{0}图层...", info.Name),key });
                    GISdataManager.readRaster(info.FullName, ref rainLayer);
                    rain = GISdataManager.Raster2Mat(rainLayer);
                    this.Invoke(pm.addInfomation, new object[] { String.Format("读取{0}图层成功",info.Name) });
                }
                catch (Exception e) { }
                if (infilLayer.ColumnCount != rainLayer.ColumnCount || infilLayer.RowCount != rainLayer.RowCount) {
                    this.Invoke(pm.addInfomation, new object[] { String.Format("该雨量图层与最大下渗量图层不匹配，跳过") });
                    continue;
                }
                this.Invoke(pm.updateProcess, new object[] { String.Format("正在存储第{0}图层...", key+1), key });
                Parallel.For(0, infilLayer.ColumnCount, i =>
                {
                    for (int j = 0; j < infilLayer.RowCount; j++)
                    {
                        if (mann[i, j] > noDataValue)
                        {
                            float pureRain = rain[i, j] - mann[i, j] * 0.2f;
                            if (pureRain > 0) output[i, j] = (pureRain * pureRain / (pureRain + mann[i, j]))/(rainTime*1000);
                            else output[i, j] = 0f;
                        }
                        else {
                            output[i, j] = 0f;
                        }
                    }
                });
                var name=info.FullName;
                var groups=Regex.Match(name,@"(\d+.img)").Groups;
                GISdataManager.exportRasterData(tBSavePath.Text+"\\rain" + groups[0].Value, infilLayer, output);
                this.Invoke(pm.addInfomation, new object[] { String.Format("存储第{0}图层成功", key+1) });
                key++;
            }
        }
    }
}
