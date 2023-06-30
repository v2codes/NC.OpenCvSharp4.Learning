using NAudio.Wave;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form10 : Form
    {
        /// <summary>
        /// 视频操作
        /// </summary>
        public VideoCapture _videoCapture;

        /// <summary>
        /// 录像操作
        /// </summary>
        public VideoWriter _videoWriter;

        /// <summary>
        /// 录像开关
        /// </summary>
        public EnumRecordingStatus _recordingStatus { get; set; }

        private WaveOutEvent _outputDevice { get; set; }
        private AudioFileReader _audioFileReader { get; set; }
        private Thread _videoPlayThread { get; set; }
        private bool _isPlayingAudio { get; set; } = false;

        public Form10()
        {
            InitializeComponent();
            _videoCapture = new VideoCapture(0);
            _recordingStatus = EnumRecordingStatus.None;
        }

        private void btnOpenCamera_Click(object sender, EventArgs e)
        {
            // _capture = new VideoCapture(0);
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

            // 硬件加速
            _videoCapture.Set(VideoCaptureProperties.Backend, (int)VideoCaptureAPIs.DSHOW);

            // 摄像头参数，可以通过其他工具进行调整，将所需的参数值用于此处配置， -- 【摄像头录像 CCTV UVC Video Camera.exe】
            //// 亮度
            //_capture.Set(VideoCaptureProperties.Brightness, 1);
            //// 对比度
            //_capture.Set(VideoCaptureProperties.Contrast, 0);
            //// 饱和度
            //_capture.Set(VideoCaptureProperties.Saturation, 100);
            //// 色调
            //_capture.Set(VideoCaptureProperties.Hue, 0);
            //// 曝光
            //_capture.Set(VideoCaptureProperties.Exposure, -1);

            var img = new Mat();

            //using var window = new Window("capture 录制预览");
            // 视频显示
            while (true)
            {
                if (_videoCapture.Read(img))
                {
                    // 在 window 窗口中播放视频 - 方法一
                    // window.ShowImage(img);
                    // 在 window 窗口中播放视频 - 方法一
                    //Cv2.ImShow("录制视频预览", img);

                    if (img.Empty())
                    {
                        break;
                    }

                    // 本地存储
                    if (_recordingStatus == EnumRecordingStatus.Recording)
                    {
                        // 录像，本地存储
                        _videoWriter.Write(img);
                    }
                    else if (_recordingStatus == EnumRecordingStatus.Stoped)
                    {
                        // 释放资源
                        _videoWriter.Release();
                        break;
                    }

                    #region old
                    //if (_isRecording)
                    //{
                    //    // 用户点击了开始录像
                    //    end = 1;
                    //    // 录像，本地存储
                    //    _writer.Write(img);
                    //}
                    //else if (end == 1)
                    //{
                    //    // 用户点击了结束录像
                    //    end = 2;
                    //    // 释放资源
                    //    _writer.Release();
                    //}
                    #endregion

                    // 在 PictureBox 中显示效果图
                    pcbImageShow.BackgroundImage = BitmapConverter.ToBitmap(img);
                    Cv2.WaitKey(1);
                }
            }

        }

        private void btnStartRecord_Click(object sender, EventArgs e)
        {
            if (_videoCapture == null)
            {
                MessageBox.Show("请打开摄像头", "Warning");
                return;
            }
            // 定义录像的数据类，true 表示保存彩色图像，仅 windows 下有效，所以通长使用默认值
            var size = new OpenCvSharp.Size(_videoCapture.FrameWidth, _videoCapture.FrameHeight); // (pcbImageShow.Width, pcbImageShow.Height);
            _videoWriter = new VideoWriter(FormUtils.VIDEO_WRITE_TEMP, FourCC.MP4V, _videoCapture.Fps, size, true);
            _recordingStatus = EnumRecordingStatus.Recording;
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            _recordingStatus = EnumRecordingStatus.Stoped;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _videoPlayThread = new Thread(PlayVideoWithPictureBox);
            _videoPlayThread.Start();
        }

        #region 音频同步播放操作
        private void PlayVideoWithPictureBox()
        {
            MessageBox.Show("VideoWriter 不读取处理音频，可以考虑结合NAudio等其他音频处理组件来实现", "Tips");
            var capture = new VideoCapture(FormUtils.VIDEO_WRITE_TEMP);
            // 计算帧率
            var sleepTime = (int)Math.Round(1000 / capture.Fps);
            while (true)
            {
                var img = new Mat();
                bool success = capture.Read(img);

                if (!success)
                {
                    // 停止播放视频音频
                    StopAudio();
                    break;
                }

                // 播放视频音频
                StartAudio();

                var resizeImg = new Mat();
                Cv2.Resize(img, resizeImg, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));

                Bitmap bitmap = BitmapConverter.ToBitmap(resizeImg);
                pcbImageShow.Invoke((MethodInvoker)(() => pcbImageShow.Image = bitmap));
                Application.DoEvents();
                Thread.Sleep(sleepTime);
            }

            capture.Release();
            capture.Dispose();
        }

        private void StartAudio()
        {
            return;
            if (_isPlayingAudio)
            {
                return;
            }

            if (_outputDevice == null)
            {
                _outputDevice = new WaveOutEvent();
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (_audioFileReader == null)
            {
                _audioFileReader = new AudioFileReader(FormUtils.VIDEO_WRITE_TEMP);
                _outputDevice.Init(_audioFileReader);
            }
            _outputDevice.Play();
            _isPlayingAudio = true;
        }

        private void StopAudio()
        {
            _outputDevice?.Stop();
            _isPlayingAudio = false;
        }


        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            _outputDevice.Dispose();
            _outputDevice = null;
            _audioFileReader.Dispose();
            _audioFileReader = null;
        }

        private void Form10_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopAudio();
        }
        #endregion
    }

    public enum EnumRecordingStatus
    {
        None,
        Recording,
        Stoped,
    }

}
