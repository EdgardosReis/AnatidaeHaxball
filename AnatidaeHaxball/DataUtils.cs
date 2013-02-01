using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web;

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

                    photoId = GenerateImageName(".png");
 
                    UploadFile(memoryStream, photoId, ConfigurationManager.AppSettings["AWSBucketShirts"]);
                }
            }

            bmp.Dispose();

            return photoId;
        }

        internal static string CreateTeamLogo(HttpPostedFileBase file)
        {
            if (file == null) 
                throw new ArgumentNullException("file", "O argumento passado ao método CreateTeamLogo não pode ser null");

            int maxWidth = 300;
            int maxHeight = 300;

            Image image = Image.FromStream(file.InputStream);

            Stream stream = file.InputStream;
            string imageName = GenerateImageName(file.FileName);
            string bucket = ConfigurationManager.AppSettings["AWSBucketTeamLogos"];

            if (image.Height > maxHeight || image.Width > maxWidth)
            {
                var ratioX = (double)maxWidth / image.Width;
                var ratioY = (double)maxHeight / image.Height;
                var ratio = Math.Min(ratioX, ratioY);

                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

                Stream resizedStream = new MemoryStream();

                newImage.Save(resizedStream, ImageFormat.Png);

                stream = resizedStream;
            }


            UploadFile(stream, imageName, bucket);

            return imageName;

        }

        private static string GenerateImageName(string extension)
        {
            return Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(extension);
        }

        public static string GetShirt(string imageId)
        {
            if (String.IsNullOrEmpty(imageId)) return "";
            return GetFileUrl(imageId, ConfigurationManager.AppSettings["AWSBucketShirts"]);
        }


        internal static void DeleteShirt(string imageId)
        {
            if (String.IsNullOrEmpty(imageId)) return;
            DeleteFile(imageId, ConfigurationManager.AppSettings["AWSBucketShirts"]);
        }

        public static string GetTeamLogo(string imageId)
        {
            if (String.IsNullOrEmpty(imageId)) return "";
            return GetFileUrl(imageId, ConfigurationManager.AppSettings["AWSBucketTeamLogos"]);
        }

        internal static void DeleteTeamLogo(string imageId)
        {
            if (String.IsNullOrEmpty(imageId)) return;
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