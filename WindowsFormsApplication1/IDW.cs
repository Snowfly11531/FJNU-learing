using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DamBreakModelApplication;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesRaster;
using System.IO;
using ESRI.ArcGIS.SpatialAnalyst;

namespace CAFloodModel
{
    public partial class IDW : Form
    {
        ProcessManager pm;
        Thread thread;
        IFeatureLayer pointLayer = null;
        IRasterLayer refLayer = null;
        String path = null;
        String filePath = null;
        String fileName = null;
        public IDW()
        {
            InitializeComponent();
            this.readDefault();
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入插值点";
                openDialog.Filter = "Shapefile(*.shp)|*.shp";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                    tBPointPath.Text = openDialog.FileName.ToString();
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

        private void btnRef_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入参考栅格数据";
                openDialog.Filter = "IMAGINE Image(*.img)|*.img";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                    tBRefPath.Text = openDialog.FileName.ToString();
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
            try
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Title = "插值数据输出";
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    if (saveDlg.FileName.ToString() == "")
                    {
                        MessageBox.Show("请输入图层名称！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    }
                    else
                    {
                        tBSavePath.Text = saveDlg.FileName.ToString();
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("数据输出错误，请重新计算！", "输出等流时单元数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.writeDefault();
            thread = new Thread(new ThreadStart(calculateIDW));
            thread.Start();
        }
        private void calculateIDW() {
            pm = new ProcessManager();
            this.Invoke(pm.createProcessWindow());
            try
            {
                GISdataManager.readSHP(tBPointPath.Text, ref pointLayer);
                this.Invoke(pm.addInfomation, new object[] { "读取point图层成功" });
            }
            catch (Exception e) { 
                
            }
            try
            {
                GISdataManager.readRaster(tBRefPath.Text, ref refLayer);
                this.Invoke(pm.addInfomation, new object[] { "读取参考图层成功" });
            }
            catch (Exception e) { 
                
            }
            path = tBSavePath.Text;
            fileName = Path.GetFileName(path);
            filePath = Path.GetDirectoryName(path);
            Console.WriteLine(fileName+","+filePath);
            IFeatureClass pointFeature = pointLayer.FeatureClass;
            IRaster refRaster = refLayer.Raster;
            List<String> fields = new List<string>();
            for (int i = 0; i < pointFeature.Fields.FieldCount;i++ )
            {
                var name=pointFeature.Fields.Field[i].Name;
                if (Regex.IsMatch(name, @"^\d+_\d+_\d+$")) {
                    fields.Add(name);
                }
            }
            this.Invoke(pm.setProcess, new object[] { 0, fields.Count });
            int key=1;
            foreach (var field in fields) {
                this.Invoke(pm.updateProcess, new object []{ string.Format("正在生成第{0}副图像", key), key });
                IDWInterpolation(pointFeature, refRaster, field);
                this.Invoke(pm.addInfomation, new object[] { string.Format("第{0}副图像生成成功", key) });
                key++;
            }
            
            this.Invoke(pm.close);
        }
        private void IDWInterpolation(IFeatureClass pointFeature,IRaster refRaster,string name) {
            IFeatureClassDescriptor pointDescript = new FeatureClassDescriptorClass();
            pointDescript.Create(pointFeature, null, name);
            object extend = (refRaster as IGeoDataset).Extent;
            IRasterProps refProps = refRaster as IRasterProps;
            object cell = refProps.MeanCellSize().X;
            IInterpolationOp interpla = new RasterInterpolationOpClass();
            IRasterAnalysisEnvironment IDWEnv = interpla as IRasterAnalysisEnvironment;
            IDWEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref cell);
            IDWEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue,ref extend);
            IGeoDataset output = interpla.IDW((IGeoDataset)pointDescript, 2, null, null);
            IGeoDataset input = refRaster as IGeoDataset;
            IExtractionOp op = new RasterExtractionOpClass();
            output=op.Raster(output, input);
            var clipRaster = (IRaster)output;
            ISaveAs pSaveAs = clipRaster as ISaveAs;
            IWorkspaceFactory workspaceFac = new RasterWorkspaceFactoryClass();
            var groups = Regex.Match(name, @"^(\d+)_(\d+)_(\d+)$").Groups;
            name = string.Format("{0:D2}{1:D2}", int.Parse(groups[2].Value), int.Parse(groups[3].Value));
            IDataset outDataset=pSaveAs.SaveAs(fileName+name+".img", workspaceFac.OpenFromFile(filePath, 0), "IMAGINE Image");
            System.Runtime.InteropServices.Marshal.ReleaseComObject(outDataset);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tBSavePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tBRefPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void tBPointPath_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
