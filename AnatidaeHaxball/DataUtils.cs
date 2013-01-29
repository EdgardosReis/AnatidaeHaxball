using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace AnatidaeHaxball
{
    public class DataUtils
    {
        internal static string CreateJogadorShirt(string path, Jogador jogador)
        {
          
            string filePath = Path.Combine(path, "shirt_back.png");
            Image bmp = Bitmap.FromFile(filePath);
            string photoId;

            string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

            using (var memoryStream = new MemoryStream())
            {
                using (Bitmap newShirt = new Bitmap(bmp.Width, bmp.Height))
                {
                    Graphics gr = Graphics.FromImage(newShirt);
                    gr.DrawImageUnscaled(bmp, 0, 0);
                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    Brush color = new SolidBrush(Color.FromArgb(255, 255, 138, 0));
                    gr.DrawString(jogador.nome, new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold), color,
                        new RectangleF(0, -25, bmp.Width, bmp.Height / 2), format);
                    gr.TextRenderingHint = TextRenderingHint.AntiAlias;
                    gr.DrawString(jogador.avatar, new Font(new FontFamily("Impact"), 90, FontStyle.Regular), color,
                        new RectangleF(0, -20, bmp.Width, bmp.Height), format);

                    newShirt.Save(memoryStream, ImageFormat.Png);

                    photoId = GenerateImageName(jogador.nome) + ".png";
 
                    UploadFile(memoryStream, photoId, ConfigurationManager.AppSettings["AWSBucketShirts"]);
                }
            }

            bmp.Dispose();

            return photoId;
        }

        public static void UploadTeamLogo(string filePath, Equipa equipa)
        {
            UploadFile(filePath, null, ConfigurationManager.AppSettings["AWSBucketTeamLogos"]);
        }

        private static string GenerateImageName(string name)
        {
            if (name.Length > 12)
            {
                name = name.Substring(0, 12);
            }
            return  name + new Random().Next(1000, 9999);// +".png";
        }

        public static string GetShirt(string imageId)
        {
            return GetFileUrl(imageId, ConfigurationManager.AppSettings["AWSBucketShirts"]);
        }


        internal static void DeleteShirt(string imageId)
        {
            DeleteFile(imageId, ConfigurationManager.AppSettings["AWSBucketShirts"]);
        }

        public static string GetTeamLogo(string imageId)
        {
            return GetFileUrl(imageId, ConfigurationManager.AppSettings["AWSBucketTeamLogos"]);
        }

        internal static void DeleteTeamLogo(string imageId)
        {
            DeleteFile(imageId, ConfigurationManager.AppSettings["AWSBucketTeamLogos"]);
        }

        private static string GetFileUrl(string key, string bucket)
        {
            if (key == null) return "";

            string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey))
            {
                GetPreSignedUrlRequest get = new GetPreSignedUrlRequest()
                    .WithBucketName(bucket)
                    .WithKey(key)
                    .WithExpires(DateTime.Now.AddMinutes(30));

                return client.GetPreSignedURL(get);
            }
        }

        private static void DeleteFile(string key, string bucket)
        {
            if (key == null) return;

            string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey))
            {
                DeleteObjectRequest delete = new DeleteObjectRequest()
                    .WithBucketName(bucket)
                    .WithKey(key);

                client.DeleteObject(delete);
            }
        }

        private static void UploadFile(Stream stream, string key, string bucket)
        {
            string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey))
            {
                PutObjectRequest request = new PutObjectRequest();
                request.WithBucketName(bucket);
                request.WithKey(key);
                request.WithInputStream(stream);
                request.AutoCloseStream = true;
                request.CannedACL = S3CannedACL.PublicRead;

                client.PutObject(request);
            }
        }

        private static void UploadFile(string filePath, string key, string bucket)
        {
            string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey))
            {
                PutObjectRequest request = new PutObjectRequest();
                request.WithBucketName(bucket);
                request.WithKey(key);
                request.WithFilePath(filePath);
                request.CannedACL = S3CannedACL.PublicRead;

                client.PutObject(request);
            }
        }


    }


}