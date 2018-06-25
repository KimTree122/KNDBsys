using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace KNDBsys.Common.VerifyCode
{
    public class Verify_Code
    {
        public string RandCode()
        {
            int CodeCount = 4;//生成4个随机数  
            string randomChar = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
            randomChar = randomChar.Replace(",", "");
            string RandomCode = "";
            System.Threading.Thread.Sleep(3);
            char[] allCharArray = randomChar.ToCharArray();
            int n = allCharArray.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < CodeCount; i++)
            {
                int rnd = random.Next(0, n);
                RandomCode += allCharArray[rnd];
            }
            return RandomCode;
        }

        #region 验证码--方式1  
        ///  <summary>  
        ///  验证码--方式1 创建验证码图片  
        ///  </summary>  
        ///  <param  name="randomcode">验证码</param>  
        public byte[] CreateImage(string randomcode)
        {
            int randAngle = 40; //随机转动角度  
            int mapwidth = (int)(randomcode.Length * 18);
            Bitmap map = new Bitmap(mapwidth, 24);//创建图片背景  
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(Color.White);//清除画面，填充背景  
                                     //graph.DrawRectangle(new Pen(Color.Silver, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框  

            Random rand = new Random();

            //验证码旋转，防止机器识别  
            char[] chars = randomcode.ToCharArray();//拆散字符串成单字符数组  
            //文字距中  
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            //定义颜色  
            Color[] c = { Color.Black, Color.Red, Color.Blue, Color.Green,
                            Color.Orange, Color.Brown, Color.DarkBlue };

            //画图片的背景噪音线  
            for (int i = 0; i < 2; i++)
            {
                int x1 = rand.Next(10);
                int x2 = rand.Next(map.Width - 10, map.Width);
                int y1 = rand.Next(map.Height);
                int y2 = rand.Next(map.Height);

                graph.DrawLine(new Pen(c[rand.Next(7)]), x1, y1, x2, y2);
            }

            for (int i = 0; i < chars.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);
                Font f = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Regular);//字体样式(参数2为字体大小)  
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                Point dot = new Point(12, 16);
                float angle = rand.Next(-randAngle, randAngle);//转动的度数  
                graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置  
                graph.RotateTransform(angle);
                graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                graph.RotateTransform(-angle);//转回去  
                graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置  
            }
            //生成图片  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            MemoryStream stream = new MemoryStream();
            map.Save(stream, ImageFormat.Jpeg);
            graph.Dispose();
            map.Dispose();
            return stream.ToArray();


        }
        #endregion

        /// <summary>  
        /// 验证码--方式2  
        /// </summary>  
        /// <param name="chkCode">验证码</param>  
        /// <returns></returns>  
        public Byte[] CreateImage2(string chkCode)
        {
            //颜色列表，用于验证码、噪线、噪点    
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码    
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            Random rnd = new Random();
            Bitmap bmp = new Bitmap(100, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线    
            for (int i = 0; i < 3; i++)
            {
                int x1 = rnd.Next(100);
                int y1 = rnd.Next(40);
                int x2 = rnd.Next(100);
                int y2 = rnd.Next(40);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串    
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);
            }
            //画噪点    
            for (int i = 0; i < 20; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }

            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            finally
            {
                //显式释放资源    
                bmp.Dispose();
                g.Dispose();
            }
        }
    }
}
