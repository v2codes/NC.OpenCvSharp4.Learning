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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            var srcOriginal = new Mat(FormUtils.IMG_GEM_1);
            var src = new Mat();
            Cv2.Resize(srcOriginal, src, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));

            // 弹窗显示
            Cv2.ImShow("原图", src);

            // 腐蚀
            // 指定大小和形状的结构元素
            var element = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            var dest = new Mat();
            Cv2.Erode(src, dest, element);

            // 输出图像到 PictureBox
            var bitmap = BitmapConverter.ToBitmap(dest);
            pcbImageShow.Image = bitmap;

            // 弹窗显示
            using (new Window("腐蚀效果", dest))
            {
                Cv2.WaitKey();
            }
        }
    }
}
