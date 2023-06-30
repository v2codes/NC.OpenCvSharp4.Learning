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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var mat = CreateAlphaMat();
            var bitmap = BitmapConverter.ToBitmap(mat);
            pictureBox1.Image = bitmap;

            Cv2.ImWrite(FormUtils.IMG_WRITE_PNG, mat);
        }

        private Mat CreateAlphaMat()
        {
            var mat = new Mat(pictureBox1.Height, pictureBox1.Width, MatType.CV_8UC4);
            for (int r = 0; r < mat.Rows; r++)
            {
                for (int c = 0; c < mat.Cols; c++)
                {
                    var rgba = new Vec4b();

                    // 蓝色
                    rgba.Item0 = 0xff; // 255
                    // 绿色
                    rgba.Item1 = (byte)(((float)mat.Cols - c) / (float)mat.Cols * 0xff);
                    // 红色
                    rgba.Item2 = (byte)(((float)mat.Rows - r) / (float)mat.Rows * 0xff);
                    // 透明度
                    rgba.Item3 = (byte)((float)0.5 * (float)(rgba[1] + rgba[2]));

                    // 设置
                    mat.Set(r, c, rgba);
                }
            }
            return mat;
        }
    }
}
