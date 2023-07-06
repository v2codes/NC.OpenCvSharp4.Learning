using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form12 : Form
    {
        /// <summary>
        /// 当前图片
        /// </summary>
        private Mat _gray = new Mat();

        /// <summary>
        /// 预测图片
        /// </summary>
        private Mat _grayPrev = new Mat();

        /// <summary>
        /// 特征点的原来位置
        /// </summary>
        private Point2f[] _points1;

        /// <summary>
        /// 特征点的新位置
        /// </summary>
        private Point2f[] _points2;

        /// <summary>
        /// 初始化跟踪点位置
        /// </summary>
        private Point2f[] _initial;

        /// <summary>
        /// 检测的最大角点数（特征数）
        /// </summary>
        private int _maxCorners = 300;

        /// <summary>
        /// 特征检测等级
        /// </summary>
        private double _qualityLevel = 0.01;

        /// <summary>
        /// 两特征点之间的最小距离
        /// </summary>
        private double _minDistance = 10.0;

        /// <summary>
        /// 跟踪特征的状态，特征的流发现为1，否则为0
        /// </summary>
        private byte[] _status;

        private float[] err;

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            Task.Run(Run);
        }

        private void Run()
        {
            var capture = new VideoCapture(FormUtils.VIDEO_ZX4R); // FormUtils.VIDEO_ZX4R
            // 计算帧率
            var sleepTime = (int)Math.Round(1000 / capture.Fps);

            // 逐帧图像
            var frame = new Mat();
            while (true)
            {
                capture.Read(frame);
                // 判断是否读取到图像
                if (frame.Empty()) break;

                var resizeFrame = new Mat();
                Cv2.Resize(frame, resizeFrame, new Size(pcbImageShow.Width, pcbImageShow.Height));

                var result = Tracking(resizeFrame);

                // 在 PictureBox 中显示效果图
                pcbImageShow.Image = BitmapConverter.ToBitmap(result);
                if (Cv2.WaitKey(sleepTime) == 27) // ESC
                {
                    break;
                }
            }

        }

        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="frame">输入视频帧图像</param>
        /// <returns>含跟踪结果的视频帧图像</returns>
        private Mat Tracking(Mat frame)
        {
            var output = new Mat();
            Cv2.CvtColor(frame, _gray, ColorConversionCodes.BGR2GRAY);
            frame.CopyTo(output);

            // 添加特征点
            var featurePoints = AddFeaturePoints();
            if (featurePoints)
            {
                // 像素级检测特征点
                // _points1 = Cv2.GoodFeaturesToTrack(_gray, _maxCorners, _featureLevel, _minDist, new Mat(), 3, true, 0.04);
                // 亚像素级检测
                // _points1 = Cv2.CornerSubPix(_gray, po, new Size(5, 5), new Size(-1, -1), new TermCriteria());

                /*
                 * Cv2.GoodFeaturesToTrack 参数说明
                 * - src: 输入图像，8位或32位浮点型单通道图像，一般用灰度图
                 * - maxCorners: 限定检测到的角点数的最大值
                 * - qualityLevel: 质量等级，质量测量值乘以这个等级就是最小特征值，小余这个数的会被抛弃，取值0.1~0.01之间，不能大与1
                 * - minDistance: 相邻角点的最小距离，小余这个距离的点将会合并处理
                 * - mask: 检测区域，如果图像不为空（需要是CV_8UC1类型、大小与图像相同），且mask值为0处不进行角点检测
                 * - blockSize: 用于计算角点时参与运算的区域大小，常用值为3，如果图像分辨率较高，则可以考虑使用较大值
                 * - useHarrisDetector: 指定角点检测方法，如果为true则使用 Harris 角点检测，否则使用 Shi Tomasi 算法
                 * - k: Harris检测方法的自由参数，建议使用默认值 0.04
                 */
                // 只用这个好像也没什么区别？ 
                _points1 = Cv2.GoodFeaturesToTrack(_gray, _maxCorners, _qualityLevel, _minDistance, new Mat(), 10, true, 0.04); // size: new Size(_gray.Width, _gray.Height), MatType.CV_8UC1
                _initial = _points1;
            }

            if (_points1.Length == 0)
            {
                return frame;
            }

            if (_grayPrev.Empty())
            {
                _gray.CopyTo(_grayPrev);
            }

            // 光流金字塔，输出图二的特征点
            _points2 = new Point2f[_points1.Length];
            Cv2.CalcOpticalFlowPyrLK(_grayPrev, _gray, _points1, ref _points2, out _status, out err);

            // 去掉一些不好的特征点
            var k = 0;
            for (int i = 0; i < _points2.Length; i++)
            {
                if (AcceptTrackedPoint(i))
                {
                    _initial[k] = _initial[i];
                    _points2[k++] = _points2[i];
                }
            }

            // 显示特征点和运动轨迹
            for (int i = 0; i < k; i++)
            {
                Cv2.Line(output, (Point)_initial[i], (Point)_points2[i], new Scalar(0, 0, 255));
                Cv2.Circle(output, (Point)_points2[i], 3, new Scalar(0, 255, 0), -1);
            }

            // 把当前跟踪结果作为下一此参考
            Swap(ref _points2, ref _points1);
            Swap(ref _grayPrev, ref _gray);

            return output;
        }

        /// <summary>
        /// 检测新点是否应该被添加
        /// </summary>
        /// <returns>是否被添加标志</returns>
        private bool AddFeaturePoints()
        {
            if (_points1 == null) return true;

            // 这个实际上是限制了点数，最好别开
            //return  points1.Length <= 10;
            //System.Diagnostics.Debug.WriteLine(points1.Length);

            return true;
        }

        /// <summary>
        /// 决定哪些跟踪点被接受
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool AcceptTrackedPoint(int i)
        {
            return _status[i] == 1 && ((Math.Abs(_points1[i].X - _points2[i].X) + Math.Abs(_points1[i].Y - _points2[i].Y)) > 5);
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
    }
}
