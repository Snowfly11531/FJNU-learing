namespace CAFloodModel
{
    partial class RainStorm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RainStorm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenManing = new System.Windows.Forms.Button();
            this.btnOpenDEM = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbManingInput = new System.Windows.Forms.TextBox();
            this.txbDemIpute = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOpenTandR = new System.Windows.Forms.Button();
            this.txbRaintxt = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txbSimTime = new System.Windows.Forms.TextBox();
            this.txbOutstep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveFloodData = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txbOutpath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 127);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "空间数据输入：";
            // 
            // btnOpenManing
            // 
            this.btnOpenManing.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenManing.Image")));
            this.btnOpenManing.Location = new System.Drawing.Point(602, 79);
            this.btnOpenManing.Name = "btnOpenManing";
            this.btnOpenManing.Size = new System.Drawing.Size(27, 23);
            this.btnOpenManing.TabIndex = 116;
            this.btnOpenManing.UseVisualStyleBackColor = true;
            this.btnOpenManing.Click += new System.EventHandler(this.btnOpenManing_Click);
            // 
            // btnOpenDEM
            // 
            this.btnOpenDEM.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDEM.Image")));
            this.btnOpenDEM.Location = new System.Drawing.Point(602, 30);
            this.btnOpenDEM.Name = "btnOpenDEM";
            this.btnOpenDEM.Size = new System.Drawing.Size(27, 23);
            this.btnOpenDEM.TabIndex = 116;
            this.btnOpenDEM.UseVisualStyleBackColor = true;
            this.btnOpenDEM.Click += new System.EventHandler(this.btnOpenDEM_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "曼宁糙率系数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "DEM输入：";
            // 
            // txbManingInput
            // 
            this.txbManingInput.Location = new System.Drawing.Point(92, 81);
            this.txbManingInput.Name = "txbManingInput";
            this.txbManingInput.Size = new System.Drawing.Size(500, 21);
            this.txbManingInput.TabIndex = 0;
            // 
            // txbDemIpute
            // 
            this.txbDemIpute.Location = new System.Drawing.Point(92, 32);
            this.txbDemIpute.Name = "txbDemIpute";
            this.txbDemIpute.Size = new System.Drawing.Size(500, 21);
            this.txbDemIpute.TabIndex = 0;
            this.txbDemIpute.TextChanged += new System.EventHandler(this.txbDemIpute_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.btnOpenTandR);
            this.groupBox4.Controls.Add(this.txbRaintxt);
            this.groupBox4.Location = new System.Drawing.Point(12, 148);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(637, 85);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "时空雨形输入：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "时间-净雨强度栅格记录表输入：";
            // 
            // btnOpenTandR
            // 
            this.btnOpenTandR.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTandR.Image")));
            this.btnOpenTandR.Location = new System.Drawing.Point(604, 34);
            this.btnOpenTandR.Name = "btnOpenTandR";
            this.btnOpenTandR.Size = new System.Drawing.Size(27, 23);
            this.btnOpenTandR.TabIndex = 116;
            this.btnOpenTandR.UseVisualStyleBackColor = true;
            this.btnOpenTandR.Click += new System.EventHandler(this.btnOpenTandR_Click);
            // 
            // txbRaintxt
            // 
            this.txbRaintxt.Location = new System.Drawing.Point(191, 37);
            this.txbRaintxt.Name = "txbRaintxt";
            this.txbRaintxt.Size = new System.Drawing.Size(403, 21);
            this.txbRaintxt.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txbSimTime);
            this.groupBox2.Controls.Add(this.txbOutstep);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(637, 86);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设置：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(455, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "s";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(182, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "s";
            // 
            // txbSimTime
            // 
            this.txbSimTime.Location = new System.Drawing.Point(366, 45);
            this.txbSimTime.Name = "txbSimTime";
            this.txbSimTime.Size = new System.Drawing.Size(83, 21);
            this.txbSimTime.TabIndex = 0;
            // 
            // txbOutstep
            // 
            this.txbOutstep.Location = new System.Drawing.Point(94, 42);
            this.txbOutstep.Name = "txbOutstep";
            this.txbOutstep.Size = new System.Drawing.Size(82, 21);
            this.txbOutstep.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "模拟总时长：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "输出步长：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSaveFloodData);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txbOutpath);
            this.groupBox3.Location = new System.Drawing.Point(12, 331);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(637, 92);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据输出：";
            // 
            // btnSaveFloodData
            // 
            this.btnSaveFloodData.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFloodData.Image")));
            this.btnSaveFloodData.Location = new System.Drawing.Point(602, 37);
            this.btnSaveFloodData.Name = "btnSaveFloodData";
            this.btnSaveFloodData.Size = new System.Drawing.Size(27, 23);
            this.btnSaveFloodData.TabIndex = 110;
            this.btnSaveFloodData.UseVisualStyleBackColor = true;
            this.btnSaveFloodData.Click += new System.EventHandler(this.btnSaveFloodData_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "输出路径设置：";
            // 
            // txbOutpath
            // 
            this.txbOutpath.Location = new System.Drawing.Point(92, 39);
            this.txbOutpath.Name = "txbOutpath";
            this.txbOutpath.Size = new System.Drawing.Size(500, 21);
            this.txbOutpath.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 24);
            this.button1.TabIndex = 6;
            this.button1.Text = "开始计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(376, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 24);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            // 
            // RainStorm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 481);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RainStorm";
            this.Text = "暴雨模式洪水动态模拟";
            this.Load += new System.EventHandler(this.RainStorm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOpenManing;
        private System.Windows.Forms.Button btnOpenDEM;
        private System.Windows.Forms.Button btnOpenTandR;
        private System.Windows.Forms.Button btnSaveFloodData;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txbManingInput;
        public System.Windows.Forms.TextBox txbDemIpute;
        public System.Windows.Forms.TextBox txbRaintxt;
        public System.Windows.Forms.TextBox txbSimTime;
        public System.Windows.Forms.TextBox txbOutstep;
        public System.Windows.Forms.TextBox txbOutpath;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
    }
}