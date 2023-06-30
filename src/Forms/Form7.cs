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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // *** 这里需要注意的是，正常情况下，先转为灰度图之后再进行边缘检测

            var srcOriginal = new Mat(FormUtils.IMG_GEM_1);
            var src = new Mat();
            Cv2.Resize(srcOriginal, src, new OpenCvSharp.Size(pcbImageShow.Width, pcbImageShow.Height));

            var gradeX = new Mat();
            var gradeY = new Mat();
            var absGradeX = new Mat();
            var absGradeY = new Mat();
            var dest = new Mat();

            // 弹窗显示
            Cv2.ImShow("原图", src);

            // 求 X 方向梯度
            Cv2.Sobel(src, gradeX, MatType.CV_16S, 1, 0, 3, 1, 1, BorderTypes.Default);
            Cv2.ConvertScaleAbs(gradeX, absGradeX);
            Cv2.ImShow("效果：X 方向Sobel", absGradeX);

            // 求 Y 方向梯度
            Cv2.Sobel(src, gradeY, MatType.CV_16S, 0, 1, 3, 1, 1, BorderTypes.Default);
            Cv2.ConvertScaleAbs(gradeY, absGradeY);
            Cv2.ImShow("效果：Y 方向Sobel", absGradeY);

            // 合并梯度
            Cv2.AddWeighted(absGradeX, 0.5, absGradeY, 0.5, 0, dest);
            Cv2.ImShow("效果：整体方向Sobel", dest);

            // 输出图像到 PictureBox
            var bitmap = BitmapConverter.ToBitmap(dest);
            pcbImageShow.Image = bitmap;
        }
    }
}
