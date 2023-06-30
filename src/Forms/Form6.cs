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
using System.Xml.Linq;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            var srcOriginal = new Mat(FormUtils.IMG_GEM_1);
            var src = new Mat();
            Cv2.Resize(srcOriginal, src, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));

            // 弹窗显示
            Cv2.ImShow("原图", src);

            // 均值滤波
            var dest = new Mat();
            Cv2.Blur(src, dest, new OpenCvSharp.Size(10, 10));

            // 输出图像到 PictureBox
            var bitmap = BitmapConverter.ToBitmap(dest);
            pcbImageShow.Image = bitmap;

            // 弹窗显示
            using (new Window("模糊效果", dest))
            {
                Cv2.WaitKey();
            }
        }
    }
}
