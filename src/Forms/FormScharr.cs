using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = OpenCvSharp.Size;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class FormScharr : Form
    {
        public FormScharr()
        {
            InitializeComponent();
        }

        private void FormScharr_Load(object sender, EventArgs e)
        {
            Run();
        }

        // https://shimat.github.io/opencvsharp_docs/html/d69c29a1-7fb1-4f78-82e9-79be971c3d03.htm

        private void Run()
        {
            // 1. 创建 gradX 和 gradY 矩阵
            var gradX = new Mat();
            var gradY = new Mat();
            var absGradX = new Mat();
            var absGradY = new Mat();
            var dest = new Mat();

            // 2. 载入原图
            var img = Cv2.ImRead(FormUtils.IMG_GEM_2); // (FormUtils.IMG_SCHARR);
            var src = new Mat();
            Cv2.Resize(img, src, new Size(pcbImageShow.Width, pcbImageShow.Height));

            // 3. 显示原图
            pcbImageShow.Image = BitmapConverter.ToBitmap(src);

            // 4. 求 X 方向梯度
            Cv2.Scharr(src, gradX, MatType.CV_16S, 1, 0, 1, 0, BorderTypes.Default);
            Cv2.ConvertScaleAbs(gradX, absGradX);
            Cv2.ImShow("【效果图】 X方向Scharr", absGradX);

            // 5. 求 Y 方向梯度
            Cv2.Scharr(src, gradY, MatType.CV_16S, 1, 0, 1, 0, BorderTypes.Default);
            Cv2.ConvertScaleAbs(gradY, absGradY);
            Cv2.ImShow("【效果图】 Y方向Scharr", absGradY);

            // 6. 合并梯度（近似）
            Cv2.AddWeighted(absGradX, 0.5, absGradY, 0.5, 0, dest);

            // 7. 显示效果图
            Cv2.ImShow("【效果图】合并梯度后Scharr", dest);

        }
    }
}
