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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            var srcOriginal = new Mat(FormUtils.IMG_GEM_2);
            var src = new Mat();
            Cv2.Resize(srcOriginal, src, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));

            // 拷贝原图
            var srcCopy = src.Clone();

            // 显示原图
            Cv2.ImShow("原图", src);

            // 创建与src同类型、同大小的矩阵 dest
            var dest = new Mat(srcCopy.Cols, srcCopy.Rows, srcCopy.Type());

            // 将原图转换为灰度图
            var gray = new Mat();
            Cv2.CvtColor(srcCopy, gray, ColorConversionCodes.BGR2GRAY);

            // 先使用 3x3 内核进行降噪
            var blur = new Mat();
            Cv2.Blur(gray, blur, new OpenCvSharp.Size(3, 3));

            // 运行 Canny 算子
            Cv2.Canny(blur, blur, 3, 9, 3);

            // 使用 Canny 算子输出边缘图 g_cannyDetectedEdges 作为掩码，将原图 src 拷到目标 dest 中
            blur.CopyTo(dest);

            // 显示效果图
            Cv2.ImShow("效果：Canny", dest);

            // 输出图像到 PictureBox
            var bitmap = BitmapConverter.ToBitmap(dest);
            pcbImageShow.Image = bitmap;
        }
    }
}
