namespace NC.OpenCvSharp4.Learning.Forms
{
    partial class Form10_1
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
            label1 = new Label();
            lblMSE = new Label();
            label3 = new Label();
            lblSSIM = new Label();
            label5 = new Label();
            lblPSNR = new Label();
            label2 = new Label();
            label4 = new Label();
            label6 = new Label();
            btnRun = new Button();
            label7 = new Label();
            lblBaseFps = new Label();
            label9 = new Label();
            lblCurrentFps = new Label();
            label8 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(81, 136);
            label1.Name = "label1";
            label1.Size = new Size(137, 22);
            label1.TabIndex = 0;
            label1.Text = "均方误差(MSE)：";
            // 
            // lblMSE
            // 
            lblMSE.AutoSize = true;
            lblMSE.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblMSE.Location = new Point(214, 136);
            lblMSE.Name = "lblMSE";
            lblMSE.Size = new Size(118, 22);
            lblMSE.TabIndex = 0;
            lblMSE.Text = "- MSE Value -";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(65, 231);
            label3.Name = "label3";
            label3.Size = new Size(159, 22);
            label3.TabIndex = 0;
            label3.Text = "结构相似度(SSIM)：";
            // 
            // lblSSIM
            // 
            lblSSIM.AutoSize = true;
            lblSSIM.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblSSIM.Location = new Point(214, 231);
            lblSSIM.Name = "lblSSIM";
            lblSSIM.Size = new Size(124, 22);
            lblSSIM.TabIndex = 0;
            lblSSIM.Text = "- SSIM Value -";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(63, 312);
            label5.Name = "label5";
            label5.Size = new Size(164, 22);
            label5.TabIndex = 0;
            label5.Text = "峰值信噪比(PSNR)：";
            // 
            // lblPSNR
            // 
            lblPSNR.AutoSize = true;
            lblPSNR.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblPSNR.Location = new Point(214, 312);
            lblPSNR.Name = "lblPSNR";
            lblPSNR.Size = new Size(129, 22);
            lblPSNR.TabIndex = 0;
            lblPSNR.Text = "- PNSR Value -";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(111, 159);
            label2.Name = "label2";
            label2.Size = new Size(236, 17);
            label2.TabIndex = 1;
            label2.Text = "预测值f(x)与目标值y之间差值平方和的均值";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(111, 253);
            label4.Name = "label4";
            label4.Size = new Size(446, 17);
            label4.TabIndex = 1;
            label4.Text = "SSIM是衡量两张图像相似度的指标，可以测量增强后的图像与真实图像之间的差异";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(111, 334);
            label6.Name = "label6";
            label6.Size = new Size(672, 17);
            label6.TabIndex = 1;
            label6.Text = "通过计算两幅图像的峰值信噪比PSNR，当PSNR的值越小时表示两幅图像越相似，当PSNR = 0 时则表示两幅图像完全相同。";
            // 
            // btnRun
            // 
            btnRun.Location = new Point(12, 12);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(776, 49);
            btnRun.TabIndex = 2;
            btnRun.Text = "启动图像相似度检测";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(81, 88);
            label7.Name = "label7";
            label7.Size = new Size(74, 22);
            label7.TabIndex = 0;
            label7.Text = "比对帧：";
            // 
            // lblBaseFps
            // 
            lblBaseFps.AutoSize = true;
            lblBaseFps.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblBaseFps.ForeColor = Color.Red;
            lblBaseFps.Location = new Point(214, 88);
            lblBaseFps.Name = "lblBaseFps";
            lblBaseFps.Size = new Size(118, 22);
            lblBaseFps.TabIndex = 0;
            lblBaseFps.Text = "- MSE Value -";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(433, 88);
            label9.Name = "label9";
            label9.Size = new Size(74, 22);
            label9.TabIndex = 0;
            label9.Text = "当前帧：";
            // 
            // lblCurrentFps
            // 
            lblCurrentFps.AutoSize = true;
            lblCurrentFps.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblCurrentFps.ForeColor = Color.Red;
            lblCurrentFps.Location = new Point(566, 88);
            lblCurrentFps.Name = "lblCurrentFps";
            lblCurrentFps.Size = new Size(118, 22);
            lblCurrentFps.TabIndex = 0;
            lblCurrentFps.Text = "- MSE Value -";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(111, 176);
            label8.Name = "label8";
            label8.Size = new Size(598, 17);
            label8.TabIndex = 1;
            label8.Text = "当真实值y和预测值f(x)的差值大于1时，会放大误差；而当差值小于1时，则会缩小误差，这是平方运算决定的。";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(111, 193);
            label10.Name = "label10";
            label10.Size = new Size(426, 17);
            label10.TabIndex = 1;
            label10.Text = "MSE对于较大的误差(>1)给予较大的惩罚，较小的误差(<1）给予较小的惩罚。";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(111, 270);
            label11.Name = "label11";
            label11.Size = new Size(441, 17);
            label11.TabIndex = 1;
            label11.Text = "取值范围为[0,1]，值越大表示输出图像和无失真图像的差距越小，即图像质量越好";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(111, 287);
            label12.Name = "label12";
            label12.Size = new Size(213, 17);
            label12.TabIndex = 1;
            label12.Text = "当两张图像越相似时，则SSIM越接近1";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(111, 351);
            label13.Name = "label13";
            label13.Size = new Size(448, 17);
            label13.TabIndex = 1;
            label13.Text = "PSNR是基于每个像素误差计算而来，所以得出的结果可能和人眼视觉有较大差异。";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(109, 368);
            label14.Name = "label14";
            label14.Size = new Size(426, 17);
            label14.TabIndex = 1;
            label14.Text = "一般来说，当两幅图像的PSNR小于30时，那么这两幅图像可以说是比较相似的";
            // 
            // Form10_1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRun);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label6);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label4);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(label2);
            Controls.Add(lblPSNR);
            Controls.Add(label5);
            Controls.Add(lblSSIM);
            Controls.Add(label3);
            Controls.Add(lblCurrentFps);
            Controls.Add(lblBaseFps);
            Controls.Add(label9);
            Controls.Add(lblMSE);
            Controls.Add(label7);
            Controls.Add(label1);
            Name = "Form10_1";
            Text = "10.1 双摄像头操作与图像相似度检测";
            Load += Form10_1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblMSE;
        private Label label3;
        private Label lblSSIM;
        private Label label5;
        private Label lblPSNR;
        private Label label2;
        private Label label4;
        private Label label6;
        private Button btnRun;
        private Label label7;
        private Label lblBaseFps;
        private Label label9;
        private Label lblCurrentFps;
        private Label label8;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
    }
}