namespace NC.OpenCvSharp4.Learning.Forms
{
    partial class Form11
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
            pcbImageShow.Location = new Point(0, 32);
            pcbImageShow.Name = "pcbImageShow";
            pcbImageShow.Size = new Size(800, 418);
            pcbImageShow.TabIndex = 0;
            pcbImageShow.TabStop = false;
            pcbImageShow.MouseDown += pcbImageShow_MouseDown;
            pcbImageShow.MouseMove += pcbImageShow_MouseMove;
            pcbImageShow.MouseUp += pcbImageShow_MouseUp;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(0, -1);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(800, 34);
            btnRun.TabIndex = 1;
            btnRun.Text = "打开摄像头，追踪目标";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // Form11
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRun);
            Controls.Add(pcbImageShow);
            Name = "Form11";
            Text = "11. 彩色目标追踪";
            Load += Form11_Load;
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pcbImageShow;
        private Button btnRun;
    }
}