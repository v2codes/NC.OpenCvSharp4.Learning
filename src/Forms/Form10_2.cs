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
    public partial class Form10_2 : Form
    {
        public Form10_2()
        {
            InitializeComponent();
        }

        private void Form10_2_Load(object sender, EventArgs e)
        {

        }

        private void Run()
        {
            VideoCapture Cap = new VideoCapture();
            // 打开ID为0的摄像头
            Cap.Open(0);
            // 判断摄像头是否成功打开
            if (!Cap.IsOpened())
            {
                MessageBox.Show("摄像头打开失败.");
                return;
            }

            var frame = new Mat();

            // 设置颜色阈值，BGR存储顺序，非常规RGB
            var lower = new Scalar(90, 50, 50);
            var upper = new Scalar(130, 255, 255);
        }
    }
}
