using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = OpenCvSharp.Size;

namespace NC.OpenCvSharp4.Learning.Forms.FormHelpers
{
    /// <summary>
    /// 计算相似度方法 Similarity 
    /// </summary>
    public class SimilarityDetection : IDisposable
    {
        /// <summary>
        /// 均方误差 MSE （Mean Squared Error）
        /// 是反映估计量与被估计量之间差异程度的一种度量。
        /// 设 t 是根据子样确定的总体参数θ的一个估计量，(θ-t)2的数学期望，称为估计量t的均方误差。
        /// 它等于σ2+b2，其中σ2与b分别是t的方差与偏倚。
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns></returns>
        public double MSE(Mat image1, Mat image2)
        {
            // 在使用OpenCV时，可以通过矩阵操作来避免 for 循环嵌套计算
            // 需要注意的是乘除操作一般要注意将图像本身的 unit8 转换成 float 后再做，否则精度误差可能会导致较大偏差
            var mat1 = image1.Clone();
            var mat2 = image2.Clone();
            var diff = new Mat();

            // 提前转换为 32F精度
            mat1.ConvertTo(mat1, MatType.CV_32F);
            mat2.ConvertTo(mat2, MatType.CV_32F);
            diff.ConvertTo(diff, MatType.CV_32F);

            // 计算两个数组之间或数组与标量之间的每个元素的绝对差
            Cv2.Absdiff(mat1, mat2, diff); // Diff = | mat1 - mat2 |

            // 对两个矩阵执行逐元素的乘法或除法。
            diff = diff.Mul(diff); // | mat1 - mat2 |.^2
            Scalar s = Cv2.Sum(diff); // 分别计算每个通道的元素之和，Scalar：从Vec派生的4元素向量的模板类。

            double sse; // square error
            if (diff.Channels() == 3)
            {
                sse = s.Val0 + s.Val1 + s.Val2; // 所有通道元素之和
            }
            else
            {
                sse = s.Val0;
            }

            var totalElements = mat2.Channels() * mat2.Total();
            var mse = (sse / totalElements);
            return mse;
        }

        /// <summary>
        /// 结构相似度指数 SSIM （Structural Similarity）
        /// 是一种衡量两幅图像相似度的指标
        /// 也是一种全参考的图像质量评价指标，它分别从亮度、对比度、结构三方面衡量图像相似度
        /// 参考 https://www.freesion.com/article/50591020225/
        /// C1、C2和C3为常数，为了避免分母为 0 而维持稳定
        /// L=255 （像素值的动态范围，一般都取值 255）
        /// K1=0.01, K2=0.03, 结构相似性的范围 -1~1
        /// 当两张图一模一样时，SSIM的值等于 1
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public double SSIM(Mat mat1, Mat mat2)
        {
            // TODO:长时间运行，会报错。。。
            const double C1 = 6.5025, C2 = 58.5225;
            var matType = MatType.CV_32F;
            Mat mat32A = new Mat(), mat32B = new Mat();
            mat1.ConvertTo(mat32A, matType);
            mat2.ConvertTo(mat32B, matType);

            var mat32A_2 = mat32A.Mul(mat32A);
            var mat32B_2 = mat32A.Mul(mat32B);
            var mat32A_B = mat32A.Mul(mat32B);

            // 高斯模糊处理
            Mat muA = new Mat(), muB = new Mat();
            Cv2.GaussianBlur(mat32A, muA, new Size(11, 11), 1.5);
            Cv2.GaussianBlur(mat32B, muB, new Size(11, 11), 1.5);

            var muA_2 = muA.Mul(muA);
            var muB_2 = muB.Mul(muB);
            var muA_B = muA.Mul(muB);

            Mat sigmaA = new Mat(), sigamB = new Mat(), sigamA_B = new Mat();
            Cv2.GaussianBlur(mat32A_2, sigmaA, new Size(11, 11), 1.5);
            sigmaA -= muA_2;

            Cv2.GaussianBlur(mat32B_2, sigamB, new Size(11, 11), 1.5);
            sigamB -= muB_2;

            Cv2.GaussianBlur(mat32A_B, sigamA_B, new Size(11, 11), 1.5);
            sigamA_B -= muA_B;

            Mat t1, t2, t3;
            t1 = 2 * muA_B + C1;
            t2 = 2 * sigamA_B + C2;
            t3 = t1.Mul(t2);

            t1 = muA_2 + muB_2 + C1;
            t2 = sigmaA + sigamB + C2;
            t1 = t1.Mul(t2);

            var ssim_map = new Mat();
            Cv2.Divide(t3, t1, ssim_map); // 用一个数组对两个数组或一个标量执行每个元素的除法
            Scalar mssim = Cv2.Mean(ssim_map); // 计算所选数组元素的平均值

            var ssim = (mssim.Val0 + mssim.Val1 + mssim.Val2) / 3;
            return ssim;
        }

        /// <summary>
        /// 峰值信噪比 PSNR （Peak Signal-to-Noise Ratio）
        /// 一种评价图像的客观标准，用来评估图像的保真性。
        /// 经常用作图像压缩等领域中信号重建质量的测量方法。
        /// 它常简单地通过均方差（MSE）进行定义，使用两个m×n单色图像I和K。PSNR的单位为分贝
        /// PSNR值越大，就代表失真越少，图像压缩中典型的峰值信噪比值在 30 到 40dB 之间，小于30dB时考虑图像无法忍受。
        /// 
        /// 通过计算两幅图像的峰值信噪比PSNR，当PSNR的值越小时表示两幅图像越相似，
        /// 当PSNR = 0 时则表示两幅图像完全相同。但是PSNR是基于每个像素误差计算而来，所以得出的结果可能和人眼视觉有较大差异。
        /// 一般来说，当两幅图像的PSNR小于30时，那么这两幅图像可以说是比较相似的。
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public double PSNR(Mat mat1, Mat mat2)
        {
            //注意，当两幅图像一样时这个函数计算出来的 PSNR 为0 
            var diff = new Mat();
            Cv2.Absdiff(mat1, mat2, diff);
            diff.ConvertTo(diff, MatType.CV_32F); // //转换为32位的 float 类型，8位不能计算平方  
            diff = diff.Mul(diff);
            var scalar = Cv2.Sum(diff); // 分别计算每个通道的元素之和
            var sse = scalar.Val0 + scalar.Val1 + scalar.Val2;

            if (sse <= 1e-10) // 对于较小的值返回零 0.0000000001
            {
                return 0;
            }
            else
            {
                var mse = sse / (double)(mat1.Channels() * mat1.Total()); //  sse/(w*h*3)  
                var psnr = 10.0 * Math.Log10((255 * 255) / mse);
                return psnr;
            }
        }

        /// <summary>
        /// 数字科学计数法处理
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private Decimal ChangeToDecimal(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.ToUpper().Contains("E"))
            {
                dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
            }
            else
            {
                dData = Convert.ToDecimal(strData);
            }
            return dData; // Math.Round(dData, 4);
        }

        public void Dispose()
        {

        }
    }
}
