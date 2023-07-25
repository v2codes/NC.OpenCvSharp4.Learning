namespace NC.OpenCvSharp4.Learning.Forms
{
    partial class FormOptimized
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
            lblValue = new Label();
            btnSetting = new Button();
            pcbImageShow = new PictureBox();
            label2 = new Label();
            lblTimes = new Label();
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(160, 24);
            label1.Name = "label1";
            label1.Size = new Size(102, 17);
            label1.TabIndex = 0;
            label1.Text = "UseOptimized：";
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.Location = new Point(268, 24);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(58, 17);
            lblValue.TabIndex = 0;
            lblValue.Text = "- Value -";
            // 
            // btnSetting
            // 
            btnSetting.Location = new Point(12, 12);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(130, 40);
            btnSetting.TabIndex = 1;
            btnSetting.Text = "开启/关闭性能优化";
            btnSetting.UseVisualStyleBackColor = true;
            btnSetting.Click += btnSetting_Click;
            // 
            // pcbImageShow
            // 
            pcbImageShow.Location = new Point(12, 58);
            pcbImageShow.Name = "pcbImageShow";
            pcbImageShow.Size = new Size(776, 380);
            pcbImageShow.TabIndex = 2;
            pcbImageShow.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(409, 24);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 0;
            label2.Text = "耗时：";
            // 
            // lblTimes
            // 
            lblTimes.AutoSize = true;
            lblTimes.Location = new Point(459, 24);
            lblTimes.Name = "lblTimes";
            lblTimes.Size = new Size(58, 17);
            lblTimes.TabIndex = 0;
            lblTimes.Text = "- Value -";
            // 
            // FormOptimized
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pcbImageShow);
            Controls.Add(btnSetting);
            Controls.Add(lblTimes);
            Controls.Add(label2);
            Controls.Add(lblValue);
            Controls.Add(label1);
            Name = "FormOptimized";
            Text = "程序性能检测及优化";
            Load += FormOptimized_Load;
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblValue;
        private Button btnSetting;
        private PictureBox pcbImageShow;
        private Label label2;
        private Label lblTimes;
    }
}