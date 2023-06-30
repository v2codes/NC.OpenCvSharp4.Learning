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

        public static string IMG_WRITE_PNG { get; set; } = "./asserts/writepng.png";

        public static string VIDEO_ZX4R { get; set; } = "./asserts/ninja-zx-4rr.mp4";

        public static string VIDEO_WRITE_TEMP { get; set; } = "./asserts/temp.mp4";
        // public static string VIDEO_WRITE_TEMP { get; set; } = "F:\\Codes\\OpenCV\\NC.OpenCvSharp4.Learning\\asserts\\temp.avi";

        public static void OpenForm(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
    }
}
