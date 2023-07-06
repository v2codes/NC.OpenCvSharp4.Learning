namespace NC.OpenCvSharp4.Learning
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn1 = new Button();
            btn2 = new Button();
            btn3 = new Button();
            btn4 = new Button();
            btn5 = new Button();
            btn6 = new Button();
            btn7 = new Button();
            btn8 = new Button();
            btn9 = new Button();
            btn10 = new Button();
            btn11 = new Button();
            btn12 = new Button();
            button6 = new Button();
            button7 = new Button();
            SuspendLayout();
            // 
            // btn1
            // 
            btn1.Location = new Point(12, 12);
            btn1.Name = "btn1";
            btn1.Size = new Size(140, 52);
            btn1.TabIndex = 0;
            btn1.Text = "1. 绘制同心圆";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += btn1_Click;
            // 
            // btn2
            // 
            btn2.Location = new Point(158, 12);
            btn2.Name = "btn2";
            btn2.Size = new Size(140, 52);
            btn2.TabIndex = 1;
            btn2.Text = "2. 打开图片";
            btn2.UseVisualStyleBackColor = true;
            btn2.Click += btn2_Click;
            // 
            // btn3
            // 
            btn3.Location = new Point(304, 12);
            btn3.Name = "btn3";
            btn3.Size = new Size(140, 52);
            btn3.TabIndex = 2;
            btn3.Text = "3. 绘制透明图";
            btn3.UseVisualStyleBackColor = true;
            btn3.Click += btn3_Click;
            // 
            // btn4
            // 
            btn4.Location = new Point(450, 12);
            btn4.Name = "btn4";
            btn4.Size = new Size(140, 52);
            btn4.TabIndex = 3;
            btn4.Text = "4. 混合显示和输出";
            btn4.UseVisualStyleBackColor = true;
            btn4.Click += btn4_Click;
            // 
            // btn5
            // 
            btn5.Location = new Point(596, 12);
            btn5.Name = "btn5";
            btn5.Size = new Size(140, 52);
            btn5.TabIndex = 4;
            btn5.Text = "5. 图像腐蚀Erode";
            btn5.UseVisualStyleBackColor = true;
            btn5.Click += btn5_Click;
            // 
            // btn6
            // 
            btn6.Location = new Point(742, 12);
            btn6.Name = "btn6";
            btn6.Size = new Size(140, 52);
            btn6.TabIndex = 5;
            btn6.Text = "6. 图像模糊Blur";
            btn6.UseVisualStyleBackColor = true;
            btn6.Click += btn6_Click;
            // 
            // btn7
            // 
            btn7.Location = new Point(888, 12);
            btn7.Name = "btn7";
            btn7.Size = new Size(140, 52);
            btn7.TabIndex = 6;
            btn7.Text = "7. 边缘检测Sobel";
            btn7.UseVisualStyleBackColor = true;
            btn7.Click += btn7_Click;
            // 
            // btn8
            // 
            btn8.Location = new Point(12, 70);
            btn8.Name = "btn8";
            btn8.Size = new Size(140, 52);
            btn8.TabIndex = 7;
            btn8.Text = "8. 边缘检测Canny";
            btn8.UseVisualStyleBackColor = true;
            btn8.Click += btn8_Click;
            // 
            // btn9
            // 
            btn9.Location = new Point(158, 70);
            btn9.Name = "btn9";
            btn9.Size = new Size(140, 52);
            btn9.TabIndex = 8;
            btn9.Text = "9. 播放视频";
            btn9.UseVisualStyleBackColor = true;
            btn9.Click += btn9_Click;
            // 
            // btn10
            // 
            btn10.Location = new Point(304, 70);
            btn10.Name = "btn10";
            btn10.Size = new Size(140, 52);
            btn10.TabIndex = 9;
            btn10.Text = "10. 摄像头录制与播放";
            btn10.UseVisualStyleBackColor = true;
            btn10.Click += btn10_Click;
            // 
            // btn11
            // 
            btn11.Location = new Point(450, 70);
            btn11.Name = "btn11";
            btn11.Size = new Size(140, 52);
            btn11.TabIndex = 11;
            btn11.Text = "11. 彩色目标追踪";
            btn11.UseVisualStyleBackColor = true;
            btn11.Click += btn11_Click;
            // 
            // btn12
            // 
            btn12.Location = new Point(596, 70);
            btn12.Name = "btn12";
            btn12.Size = new Size(140, 52);
            btn12.TabIndex = 11;
            btn12.Text = "12. 光流法检测运动目标";
            btn12.UseVisualStyleBackColor = true;
            btn12.Click += btn12_Click;
            // 
            // button6
            // 
            button6.Location = new Point(742, 70);
            button6.Name = "button6";
            button6.Size = new Size(140, 52);
            button6.TabIndex = 12;
            button6.Text = "6. 图像模糊Blur";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btn6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(888, 70);
            button7.Name = "button7";
            button7.Size = new Size(140, 52);
            button7.TabIndex = 13;
            button7.Text = "7. 边缘检测Sobel";
            button7.UseVisualStyleBackColor = true;
            button7.Click += btn7_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 729);
            Controls.Add(button7);
            Controls.Add(btn7);
            Controls.Add(button6);
            Controls.Add(btn6);
            Controls.Add(btn12);
            Controls.Add(btn5);
            Controls.Add(btn11);
            Controls.Add(btn4);
            Controls.Add(btn10);
            Controls.Add(btn3);
            Controls.Add(btn9);
            Controls.Add(btn2);
            Controls.Add(btn8);
            Controls.Add(btn1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OpenCvSharp4基本操作示例";
            ResumeLayout(false);
        }

        #endregion

        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn5;
        private Button btn6;
        private Button btn7;
        private Button btn8;
        private Button btn9;
        private Button btn10;
        private Button btn11;
        private Button btn12;
        private Button button6;
        private Button button7;
    }
}