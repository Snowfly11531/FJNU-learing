namespace CAFloodModel
{
    partial class IDW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDW));
            this.tBPointPath = new System.Windows.Forms.TextBox();
            this.tBRefPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPoint = new System.Windows.Forms.Button();
            this.btnRef = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tBSavePath = new System.Windows.Forms.TextBox();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tBPointPath
            // 
            this.tBPointPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBPointPath.Location = new System.Drawing.Point(119, 47);
            this.tBPointPath.Name = "tBPointPath";
            this.tBPointPath.Size = new System.Drawing.Size(511, 26);
            this.tBPointPath.TabIndex = 0;
            this.tBPointPath.TextChanged += new System.EventHandler(this.tBPointPath_TextChanged);
            // 
            // tBRefPath
            // 
            this.tBRefPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBRefPath.Location = new System.Drawing.Point(119, 92);
            this.tBRefPath.Name = "tBRefPath";
            this.tBRefPath.Size = new System.Drawing.Size(511, 26);
            this.tBRefPath.TabIndex = 1;
            this.tBRefPath.TextChanged += new System.EventHandler(this.tBRefPath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(22, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "插值点输入：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "参考栅格输入：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnPoint
            // 
            this.btnPoint.Image = ((System.Drawing.Image)(resources.GetObject("btnPoint.Image")));
            this.btnPoint.Location = new System.Drawing.Point(636, 49);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(27, 23);
            this.btnPoint.TabIndex = 117;
            this.btnPoint.UseVisualStyleBackColor = true;
            this.btnPoint.Click += new System.EventHandler(this.btnPoint_Click);
            // 
            // btnRef
            // 
            this.btnRef.Image = ((System.Drawing.Image)(resources.GetObject("btnRef.Image")));
            this.btnRef.Location = new System.Drawing.Point(636, 94);
            this.btnRef.Name = "btnRef";
            this.btnRef.Size = new System.Drawing.Size(27, 23);
            this.btnRef.TabIndex = 118;
            this.btnRef.UseVisualStyleBackColor = true;
            this.btnRef.Click += new System.EventHandler(this.btnRef_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 120;
            this.label3.Text = "插值点输入：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // tBSavePath
            // 
            this.tBSavePath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBSavePath.Location = new System.Drawing.Point(119, 168);
            this.tBSavePath.Name = "tBSavePath";
            this.tBSavePath.Size = new System.Drawing.Size(511, 26);
            this.tBSavePath.TabIndex = 119;
            this.tBSavePath.TextChanged += new System.EventHandler(this.tBSavePath_TextChanged);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveData.Image")));
            this.btnSaveData.Location = new System.Drawing.Point(636, 170);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(27, 23);
            this.btnSaveData.TabIndex = 121;
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(119, 267);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(142, 24);
            this.btnRun.TabIndex = 122;
            this.btnRun.Text = "开始计算";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(409, 267);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 24);
            this.button3.TabIndex = 123;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // IDW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 349);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBSavePath);
            this.Controls.Add(this.btnRef);
            this.Controls.Add(this.btnPoint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBRefPath);
            this.Controls.Add(this.tBPointPath);
            this.Name = "IDW";
            this.Text = "IDW";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBPointPath;
        private System.Windows.Forms.TextBox tBRefPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPoint;
        private System.Windows.Forms.Button btnRef;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBSavePath;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button button3;

    }
}