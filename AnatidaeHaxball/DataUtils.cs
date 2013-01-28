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
            string photoId = "";

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

                    string shirtName = jogador.nome;

                    newShirt.Save(memoryStream, ImageFormat.Png);

                    //PhotoInfo photo = flickr.PhotosGetInfo("8421237809");

                    //string filePath2 = Path.Combine(uploadPath, shirtName);

                    //newShirt.Save(filePath2+".png", ImageFormat.Png);


                    if (jogador.nome.Length > 12)
                    {
                        photoId = jogador.nome.Substring(0, 12);
                    }
                    photoId += new Random().Next(1000, 9999)+".png";

                    using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client("AKIAJJC4RIZDLORP65UA", "oNK/qZGrdFTNZBR5bYgkZfFtlVYQVGrEdv1tfShU"))
                    {
                        PutObjectRequest request = new PutObjectRequest();
                        request.WithBucketName("anatidae-haxball/equipamentos");
                        request.WithKey(photoId);
                        request.WithInputStream(memoryStream);
                        request.AutoCloseStream = true;
                        request.CannedACL = S3CannedACL.PublicRead;

                        client.PutObject(request);
                    }


                }
            }

            bmp.Dispose();

            return photoId;
        }

        public static string GetShirt(string imageId)
        {
            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client("AKIAJJC4RIZDLORP65UA", "oNK/qZGrdFTNZBR5bYgkZfFtlVYQVGrEdv1tfShU"))
            {
                GetPreSignedUrlRequest get = new GetPreSignedUrlRequest()
                    .WithBucketName("anatidae-haxball/equipamentos")
                    .WithKey(imageId)
                    .WithExpires(DateTime.Now.AddMinutes(30));

                return client.GetPreSignedURL(get);
            }
        }

    }


}