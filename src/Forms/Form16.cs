using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form16 : Form
    {
        private Mat _srcImage { get; set; }

        Rect _rectangle;
        bool _onDrawing = false; // 是否进行绘制中
        Random _random = new Random();

        public Form16()
        {
            InitializeComponent();
        }
        private void Form16_Load(object sender, EventArgs e)
        {
            _srcImage = new Mat(pcbImageShow.Height, pcbImageShow.Width, MatType.CV_8UC3, Scalar.All(0));
            Task.Run(Run);
        }

        private void Run()
        {
            // 准备参数
            _rectangle = new Rect(-1, -1, 0, 0);
            var tempImage = new Mat();

            // 设置OpenCV窗口鼠标操作回调函数
            //Cv2.NamedWindow(WINDOW_NAME);
            //CvMouseCallback GetRGBCvMouseCallback = new CvMouseCallback(on_MouseHandle);
            //Cv2.SetMouseCallback(WINDOW_NAME, GetRGBCvMouseCallback);

            // 程序主循环，当进行绘制的标识符为真时，进行绘制
            while (true)
            {
                // 拷贝源图到临时变量
                _srcImage.CopyTo(tempImage);
                if (_onDrawing)
                {
                    // 当进行绘制的标识符为真，则进行绘制
                    DrawRectangle(tempImage, _rectangle);
                }

                pcbImageShow.Image = BitmapConverter.ToBitmap(tempImage);

                Thread.Sleep(100);
                // 按下ESC键，程序退出
                if (Cv2.WaitKey(10) == 27)
                    break;
            }
        }

        private void pcbImageShow_MouseDown(object sender, MouseEventArgs e)
        {
            // 左键按下消息
            if (e.Button == MouseButtons.Left)
            {
                _onDrawing = true;
                _rectangle = new Rect(e.X, e.Y, 0, 0); // 记录起始点
            }
        }

        private void pcbImageShow_MouseMove(object sender, MouseEventArgs e)
        {
            // 鼠标移动消息
            if (e.Button == MouseButtons.Left)
            {
                // 如果是否进行绘制的标识符为真，则记录下长和宽到 Rect 型变量中
                if (_onDrawing)
                {
                    _rectangle.Width = e.X - _rectangle.X;
                    _rectangle.Height = e.Y - _rectangle.Y;
                }
            }
        }

        private void pcbImageShow_MouseUp(object sender, MouseEventArgs e)
        {
            //左键抬起消息
            if (e.Button == MouseButtons.Left)
            {
                // 设置标识符为false
                _onDrawing = false;
                //对宽和高小于0的处理
                if (_rectangle.Width < 0)
                {
                    _rectangle.X += _rectangle.Width;
                    _rectangle.Width *= -1;
                }
                if (_rectangle.Height < 0)
                {
                    _rectangle.Y += _rectangle.Height;
                    _rectangle.Height *= -1;
                }

                //调用函数进行绘制
                DrawRectangle(_srcImage, _rectangle);
            }
        }

        private void DrawRectangle(Mat img, Rect rectangle)
        {
            if ((rectangle.BottomRight.X > rectangle.TopLeft.X) && (rectangle.BottomRight.Y > rectangle.TopLeft.Y))
            {
                // 随机颜色
                Cv2.Rectangle(img, rectangle.TopLeft, rectangle.BottomRight, new Scalar(_random.Next(255), _random.Next(255), _random.Next(255)), 2);
            }
        }
    }
}
