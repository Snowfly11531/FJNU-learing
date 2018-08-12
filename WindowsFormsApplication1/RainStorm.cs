using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using DamBreakModelApplication;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace CAFloodModel
{
    public partial class RainStorm : Form
    {
        struct Dirction {
            public int x ;
            public int y ;
        }
        //输入变量
        IRasterLayer demRaster = new RasterLayer();
        IRasterLayer manningRaster = new RasterLayer();
        IFeatureLayer DBpointshp = new FeatureLayer();
        IRasterLayer rainDeepRaster = new RasterLayer();
        List<string[]> rainRecordList;
        string DemPath, ManningPath, RainRecordPath;
        private int stepTime = 1;//计算步长
        long simTime, outStep;
        string Outpath = null;
        //dem
        float nodataValue = -9999;
        public float[,] dem;
        public float flowLength, flowLength2;//flowLength即分辨率40m
        int rowCount, colCount; //数据行数，列数 
        //降雨
        public float[,] rainDeepMat;
        //流速计算
        public float[,] manNing, slope, flowVel;
        Dirction[,] flowDir;
        public float maxSpeed = 10f;
        public float velLoss = 0.3f;

        //演进过程
        bool[,] canCuculate;
        public float[,] waterDeep, tempDeep;

        //记录变量
        public int submergeCount = 0;//淹没面积
        //
        ProcessManager pm;
        Thread thread;

        private void button1_Click(object sender, EventArgs e)
        {
            this.writeDefault();
            thread = new Thread(new ThreadStart(CalculateFlood));
            thread.Start();
            this.button1.Enabled = false;
        }
        public RainStorm()
        {
            InitializeComponent();
            txbDemIpute.Text = @"DEM.img";
            txbManingInput.Text = @"Manning.img";
            txbOutpath.Text = @"out";
            txbRaintxt.Text = @"rain.txt";
            txbSimTime.Text = "";
            txbOutstep.Text = "";
        }
        void CalculateFlood()
        {
             pm = new ProcessManager();
            simTime = Convert.ToInt64(txbSimTime.Text);
            outStep = Convert.ToInt64(txbOutstep.Text);
            //MaxExchangeRate = Convert.ToSingle(txbExRate.Text)/ 100;
            if (outStep < stepTime)
            {
                MessageBox.Show("输出步长应该大于1s!");
                return;
            }
            this.Invoke(pm.createProcessWindow());  //弹出进度窗体
            this.Invoke(pm.setProcess, new object[] { 0, Convert.ToInt32(simTime) });
            int time = 0, number = 1;
            this.Invoke(pm.updateProcess, new object[] { string.Format("数据正在初始化："), 0 });
            //初始化模拟数据
            if (!FloodStart())
            {
                this.Invoke(pm.addInfomation, new object[] { "初始化数据失败！" });
                return;
            }
            else {
                this.Invoke(pm.addInfomation, new object[] { "初始化数据成功！" });
            }
            //溃坝计算
            for (int i = 0; i <= simTime; i += stepTime)
            {
                if (!calcuRain(i, rainRecordList)) {
                    this.Invoke(pm.addInfomation, new object[] { "获取净雨强度数据失败，程序结束" });
                    return;
                }
                calcuSlopeDir();//计算坡度和水流流
                calcuVelocity();  //计算流速
                calcuFlowProcess();  //计算水流过程
                //判断是否到达输出步长
                Outpath = txbOutpath.Text;
                if (time >= outStep)
                {
                    this.Invoke(pm.addInfomation, new object[] { String.Format("正在生成第{0}幅淹没图像...", number.ToString())});
                    OutputRaster(Outpath, number);
                    this.Invoke(pm.addInfomation, new object[] { String.Format("第{0}幅淹没图像生成成功", number.ToString()) });
                    time = 0;
                    number += 1;
                }
                this.Invoke(pm.updateProcess, new object[] { "已经计算：" + i + "s;   模拟总时长" + simTime + "s", i });
                time = time + stepTime;
            }
        }

        Boolean FloodStart()
        {
            //读入dem，manning，rain
            this.Invoke(pm.addInfomation, new object[] { "正在读取DEM文件..." });
            DemPath = txbDemIpute.Text;
            GISdataManager.readRaster(DemPath, ref demRaster);
            dem = GISdataManager.Raster2Mat(demRaster);
            this.Invoke(pm.addInfomation, new object[] { "DEM数据读取完毕，并写入DEM数组中" });
            this.Invoke(pm.addInfomation, new object[] { "正在读取曼宁系数文件..." });
            ManningPath = txbManingInput.Text;
            GISdataManager.readRaster(ManningPath, ref manningRaster);
            manNing = GISdataManager.Raster2Mat(manningRaster);
            this.Invoke(pm.addInfomation, new object[] { "曼宁系数数据读取完毕，并写入曼宁系数数组中" });
            RainRecordPath = txbRaintxt.Text;
            rainRecordList = TxTReader.txt2List2(RainRecordPath);
            this.Invoke(pm.addInfomation, new object[] { "正在判断雨量格式是否正确..." });
            if (rainRecordList.Count > 0)
            {
                this.Invoke(pm.addInfomation, new object[] { "雨量数据格式正确" });
            }
            else
            {
                this.Invoke(pm.addInfomation, new object[] { "请重新输入有效降雨数据" });
                return false;
            }
            //获取栅格分辨率
            IRasterInfo rasterinfo = (demRaster.Raster as IRawBlocks).RasterInfo;
            flowLength = Convert.ToInt32(rasterinfo.CellSize.X);
            this.Invoke(pm.addInfomation, new object[] {String.Format("DEM栅格宽度为：{0}m",flowLength) });
            //maxSpeed = (float)Math.Sqrt(flowLength * 10);
            maxSpeed = 10f;
            flowLength2 = flowLength * 1.141f;
            //判断DEM与曼宁系数栅格是否一致！
            if (demRaster.ColumnCount != manningRaster.ColumnCount || demRaster.RowCount != manningRaster.RowCount)
            {
                this.Invoke(pm.addInfomation, new object[] { "曼宁糙率栅格行列与DEM不一致，请对应后重新输入！" });
                return false;
            }
            rowCount = demRaster.RowCount;
            colCount = demRaster.ColumnCount;
            Console.WriteLine(rowCount+ ","+ colCount);
            //初始化中间参数
            slope = new float[colCount,rowCount];          //坡度矩阵
            flowDir = new Dirction[colCount,rowCount];         //流向矩阵
            canCuculate = new bool[colCount,rowCount];     //用于判断矩阵中的点是否在DEM范围内
            waterDeep = new float[colCount,rowCount];      //水深矩阵
            tempDeep = new float[colCount,rowCount];       //
            flowVel = new float[colCount,rowCount];
            for (int i = 0; i < colCount; i++)
                for (int j = 0; j < rowCount; j++)
                {
                    slope[i, j] = 0f;
                    flowDir[i, j] = new Dirction() { x=0,y=0};
                    if (dem[i, j] == nodataValue) waterDeep[i, j] = nodataValue;
                    else waterDeep[i, j] = 0f;
                    tempDeep[i, j] = 0f;
                    flowVel[i, j] = 0f;
                }

            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (dem[i,j] != nodataValue)
                    {
                        canCuculate[i,j] = true;
                    }
                }
            }
            return true;
        }
        Boolean calcuRain(int time, List<string[]> RainRecordList)
        {
            if (rainRecordList.Count != 0)
            {
                string[] record = RainRecordList[0];
                int rainTime = Convert.ToInt32(record[0]);
                if (time == 0) rainTime = 0;
                if (time >= rainTime)
                {
                    if (time != 0) RainRecordList.RemoveAt(0);
                    if (rainRecordList.Count == 0) return true;
                    record = RainRecordList[0];
                    int nextTime = Convert.ToInt32(record[0]);
                    string path = record[1];
                    this.Invoke(pm.addInfomation, new object[] { String.Format("正在读取{1}-{2}s雨量数据，路径为：{0}", path, rainTime, nextTime) });
                    try
                    {
                        GISdataManager.readRaster(path, ref rainDeepRaster);
                        rainDeepMat = GISdataManager.Raster2Mat(rainDeepRaster);
                        Parallel.For(0, colCount, i =>
                        {
                            for (int j = 0; j < rowCount; j++)
                            {
                                ////如果到达数据边界就不参与计算
                                if (i - 1 < 0 || i + 1 > colCount - 1 || j - 1 < 0 || j + 1 > rowCount - 1)
                                {
                                    canCuculate[i, j] = false;
                                    continue;
                                }

                                if (dem[i - 1, j - 1] < nodataValue || dem[i - 1, j + 1] < nodataValue || dem[i + 1, j - 1] < nodataValue || dem[i + 1, j + 1] < nodataValue ||
                                    dem[i - 1, j] < nodataValue || dem[i, j + 1] < nodataValue || dem[i, j - 1] < nodataValue || dem[i + 1, j] < nodataValue || dem[i, j] < nodataValue)
                                {
                                    canCuculate[i, j] = false;
                                    continue;
                                }
                                if (rainDeepMat[i - 1, j - 1] < nodataValue || rainDeepMat[i - 1, j + 1] < nodataValue || rainDeepMat[i + 1, j - 1] < nodataValue || rainDeepMat[i + 1, j + 1] < nodataValue ||
                                    rainDeepMat[i - 1, j] < nodataValue || rainDeepMat[i, j + 1] < nodataValue || rainDeepMat[i, j - 1] < nodataValue || rainDeepMat[i + 1, j] < nodataValue || rainDeepMat[i, j] < nodataValue)
                                {
                                    canCuculate[i, j] = false;
                                    continue;
                                }
                                if (manNing[i - 1, j - 1] < nodataValue || manNing[i - 1, j + 1] < nodataValue || manNing[i + 1, j - 1] < nodataValue || manNing[i + 1, j + 1] < nodataValue ||
                                    manNing[i - 1, j] < nodataValue || manNing[i, j + 1] < nodataValue || manNing[i, j - 1] < nodataValue || manNing[i + 1, j] < nodataValue || manNing[i, j] < nodataValue)
                                {
                                    canCuculate[i, j] = false;
                                    continue;
                                }
                            }
                        });
                        this.Invoke(pm.addInfomation, new object[] { String.Format("读取{0}-{1}s的净雨强度读取成功", rainTime, nextTime) });
                    }
                    catch (Exception e)
                    {
                        this.Invoke(pm.addInfomation, new object[] { String.Format("读取{0}-{1}s的净雨强度读取失败,错误代码为{2}", rainTime, nextTime, e.ToString()) });
                        return false;
                    }
                }
                for (int i = 0; i < colCount; i++)
                {
                    for (int j = 0; j < rowCount; j++)
                    {
                        if (rainDeepMat[i, j] != -9999 && dem[i, j] != -9999 && canCuculate[i, j])
                        {
                            waterDeep[i, j] += rainDeepMat[i, j] * stepTime;
                        }
                    }
                }
            }
            return true;
        }
        void calcuSlopeDir()        //计算流向
        {
            Parallel.For(0, colCount, i =>
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (canCuculate[i, j])
                    {
                        if (waterDeep[i, j] <= 0f)//只计算有水的单元格
                        {
                            flowDir[i, j].x = 0;
                            flowDir[i, j].y = 0;
                            slope[i, j] = 0;
                            continue;
                        }
                        float high = dem[i, j] + waterDeep[i, j];
                        float[] s = new float[8];
                        s[0] = (high - dem[i - 1, j - 1] - waterDeep[i - 1, j - 1]) / flowLength2;
                        s[1] = (high - dem[i - 1, j] - waterDeep[i - 1, j]) / flowLength;
                        s[2] = (high - dem[i - 1, j + 1] - waterDeep[i - 1, j + 1]) / flowLength2;
                        s[3] = (high - dem[i, j - 1] - waterDeep[i, j - 1]) / flowLength;
                        s[4] = (high - dem[i, j + 1] - waterDeep[i, j + 1]) / flowLength;
                        s[5] = (high - dem[i + 1, j - 1] - waterDeep[i + 1, j - 1])/flowLength2;
                        s[6] = (high - dem[i + 1, j] - waterDeep[i + 1, j]) / flowLength;
                        s[7] = (high - dem[i + 1, j + 1] - waterDeep[i + 1, j + 1]) / flowLength2;
                        float max = s.Max();
                        int index = max > 0 ? Array.IndexOf(s, max) : -1;
                        switch (index)
                        {
                            case 0:
                                flowDir[i, j].x = -1; flowDir[i, j].y = -1;
                                break;
                            case 1:
                                flowDir[i, j].x = -1; flowDir[i, j].y = 0;
                                break;
                            case 2:
                                flowDir[i, j].x = -1; flowDir[i, j].y = 1;
                                break;
                            case 3:
                                flowDir[i, j].x = 0; flowDir[i, j].y = -1;
                                break;
                            case 4:
                                flowDir[i, j].x = 0; flowDir[i, j].y = 1;
                                break;
                            case 5:
                                flowDir[i, j].x = 1; flowDir[i, j].y = -1;
                                break;
                            case 6:
                                flowDir[i, j].x = 1; flowDir[i, j].y = 0;
                                break;
                            case 7:
                                flowDir[i, j].x = 1; flowDir[i, j].y = 1;
                                break;
                            default:
                                flowDir[i, j].x = 0; flowDir[i, j].y = 0;
                                break;
                        }
                        slope[i, j] = max > 0 ? max : 0;
                    }
                }
            });
        }     
        void calcuVelocity()        //计算流速
        {
            Parallel.For(0, colCount, i =>
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (canCuculate[i, j])
                    {
                        if ((flowDir[i, j].x == 0 && flowDir[i, j].y == 0) || manNing[i, j] <= 0f)
                        {
                            flowVel[i, j] = 0f;
                        }
                        else
                        {
                            float speed = Convert.ToSingle(Math.Pow(waterDeep[i, j], 0.666f) * Math.Pow(slope[i, j], 0.5f) / manNing[i, j]);//曼宁公式
                            flowVel[i, j] += speed;
                            flowVel[i, j] = flowVel[i, j] > maxSpeed ? maxSpeed : flowVel[i, j];
                            //if (waterDeep[i, j] == 0)
                            //{
                            //    flowVel[i + flowDir[i, j].x, j + flowDir[i, j].y] += flowVel[i, j] * velLoss;   //Vc2=d*Vc1+V
                            //    flowVel[i + flowDir[i, j].x, j + flowDir[i, j].y] =
                            //        flowVel[i + flowDir[i, j].x, j + flowDir[i, j].y] > maxSpeed ? maxSpeed : flowVel[i + flowDir[i, j].x, j + flowDir[i, j].y];
                            //}
                        }
                    }

                }
            });
        }
        void calcuFlowProcess()     //计算流动过程的水量分配,并将需要重新分配的雨量存入零时雨量数据tempDeep中，然后将rainDeep和tempDeep叠加
        {
            //计算水深
            Parallel.For(0, colCount, i => { 
                for (int j = 0; j < rowCount; j++)
                {
                    if (canCuculate[i, j] && !(flowDir[i, j].x == 0 && flowDir[i, j].y == 0))
                    {
                        float flowTime = 0f;
                        if (flowDir[i, j].x != 0 && flowDir[i, j].y != 0) flowTime = flowLength2 / flowVel[i, j];
                        else flowTime = flowLength / flowVel[i, j];
                        if (flowTime <= stepTime)//时间间隔内全部流完
                        {
                            float water = waterDeep[i, j];
                            tempDeep[i + flowDir[i, j].x, j + flowDir[i, j].y] += water;
                            waterDeep[i, j] -= water;
                        }
                        else
                        {
                            float water = waterDeep[i, j] * stepTime / flowTime;
                            tempDeep[i + flowDir[i, j].x, j + flowDir[i, j].y] += water;
                            waterDeep[i, j] -= water;
                        }
                    }
                }
            });
            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (canCuculate[i, j] == true)
                    {
                        waterDeep[i, j] += tempDeep[i, j];
                        tempDeep[i, j] = 0f;
                        flowVel[i, j] = 0f;
                    }
                }
            }
        }
        void OutputRaster(string outpath, int number)
        {
            string path = outpath + number + ".img";
            float[,] tempRaster=new float[colCount,rowCount];
            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    tempRaster[i, j] = waterDeep[i, j] <= nodataValue ? 0 : waterDeep[i, j];
                }
            }
            GISdataManager.exportRasterData(path, demRaster, tempRaster);
        }

