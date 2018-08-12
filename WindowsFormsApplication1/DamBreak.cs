using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using DamBreakModelApplication;

namespace CAFloodModel
{
    public partial class DamBreak : Form
    {
        //输入变量
        IRasterLayer demRaster=new RasterLayer();
        IRasterLayer manningRaster=new RasterLayer();
        IFeatureLayer DBpointshp=new FeatureLayer();
        List<string[]> HydroRecordList = new List<string[]>();
        string DemPath, ManningPath,DBpointPath,HydroPath;
        private int stepTime = 1;//计算步长
        long simTime, outStep;
        string Outpath = null;
        //float OutVol=200;//固定流量
        //溃坝点
        int DPcolIndex, DProwIndex;
        List<int[]> dbPointList=new List<int[]>();

        //dem
        float nodataValue=-9999;
        public float[,] dem;
        public float flowLength, flowLength2;//flowLength即分辨率40m
        int rowCount, colCount; //数据行数，列数 

        //流速计算
        public float[,] manNing, slope, flowVel;
        byte[,] flowDir;
        public float maxSpeed = 10f;


        //演进过程
        bool[,] arrived;
        List<int[]> waterGrids = new List<int[]>(); //有水的格网单元：用行列号表示
        List<int[]> tempWaterGrid = new List<int[]>();
        public float[,] waterDeep, tempDeep;

        //记录变量
        public int submergeCount = 0;//淹没面积

        //线程与精度条窗口
        Thread thread;
        ProcessManager pm;

        public DamBreak()
        {
            InitializeComponent();
            txbDemIpute.Text = @"DEM.img";
            txbManingInput.Text = @"Manning.img";
            txbOutpath.Text = "out";
            txbHydroghraph.Text = @"VolumeProcessList.txt";
            txbDBpoint.Text = @"Point.shp";
            txbSimTime.Text = "";
            txbOutstep.Text = "";                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(CalculateDB));
            thread.Start();
            this.UseWaitCursor = true;
            button1.Enabled = false;
            
        }

