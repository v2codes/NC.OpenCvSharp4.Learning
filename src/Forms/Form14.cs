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
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;

namespace NC.OpenCvSharp4.Learning.Forms
{
    /*
     * 人脸识别的原理是采用了神经网络算法，先进行脸部识别，然后再在脸部识别的基础上，将人脸裁剪出来进行眼睛识别，
     * 这种方法只能说识别成功率不会特别高，而且受环境、角度影响较大。如果要达到人脸打卡的那种识别成功率，这种方法，肯定是不行的。
     * 
     * 其中“haarcascade_frontalface_alt.xml”就是人脸识别的神经网络文件，
     * “haarcascade_eye_tree_eyeglasses.xml”是眼睛识别的神经网络文件。
     * 大家如果想知道神经网络文件是如何生成的，可以查查 opencv3 神经网络汉字识别 相关内容
     */

    public partial class Form14 : Form
    {
        // 对象检测的级联分类器

        /// <summary>
        /// 人脸
        /// </summary>
        private CascadeClassifier _faceCascade = new CascadeClassifier();

        /// <summary>
        /// 眼睛
        /// </summary>
        private CascadeClassifier _eyesCascade = new CascadeClassifier();

        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            // 加载神经网络文件
            if (!_faceCascade.Load(FormUtils.HaarcascadeFrontalfaceAltXML))
            {
                MessageBox.Show("加载 haarcascade_frontalface_alt.xml 文件出错.");
                btnRun.Enabled = false;
            }
            if (!_eyesCascade.Load(FormUtils.HaarcascadeEyeTreeEyeglassesXML))
            {
                MessageBox.Show("加载 haarcascade_eye_tree_eyeglasses.xml 文件出错.");
                btnRun.Enabled = false;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var videoCapture = new VideoCapture();
            // 打开ID为0的摄像头
            videoCapture.Open(0);
            // 判断摄像头是否成功打开
            if (!videoCapture.IsOpened())
            {
                MessageBox.Show("摄像头打开失败.");
                return;
            }

            // 设置采集的图像尺寸为
            videoCapture.Set(VideoCaptureProperties.FrameWidth, pcbImageShow.Width);
            videoCapture.Set(VideoCaptureProperties.FrameHeight, pcbImageShow.Height);
            videoCapture.FrameWidth = pcbImageShow.Width;
            videoCapture.FrameHeight = pcbImageShow.Height;
            // _videoCapture.Set(VideoCaptureProperties.Exposure, 0); // 曝光

            var frame = new Mat();
            // var window = new Window("frame");
            while (true)
            {
                if (videoCapture.Read(frame))
                {
                    //人脸识别
                    RecognizeFace(frame);
                    // 在Window窗口中播放视频(方法1)
                    // window.ShowImage(frame);

                    // 在Window窗口中播放视频(方法2)
                    //Cv2.ImShow("frame", frame);

                    // 在pictureBox1中显示效果图
                    pcbImageShow.Image = BitmapConverter.ToBitmap(frame);

                    if (Cv2.WaitKey(10) == 27)
                        break;
                }
            }
        }

        private Mat RecognizeFace(Mat frame)
        {
            // 转换灰度图
            var gray = new Mat();
            Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

            // 直方图均衡化，用于提高图像质量
            Cv2.EqualizeHist(gray, gray);

            // 人脸检测
            // opencv3：HaarDetectionType.ScaleImage
            // opencv4：HaarDetectionTypes.ScaleImage

            Rect[] faces = _faceCascade.DetectMultiScale(gray, 1.1, 2, 0 | HaarDetectionTypes.ScaleImage, new Size(30, 30));
            for (int i = 0; i < faces.Length; i++)
            {
                // 绘制脸部区域
                Point center = new Point() { X = (faces[i].X + faces[i].Width / 2), Y = (faces[i].Y + faces[i].Width / 2) };
                Cv2.Ellipse(frame, center, new Size(faces[i].Width / 2, faces[i].Height / 2), 0, 0, 360, new Scalar(255, 0, 255), 2, LineTypes.Link8, 0);

                // 绘制眼睛区域
                Mat faceROI = new Mat(gray, faces[i]);
                Cv2.ImShow("faceROI", faceROI);

                Rect[] eyes = _eyesCascade.DetectMultiScale(faceROI, 1.1, 2, 0 | HaarDetectionTypes.ScaleImage, new Size(30, 30));
                for (int j = 0; j < eyes.Length; j++)
                {
                    Point eye_center = new Point() { X = (faces[i].X + eyes[j].X + eyes[j].Width / 2), Y = (faces[i].Y + eyes[j].Y + eyes[j].Height / 2) };
                    int radius = (int)Math.Round((decimal)((eyes[j].Width + eyes[j].Height) * 0.25), 0, MidpointRounding.AwayFromZero);
                    Cv2.Circle(frame, eye_center.X, eye_center.Y, radius, new Scalar(0, 0, 255), 3, LineTypes.Link8, 0);
                }
            }

            return frame;
        }
    }
}
