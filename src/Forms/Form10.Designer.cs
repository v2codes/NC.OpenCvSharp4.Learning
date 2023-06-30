namespace NC.OpenCvSharp4.Learning.Forms
{
    partial class Form10
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
            btnOpenCamera = new Button();
            btnStartRecord = new Button();
            btnStopRecord = new Button();
            btnPlay = new Button();
            pcbImageShow = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).BeginInit();
            SuspendLayout();
            // 
            // btnOpenCamera
            // 
            btnOpenCamera.Location = new Point(12, 12);
            btnOpenCamera.Name = "btnOpenCamera";
            btnOpenCamera.Size = new Size(140, 52);
            btnOpenCamera.TabIndex = 0;
            btnOpenCamera.Text = "打开摄像头";
            btnOpenCamera.UseVisualStyleBackColor = true;
            btnOpenCamera.Click += btnOpenCamera_Click;
            // 
            // btnStartRecord
            // 
            btnStartRecord.Location = new Point(356, 12);
            btnStartRecord.Name = "btnStartRecord";
            btnStartRecord.Size = new Size(140, 52);
            btnStartRecord.TabIndex = 1;
            btnStartRecord.Text = "开始录像";
            btnStartRecord.UseVisualStyleBackColor = true;
            btnStartRecord.Click += btnStartRecord_Click;
            // 
            // btnStopRecord
            // 
            btnStopRecord.Location = new Point(502, 12);
            btnStopRecord.Name = "btnStopRecord";
            btnStopRecord.Size = new Size(140, 52);
            btnStopRecord.TabIndex = 2;
            btnStopRecord.Text = "停止录像";
            btnStopRecord.UseVisualStyleBackColor = true;
            btnStopRecord.Click += btnStopRecord_Click;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(648, 12);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(140, 52);
            btnPlay.TabIndex = 3;
            btnPlay.Text = "播放录像";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // pcbImageShow
            // 
            pcbImageShow.Location = new Point(12, 70);
            pcbImageShow.Name = "pcbImageShow";
            pcbImageShow.Size = new Size(776, 368);
            pcbImageShow.TabIndex = 4;
            pcbImageShow.TabStop = false;
            // 
            // Form10
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pcbImageShow);
            Controls.Add(btnPlay);
            Controls.Add(btnStopRecord);
            Controls.Add(btnStartRecord);
            Controls.Add(btnOpenCamera);
            Name = "Form10";
            Text = "10. 摄像头录制和播放";
            FormClosed += Form10_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pcbImageShow).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenCamera;
        private Button btnStartRecord;
        private Button btnStopRecord;
        private Button btnPlay;
        private PictureBox pcbImageShow;
    }
}