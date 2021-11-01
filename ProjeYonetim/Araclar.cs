using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProjeYonetim
{
    public class Araclar
    {
        public DB_ProjeYonetimEntities DbContext = new DB_ProjeYonetimEntities();

        public string CookieGet(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                return String.Empty;
            }
            else
            {
                return HttpContext.Current.Request.Cookies[cookieName].Value;
            }
        }

        //Girilen şifreyi SHA512 ile encode eder.
        public string CreateSHA512Hash(string sifre)
        {
            using (SHA512 mySHA512Hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(sifre);
                byte[] hashBytes = mySHA512Hash.ComputeHash(sourceBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }

        //Fotoğrafı "orijinal [en x boy] oranını bozmadan" istenilen genişlik ve yükseklik değerine göre yeniden boyutlandırır.
        //Kenarlarda kalan boşlukları istenen renk ile doldurur.
        public Image ResizeImage(Image imgPhoto, int width, int height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

    }
}