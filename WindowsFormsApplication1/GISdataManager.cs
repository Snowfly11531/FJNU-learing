using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
using System.Threading.Tasks;
using System.IO;

namespace DamBreakModelApplication
{
    class GISdataManager
    {
        public static void readRaster(string path, ref IRasterLayer rasterLayer)    //读取栅格数据并保存到rasterlayer中
        {
            if (path.Trim() == "")
                return;
            string strFileDirectory = path.Substring(0, path.LastIndexOf('\\'));
            string strFileName = path.Substring(path.LastIndexOf('\\') + 1);

            IWorkspaceFactory rasterWorkspaceFac = new RasterWorkspaceFactory();
            IRasterWorkspace rasterWorkspace = rasterWorkspaceFac.OpenFromFile(strFileDirectory, 0) as IRasterWorkspace;
            IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset(strFileName);
            IRasterLayer rasterLayer2 = new RasterLayer();
            rasterLayer2.CreateFromDataset(rasterDataset);
            rasterLayer = rasterLayer2;
        }

        public static float[,] Raster2Mat(IRasterLayer rasterlayer) //将栅格数据转为二元数组
        {
            IRaster raster = rasterlayer.Raster;
            IRaster2 raster2 = raster as IRaster2;
            IRasterProps pRasterProps = (IRasterProps)raster;
            IPnt pntstart = new DblPntClass();
            pntstart.SetCoords(0, 0);
            IPnt pntend = new DblPntClass();
            pntend.SetCoords(pRasterProps.Width, pRasterProps.Height);
            IPixelBlock3 unionPixelBlock = (IPixelBlock3)raster.CreatePixelBlock(pntend);
            System.Single[,] floatMat;
            try
            {
                raster.Read(pntstart, (IPixelBlock)unionPixelBlock);
                floatMat = (System.Single[,])unionPixelBlock.get_PixelData(0);
            }
            catch (Exception e) {
                raster.Read(pntstart, (IPixelBlock)unionPixelBlock);
                int[,] intMat = (int[,])unionPixelBlock.get_PixelData(0);
                floatMat = new System.Single[ pRasterProps.Width,pRasterProps.Height];
                Parallel.For(0, pRasterProps.Width, i =>
                {
                    for (int j = 0; j < pRasterProps.Height; j++)
                    {
                        floatMat[i,j] = Convert.ToSingle(intMat[i, j]);
                    }
                });
            }

            return floatMat;
        }

        public static void exportRasterData(string parth, IRasterLayer rasterLayer, float[,] rasterMat)   //输出栅格数据
        {
            string directory = parth.Substring(0, parth.LastIndexOf("\\"));
            string name = parth.Substring(parth.LastIndexOf("\\") + 1);
            IWorkspaceFactory workspaceFac = new RasterWorkspaceFactoryClass();
            IRasterWorkspace2 rasterWorkspace2 = workspaceFac.OpenFromFile(directory, 0) as IRasterWorkspace2;

            IRasterInfo rasterInfo = (rasterLayer.Raster as IRawBlocks).RasterInfo;
            IPoint originPoint = new Point();
            originPoint.PutCoords(rasterInfo.Origin.X, rasterInfo.Origin.Y - (rasterLayer.Raster as IRasterProps).Height * (rasterLayer.Raster as IRasterProps).MeanCellSize().Y);
            IRasterProps rasterProps = rasterLayer.Raster as IRasterProps;
            IRasterDataset rasterDataSet = rasterWorkspace2.CreateRasterDataset(name, "IMAGINE Image", originPoint, rasterProps.Width, rasterProps.Height, 
                rasterProps.MeanCellSize().X, rasterProps.MeanCellSize().Y, 1, rstPixelType.PT_FLOAT, rasterProps.SpatialReference, true) as IRasterDataset2;

            IRaster2 raster2 = rasterDataSet.CreateDefaultRaster() as IRaster2;

            IPnt pntClass = new Pnt();
            pntClass.X = rasterProps.Width;
            pntClass.Y = rasterProps.Height;
            IRasterCursor rasterCursor = raster2.CreateCursorEx(pntClass);
            IRasterCursor inRasterCursor = (rasterLayer.Raster as IRaster2).CreateCursorEx(pntClass);

            IRasterEdit rasterEdit = raster2 as IRasterEdit;
            if (rasterEdit.CanEdit())
            {
                IPixelBlock3 pixelBlock3 = rasterCursor.PixelBlock as IPixelBlock3;
                IPixelBlock3 inPixelBlock3 = inRasterCursor.PixelBlock as IPixelBlock3;
                System.Array pixels = (System.Array)rasterMat;
                pixelBlock3.set_PixelData(0, (System.Array)pixels);
                rasterEdit.Write(rasterCursor.TopLeft, (IPixelBlock)pixelBlock3);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pixelBlock3);
            }
            rasterEdit.Refresh();
            IGeoDataset inDataset = rasterLayer.Raster as IGeoDataset;
            IGeoDataset outDataset = rasterDataSet as IGeoDataset;
            IExtractionOp op = new RasterExtractionOpClass();
            var outDataset1=op.Raster(outDataset, inDataset);
            var clipRaster = (IRaster)outDataset1;
            ISaveAs pSaveAs = clipRaster as ISaveAs;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterEdit);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(raster2);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterDataSet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(rasterWorkspace2);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workspaceFac);
            if (File.Exists(parth))
            {
                File.Delete(parth);
            }
            workspaceFac = new RasterWorkspaceFactoryClass();
            IDataset outdataset=pSaveAs.SaveAs(name, workspaceFac.OpenFromFile(directory, 0), "IMAGINE Image");
            System.Runtime.InteropServices.Marshal.ReleaseComObject(outdataset);
            return;
        }
        public static void readSHP(string path, ref IFeatureLayer featureLayer) //读取shapefile文件
        {
            if (path.Trim() == "")
                return;
            string strFileDirectory = path.Substring(0, path.LastIndexOf('\\'));
            string strFileName = path.Substring(path.LastIndexOf('\\') + 1);


            IWorkspaceFactory pFeatureWsFactory = new ShapefileWorkspaceFactory();
            IWorkspace ws = pFeatureWsFactory.OpenFromFile(strFileDirectory, 0);
            IFeatureWorkspace pFeatureWorkspace = ws as IFeatureWorkspace;
            IFeatureLayer pFeatureLayer = new FeatureLayer();
            //得到图层要素类
            pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(strFileName);
            pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
            featureLayer = pFeatureLayer;
        }
    }
}
