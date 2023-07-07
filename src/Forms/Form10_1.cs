using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NC.OpenCvSharp4.Learning.Forms.FormHelpers;
using Size = OpenCvSharp.Size;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form10_1 : Form
    {
        public Form10_1()
        {
            InitializeComponent();
        }

        private void Form10_1_Load(object sender, EventArgs e)
        {
            //  双摄像头调用
            // Task.Run(OpenMultiCameras);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // 相似度检测
            SimilarityDetection();
        }

        /// <summary>
        /// 计算相似度
        /// </summary>
        private void SimilarityDetection()
        {
            var capture = new VideoCapture(FormUtils.VIDEO_ZX4R);

            // 设置要对比的帧数，此处设为第10帧，其他帧与之对比检测
            var baseFrameIndex = 10;
            var baseFrame = new Mat();
            lblBaseFps.Text = baseFrameIndex.ToString();

            var temp = new Mat();
            var gray = new Mat();

            // 第一帧作为相似度对比参考，这里用了 第10帧
            //if (capture.Read(baseFrame))
            //{
            //    Cv2.Resize(baseFrame, baseFrame, new Size(this.Width, this.Height));
            //    Cv2.CvtColor(baseFrame, baseFrame, ColorConversionCodes.BGR2GRAY);
            //    Cv2.ImShow("temp", baseFrame);
            //}

            var frameIndex = 1;
            while (true)
            {
                if (!capture.Read(temp))
                {
                    continue;
                }

                // 逐帧显示
                Cv2.Resize(temp, temp, new Size(this.Width, this.Height));

                Cv2.CvtColor(temp, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.ImShow("temp gray", gray);

                // 获取第10帧，作为对比参考
                if (frameIndex == baseFrameIndex)
                {
                    baseFrame = gray.Clone();
                    Cv2.ImShow("baseFrame gray", baseFrame);
                }

                // 如果第10帧图像已有值，则进行相似度检测
                if (!baseFrame.Empty())
                {

                    using (var si = new SimilarityDetection())
                    {
                        lblMSE.Text = $"{si.MSE(baseFrame, gray)}";
                        lblSSIM.Text = $"{si.SSIM(baseFrame, gray)}";
                        lblPSNR.Text = $"{si.PSNR(baseFrame, gray)}";
                    }
                    //break;
                }

                lblCurrentFps.Text = frameIndex.ToString();

                frameIndex++;

                if (Cv2.WaitKey(30) == 27)
                {
                    break;
                }
            }
            capture.Release();
        }

        /// <summary>
        /// 双摄像头调用
        /// </summary>
        private void OpenMultiCameras()
        {
            VideoCapture Cap0 = new VideoCapture();
            VideoCapture Cap1 = new VideoCapture();

            // 打开ID为0的摄像头
            Cap0.Open(0);
            // 判断摄像头是否成功打开
            if (!Cap0.IsOpened())
            {
                MessageBox.Show("摄像头0打开失败.");
                return;
            }

            // 打开ID为1的摄像头
            Cap1.Open(1);
            // 判断摄像头是否成功打开
            if (!Cap1.IsOpened())
            {
                MessageBox.Show("摄像头1打开失败.");
                return;
            }

            Cap0.Set(VideoCaptureProperties.FrameWidth, 320);  // 设置采集的图像宽度：320
            Cap0.Set(VideoCaptureProperties.FrameHeight, 240); // 设置采集的图像高度：240
            Cap1.Set(VideoCaptureProperties.FrameWidth, 320);  // 设置采集的图像宽度：320
            Cap1.Set(VideoCaptureProperties.FrameHeight, 240); // 设置采集的图像高度：240

            Mat frame0 = new Mat(), frame1 = new Mat();

            while (Cap0.IsOpened() && Cap1.IsOpened())
            {
                if (Cap0.Read(frame0))
                {
                    Cv2.ImShow("frame0", frame0);
                    Cv2.SetWindowTitle("frame0", "On Top");
                }
                if (Cap1.Read(frame1))
                {
                    Cv2.ImShow("frame1", frame1);
                    Cv2.MoveWindow("frame1", frame0.Cols, 0);
                }

                if (Cv2.WaitKey(2) == 27)// ESC按钮
                    break;
            }

            // When everything done, release the capture
            Cap0.Release();
            Cap1.Release();
        }

    }

}


