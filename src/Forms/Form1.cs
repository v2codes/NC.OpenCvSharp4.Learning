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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Frm1_Load(object sender, EventArgs e)
        {
            var src = new Mat(new OpenCvSharp.Size(this.Width, this.Height), MatType.CV_8UC3, Scalar.All(255));
            Cv2.Circle(src, this.Width / 2, this.Height / 2, 120, new Scalar(255, 0, 0), 20);
            Cv2.Circle(src, this.Width / 2, this.Height / 2, 100, new Scalar(0, 255, 0), 20);
            Cv2.Circle(src, this.Width / 2, this.Height / 2, 80, new Scalar(0, 0, 255), 20);
            var bitmap = BitmapConverter.ToBitmap(src);
            pcbImageShow.Image = bitmap;
        }
    }
}
