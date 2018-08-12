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
using DamBreakModelApplication;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace CAFloodModel
{
    public partial class RS_DB : Form
    {
        //输入变量
        IRasterLayer demRaster = new RasterLayer();
        IRasterLayer manningRaster = new RasterLayer();
        IFeatureLayer DBpointshp = new FeatureLayer();
        IRasterLayer rainDeepRaster = new RasterLayer();
        List<string[]> rainRecordList;
        List<string[]> HydroRecordList = new List<string[]>();
        string DemPath, ManningPath, DBpointPath, HydroPath, RainRecordPath;
        private int stepTime = 1;//计算步长
        long simTime, outStep;
        string Outpath = null;

        //溃坝点
        int DPcolIndex, DProwIndex;
        List<int[]> dbPointList = new List<int[]>();

        //dem
        float nodataValue = -9999;
        public float[,] dem;
        public float flowLength, flowLength2;//flowLength即分辨率40m
        int rowCount, colCount; //数据行数，列数 

        //降雨
        public float[,] rainDeepMat;

        //流速计算
        public float[,] manNing, slope, flowVel;
        byte[,] flowDir;
        public float maxSpeed = 10f;


        //演进过程
        bool[,] arrived;
        bool[,] canCuculate;
        List<int[]> tempWaterGrid = new List<int[]>();
        public float[,] waterDeep, tempDeep;

        //记录变量
        public int submergeCount = 0;//淹没面积

        //真实运算时间
        //线程与精度条窗口
        Thread thread;
        ProcessManager pm;

        public RS_DB()
        {
            InitializeComponent();

            txbDemIpute.Text = @"DEM.img";
            txbManingInput.Text = @"Minning.img";
            txbOutpath.Text = @"out";
            txbSimTime.Text = "";
            txbOutstep.Text = "";
            //
            txbRaintxt.Text = @"rain.txt";
            txbHydroghraph.Text = @"Hydrograph.txt";
            txbDBpoint.Text = @"point.shp";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(CalculateFlood));
            thread.Start();
            this.button1.Enabled = false;
        }

        void CalculateFlood()
        {
            pm = new ProcessManager();
            simTime = Convert.ToInt64(txbSimTime.Text);
            outStep = Convert.ToInt64(txbOutstep.Text);
            if (outStep < stepTime)
            {
                MessageBox.Show("输出步长应该大于1s!");
                return;
            }
            this.Invoke(pm.createProcessWindow());
            this.Invoke(pm.setProcess, new object[] { 0, Convert.ToInt32(simTime) });
            int time = 0, number = 1;
            this.Invoke(pm.updateProcess, new object[] { string.Format("数据正在初始化："), 0 });
            //初始化模拟数据
            FloodStart();
            //溃坝计算
            for (int i = 0; i <= simTime; i += stepTime)
            {
                calcuRain(i, rainRecordList);
                addWater(i);//每个时间间隔内增加水流
                calcuSlopeDir();//计算坡度和水流流向
                calcuVelocity();  //计算流速
                calcuFlowProcess();  //计算水流过程


                //判断是否到达输出步长
                Outpath = txbOutpath.Text;
                if (time >= outStep)
                {
                    OutputRaster(Outpath, number);
                    time = 0;
                    number += 1;
                }
                this.Invoke(pm.updateProcess, new object[] { "已经计算：" + i + "s;   模拟总时长" + simTime + "s", i });
                time = time + stepTime;
            }
        }

        void FloodStart()
        {
            //读入dem，manning，rain
            DemPath = txbDemIpute.Text;
            GISdataManager.readRaster(DemPath, ref demRaster);
            dem = GISdataManager.Raster2Mat(demRaster);
            ManningPath = txbManingInput.Text;
            GISdataManager.readRaster(ManningPath, ref manningRaster);
            manNing = GISdataManager.Raster2Mat(manningRaster);
            RainRecordPath = txbRaintxt.Text;
            rainRecordList = TxTReader.txt2List2(RainRecordPath);
            if (rainRecordList.Count > 0)
            {
                string[] record = rainRecordList[0];
                string path;
                path = record[1];
                GISdataManager.readRaster(path, ref rainDeepRaster);
                rainDeepMat = GISdataManager.Raster2Mat(rainDeepRaster);
            }
            else
            {
                MessageBox.Show("请重新输入有效降雨数据！");
                return;
            }

            //获取栅格分辨率
            IRasterInfo rasterinfo = (demRaster.Raster as IRawBlocks).RasterInfo;
            flowLength = Convert.ToInt32(rasterinfo.CellSize.X);
            flowLength2 = flowLength * 1.141f;
            //判断DEM与曼宁系数栅格是否一致！
            if (demRaster.ColumnCount != manningRaster.ColumnCount || demRaster.RowCount != manningRaster.RowCount)
            {
                MessageBox.Show("请保证曼宁糙率栅格行列是否与DEM一致，请对应后重新输入！");
                return;
            }
            rowCount = demRaster.RowCount;
            colCount = demRaster.ColumnCount;
            //初始化中间参数
            slope = new float[rowCount, colCount];
            flowDir = new byte[rowCount, colCount];
            canCuculate = new bool[rowCount, colCount];
            waterDeep = new float[rowCount, colCount];
            tempDeep = new float[rowCount, colCount];
            flowVel = new float[rowCount, colCount];
            arrived = new bool[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    slope[i, j] = 0f;
                    flowDir[i, j] = 0;
                    waterDeep[i, j] = 0f;
                    tempDeep[i, j] = 0f;
                    flowVel[i, j] = 0f;
                    arrived[i, j] = false;
                }
            DBpointPath = txbDBpoint.Text;
            GISdataManager.readSHP(DBpointPath, ref DBpointshp);
            IFeatureClass featureClass = DBpointshp.FeatureClass;
            int count = featureClass.FeatureCount(new QueryFilter());
            for (int i = 0; i < count; i++)
            {
                IFeature feature = featureClass.GetFeature(i);
                IGeometry Geo = feature.Shape;
                IPoint point = Geo as IPoint;
                double x, y;
                x = point.X;
                y = point.Y;
                //获取出水点在Mit中的位置
                IRaster raster = demRaster.Raster;
                IRaster2 raster2 = raster as IRaster2;
                DPcolIndex = raster2.ToPixelColumn(x);
                DProwIndex = raster2.ToPixelRow(y);
                dbPointList.Add(new int[2] { DProwIndex, DPcolIndex });
                arrived[DProwIndex, DPcolIndex] = true;
            }
            //读取流量过程线表格
            HydroPath = txbHydroghraph.Text;
            HydroRecordList = TxTReader.txt2List3(HydroPath, dbPointList.Count);
            //初始化计算范围
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (dem[i, j] != nodataValue)
                    {
                        canCuculate[i, j] = true;
                    }
                }
            }

        }
        void calcuRain(int time, List<string[]> RainRecordList)
        {
            if (RainRecordList.Count == 0)
            {
                return;
            }
            string[] record = RainRecordList[0];
            int rainTime = Convert.ToInt32(record[0]);
            string path = record[1];
            if (time < rainTime)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        ////如果到达数据边界就不参与计算
                        if (i - 1 < 0 || i + 1 > rowCount - 1 || j - 1 < 0 || j + 1 > colCount - 1)
                        {
                            canCuculate[i, j] = false;
                            continue;
                        }

                        if (dem[i - 1, j - 1] == nodataValue || dem[i - 1, j + 1] == nodataValue || dem[i + 1, j - 1] == nodataValue || dem[i + 1, j + 1] == nodataValue)
                        {
                            canCuculate[i, j] = false;
                            continue;
                        }

                        if (rainDeepMat[i, j] != -9999 && dem[i, j] != -9999 && canCuculate[i, j])
                        {
                            waterDeep[i, j] += rainDeepMat[i, j] * stepTime;
                        }
                    }
                }
            }
            else
            {
                RainRecordList.RemoveAt(0);
                if (RainRecordList.Count > 0)
                {
                    record = RainRecordList[0];
                    rainTime = Convert.ToInt32(record[0]);
                    path = record[1];
                    GISdataManager.readRaster(path, ref rainDeepRaster);
                    rainDeepMat = GISdataManager.Raster2Mat(rainDeepRaster);
                    for (int i = 0; i < rowCount; i++)
                        for (int j = 0; j < colCount; j++)
                        {
                            if (i - 1 < 0 || i + 1 > rowCount - 1 || j - 1 < 0 || j + 1 > colCount - 1)
                            {
                                canCuculate[i, j] = false;
                                continue;
                            }

                            if (dem[i - 1, j - 1] == nodataValue || dem[i - 1, j + 1] == nodataValue || dem[i + 1, j - 1] == nodataValue || dem[i + 1, j + 1] == nodataValue)
                            {
                                canCuculate[i, j] = false;
                                continue;
                            }

                            if (rainDeepMat[i, j] != -9999 && dem[i, j] != -9999 && canCuculate[i, j])
                            {
                                if (canCuculate[i, j])
                                {
                                    waterDeep[i, j] += rainDeepMat[i, j] * stepTime;
                                }
                            }
                        }
                }
                else
                {
                    return;
                }
            }

        }
        void addWater(int time)
        {
            if (HydroRecordList.Count == 0)
            {
                return;
            }
            string[] record = HydroRecordList[0];
            int dbTime = Convert.ToInt32(record[0]);
            if (time < dbTime)
            {
                for (int i = 1; i < record.Length; i++)
                {
                    int[] item;
                    int rowIndex, colIndex;
                    item = dbPointList[i - 1];
                    rowIndex = item[0];
                    colIndex = item[1];
                    int OutVol = Convert.ToInt32(record[i]);
                    float h = OutVol / (flowLength * flowLength); //每个单元格增加的水深
                    waterDeep[rowIndex, colIndex] += h;
                }
            }
            else
            {
                HydroRecordList.RemoveAt(0);
                if (HydroRecordList.Count > 0)
                {
                    for (int i = 1; i < record.Length; i++)
                    {
                        int[] item;
                        int rowIndex, colIndex;
                        item = dbPointList[i - 1];
                        rowIndex = item[0];
                        colIndex = item[1];
                        int OutVol = Convert.ToInt32(record[i]);
                        float h = OutVol / (flowLength * flowLength); //每个单元格增加的水深
                        waterDeep[rowIndex, colIndex] += h;
                    }
                }
                else
                {
                    return;
                }

            }


        }
        void calcuSlopeDir()
        {
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    if (canCuculate[i, j])
                    {
                        float high = dem[i, j] + waterDeep[i, j];

                        float[] s = new float[8];
                        s[0] = (high - dem[i - 1, j - 1] - waterDeep[i - 1, j - 1]) / flowLength2;
                        s[1] = (high - dem[i - 1, j] - waterDeep[i - 1, j]) / flowLength;
                        s[2] = (high - dem[i - 1, j + 1] - waterDeep[i - 1, j + 1]) / flowLength2;
                        s[3] = (high - dem[i, j - 1] - waterDeep[i, j - 1]) / flowLength;
                        s[4] = (high - dem[i, j + 1] - waterDeep[i, j + 1]) / flowLength;
                        s[5] = (high - dem[i + 1, j - 1] - waterDeep[i + 1, j - 1]) / flowLength2;
                        s[6] = (high - dem[i + 1, j] - waterDeep[i + 1, j]) / flowLength;
                        s[7] = (high - dem[i + 1, j + 1] - waterDeep[i + 1, j + 1]) / flowLength2;

                        float max = s[0];
                        byte index = 0;
                        for (byte k = 1; k < 8; k++)
                        {
                            if (max < s[k])
                            {
                                max = s[k];
                                index = k;
                            }
                        }
                        if (waterDeep[i, j] <= 0f)//只计算有水的单元格
                        {
                            flowDir[i, j] = 0;
                        }
                        else
                            flowDir[i, j] = (byte)(index + 1);
                        slope[i, j] = max;
                    }
                }
        }
        void calcuVelocity()
        {
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    if (canCuculate[i, j])
                    {
                        if (waterDeep[i, j] <= 0f || slope[i, j] <= 0f || manNing[i, j] <= 0f)
                        {
                            flowVel[i, j] = 0f;
                        }
                        else
                        {
                            float speed = Convert.ToSingle(Math.Pow(waterDeep[i, j], 0.666f) * Math.Pow(slope[i, j], 0.5f) / manNing[i, j]);//曼宁公式
                            flowVel[i, j] = speed > maxSpeed ? maxSpeed : speed;
                        }
                    }

                }
        }
        void calcuFlowProcess()
        {
            //计算水深
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    if (canCuculate[i, j])
                    {
                        if (flowDir[i, j] == (byte)0)
                            continue;
                        float flowTime = 0;
                        #region
                        //1、向左上角流
                        if (flowDir[i, j] == 1)
                        {
                            flowTime = flowLength2 / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {
                                float water = waterDeep[i, j];
                                tempDeep[i - 1, j - 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i - 1, j - 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i - 1, j - 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i - 1, j - 1 });
                                arrived[i - 1, j - 1] = true;
                            }
                        }
                        //2、向上流
                        else if (flowDir[i, j] == 2)
                        {
                            flowTime = flowLength / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {
                                float water = waterDeep[i, j];
                                tempDeep[i - 1, j] += water;
                                waterDeep[i, j] -= water;
                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i - 1, j] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i - 1, j] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i - 1, j });
                                arrived[i - 1, j] = true;
                            }

                        }
                        //3、向右上角流
                        else if (flowDir[i, j] == 3)
                        {
                            flowTime = flowLength / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {
                                float water = waterDeep[i, j];
                                tempDeep[i - 1, j + 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i - 1, j + 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i - 1, j + 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i - 1, j + 1 });
                                arrived[i - 1, j + 1] = true;
                            }

                        }
                        //4、向左流
                        else if (flowDir[i, j] == 4)
                        {
                            flowTime = flowLength / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {
                                float water = waterDeep[i, j];
                                tempDeep[i, j - 1] += water;
                                waterDeep[i, j] = 0.0f;

                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i, j - 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i, j - 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i, j - 1 });
                                arrived[i, j - 1] = true;
                            }
                        }
                        //5、向右流
                        else if (flowDir[i, j] == 5)
                        {
                            flowTime = flowLength / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {

                                float water = waterDeep[i, j];
                                tempDeep[i, j + 1] += water;
                                waterDeep[i, j] = 0.0f;

                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i, j + 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i, j + 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i, j + 1 });
                                arrived[i, j + 1] = true;
                            }

                        }
                        //6、向左下流
                        else if (flowDir[i, j] == 6)
                        {
                            flowTime = flowLength / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {
                                float water = waterDeep[i, j];
                                tempDeep[i + 1, j - 1] += water;
                                waterDeep[i, j] = 0.0f;

                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i + 1, j - 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i + 1, j - 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i + 1, j - 1 });
                                arrived[i + 1, j - 1] = true;
                            }

                        }
                        //7、向下流
                        else if (flowDir[i, j] == 7)
                        {
                            flowTime = flowLength / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {
                                float water = waterDeep[i, j];
                                tempDeep[i + 1, j] += water;
                                waterDeep[i, j] = 0.0f;

                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i + 1, j] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i + 1, j] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i + 1, j });
                                arrived[i + 1, j] = true;
                            }

                        }
                        //8、向右下流
                        else if (flowDir[i, j] == 8)
                        {
                            flowTime = flowLength / flowVel[i, j];
                            if (flowTime <= stepTime)//时间间隔内全部流完
                            {
                                float water = waterDeep[i, j];
                                tempDeep[i + 1, j + 1] += water;
                                waterDeep[i, j] = 0.0f;

                            }
                            else
                            {
                                float water = waterDeep[i, j] * stepTime / flowTime;
                                tempDeep[i + 1, j + 1] += water;
                                waterDeep[i, j] -= water;
                            }
                            if (arrived[i + 1, j + 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                            {
                                tempWaterGrid.Add(new int[2] { i + 1, j + 1 });
                                arrived[i + 1, j + 1] = true;
                            }
                        }
                        #endregion
                    }
                }

            for (int i = 0; i < tempWaterGrid.Count; i++)
            {
                canCuculate[tempWaterGrid[i][0], tempWaterGrid[i][1]] = true;
            }
            tempWaterGrid.Clear();
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    if (canCuculate[i, j] == true)
                    {
                        waterDeep[i, j] += tempDeep[i, j];
                        tempDeep[i, j] = 0f;
                    }

                }
        }
        void OutputRaster(string outpath, int number)
        {
            string path = outpath + number + ".img";
            GISdataManager.exportRasterData(path, demRaster, waterDeep);
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

        private void btnOpenDBpoints_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入溃坝点位置数据";
                openDialog.Filter = "Shpfiles(*.shp)|*.shp";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                    txbDBpoint.Text = openDialog.FileName.ToString();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("数据加载失败，请重新加载！", "添加溃坝点流量过程数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnOpenHytrography_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入溃坝点流量过程数据";
                openDialog.Filter = "TXT(*.txt)|*.txt";
                openDialog.Multiselect = false;
                openDialog.RestoreDirectory = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //得到所加载数据的路径
                    txbHydroghraph.Text = openDialog.FileName.ToString();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("数据加载失败，请重新加载！", "添加溃坝点流量过程数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
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



    }
}
