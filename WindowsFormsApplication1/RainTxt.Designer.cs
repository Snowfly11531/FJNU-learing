namespace CAFloodModel
{
    partial class RainTxt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RainTxt));
            this.btnRain = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tBRainPath = new System.Windows.Forms.TextBox();
            this.userControl11 = new CAFloodModel.UserControl1();
            this.label2 = new System.Windows.Forms.Label();
            this.tBInternal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tBSavePath = new System.Windows.Forms.TextBox();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRain
            // 
            this.btnRain.Image = ((System.Drawing.Image)(resources.GetObject("btnRain.Image")));
            this.btnRain.Location = new System.Drawing.Point(457, 35);
            this.btnRain.Name = "btnRain";
            this.btnRain.Size = new System.Drawing.Size(27, 23);
            this.btnRain.TabIndex = 120;
            this.btnRain.UseVisualStyleBackColor = true;
            this.btnRain.Click += new System.EventHandler(this.btnRain_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 119;
            this.label1.Text = "插值点输入：";
            // 
            // tBRainPath
            // 
            this.tBRainPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBRainPath.Location = new System.Drawing.Point(110, 34);
            this.tBRainPath.Name = "tBRainPath";
            this.tBRainPath.Size = new System.Drawing.Size(341, 26);
            this.tBRainPath.TabIndex = 118;
            // 
            // userControl11
            // 
            this.userControl11.Location = new System.Drawing.Point(-1, 63);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(503, 241);
            this.userControl11.TabIndex = 121;
            this.userControl11.Load += new System.EventHandler(this.userControl11_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 123;
            this.label2.Text = "时间间隔：";
            // 
            // tBInternal
            // 
            this.tBInternal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBInternal.Location = new System.Drawing.Point(110, 302);
            this.tBInternal.Name = "tBInternal";
            this.tBInternal.Size = new System.Drawing.Size(341, 26);
            this.tBInternal.TabIndex = 122;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(457, 307);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 15);
            this.label7.TabIndex = 138;
            this.label7.Text = "s";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(342, 394);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 24);
            this.button3.TabIndex = 140;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(15, 394);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(142, 24);
            this.btnRun.TabIndex = 139;
            this.btnRun.Text = "开始生成";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(13, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 142;
            this.label3.Text = "输出txt位置：";
            // 
            // tBSavePath
            // 
            this.tBSavePath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBSavePath.Location = new System.Drawing.Point(110, 344);
            this.tBSavePath.Name = "tBSavePath";
            this.tBSavePath.Size = new System.Drawing.Size(341, 26);
            this.tBSavePath.TabIndex = 141;
            // 
            // btnSaveData
            // 
            this.btnSaveData.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveData.Image")));
            this.btnSaveData.Location = new System.Drawing.Point(457, 344);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(27, 23);
            this.btnSaveData.TabIndex = 143;
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // RainTxt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 442);
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBSavePath);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tBInternal);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.btnRain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBRainPath);
            this.Name = "RainTxt";
            this.Text = "RainTxt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBRainPath;
        private UserControl1 userControl11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBInternal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBSavePath;
        private System.Windows.Forms.Button btnSaveData;
    }
}