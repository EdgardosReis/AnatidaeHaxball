using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball
{
    public class DataUtils
    {
        internal static string CreateJogadorShirt(string path, Jogador jogador)
        {
            string filePath = Path.Combine(path, "shirt_back.png");

            Image bmp = Bitmap.FromFile(filePath);
            Bitmap newImage = new Bitmap(bmp.Width, bmp.Height);

            Graphics gr = Graphics.FromImage(newImage);
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

            string shirtName = (jogador.nome + jogador.idJogador).GetHashCode() + ".png";

            //newImage.Save(Path.Combine(path, shirtName));

            return shirtName;
        }

    }
}