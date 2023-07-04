using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form11 : Form
    {
        public Mat _image = new Mat();

        /// <summary>
        /// 是否正在框选
        /// </summary>
        public bool _onMouseSelecting = false;

        /// <summary>
        /// 如果为 -1，代表鼠标左键弹起，区域划定完成
        /// -- 如果有操作，_trackObject 等于 1 或 -1
        /// </summary>
        public int _trackObject = 0;

        /// <summary>
        /// 已选中矩形区域
        /// </summary>
        public Rect _selectedRectangle;
        public OpenCvSharp.Point _origin;

        /// <summary>
        /// 视频操作
        /// </summary>
        public VideoCapture _videoCapture;

        public Form11()
        {
            InitializeComponent();
            _videoCapture = new VideoCapture(0);
        }

        private void Form11_Load(object sender, EventArgs e)
        {
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // 官方直方图 demo
            // CalcHist();

            // 图像跟踪
            ImageTracking();
        }

        #region 官方直方图 demo
        private void CalcHist()
        {
            using var src = new Mat(FormUtils.IMG_GEM_1, ImreadModes.Grayscale);
            Cv2.Resize(src, src, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));

            using var hist = new Mat();
            Cv2.CalcHist(
                images: new[] { src },
                channels: new[] { 0 },
                mask: null,
                hist: hist,
                dims: 1,
                histSize: new[] { 256 },
                ranges: new[] { new Rangef(0, 256) });

            if (Debugger.IsAttached)
            {
                const int histW = 512;
                const int histH = 400;
                var binW = Math.Round((double)histW / 256);
                using var histImage = new Mat(histH, histW, MatType.CV_8UC3, Scalar.All(0));
                Cv2.Normalize(hist, hist, 0, histImage.Rows, NormTypes.MinMax, -1);

                for (int i = 0; i < 256; i++)
                {
                    var pt1 = new Point2d(binW * (i - 1), histH - Math.Round(hist.At<float>(i - 1)));
                    var pt2 = new Point2d(binW * (i), histH - Math.Round(hist.At<float>(i)));
                    Cv2.Line(
                        histImage, (OpenCvSharp.Point)pt1, (OpenCvSharp.Point)pt2,
                        Scalar.Red, 1, LineTypes.Link8);
                }

                Window.ShowImages(src, histImage);
            }
        }
        #endregion

        private void ImageTracking()
        {
            var phrages = new float[] { 0, 180 };
            var trackWindow = new Rect();

            // 打开ID为0的摄像头
            _videoCapture.Open(0);
            // 判断摄像头是否已打开
            if (!_videoCapture.IsOpened())
            {
                MessageBox.Show("摄像头打开失败", "Error");
                return;
            }

            // 设置采集的图像尺寸
            _videoCapture.Set(VideoCaptureProperties.FrameWidth, pcbImageShow.Width);
            _videoCapture.Set(VideoCaptureProperties.FrameHeight, pcbImageShow.Height);
            _videoCapture.FrameWidth = pcbImageShow.Width;
            _videoCapture.FrameHeight = pcbImageShow.Height;

            /*
             * 常见颜色空间通道模式
             * RGB：基于三原色
             * HSV：由A. R. Smith在1978年创建的一种颜色空间, 也称六角锥体模型(Hexcone Model)；
             *   色调（H：hue）：
             *      用角度度量，取值范围为0°～360°，从红色开始按逆时针方向计算，红色为0°，绿色为120°,蓝色为240°。
             *      它们的补色是：黄色为60°，青色为180°，品红为300°；（OpenCV中H的取值范围为0~180，8bit存储时）；
             *   饱和度（S：saturation）：
             *      取值范围为0~255，值越大，颜色越饱和；
             *   亮度（V：value）：
             *      取值范围为0(黑色)～255(白色)；
             * YUV：亮度、色度
             *  
             */

            var frame = new Mat(); // 读取帧
            var hsv = new Mat(); // HSV色彩空间图像
            var hue = new Mat();
            var mask = new Mat();
            var hist = new Mat();
            var backproj = new Mat();
            //Cv2.NamedWindow("Histogram"); // 直方图window

            //Cv2.NamedWindow("CamShift Demo");
            // 设置 OpenCV 窗体鼠标事件回调
            //var rgbCvMouseCallback = new MouseCallback(OnCvMouseCallback);
            //Cv2.SetMouseCallback("CamShift Demo", rgbCvMouseCallback);

            while (true)
            {
                // 读取当前帧，读取成功继续，否则继续或停止
                if (!_videoCapture.Read(frame)) continue;
                if (frame.Empty()) break;

                // 将当前帧复制到 _image 中
                frame.CopyTo(_image);

                // 将 _image 转为 HSV 色彩空间图像，保存到 hsv 变量中
                Cv2.CvtColor(_image, hsv, ColorConversionCodes.BGR2HSV);

                // 如果有操作，_trackObject 等于 1 或 -1
                if (_trackObject != 0)
                {
                    // 亮度范围设置
                    int _vmin = 10, _vmax = 256;

                    // 色彩范围检测
                    // Scalar：色调、饱和度、亮度，第一个Scalar是最小值，第二个Scalar是最大值
                    Cv2.InRange(hsv, new Scalar(0, 30, Math.Min(_vmin, _vmax)), new Scalar(180, 256, Math.Max(_vmin, _vmax)), mask);
                    hue.Create(hsv.Size(), hsv.Depth()); // 创建一个与hsv尺寸和深度一样的图像 hue

                    // 从输入中拷贝某通道到输出中特定的通道
                    // 官方代码参考大概位置：./resources/Form11/Sample-4.1.0-20190417/SamplesCS/Samples/MergeSplitSample.cs: 51 
                    var ch = new int[] { 0, 0 };
                    var input = new Mat[] { hsv };
                    var output = new Mat[] { hue };
                    Cv2.MixChannels(input, output, ch);

                    var range0 = new Rangef(phrages[0], phrages[1]); // 从0开始（含），到180结束（不含）
                    var range = new Rangef[3] { range0, range0, range0 }; // 三个通道范围，通道2/通道3 分别和通道1一样

                    if (_trackObject < 0) // 如果为-1，代表鼠标左键弹起，区域划定完成
                    {
                        // hue是视频帧处理后的图像，_selectedRectangle 是鼠标选定的矩形区域，同时创建一个感兴趣区域和一个标记感兴趣区域
                        var roi = new Mat(hue, _selectedRectangle);
                        var maskRoi = new Mat(mask, _selectedRectangle);

                        // 计算一组图像的联合密集直方图
                        Cv2.CalcHist(images: new[] { roi }, channels: new[] { 0 }, mask: maskRoi, hist: hist, dims: 1,
                                     histSize: new[] { 16 }, ranges: new[] { new Rangef(0, 180) });
                        Cv2.Normalize(hist, hist, 0, 200, NormTypes.MinMax);
                        trackWindow = _selectedRectangle;
                        _trackObject = -1;
                    }

                    var arrs2 = new Mat[] { hue };
                    Cv2.CalcBackProject(arrs2, channels: new[] { 0 }, hist, backproj, range);
                    backproj &= mask;
                    var trackBox = Cv2.CamShift(backproj, ref trackWindow, new TermCriteria(CriteriaTypes.Count | CriteriaTypes.Eps, 10, 1));

                    if ((trackWindow.Width * trackWindow.Height) <= 1)
                    {
                        int cols = backproj.Cols,
                            rows = backproj.Rows,
                            r = (Math.Min(cols, rows) + 5) / 6;
                        trackWindow = new Rect(trackWindow.X - r, trackWindow.Y - r, trackWindow.X + r, trackWindow.Y + r)
                                      & new Rect(0, 0, cols, rows);
                    }

                    // 投影视图
                    // Cv2.CvtColor(backproj, _image, ColorConversionCodes.GRAY2BGR);

                    Cv2.Ellipse(_image, trackBox, new Scalar(0, 0, 255), 3, LineTypes.AntiAlias);
                }

                if (_onMouseSelecting && _selectedRectangle.Width > 0 && _selectedRectangle.Height > 0)
                {
                    var roi = new Mat(_image, _selectedRectangle);
                    // 将src像素的像素值按位取非，比如某像素值为：23，则输出像素值为232，因为23的二进制为10111，按位取反得到11101000即232。
                    Cv2.BitwiseNot(roi, roi);
                }

                pcbImageShow.Image = BitmapConverter.ToBitmap(_image);
                //Cv2.ImShow("CamShift Demo", _image);

                if ((char)Cv2.WaitKey(10) == 27) // ESC 按键
                    break;
            }
        }

        #region OpenCv 鼠标事件回调
        /// <summary>
        /// OpenCv 鼠标事件回调
        /// </summary>
        /// <param name="event"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="flags"></param>
        /// <param name="userData"></param>
        public void OnCvMouseCallback(MouseEventTypes @event, int x, int y, MouseEventFlags flags, IntPtr userData)
        {
            // 当按下鼠标左键时，框选标记为 true, 执行如下代码得到矩形区域 _selectedRectangle
            if (_onMouseSelecting)
            {
                _selectedRectangle.X = Math.Min(x, _origin.X);
                _selectedRectangle.Y = Math.Min(y, _origin.Y);
                _selectedRectangle.Width = Math.Abs(x - _origin.X);
                _selectedRectangle.Height = Math.Abs(y - _origin.Y);

                // 矩形区域与 image 进行与运算，结果保存到矩形区域中
                _selectedRectangle &= new Rect(0, 0, _image.Cols, _image.Rows);
            }

            if (@event == MouseEventTypes.LButtonDown)
            {
                _origin = new OpenCvSharp.Point(x, y);
                _selectedRectangle = new Rect(x, y, 0, 0);
                _onMouseSelecting = true;
            }
            else if (@event == MouseEventTypes.LButtonUp)
            {
                _onMouseSelecting = false;
                if (_selectedRectangle.Width > 0 && _selectedRectangle.Height > 0)
                {
                    _trackObject = -1;
                }
            }
        }
        #endregion

        #region PictureBox 鼠标事件处理
        private void pcbImageShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _origin = new OpenCvSharp.Point(e.X, e.Y);
            _selectedRectangle = new Rect(e.X, e.Y, 0, 0);
            _onMouseSelecting = true;
        }

        private void pcbImageShow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _onMouseSelecting = false;
            if (_selectedRectangle.Width > 0 && _selectedRectangle.Height > 0)
            {
                _trackObject = -1;
            }
        }

        private void pcbImageShow_MouseMove(object sender, MouseEventArgs e)
        {
            // 当按下鼠标左键时，框选标记为 true, 执行如下代码得到矩形区域 _selection
            if (_onMouseSelecting)
            {
                _selectedRectangle.X = Math.Min(e.X, _origin.X);
                _selectedRectangle.Y = Math.Min(e.Y, _origin.Y);
                _selectedRectangle.Width = Math.Abs(e.X - _origin.X);
                _selectedRectangle.Height = Math.Abs(e.Y - _origin.Y);

                // 矩形区域与 image 进行与运算，结果保存到矩形区域中
                _selectedRectangle &= new Rect(0, 0, _image.Cols, _image.Rows);
            }
        }
        #endregion
    }
}