        void CalculateDB()
        {
            pm=new ProcessManager();
            simTime = Convert.ToInt64(txbSimTime.Text);
            outStep = Convert.ToInt64(txbOutstep.Text);
            //MaxExchangeRate = Convert.ToSingle(txbExRate.Text)/ 100;
            if (outStep < stepTime)
            {
                MessageBox.Show("输出步长应该大于1s!");
                return;
            }
            this.Invoke(pm.createProcessWindow());
            this.Invoke(pm.setProcess, new object[] { 0,Convert.ToInt32(simTime)});
            //为输出栅格设置技术变量
            int time = 0, number = 1;
            this.Invoke(pm.updateProcess, new object[] { string.Format("数据正在初始化："), 0 });
            //初始化模拟数据
            DemBreakStart();
            //溃坝计算
            for (int i = 0; i <= simTime; i += stepTime)
            {

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


         void DemBreakStart()
        {
            //读入dem，manning
            DemPath = txbDemIpute.Text;
            GISdataManager.readRaster(DemPath, ref demRaster); 
             //
            //IRasterProps rasterprops = demRaster.Raster as IRasterProps;
            //object nodataValue1 = rasterprops.NoDataValue;
            //float[] mit = nodataValue1 as float[];
            //nodataValue = mit[0];
             //
            dem = GISdataManager.Raster2Mat(demRaster);
            ManningPath = txbManingInput.Text;
            GISdataManager.readRaster(ManningPath, ref manningRaster);
            manNing = GISdataManager.Raster2Mat(manningRaster);

            //获取栅格分辨率
            IRasterInfo rasterinfo = (demRaster.Raster as IRawBlocks).RasterInfo;
            flowLength=Convert.ToInt32(rasterinfo.CellSize.X);
            flowLength2 = flowLength * 1.141f;

            //判断DEM与曼宁系数栅格是否一致！
            if (demRaster.ColumnCount != manningRaster.ColumnCount || demRaster.RowCount != manningRaster.RowCount)
            {
                MessageBox.Show("请在检查DEM与曼宁糙率栅格行列是否对应后重新输入！");
                return;
            }
            rowCount = demRaster.RowCount;
            colCount = demRaster.ColumnCount;

            //初始化中间参数
            slope = new float[rowCount, colCount];
            flowDir = new byte[rowCount, colCount];
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
             DBpointPath=txbDBpoint.Text;
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
                waterGrids.Add(new int[2] { DProwIndex, DPcolIndex });
            }
             //读取流量过程线表格
             HydroPath = txbHydroghraph.Text;
             HydroRecordList=TxTReader.txt2List3(HydroPath, dbPointList.Count);
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
                    item = dbPointList[i-1];
                    rowIndex = item[0];
                    colIndex = item[1];
                    int OutVol = Convert.ToInt32(record[i]);
                    float h = OutVol / (flowLength * flowLength); //每个单元格增加的水深
                    waterDeep[rowIndex, colIndex] += h * stepTime; 
                }
            }
            else
            {
                HydroRecordList.RemoveAt(0);
                if (HydroRecordList.Count>0)
                {
                    for (int i = 1; i < record.Length; i++)
                    {
                        int[] item;
                        int rowIndex,colIndex;
                        item=dbPointList[i-1];
                        rowIndex = item[0];
                        colIndex = item[1];
                        int OutVol = Convert.ToInt32(record[i]);
                        float h = OutVol / (flowLength * flowLength); //每个单元格增加的水深
                        waterDeep[rowIndex, colIndex] += h * stepTime; 
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
            for (int i = 0; i < waterGrids.Count; i++)
            {
                int[] item = waterGrids[i];

                if (dem[item[0] - 1, item[1] - 1] == nodataValue || dem[item[0] - 1, item[1] + 1] == nodataValue || dem[item[0] + 1, item[1] - 1] == nodataValue || dem[item[0] + 1, item[1] + 1] == nodataValue)
                {
                    waterGrids.Remove(item);
                    //Debug.Log(string.Format("计算坡度和水流时，因为DEM到达边界，移除了单元格：{0},{1}", item[0], item[1]));
                    continue;//如果到达边界则不计算
                }
                if (item[0] - 1 < 0 || item[0] + 1 > rowCount - 1 || item[1] - 1 < 0 || item[1] + 1 > colCount - 1)
                {
                    waterGrids.Remove(item);
                    continue;
                }

                float high = dem[item[0], item[1]] + waterDeep[item[0], item[1]];

                float[] s = new float[8];
                s[0] = (high - dem[item[0] - 1, item[1] - 1] - waterDeep[item[0] - 1, item[1] - 1]) / flowLength2;
                s[1] = (high - dem[item[0] - 1, item[1]] - waterDeep[item[0] - 1, item[1]]) / flowLength;
                s[2] = (high - dem[item[0] - 1, item[1] + 1] - waterDeep[item[0] - 1, item[1] + 1]) / flowLength2;
                s[3] = (high - dem[item[0], item[1] - 1] - waterDeep[item[0], item[1] - 1]) / flowLength;
                s[4] = (high - dem[item[0], item[1] + 1] - waterDeep[item[0], item[1] + 1]) / flowLength;
                s[5] = (high - dem[item[0] + 1, item[1] - 1] - waterDeep[item[0] + 1, item[1] - 1]) / flowLength2;
                s[6] = (high - dem[item[0] + 1, item[1]] - waterDeep[item[0] + 1, item[1]]) / flowLength;
                s[7] = (high - dem[item[0] + 1, item[1] + 1] - waterDeep[item[0] + 1, item[1] + 1]) / flowLength2;

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
                if (waterDeep[item[0], item[1]] <= 0f)//只计算有水的单元格
                {
                    flowDir[item[0], item[1]] = 0;
                }
                else
                    flowDir[item[0], item[1]] = (byte)(index + 1);
                slope[item[0], item[1]] = max;
            }
        }
        void calcuVelocity()
        {
            for (int i = 0; i < waterGrids.Count; i++)
            {
                int[] item = waterGrids[i];
                if (waterDeep[item[0], item[1]] <= 0f || slope[item[0], item[1]] <= 0f || manNing[item[0], item[1]] <= 0f)
                {
                    flowVel[item[0], item[1]] = 0f;
                }
                else
                {
                    float speed = Convert.ToSingle(Math.Pow(waterDeep[item[0], item[1]], 0.666f) * Math.Pow(slope[item[0], item[1]], 0.5f) / manNing[item[0], item[1]]);//曼宁公式
                    flowVel[item[0], item[1]] = speed > maxSpeed ? maxSpeed : speed;
                }
            }
        }
        void calcuFlowProcess()
        {
            //计算水深
            for (int i = 0; i < waterGrids.Count; i++)
            {
                int[] item = waterGrids[i];
                if (flowDir[item[0], item[1]] == (byte)0)
                    continue;
                float flowTime = 0;
                #region
                //1、向左上角流
                if (flowDir[item[0], item[1]] == 1)
                {
                    flowTime = flowLength2 / flowVel[item[0], item[1]];
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {
                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0] - 1, item[1] - 1] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0] - 1, item[1] - 1] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0] - 1, item[1] - 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0] - 1, item[1] - 1 });
                        arrived[item[0] - 1, item[1] - 1] = true;
                    }
                }
                //2、向上流
                else if (flowDir[item[0], item[1]] == 2)
                {
                    flowTime = flowLength / flowVel[item[0], item[1]];
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {
                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0] - 1, item[1]] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0] - 1, item[1]] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0] - 1, item[1]] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0] - 1, item[1] });
                        arrived[item[0] - 1, item[1]] = true;
                    }

                }
                //3、向右上角流
                else if (flowDir[item[0], item[1]] == 3)
                {
                    flowTime = flowLength / flowVel[item[0], item[1]] ;
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {
                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0] - 1, item[1] + 1] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0] - 1, item[1] + 1] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0] - 1, item[1] + 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0] - 1, item[1] + 1 });
                        arrived[item[0] - 1, item[1] + 1] = true;
                    }

                }
                //4、向左流
                else if (flowDir[item[0], item[1]] == 4)
                {
                    flowTime = flowLength / flowVel[item[0], item[1]];
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {
                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0], item[1] - 1] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0], item[1] - 1] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0], item[1] - 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0], item[1] - 1 });
                        arrived[item[0], item[1] - 1] = true;
                    }
                }
                //5、向右流
                else if (flowDir[item[0], item[1]] == 5)
                {
                    flowTime = flowLength / flowVel[item[0], item[1]];
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {

                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0], item[1] + 1] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0], item[1] + 1] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0], item[1] + 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0], item[1] + 1 });
                        arrived[item[0], item[1] + 1] = true;
                    }

                }
                //6、向左下流
                else if (flowDir[item[0], item[1]] == 6)
                {
                    flowTime = flowLength / flowVel[item[0], item[1]];
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {
                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0] + 1, item[1] - 1] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0] + 1, item[1] - 1] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0] + 1, item[1] - 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0] + 1, item[1] - 1 });
                        arrived[item[0] + 1, item[1] - 1] = true;
                    }

                }
                //7、向下流
                else if (flowDir[item[0], item[1]] == 7)
                {
                    flowTime = flowLength / flowVel[item[0], item[1]] ;
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {
                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0] + 1, item[1]] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0] + 1, item[1]] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0] + 1, item[1]] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0] + 1, item[1] });
                        arrived[item[0] + 1, item[1]] = true;
                    }

                }
                //8、向右下流
                else if (flowDir[item[0], item[1]] == 8)
                {
                    flowTime = flowLength / flowVel[item[0], item[1]] ;
                    if (flowTime <= stepTime)//时间间隔内全部流完
                    {
                        float water = waterDeep[item[0], item[1]];
                        tempDeep[item[0] + 1, item[1] + 1] += water;
                        //waterDeep[item[0], item[1]] = 0.0f;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    else
                    {
                        float water = waterDeep[item[0], item[1]] * stepTime / flowTime;
                        tempDeep[item[0] + 1, item[1] + 1] += water;
                        waterDeep[item[0], item[1]] -= water;
                    }
                    if (arrived[item[0] + 1, item[1] + 1] == false)//水流流到的这个单元格之前没有流到，则在waterGrids中添加记录
                    {
                        tempWaterGrid.Add(new int[2] { item[0] + 1, item[1] + 1 });
                        arrived[item[0] + 1, item[1] + 1] = true;
                    }
                }
                #endregion
            }
            for (int i = 0; i < tempWaterGrid.Count; i++)
            {
                waterGrids.Add(new int[2] { tempWaterGrid[i][0], tempWaterGrid[i][1] });
            }
            submergeCount = waterGrids.Count;
            tempWaterGrid.Clear();
            for (int i = 0; i < waterGrids.Count; i++)
            {
                waterDeep[waterGrids[i][0], waterGrids[i][1]] += tempDeep[waterGrids[i][0], waterGrids[i][1]];
                tempDeep[waterGrids[i][0], waterGrids[i][1]] = 0f;
            }
        }

        void OutputRaster(string outpath,int number)
        {
            string path = outpath + number+".img";
            GISdataManager.exportRasterData(path, demRaster, waterDeep);                    
        }

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

        private void btnOpenDBpoints_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Title = "输入水库位置数据";
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

        private void btnSaveFloodData_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Title = "淹没数据输出";
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

