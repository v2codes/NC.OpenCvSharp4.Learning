namespace NC.OpenCvSharp4.Learning.Forms
{
    partial class Form13
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
            pcbImageShow = new PictureBox();
            btnRun = new Button();
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).BeginInit();
            SuspendLayout();
            // 
            // pcbImageShow
            // 
            pcbImageShow.Location = new Point(0, 52);
            pcbImageShow.Name = "pcbImageShow";
            pcbImageShow.Size = new Size(800, 398);
            pcbImageShow.TabIndex = 0;
            pcbImageShow.TabStop = false;
            pcbImageShow.MouseClick += pcbImageShow_MouseClick;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(0, 1);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(800, 45);
            btnRun.TabIndex = 1;
            btnRun.Text = "打开摄像头，启动点追踪";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // Form13
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRun);
            Controls.Add(pcbImageShow);
            Name = "Form13";
            Text = "13. 点追踪";
            Load += Form13_Load;
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pcbImageShow;
        private Button btnRun;
    }
}