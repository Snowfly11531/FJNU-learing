namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.暴雨模拟ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iDW插值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.求解净雨强度图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成时间雨型文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暴雨模拟ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.图像输出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换页面方向ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加坐标系ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.输出图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axToolbarControl1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(818, 474);
            this.splitContainer1.TabIndex = 0;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 22);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(818, 28);
            this.axToolbarControl1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.暴雨模拟ToolStripMenuItem,
            this.图像输出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(818, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 暴雨模拟ToolStripMenuItem
            // 
            this.暴雨模拟ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iDW插值ToolStripMenuItem,
            this.求解净雨强度图层ToolStripMenuItem,
            this.生成时间雨型文件ToolStripMenuItem,
            this.暴雨模拟ToolStripMenuItem1});
            this.暴雨模拟ToolStripMenuItem.Name = "暴雨模拟ToolStripMenuItem";
            this.暴雨模拟ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.暴雨模拟ToolStripMenuItem.Text = "暴雨模拟";
            // 
            // iDW插值ToolStripMenuItem
            // 
            this.iDW插值ToolStripMenuItem.Name = "iDW插值ToolStripMenuItem";
            this.iDW插值ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.iDW插值ToolStripMenuItem.Text = "IDW插值";
            this.iDW插值ToolStripMenuItem.Click += new System.EventHandler(this.iDW插值ToolStripMenuItem_Click);
            // 
            // 求解净雨强度图层ToolStripMenuItem
            // 
            this.求解净雨强度图层ToolStripMenuItem.Name = "求解净雨强度图层ToolStripMenuItem";
            this.求解净雨强度图层ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.求解净雨强度图层ToolStripMenuItem.Text = "求解净雨强度图层";
            this.求解净雨强度图层ToolStripMenuItem.Click += new System.EventHandler(this.求解净雨强度图层ToolStripMenuItem_Click);
            // 
            // 生成时间雨型文件ToolStripMenuItem
            // 
            this.生成时间雨型文件ToolStripMenuItem.Name = "生成时间雨型文件ToolStripMenuItem";
            this.生成时间雨型文件ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.生成时间雨型文件ToolStripMenuItem.Text = "生成时间雨型文件";
            this.生成时间雨型文件ToolStripMenuItem.Click += new System.EventHandler(this.生成时间雨型文件ToolStripMenuItem_Click);
            // 
            // 暴雨模拟ToolStripMenuItem1
            // 
            this.暴雨模拟ToolStripMenuItem1.Name = "暴雨模拟ToolStripMenuItem1";
            this.暴雨模拟ToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.暴雨模拟ToolStripMenuItem1.Text = "暴雨模拟";
            this.暴雨模拟ToolStripMenuItem1.Click += new System.EventHandler(this.暴雨模拟ToolStripMenuItem1_Click);
            // 
            // 图像输出ToolStripMenuItem
            // 
            this.图像输出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.切换页面方向ToolStripMenuItem,
            this.添加坐标系ToolStripMenuItem,
            this.输出图像ToolStripMenuItem});
            this.图像输出ToolStripMenuItem.Name = "图像输出ToolStripMenuItem";
            this.图像输出ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图像输出ToolStripMenuItem.Text = "图像输出";
            // 
            // 切换页面方向ToolStripMenuItem
            // 
            this.切换页面方向ToolStripMenuItem.Name = "切换页面方向ToolStripMenuItem";
            this.切换页面方向ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.切换页面方向ToolStripMenuItem.Text = "切换页面方向";
            this.切换页面方向ToolStripMenuItem.Click += new System.EventHandler(this.切换页面方向ToolStripMenuItem_Click);
            // 
            // 添加坐标系ToolStripMenuItem
            // 
            this.添加坐标系ToolStripMenuItem.Name = "添加坐标系ToolStripMenuItem";
            this.添加坐标系ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加坐标系ToolStripMenuItem.Text = "添加坐标系";
            this.添加坐标系ToolStripMenuItem.Click += new System.EventHandler(this.添加坐标系ToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(818, 420);
            this.splitContainer2.SplitterDistance = 178;
            this.splitContainer2.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(178, 420);
            this.axTOCControl1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(636, 420);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(628, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "view";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(3, 3);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(622, 388);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.axMapControl1_OnViewRefreshed);
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axLicenseControl1);
            this.tabPage2.Controls.Add(this.axPageLayoutControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(628, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "pageLayout";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(346, 262);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            this.axLicenseControl1.Enter += new System.EventHandler(this.axLicenseControl1_Enter);
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(3, 3);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(622, 388);
            this.axPageLayoutControl1.TabIndex = 0;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            this.axPageLayoutControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseUpEventHandler(this.axPageLayoutControl1_OnMouseUp);
            this.axPageLayoutControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseMoveEventHandler(this.axPageLayoutControl1_OnMouseMove);
            this.axPageLayoutControl1.OnKeyUp += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnKeyUpEventHandler(this.axPageLayoutControl1_OnKeyUp);
            // 
            // 输出图像ToolStripMenuItem
            // 
            this.输出图像ToolStripMenuItem.Name = "输出图像ToolStripMenuItem";
            this.输出图像ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.输出图像ToolStripMenuItem.Text = "输出图像";
            this.输出图像ToolStripMenuItem.Click += new System.EventHandler(this.输出图像ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 474);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 暴雨模拟ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iDW插值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 求解净雨强度图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成时间雨型文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 暴雨模拟ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 图像输出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换页面方向ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加坐标系ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 输出图像ToolStripMenuItem;

    }
}

