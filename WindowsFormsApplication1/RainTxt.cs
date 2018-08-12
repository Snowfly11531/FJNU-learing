using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CAFloodModel
{
    public partial class RainTxt : Form
    {
        public RainTxt()
        {
            InitializeComponent();
            this.readDefault();
            if (tBRainPath.Text.Length != 0) {
                DirectoryInfo dirInfo = new DirectoryInfo(tBRainPath.Text);
                List<string> nameList = new List<string>();
                foreach (var info in dirInfo.GetFiles("*.img"))
                {
                    nameList.Add(info.Name);
                }
                userControl11.addlist(nameList);
            }
        }
        public void addList(){
            
        }
        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void btnRain_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择净雨强度文件存放目录";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                tBRainPath.Text = folder.SelectedPath;
                DirectoryInfo dirInfo=new DirectoryInfo(tBRainPath.Text);
                List<string> nameList=new List<string>();
                foreach(var info in dirInfo.GetFiles("*.img")){
                    nameList.Add(info.Name);
                }
                userControl11.addlist(nameList);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.writeDefault();
            int time=3600;
            int timeInternal = 1;
            int.TryParse(tBInternal.Text,out time);
            List<string> names=userControl11.getList();
            FileStream fs = new FileStream(tBSavePath.Text, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach(var name in names){
                sw.WriteLine(time*timeInternal+","+tBRainPath.Text+"\\"+name);
                timeInternal++;
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            MessageBox.Show("生成时间雨型成功！");
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Title = "时间雨型数据输出";
                saveDlg.Filter = "文本文件(*.txt)|*.txt";
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    tBSavePath.Text = saveDlg.FileName.ToString();
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
    }
}
