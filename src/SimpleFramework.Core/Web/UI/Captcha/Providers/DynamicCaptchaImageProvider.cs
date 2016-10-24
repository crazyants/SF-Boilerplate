using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using SimpleFramework.Core.UI.Captcha.Contracts;
using SimpleFramework.Core.Common;

namespace SimpleFramework.Core.UI.Captcha.Providers
{
    /// <summary>
    /// The default captcha image provider
    /// </summary>
    public class DynamicCaptchaImageProvider : ICaptchaImageProvider
    {
        private readonly IRandomNumberProvider _randomNumberProvider;

        /// <summary>
        /// 每个字符的宽度
        /// </summary>
        public const int ImageWidthPerChar = 20;
        /// <summary>
        /// 验证码图片的高度
        /// </summary>
        public const int ImageHeight = 32;
        /// <summary>
        /// 图片边距
        /// </summary>
        public const int ImagePadding = 5;
        /// <summary>
        /// 干扰线数量
        /// </summary>
        public const int InterferenceLines = 3;
        /// <summary>
        /// 字符图表的最大拉伸量
        /// </summary>
        public const int CharGraphicMaxPadding = 5;
        /// <summary>
        /// 字符图表的最大旋转量
        /// </summary>
        public const int CharGraphicMaxRotate = 35;
        /// <summary>
        /// The default captcha image provider
        /// </summary>
        public DynamicCaptchaImageProvider(IRandomNumberProvider randomNumberProvider)
        {
            randomNumberProvider.CheckArgumentNull(nameof(randomNumberProvider));

            _randomNumberProvider = randomNumberProvider;
        }

        /// <summary>
        /// Creates the captcha image.
        /// </summary>
        public byte[] DrawCaptcha(string message, string foreColor, string backColor, float fontSize, string fontName)
        {
            var pic = Generate(message, 4);

            using (var stream = new MemoryStream())
            {
                pic.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }

        }

        private static SizeF measureString(string text, Font f)
        {
            using (var bmp = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    return g.MeasureString(text, f);
                }
            }
        }
        private void distortImage(int height, int width, Bitmap pic)
        {
            using (var copy = (Bitmap)pic.Clone())
            {
                double distort = _randomNumberProvider.Next(1, 6) * (_randomNumberProvider.Next(10) == 1 ? 1 : -1);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Adds a simple wave
                        int newX = (int)(x + (distort * Math.Sin(Math.PI * y / 84.0)));
                        int newY = (int)(y + (distort * Math.Cos(Math.PI * x / 44.0)));
                        if (newX < 0 || newX >= width) newX = 0;
                        if (newY < 0 || newY >= height) newY = 0;
                        pic.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }
        }


        /// <summary>
		/// 生成验证码并储存验证码到会话中
		/// </summary>
		/// <param name="key">使用的键名</param>
		/// <param name="digits">验证码位数</param>
		/// <param name="chars">使用的字符列表</param>
		/// <returns></returns>
		private Image Generate(string key, int digits = 4, string chars = null)
        {
            // 生成验证码
            var captchaCode = RandomUtils.RandomString(
                digits, chars ?? "23456789ABCDEFGHJKLMNPQRSTUWXYZ");
            var image = new Bitmap(ImageWidthPerChar * digits + ImagePadding * 2, ImageHeight);
            var font = new Font("Arial", ImageWidthPerChar, FontStyle.Bold);
            var rand = RandomUtils.Generator;
            using (var graphic = Graphics.FromImage(image))
            {
                // 描画背景
                var backgroundBrush = new SolidBrush(Color.White);
                graphic.FillRectangle(backgroundBrush, new RectangleF(0, 0, image.Width, image.Height));
                // 添加干扰线
                var pen = new Pen(Color.Black);
                for (int x = 0; x < InterferenceLines; ++x)
                {
                    var pointStart = new Point(rand.Next(image.Width), rand.Next(image.Height));
                    var pointFinish = new Point(rand.Next(image.Width), rand.Next(image.Height));
                    graphic.DrawLine(pen, pointStart, pointFinish);
                }
                // 逐个字符描画，并进行不规则拉伸
                var stringFormat = StringFormat.GenericDefault;
                var randomPadding = new Func<int>(() => rand.Next(CharGraphicMaxPadding));
                var textBrush = new SolidBrush(Color.Black);
                for (int x = 0; x < captchaCode.Length; ++x)
                {
                    var path = new GraphicsPath();
                    var rect = new RectangleF(
                        ImageWidthPerChar * x + ImagePadding, ImagePadding,
                        ImageWidthPerChar, image.Height - ImagePadding);
                    var str = captchaCode[x].ToString();
                    path.AddString(str, font.FontFamily, (int)font.Style, font.Size, rect, stringFormat);
                    // 拉伸
                    var points = new PointF[] {
                        new PointF(rect.X + randomPadding(), randomPadding()),
                        new PointF(rect.X + rect.Width - randomPadding(), randomPadding()),
                        new PointF(rect.X + randomPadding(), image.Height - randomPadding()),
                        new PointF(rect.X + rect.Width - randomPadding(), image.Height - randomPadding()),
                    };
                    path.Warp(points, rect);
                    // 旋转
                    var matrix = new Matrix();
                    matrix.RotateAt(rand.Next(-CharGraphicMaxRotate, CharGraphicMaxRotate), rect.Location);
                    graphic.Transform = matrix;
                    // 描画到图层
                    graphic.FillPath(textBrush, path);
                }
            }
            // 储存到会话，会话的过期时间最少是30分钟
            //var sessionManager = Application.Ioc.Resolve<SessionManager>();
            //var session = sessionManager.GetSession();
            //session.Items[SessionItemKeyPrefix + key] = captchaCode;
            //session.SetExpiresAtLeast(TimeSpan.FromMinutes(MakeSessionAliveAtLeast));
            //sessionManager.SaveSession();
            // 返回图片对象
            return image;


        }
    }
}
