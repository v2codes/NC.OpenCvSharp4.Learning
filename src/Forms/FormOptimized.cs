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
    public partial class FormOptimized : Form
    {
        public FormOptimized()
        {
            InitializeComponent();
        }

        // 参考文档：OpenCV-Python-Tutorial-中文版.pdf（P47 检测程序效率）

        private void FormOptimized_Load(object sender, EventArgs e)
        {
            lblValue.Text = Cv2.UseOptimized().ToString();

            RunTest();
        }

        private void RunTest()
        {
            var src = Cv2.ImRead(FormUtils.IMG_GEM_1);
            var img1 = new Mat();
            Cv2.Resize(src, img1, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));
            pcbImageShow.Image = BitmapConverter.ToBitmap(img1);

            // 返回特定事件之后的 Ticks 数(例如，当机器启动)。
            // 它可用于初始化RNG或通过读取函数来测量函数执行时间，函数调用前后的刻度计数。
            var startTicks = Cv2.GetTickCount();
            for (int i = 5; i < 49; i += 2)
            {
                // 使用中值滤波器平滑图像
                Cv2.MedianBlur(img1, img1, i);
                pcbImageShow.Image = BitmapConverter.ToBitmap(img1);

            }

            var endTicks = Cv2.GetTickCount();
            var time = (endTicks - startTicks) / Cv2.GetTickFrequency();  //  时钟频率 或者 每秒的 Ticks 数
            lblTimes.Text = $"{time}s";

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Cv2.SetUseOptimized(!Cv2.UseOptimized());
            lblValue.Text = Cv2.UseOptimized().ToString();
            RunTest();
        }
    }
}
