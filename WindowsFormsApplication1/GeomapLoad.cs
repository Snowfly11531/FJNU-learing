using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Display;

namespace CAFloodModel
{
    public class GeomapLoad
    {
        public static void CopyAndOverwriteMap(AxMapControl axMapControl, AxPageLayoutControl axPageLayoutControl)
        {
            IObjectCopy objectCopy = new ObjectCopyClass();
            object toCopyMap = axMapControl.Map;
            object copiedMap = objectCopy.Copy(toCopyMap);
            IPageLayout pagelayout = axPageLayoutControl.PageLayout;
            object overwriteMap = (pagelayout as IActiveView).FocusMap;
            objectCopy.Overwrite(toCopyMap, ref overwriteMap);
            IGraphicsContainer pGraphicsContainer = pagelayout as IGraphicsContainer;
            IMapFrame pMapFrame = (IMapFrame)pGraphicsContainer.FindFrame(overwriteMap);
            IElement pElement = pMapFrame as IElement;
            IEnumElement pEnumElement=pGraphicsContainer.LocateElementsByEnvelope(pElement.Geometry.Envelope);
            IElement pElementIcon = pEnumElement.Next();
            while (pElementIcon != null) {
                if (pElementIcon is IMapSurroundFrame) {
                    if ((pElementIcon as IMapSurroundFrame).MapSurround is IScaleBar) {
                        (pElementIcon as IMapSurroundFrame).MapFrame = pMapFrame;
                    }
                }
                pElementIcon = pEnumElement.Next();
            }
            axPageLayoutControl.ActiveView.Refresh();
        }
        public static void changeOritation(AxPageLayoutControl axPageLayoutControl) {
            IPageLayout pageLayout = axPageLayoutControl.PageLayout;
            IActiveView activeView=pageLayout as IActiveView;
            IMap pMap = activeView.FocusMap;
            IGraphicsContainer pGraphicsContainer = pageLayout as IGraphicsContainer;
            IMapFrame mapFrame = (IMapFrame)pGraphicsContainer.FindFrame(pMap);
            IElement pElement = (IElement)mapFrame;
            IEnvelope pEnvelope=new EnvelopeClass();
            if(axPageLayoutControl.Page.Orientation==2){
                pEnvelope.PutCoords(0.5,0.5,20.5,29.2);
                axPageLayoutControl.Page.PutCustomSize(29.7,21);
                axPageLayoutControl.Page.Orientation = 1;
            }else{
                pEnvelope.PutCoords(0.5,0.5,29.2,20.5);
                axPageLayoutControl.Page.PutCustomSize(21,29.7);
                axPageLayoutControl.Page.Orientation = 2;
            }
            pElement.Geometry = pEnvelope;
            activeView.Refresh();
        }
        public static void addScalebar(AxPageLayoutControl axPageLayoutControl, String type) {
            IPageLayout pageLayout = axPageLayoutControl.PageLayout;
            IActiveView activeView = pageLayout as IActiveView;
            IMap pMap = activeView.FocusMap;
            IGraphicsContainer pGraphicsContainer = pageLayout as IGraphicsContainer;
            IMapFrame mapFrame = (IMapFrame)pGraphicsContainer.FindFrame(pMap);
            UID uid=new UIDClass();
            uid.Value=type;
            IMapSurroundFrame pMapSurroundFrame = mapFrame.CreateSurroundFrame(uid, null);
            IMapSurround pMapSurround = pMapSurroundFrame.MapSurround;
            IScaleBar pScaleBar = (IScaleBar)pMapSurround;
            pScaleBar.LabelPosition = esriVertPosEnum.esriBelow;
            pScaleBar.UseMapSettings();
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(0.8, 0.8, 13, 2);
            IElement element = pMapSurroundFrame as IElement;
            element.Geometry = pEnvelope;
            pGraphicsContainer.AddElement(element, 0);
            activeView.Refresh();
        }
        public static IElement getElement(AxPageLayoutControl axPageLayoutControl, IPoint pPoint) {
            IPageLayout pageLayout = axPageLayoutControl.PageLayout;
            IGraphicsContainer pGraphicsContainer = pageLayout as IGraphicsContainer;
            IEnumElement pEnumElement=pGraphicsContainer.LocateElements(pPoint, 0);
            pEnumElement.Reset();
            var pElement = pEnumElement.Next();
            while (pElement != null && (pElement is IMapFrame)) {
                pElement = pEnumElement.Next();
            }
            return pElement;
        }
        public static void setElementPosition(AxPageLayoutControl axPageLayoutControl, IElement pElement, IPoint pPoint) {
            IPageLayout pageLayout = axPageLayoutControl.PageLayout;
            IActiveView activeView = pageLayout as IActiveView;
            IEnvelope pEnvelope = pElement.Geometry.Envelope;
            pEnvelope.CenterAt(pPoint);
            pElement.Geometry = pEnvelope;
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        public static void exportMaptoJPEG(AxPageLayoutControl axPageLayoutControl) {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title="保存图像";
            saveDialog.FileName = "output";
            saveDialog.Filter = "JGP图片(*.jpg)|*.jpg";
            if (saveDialog.ShowDialog() == DialogResult.OK) {
                double resolution = 
                    axPageLayoutControl.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;
                IExport pExport = new ExportJPEGClass();
                pExport.ExportFileName = saveDialog.FileName;
                pExport.Resolution = resolution;
                tagRECT deviceRect = 
                    axPageLayoutControl.ActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                IEnvelope pDeviceEnvelope = new EnvelopeClass();
                pDeviceEnvelope.PutCoords(deviceRect.left, deviceRect.bottom, 
                    deviceRect.right, deviceRect.top);
                pExport.PixelBounds = pDeviceEnvelope;

                axPageLayoutControl.ActiveView.Output(pExport.StartExporting(),(int)pExport.Resolution,
                    ref deviceRect,axPageLayoutControl.ActiveView.Extent,new CancelTrackerClass());
                Application.DoEvents();
                pExport.FinishExporting();
            }
        }
    }
}