//取消按钮事件
        private void button2_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
                this.Invoke(pm.close);
                
                
            }
            this.Close();
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            this.UseWaitCursor = false;
        }

        private void btnOpenDEM_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入流域DEM数据";
                openDialog.Filter = "IMAGINE Image(*.img)|*.img";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                    txbDemIpute.Text = openDialog.FileName.ToString();
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

        private void btnOpenManing_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入流域曼宁糙率数据";
                openDialog.Filter = "IMAGINE Image(*.img)|*.img";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                    txbManingInput.Text = openDialog.FileName.ToString();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("数据加载失败，请重新加载！", "添加流域曼宁糙率数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnOpenTandR_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入时间-净雨强度栅格记录表";
                openDialog.Filter = "TXT(*.txt)|*.txt";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                   txbRaintxt.Text = openDialog.FileName.ToString();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("数据加载失败，请重新加载！", "添加时间-净雨强度栅格记录表", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnSaveFloodData_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Title = "淹没数据输出";
                //saveDlg.Filter = "Excel File(*.xls)|*.xls";
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    if (saveDlg.FileName.ToString() == "")
                    {
                        MessageBox.Show("请输入图层名称！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    }
                    else
                    {
                        txbOutpath.Text = saveDlg.FileName.ToString();
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

        private void RainStorm_Load(object sender, EventArgs e)
        {
            this.readDefault();
        }

        private void txbDemIpute_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
