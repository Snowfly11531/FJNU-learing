using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CAFloodModel;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private IElement pElement;
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop); 
            InitializeComponent();
        }

        private void iDW插值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDW idw = new IDW();
            idw.Show();
        }

        private void axLicenseControl1_Enter(object sender, EventArgs e)
        {

        }

        private void 求解净雨强度图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RainForce rainforce = new RainForce();
            rainforce.Show();
        }

        private void 暴雨模拟ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RainStorm rainstorm = new RainStorm();
            rainstorm.Show();
        }

        private void 生成时间雨型文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RainTxt raintxt = new RainTxt();
            raintxt.Show();
        }

        private void axMapControl1_OnAfterScreenDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView pAcv = axPageLayoutControl1.ActiveView.FocusMap as IActiveView;
            IDisplayTransformation displayTransformation = pAcv.ScreenDisplay.DisplayTransformation;
            displayTransformation.VisibleBounds = axMapControl1.Extent;//设置焦点地图的可视范围
            GeomapLoad.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
        }

        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            GeomapLoad.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
        }

        private void axMapControl1_OnViewRefreshed(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnViewRefreshedEvent e)
        {
            GeomapLoad.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
        }

        private void 切换页面方向ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeomapLoad.changeOritation(axPageLayoutControl1);
        }

        private void 添加坐标系ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeomapLoad.addScalebar(axPageLayoutControl1, "esriCarto.ScaleLine");
        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            IPoint pPoint=new PointClass();
            pPoint.X=e.pageX;
            pPoint.Y=e.pageY;
            Console.WriteLine(pPoint.X+","+ pPoint.Y);
            pElement=GeomapLoad.getElement(axPageLayoutControl1, pPoint);
            IBorder pBorder=new SymbolBorder();
            pBorder.Gap=1.0;
            (pElement as IMapSurroundFrame).Border = pBorder;
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void axPageLayoutControl1_OnKeyUp(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnKeyUpEvent e)
        {
        }

        private void axPageLayoutControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseMoveEvent e)
        {
            if (pElement is IMapSurroundFrame) {
                Console.WriteLine("shide");
                IPoint pPoint=new PointClass();
                pPoint.X=e.pageX;
                pPoint.Y=e.pageY;
                GeomapLoad.setElementPosition(axPageLayoutControl1, pElement, pPoint);
            }
        }

        private void axPageLayoutControl1_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseUpEvent e)
        {
            if(pElement!=null)(pElement as IMapSurroundFrame).Border = null;
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            pElement = null;
        }

        private void 输出图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeomapLoad.exportMaptoJPEG(axPageLayoutControl1);
        }
    }
}
