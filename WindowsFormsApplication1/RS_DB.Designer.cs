namespace CAFloodModel
{
    partial class RS_DB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RS_DB));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenManing = new System.Windows.Forms.Button();
            this.btnOpenDEM = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbManingInput = new System.Windows.Forms.TextBox();
            this.txbDemIpute = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnOpenHytrography = new System.Windows.Forms.Button();
            this.btnOpenDBpoints = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbDBpoint = new System.Windows.Forms.TextBox();
            this.txbHydroghraph = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txbSimTime = new System.Windows.Forms.TextBox();
            this.txbOutstep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveFloodData = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txbOutpath = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnOpenTandR = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txbRaintxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenManing);
            this.groupBox1.Controls.Add(this.btnOpenDEM);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txbManingInput);
            this.groupBox1.Controls.Add(this.txbDemIpute);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(853, 159);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "空间数据输入：";
            // 
            // btnOpenManing
            // 
            this.btnOpenManing.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenManing.Image")));
            this.btnOpenManing.Location = new System.Drawing.Point(800, 99);
            this.btnOpenManing.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenManing.Name = "btnOpenManing";
            this.btnOpenManing.Size = new System.Drawing.Size(36, 29);
            this.btnOpenManing.TabIndex = 119;
            this.btnOpenManing.UseVisualStyleBackColor = true;
            this.btnOpenManing.Click += new System.EventHandler(this.btnOpenManing_Click);
            // 
            // btnOpenDEM
            // 
            this.btnOpenDEM.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDEM.Image")));
            this.btnOpenDEM.Location = new System.Drawing.Point(800, 40);
            this.btnOpenDEM.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDEM.Name = "btnOpenDEM";
            this.btnOpenDEM.Size = new System.Drawing.Size(36, 29);
            this.btnOpenDEM.TabIndex = 118;
            this.btnOpenDEM.UseVisualStyleBackColor = true;
            this.btnOpenDEM.Click += new System.EventHandler(this.btnOpenDEM_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "曼宁糙率系数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "DEM输入：";
            // 
            // txbManingInput
            // 
            this.txbManingInput.Location = new System.Drawing.Point(123, 101);
            this.txbManingInput.Margin = new System.Windows.Forms.Padding(4);
            this.txbManingInput.Name = "txbManingInput";
            this.txbManingInput.Size = new System.Drawing.Size(668, 25);
            this.txbManingInput.TabIndex = 0;
            // 
            // txbDemIpute
            // 
            this.txbDemIpute.Location = new System.Drawing.Point(123, 40);
            this.txbDemIpute.Margin = new System.Windows.Forms.Padding(4);
            this.txbDemIpute.Name = "txbDemIpute";
            this.txbDemIpute.Size = new System.Drawing.Size(668, 25);
            this.txbDemIpute.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnOpenHytrography);
            this.groupBox4.Controls.Add(this.btnOpenDBpoints);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txbDBpoint);
            this.groupBox4.Controls.Add(this.txbHydroghraph);
            this.groupBox4.Location = new System.Drawing.Point(16, 181);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(853, 140);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "水库信息输入：";
            // 
            // btnOpenHytrography
            // 
            this.btnOpenHytrography.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenHytrography.Image")));
            this.btnOpenHytrography.Location = new System.Drawing.Point(800, 86);
            this.btnOpenHytrography.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenHytrography.Name = "btnOpenHytrography";
            this.btnOpenHytrography.Size = new System.Drawing.Size(36, 29);
            this.btnOpenHytrography.TabIndex = 121;
            this.btnOpenHytrography.UseVisualStyleBackColor = true;
            this.btnOpenHytrography.Click += new System.EventHandler(this.btnOpenHytrography_Click);
            // 
            // btnOpenDBpoints
            // 
            this.btnOpenDBpoints.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDBpoints.Image")));
            this.btnOpenDBpoints.Location = new System.Drawing.Point(800, 34);
            this.btnOpenDBpoints.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDBpoints.Name = "btnOpenDBpoints";
            this.btnOpenDBpoints.Size = new System.Drawing.Size(36, 29);
            this.btnOpenDBpoints.TabIndex = 120;
            this.btnOpenDBpoints.UseVisualStyleBackColor = true;
            this.btnOpenDBpoints.Click += new System.EventHandler(this.btnOpenDBpoints_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 40);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "水库位置：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "流量过程数据：";
            // 
            // txbDBpoint
            // 
            this.txbDBpoint.Location = new System.Drawing.Point(139, 36);
            this.txbDBpoint.Margin = new System.Windows.Forms.Padding(4);
            this.txbDBpoint.Name = "txbDBpoint";
            this.txbDBpoint.Size = new System.Drawing.Size(652, 25);
            this.txbDBpoint.TabIndex = 0;
            // 
            // txbHydroghraph
            // 
            this.txbHydroghraph.Location = new System.Drawing.Point(139, 89);
            this.txbHydroghraph.Margin = new System.Windows.Forms.Padding(4);
            this.txbHydroghraph.Name = "txbHydroghraph";
            this.txbHydroghraph.Size = new System.Drawing.Size(652, 25);
            this.txbHydroghraph.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txbSimTime);
            this.groupBox2.Controls.Add(this.txbOutstep);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(16, 442);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(853, 68);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设置：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(684, 29);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 15);
            this.label10.TabIndex = 122;
            this.label10.Text = "s";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(240, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 15);
            this.label9.TabIndex = 122;
            this.label9.Text = "s";
            // 
            // txbSimTime
            // 
            this.txbSimTime.Location = new System.Drawing.Point(565, 25);
            this.txbSimTime.Margin = new System.Windows.Forms.Padding(4);
            this.txbSimTime.Name = "txbSimTime";
            this.txbSimTime.Size = new System.Drawing.Size(109, 25);
            this.txbSimTime.TabIndex = 0;
            // 
            // txbOutstep
            // 
            this.txbOutstep.Location = new System.Drawing.Point(123, 25);
            this.txbOutstep.Margin = new System.Windows.Forms.Padding(4);
            this.txbOutstep.Name = "txbOutstep";
            this.txbOutstep.Size = new System.Drawing.Size(108, 25);
            this.txbOutstep.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(455, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "模拟总时长：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "输出步长：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSaveFloodData);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txbOutpath);
            this.groupBox3.Location = new System.Drawing.Point(16, 518);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(853, 115);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据输出：";
            // 
            // btnSaveFloodData
            // 
            this.btnSaveFloodData.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFloodData.Image")));
            this.btnSaveFloodData.Location = new System.Drawing.Point(797, 46);
            this.btnSaveFloodData.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveFloodData.Name = "btnSaveFloodData";
            this.btnSaveFloodData.Size = new System.Drawing.Size(36, 29);
            this.btnSaveFloodData.TabIndex = 123;
            this.btnSaveFloodData.UseVisualStyleBackColor = true;
            this.btnSaveFloodData.Click += new System.EventHandler(this.btnSaveFloodData_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 52);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "输出路径设置：";
            // 
            // txbOutpath
            // 
            this.txbOutpath.Location = new System.Drawing.Point(123, 49);
            this.txbOutpath.Margin = new System.Windows.Forms.Padding(4);
            this.txbOutpath.Name = "txbOutpath";
            this.txbOutpath.Size = new System.Drawing.Size(665, 25);
            this.txbOutpath.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnOpenTandR);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txbRaintxt);
            this.groupBox5.Location = new System.Drawing.Point(16, 329);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(853, 106);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "时空雨形输入：";
            // 
            // btnOpenTandR
            // 
            this.btnOpenTandR.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTandR.Image")));
            this.btnOpenTandR.Location = new System.Drawing.Point(797, 42);
            this.btnOpenTandR.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenTandR.Name = "btnOpenTandR";
            this.btnOpenTandR.Size = new System.Drawing.Size(36, 29);
            this.btnOpenTandR.TabIndex = 122;
            this.btnOpenTandR.UseVisualStyleBackColor = true;
            this.btnOpenTandR.Click += new System.EventHandler(this.btnOpenTandR_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(225, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "时间-净雨强度栅格记录表输入：";
            // 
            // txbRaintxt
            // 
            this.txbRaintxt.Location = new System.Drawing.Point(255, 45);
            this.txbRaintxt.Margin = new System.Windows.Forms.Padding(4);
            this.txbRaintxt.Name = "txbRaintxt";
            this.txbRaintxt.Size = new System.Drawing.Size(533, 25);
            this.txbRaintxt.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 658);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "开始计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(505, 658);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(189, 30);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            // 
            // RS_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 715);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RS_DB";
            this.Text = "暴雨-水库泄洪、溃坝模式洪水动态模拟";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbManingInput;
        private System.Windows.Forms.TextBox txbDemIpute;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbDBpoint;
        private System.Windows.Forms.TextBox txbHydroghraph;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txbSimTime;
        private System.Windows.Forms.TextBox txbOutstep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbOutpath;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbRaintxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOpenDEM;
        private System.Windows.Forms.Button btnOpenManing;
        private System.Windows.Forms.Button btnOpenDBpoints;
        private System.Windows.Forms.Button btnOpenHytrography;
        private System.Windows.Forms.Button btnOpenTandR;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSaveFloodData;
    }
}