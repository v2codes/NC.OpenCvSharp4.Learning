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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form13 : Form
    {

        private VideoCapture _videoCapture = new VideoCapture();
        private Point2f _point;
        bool _addRemovePoint = false;

        public Form13()
        {
            InitializeComponent();
        }

        private void Form13_Load(object sender, EventArgs e)
        {
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void Run()
        {
            // 打开摄像头
            _videoCapture.Open(0);
            // 判断摄像头是否成功打开
            if (!_videoCapture.IsOpened())
            {
                MessageBox.Show("摄像头打开失败.");
                return;
            }

            // 设置采集的图像尺寸为
            _videoCapture.Set(VideoCaptureProperties.FrameWidth, pcbImageShow.Width);
            _videoCapture.Set(VideoCaptureProperties.FrameHeight, pcbImageShow.Height);
            _videoCapture.FrameWidth = pcbImageShow.Width;
            _videoCapture.FrameHeight = pcbImageShow.Height;
            // _videoCapture.Set(VideoCaptureProperties.Exposure, 0); // 曝光

            var frame = new Mat();
            var image = new Mat();
            var gray = new Mat();
            var prevGray = new Mat();
            var needToInit = true;
            var nightMode = false;
            Point2f[] points1 = null;
            Point2f[] points2 = null;
            const int MAX_COUNT = 500;

            // 定义迭代算法终止标准的类
            var criteria = new TermCriteria(CriteriaTypes.MaxIter | CriteriaTypes.Eps, 20, 0.03);

            //Cv2.NamedWindow("CamShift Demo");
            //Cv2.SetMouseCallback("CamShift Demo", new MouseCallback(onMouse));
            //var window = new Window("CamShift Demo");

            while (true)
            {
                if (_videoCapture.Read(frame))
                {
                    frame.CopyTo(image);
                    Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

                    // 自动初始化
                    if (needToInit)
                    {
                        //points2 = Cv2.GoodFeaturesToTrack(gray, 500, 0.01, 10, new Mat(), 3, false, 0.04);

                        // 像素级检测特征点
                        // GoodFeaturesToTrack 它不仅支持Harris角点检测，也支持Shi Tomasi算法的角点检测。
                        Point2f[] po = Cv2.GoodFeaturesToTrack(gray, MAX_COUNT, 0.01, 10, new Mat(), 3, false, 0.04);

                        // 亚像素级检测
                        // 但是，GoodFeaturesToTrack 函数检测到的角点依然是像素级别的，若想获取更为精细的角点坐标，
                        // 则需要调用cv::cornerSubPix()函数进一步细化处理，即亚像素。
                        points2 = Cv2.CornerSubPix(gray, po, new Size(10, 10), new Size(-1, -1), criteria);
                        _addRemovePoint = false;
                    }
                    else if (points1 != null && points1.Length > 0)
                    {
                        byte[] status;
                        float[] error;
                        if (prevGray.Empty())
                        {
                            gray.CopyTo(prevGray);
                        }

                        // 光流金字塔，输出图二的特征点
                        // 由于目标对象或者摄像机的移动造成的图像对象在连续两帧图像中的移动被称为光流。
                        // 它是一个 2D 向量场，可以用来表示一个点从第一帧图像到第二帧图像之间的移动。
                        points2 = new Point2f[points1.Length];
                        Cv2.CalcOpticalFlowPyrLK(prevGray, gray, points1, ref points2, out status, out error); // 前一帧的角点和当前帧的图像作为输入来得到角点在当前帧的位置

                        int i, k;
                        for (i = k = 0; i < points2.Length; i++)
                        {
                            if (_addRemovePoint)
                            {
                                Point2f p = _point;
                                Point2f p2 = points2[i];

                                // 计算两点之间的欧几里德距离
                                // C# 里没有计算范数的函数Norm，我这里直接按距离算了
                                // var norm = Math.Sqrt(Math.Abs(p.X - p2.X) * Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) * Math.Abs(p.Y - p2.Y));

                                #region v4版本，已有 Norm 函数，这个函数是求解范数，怎么用呢？

                                // 统计函数~L1、L2、无穷范数、汉明范数(norm,NORM_HAMMING2)
                                // L1范数是所有元素的绝对值的和；L2范数是所有元素(绝对值)的平方和再开方；无穷范数是所有元素取绝对值后再取最大值；
                                // 在OpenCV中所有元素展开成一个集合构成了上述x1，x2……xn；

                                // 汉明范数不在上述定义中，汉明范数又称汉明距离，最开始用于数据传输的差错控制编码，表示两个相同长度的二进制数值其对应bit位不同的数量。
                                // 统计两个数对应bit位的差异，需要对两个数值进行异或运算，统计异或结果中1的个数就是它们的汉明范数(汉明距离)。
                                // 统计单个数值的汉明范数可以看做将这个数和0进行异或运算后，统计异或结果中1的个数，因为一个数值和0进行异或得到的是其自己，所以统计单个数值的汉明范数就是统计自身bit位为1的个数。
                                // 计算一个数据集合的汉明范数就是计算这些单个元素汉明范数的总和。
                                // 更多资料：http://www.juzicode.com/opencv-python-statistics-norm/

                                var norm2 = Cv2.Norm(InputArray.Create(new[] { p }), InputArray.Create(new[] { p2 }));
                                #endregion

                                if (norm2 <= 5)
                                {
                                    _addRemovePoint = false;
                                    continue;
                                }
                            }
                            if (status[i] == 0x00)
                            {
                                continue;
                            }

                            points2[k++] = points2[i];
                            Cv2.Circle(image, (Point)points2[i], 3, new Scalar(0, 255, 0), 8);


                        }

                        // C++ 的resize功能
                        points2 = points2.ToList().Take(k).ToList().ToArray();
                    }

                    if (_addRemovePoint && (points2 == null || points2.Length < MAX_COUNT))
                    {
                        Point2f[] tmp = new Point2f[] { _point };
                        Point2f[] tmp2 = Cv2.CornerSubPix(gray, tmp, new Size(10, 10), new Size(-1, -1), criteria);

                        // C++ 的push_back功能
                        List<Point2f> a = points2.ToList();
                        a.Add(tmp2[0]);
                        points2 = a.ToArray();

                        _addRemovePoint = false;
                    }

                    needToInit = false;

                    // 在Window窗口中播放视频(方法1)
                    // window.ShowImage(image);
                    // 在Window窗口中播放视频(方法2)
                    // Cv2.ImShow("CamShift Demo", Img);
                    // 在pictureBox1中显示效果图
                    pcbImageShow.Image = BitmapConverter.ToBitmap(image);

                    char c = (char)Cv2.WaitKey(10);
                    if (c == 27) break;
                    switch (c)
                    {
                        case 'r':
                            needToInit = true;
                            break;
                        case 'c':
                            points1 = new Point2f[] { };
                            points2 = new Point2f[] { };
                            break;
                        case 'n':
                            nightMode = !nightMode;
                            break;
                    }

                    Swap(ref points2, ref points1);
                    Swap(ref prevGray, ref gray);
                }
            }
        }

        public void onMouse(MouseEventTypes @event, int x, int y, MouseEventFlags flags, IntPtr userData)
        {
            if (@event == MouseEventTypes.LButtonDown)
            {
                _point = new Point2f((float)x, (float)y);
                _addRemovePoint = true;
            }
        }

        private void pcbImageShow_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _point = new Point2f((float)e.X, (float)e.Y);
            _addRemovePoint = true;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
    }
}
