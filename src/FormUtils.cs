using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NC.OpenCvSharp4.Learning
{
    public static class FormUtils
    {
        public static string IMG_GEM_1 { get; set; } = "./asserts/GEM.1.jpg";
        
        public static string IMG_GEM_2 { get; set; } = "./asserts/GEM.2.jpg";
        public static string IMG_SCHARR { get; set; } = "./asserts/scharr.jpg";

        public static string IMG_WRITE_PNG { get; set; } = "./asserts/writepng.png";

        public static string VIDEO_ZX4R { get; set; } = "./asserts/ninja-zx-4rr.mp4";
        public static string VIDEO_1_AVI { get; set; } = "./asserts/1.avi";

        public static string VIDEO_WRITE_TEMP { get; set; } = "./asserts/temp.mp4";
        // public static string VIDEO_WRITE_TEMP { get; set; } = "F:\\Codes\\OpenCV\\NC.OpenCvSharp4.Learning\\asserts\\temp.avi";

        /// <summary>
        /// 人脸识别的神经网络文件
        /// </summary>
        public static string HaarcascadeFrontalfaceAltXML { get; set; } = "./resources/Form14/haarcascade_frontalface_alt.xml";

        /// <summary>
        /// 眼睛识别的神经网络文件
        /// </summary>
        public static string HaarcascadeEyeTreeEyeglassesXML { get; set; } = "./resources/Form14/haarcascade_eye_tree_eyeglasses.xml";

        public static void OpenForm(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
    }
}
