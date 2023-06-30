using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace NC.OpenCvSharp4.Learning.Forms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void frm2_Load(object sender, EventArgs e)
        {
            // 读取一张图片
            // var src = Cv2.ImRead(FormUtils.Img_1);
            var src = new Mat(FormUtils.IMG_GEM_1);
            if (src.Empty())
            {
                return;
            }
            // 显示图片
            //Cv2.ImShow("imShow", src);
            //Cv2.WaitKey(0);

            var resizeMat = new Mat();
            Cv2.Resize(src, resizeMat, new OpenCvSharp.Size(pcbShowImage.Width, pcbShowImage.Height));
            var bitmap = BitmapConverter.ToBitmap(resizeMat);

            pcbShowImage.Image = bitmap;
        }
    }
}
