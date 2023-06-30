using OpenCvSharp;
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
    public partial class Form11 : Form
    {
        public Mat _image = new Mat();
        public bool _selectObject = false;
        public int _trackObject = 0;
        public Rect _selection;
        public OpenCvSharp.Point _origin;

        /// <summary>
        /// 视频操作
        /// </summary>
        public VideoCapture _videoCapture = new VideoCapture(0);

        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }
    }
}
