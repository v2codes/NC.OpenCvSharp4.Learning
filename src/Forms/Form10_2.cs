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
    public partial class Form10_2 : Form
    {
        public Form10_2()
        {
            InitializeComponent();
        }

        private void Form10_2_Load(object sender, EventArgs e)
        {

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void Run()
        {
            VideoCapture capture = new VideoCapture();
            // 打开ID为0的摄像头
            capture.Open(0);
            // 判断摄像头是否成功打开
            if (!capture.IsOpened())
            {
                MessageBox.Show("摄像头打开失败.");
                return;
            }

            capture.FrameWidth = pcbImageShow.Width;
            capture.FrameHeight = pcbImageShow.Height;
            // RGB
            // BGR
            // 设置颜色阈值，BGR存储顺序，非常规的RGB
            var lower = new Scalar(19, 78, 39); // RGB(39,78,19) // new Scalar(90, 50, 50);
            var upper = new Scalar(168, 255, 182);  // RGB(182,215,168) // new Scalar(130, 255, 255);

            var frame = new Mat();
            while (true)
            {
                if (capture.Read(frame))
                {
                    var hsv = new Mat();
                    var res = new Mat();
                    var mask = new Mat();

                    // 切换到 HSV
                    Cv2.CvtColor(frame, hsv, ColorConversionCodes.BGR2HSV);

                    // 根据阈值构建掩模
                    // 检查数组元素是否位于其他两个数组的元素之间
                    Cv2.InRange(hsv, lower, upper, mask);

                    // 对原图像和掩模进行位运算
                    Cv2.BitwiseAnd(frame, frame, res, mask);

                    // 显示图像
                    pcbImageShow.Image = BitmapConverter.ToBitmap(frame); // 原图

                    Cv2.ImShow("mask", mask);
                    //Cv2.MoveWindow("mask", frame.Width, 0);// 右边
                    Cv2.ImShow("res", res);
                    //Cv2.MoveWindow("res", 0, frame.Height);// 下边

                    if (Cv2.WaitKey(10) == 27)
                    {
                        break;
                    }
                }
            }
        }
    }
}
