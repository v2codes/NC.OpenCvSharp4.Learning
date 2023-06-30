namespace NC.OpenCvSharp4.Learning.Forms
{
    partial class Form2
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
            pcbShowImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pcbShowImage).BeginInit();
            SuspendLayout();
            // 
            // pcbShowImage
            // 
            pcbShowImage.Dock = DockStyle.Fill;
            pcbShowImage.Location = new Point(0, 0);
            pcbShowImage.Name = "pcbShowImage";
            pcbShowImage.Size = new Size(800, 450);
            pcbShowImage.TabIndex = 0;
            pcbShowImage.TabStop = false;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pcbShowImage);
            Name = "Form2";
            Text = "2. 打开图片";
            Load += frm2_Load;
            ((System.ComponentModel.ISupportInitialize)pcbShowImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pcbShowImage;
    }
}