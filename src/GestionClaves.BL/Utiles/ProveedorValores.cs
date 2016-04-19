using GestionClaves.Modelos.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace GestionClaves.BL.Utiles
{
    public class ProveedorValores : IProveedorValores
    {
        public int LongContrasena { get; set; }

        public int LongTextoCaptcha { get; set; }

        public int AnchoImgCaptcha { get; set; }
        public int AltoImgCaptcha { get; set; }
        public string FontFamilyNameImgCaptcha { get; set; }
        public int FontSizeImgCaptcha { get; set; }

        public string CaracteresContrasena { get; set; }
        public string CaracteresCaptcha { get; set; }

        public ProveedorValores()
        {
            LongContrasena = 12;
            LongTextoCaptcha = 6;
            AnchoImgCaptcha = 200;
            AltoImgCaptcha = 100;
            FontFamilyNameImgCaptcha = "Taoma";
            FontSizeImgCaptcha = 25;
            CaracteresContrasena= "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@$?";
            CaracteresCaptcha = "abcdefghkmnpqrstuvwxyzABCDEFGHKLMNPQRSTUVWXYZ23456789@?+*";
        }



        public string CrearContrasenaAleatoria()
        {
            return CrearCadenaAleatoria(CaracteresContrasena, LongContrasena);
        }

        
        public string CrearTextoCaptcha()
        {
            return CrearCadenaAleatoria(CaracteresCaptcha, LongTextoCaptcha);
        }

        public string GenerarBase64Captcha(string txt)
        {
            var bImage = GenerarImagenCaptcha(txt);
            var sigBase64 = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                bImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                sigBase64 = Convert.ToBase64String(byteImage); //Get Base64
            }
            return sigBase64;
        }


        //
        public Bitmap GenerarImagenCaptcha(string txt)
        {
            return CrearImagen(txt);
        }

        

        private Bitmap CrearImagen(string txt)
        {
            bool noisy = true;
            var width = AnchoImgCaptcha;
            var height = AltoImgCaptcha;
            var fontFamilyName = FontFamilyNameImgCaptcha;
            var fontSize = FontSizeImgCaptcha;

            var rand = new Random((int)DateTime.Now.Ticks);
            var bmp = new Bitmap(width, height);
            using (var mem = new MemoryStream())

            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    var br = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
                    int i;
                    int max_dimension = System.Math.Max(width, height);
                    var pen = new Pen(Color.Yellow);
                    for (i = 0; i <= (int)width * height / 100; i++)
                    {
                        pen.Color = Color.FromArgb(
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)));

                        int X = rand.Next(width);
                        int Y = rand.Next(height);
                        int W = (int)rand.Next(max_dimension) / 30;
                        int H = (int)rand.Next(max_dimension) / 30;

                        gfx.DrawEllipse(pen, X, Y, W, H);
                    }

                    // Mess things up a bit.

                    for (i = 0; i <= (int)width * height / 100; i++)
                    {
                        int X = rand.Next(width);
                        int Y = rand.Next(height);
                        int W = (int)rand.Next(max_dimension) / 40;
                        int H = (int)rand.Next(max_dimension) / 40;
                        gfx.FillEllipse(br, X, Y, W, H);
                    }
                    for (i = 1; i <= 30; i++)
                    {
                        int x1 = rand.Next(width);
                        int y1 = rand.Next(height);
                        int x2 = rand.Next(width);
                        int y2 = rand.Next(height);
                        gfx.DrawLine(Pens.DarkGray, x1, y1, x2, y2);
                    }
                    for (i = 1; i <= 30; i++)
                    {
                        int x1 = rand.Next(width);
                        int y1 = rand.Next(height);
                        int x2 = rand.Next(width);
                        int y2 = rand.Next(height);
                        gfx.DrawLine(Pens.LightGray, x1, y1, x2, y2);
                    }

                    var l = txt.Length;
                    for (i = 0; i < l / 2; i++)
                    {
                        txt = txt.Insert(rand.Next(0, l - 1), " ");
                    }

                }

                //add question 

                gfx.DrawString(txt, new Font(fontFamilyName, fontSize), Brushes.Gray,
                    rand.Next(0, width / 25), rand.Next(2, height / 2));//5-55
            }
            /*try
            {
                bmp.Save(txt + ".png");
            }
            catch (Exception) {
            }*/
            return bmp;
        }
        

        private string CrearCadenaAleatoria(string permitidos, int longitud)
        {            
            Byte[] randomBytes = new Byte[longitud];
            char[] chars = new char[longitud];
            int allowedCharCount = permitidos.Length;

            var randomObj = new Random();
            for (int i = 0; i < longitud; i++)
            {
                randomObj.NextBytes(randomBytes);
                chars[i] = permitidos[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        public string CrearToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
