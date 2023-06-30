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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // 将图2叠加在图1上，合并图片显示
            // 读取图片
            var img1Original = Cv2.ImRead(FormUtils.IMG_GEM_1);
            var img2Original = new Mat(FormUtils.IMG_WRITE_PNG);
            var img1 = new Mat();
            var img2 = new Mat();
            Cv2.Resize(img1Original, img1, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height)); // 原图太大，修改小一点便于显示
            Cv2.Resize(img2Original, img2, new OpenCvSharp.Size(pcbImageShow.Width / 5, pcbImageShow.Height / 5)); // 原图太大，修改小一点便于叠加

            // 设置图片2在图片1上叠加显示的区域
            var imgROI = img1[new Rect() { X = img1.Width - img2.Width, Y = img1.Height - img2.Height, Height = img2.Rows, Width = img2.Cols }];
            // 重叠两张图片
            Cv2.AddWeighted(imgROI, 1, img2, 0.5, 0d, imgROI);
            // 显示图片到 PictureBox
            var bitmap = BitmapConverter.ToBitmap(img1);
            pcbImageShow.Image = bitmap;
            // 弹窗显示
            using (new Window("合并", img1))
            {
                Cv2.WaitKey(0);
            }
        }
    }
}
