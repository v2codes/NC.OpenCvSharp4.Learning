using NAudio.Wave;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form9 : Form
    {
        private VideoCapture _capture { get; set; }

        private WaveOutEvent _outputDevice { get; set; }

        private AudioFileReader _audioFile { get; set; }

        private Thread _videoPlayThread { get; set; }

        private bool _isPlayingAudio { get; set; } = false;

        public Form9()
        {
            InitializeComponent();
            _capture = new VideoCapture(FormUtils.VIDEO_ZX4R);
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            // OpenCv Window 
            //PlayVieoWithCv2Window();

            _videoPlayThread = new Thread(PlayVideoWithPictureBox);
            _videoPlayThread.Start();
        }

        private void PlayVideoWithPictureBox()
        {
            // 计算帧率
            var sleepTime = (int)Math.Round(1000 / _capture.Fps);
            while (true)
            {
                var img = new Mat();
                bool success = _capture.Read(img);

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

            _capture.Release();
            _capture.Dispose();
        }

        private void PlayVieoWithCv2Window()
        {
            //var capture = new VideoCapture(FormUtils.Video_ZX4R);

            // 计算帧率
            var sleepTime = (int)Math.Round(1000 / _capture.Fps);

            using (var window = new Window("capture"))
            {
                var isPlayingAudio = false;
                // 逐帧读取视频
                while (true)
                {
                    // 声明 Mat 实例
                    var img = new Mat();
                    _capture.Read(img);

                    // 判断是否还有未播放图像
                    if (img.Empty())
                    {
                        // 停止播放视频音频
                        StopAudio();
                        break;
                    }

                    // 播放视频音频
                    if (!isPlayingAudio)
                    {
                        StartAudio();
                        isPlayingAudio = true;
                    }

                    var resizeImg = new Mat();
                    Cv2.Resize(img, resizeImg, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));

                    // 在 Window 窗口中播放视频 - 方法一
                    window.ShowImage(resizeImg);
                    // 在 Window 窗口中播放视频 - 方法二
                    // Cv2.ImShow("Video", img);

                    // 输出图像到 PictureBox
                    var bitmap = BitmapConverter.ToBitmap(resizeImg);
                    pcbImageShow.BackgroundImage = bitmap;

                    Cv2.WaitKey(sleepTime);
                }
            }
        }

        private void StartAudio()
        {
            if (_isPlayingAudio)
            {
                return;
            }

            if (_outputDevice == null)
            {
                _outputDevice = new WaveOutEvent();
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (_audioFile == null)
            {
                _audioFile = new AudioFileReader(FormUtils.VIDEO_ZX4R);
                _outputDevice.Init(_audioFile);
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
            _audioFile.Dispose();
            _audioFile = null;
        }

        private void Form9_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopAudio();
        }
    }
}
