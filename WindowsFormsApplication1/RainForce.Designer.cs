namespace CAFloodModel
{
    partial class RainForce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RainForce));
            this.button3 = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tBSavePath = new System.Windows.Forms.TextBox();
            this.btnInfil = new System.Windows.Forms.Button();
            this.btnRain = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tBInfilPath = new System.Windows.Forms.TextBox();
            this.tBRainPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tBRainTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(414, 272);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 24);
            this.button3.TabIndex = 134;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(124, 272);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(142, 24);
            this.btnRun.TabIndex = 133;
            this.btnRun.Text = "开始计算";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveData.Image")));
            this.btnSaveData.Location = new System.Drawing.Point(641, 187);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(27, 23);
            this.btnSaveData.TabIndex = 132;
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(21, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 131;
            this.label3.Text = "输出文件夹：";
            // 
            // tBSavePath
            // 
            this.tBSavePath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBSavePath.Location = new System.Drawing.Point(124, 185);
            this.tBSavePath.Name = "tBSavePath";
            this.tBSavePath.Size = new System.Drawing.Size(511, 26);
            this.tBSavePath.TabIndex = 130;
            // 
            // btnInfil
            // 
            this.btnInfil.Image = ((System.Drawing.Image)(resources.GetObject("btnInfil.Image")));
            this.btnInfil.Location = new System.Drawing.Point(641, 99);
            this.btnInfil.Name = "btnInfil";
            this.btnInfil.Size = new System.Drawing.Size(27, 23);
            this.btnInfil.TabIndex = 129;
            this.btnInfil.UseVisualStyleBackColor = true;
            this.btnInfil.Click += new System.EventHandler(this.btnInfil_Click);
            // 
            // btnRain
            // 
            this.btnRain.Image = ((System.Drawing.Image)(resources.GetObject("btnRain.Image")));
            this.btnRain.Location = new System.Drawing.Point(641, 54);
            this.btnRain.Name = "btnRain";
            this.btnRain.Size = new System.Drawing.Size(27, 23);
            this.btnRain.TabIndex = 128;
            this.btnRain.UseVisualStyleBackColor = true;
            this.btnRain.Click += new System.EventHandler(this.btnRain_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 127;
            this.label2.Text = "最大入渗量输入：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 126;
            this.label1.Text = "雨量数据文件夹：";
            // 
            // tBInfilPath
            // 
            this.tBInfilPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBInfilPath.Location = new System.Drawing.Point(124, 97);
            this.tBInfilPath.Name = "tBInfilPath";
            this.tBInfilPath.Size = new System.Drawing.Size(511, 26);
            this.tBInfilPath.TabIndex = 125;
            // 
            // tBRainPath
            // 
            this.tBRainPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBRainPath.Location = new System.Drawing.Point(124, 52);
            this.tBRainPath.Name = "tBRainPath";
            this.tBRainPath.Size = new System.Drawing.Size(511, 26);
            this.tBRainPath.TabIndex = 124;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 136;
            this.label4.Text = "降雨历时：";
            // 
            // tBRainTime
            // 
            this.tBRainTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBRainTime.Location = new System.Drawing.Point(124, 141);
            this.tBRainTime.Name = "tBRainTime";
            this.tBRainTime.Size = new System.Drawing.Size(511, 26);
            this.tBRainTime.TabIndex = 135;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(641, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 15);
            this.label7.TabIndex = 137;
            this.label7.Text = "s";
            // 
            // RainForce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 349);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tBRainTime);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBSavePath);
            this.Controls.Add(this.btnInfil);
            this.Controls.Add(this.btnRain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBInfilPath);
            this.Controls.Add(this.tBRainPath);
            this.Name = "RainForce";
            this.Text = "depth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBSavePath;
        private System.Windows.Forms.Button btnInfil;
        private System.Windows.Forms.Button btnRain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBInfilPath;
        private System.Windows.Forms.TextBox tBRainPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBRainTime;
        private System.Windows.Forms.Label label7;
    }
}