namespace NC.OpenCvSharp4.Learning.Forms
{
    partial class Form16
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
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).BeginInit();
            SuspendLayout();
            // 
            // pcbImageShow
            // 
            pcbImageShow.BackColor = Color.Black;
            pcbImageShow.Dock = DockStyle.Fill;
            pcbImageShow.Location = new Point(0, 0);
            pcbImageShow.Name = "pcbImageShow";
            pcbImageShow.Size = new Size(800, 450);
            pcbImageShow.TabIndex = 0;
            pcbImageShow.TabStop = false;
            pcbImageShow.MouseDown += pcbImageShow_MouseDown;
            pcbImageShow.MouseMove += pcbImageShow_MouseMove;
            pcbImageShow.MouseUp += pcbImageShow_MouseUp;
            // 
            // Form16
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pcbImageShow);
            Name = "Form16";
            Text = "16. 鼠标绘制矩形";
            Load += Form16_Load;
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pcbImageShow;
    }
}