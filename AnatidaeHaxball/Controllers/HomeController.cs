using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnatidaeHaxball.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            string filePath = Path.Combine(HttpContext.Server.MapPath("../Images/Player_Shirts"),
                                "shirt_back.jpg");

            var bmp = Bitmap.FromFile(filePath);
            var newImage = new Bitmap(bmp.Width, bmp.Height);

            var gr = Graphics.FromImage(newImage);
            gr.DrawImageUnscaled(bmp, 0, 0);

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            gr.DrawString("10", new Font(FontFamily.GenericSansSerif, 120, FontStyle.Bold), Brushes.Orange,
                new RectangleF(0 , -30, bmp.Width, bmp.Height), format);

            gr.DrawString("loled", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Bold), Brushes.Orange,
                new RectangleF(0, -50, bmp.Width, bmp.Height/2), format);

            newImage.Save(Path.Combine(HttpContext.Server.MapPath("../Images"),
                                "newImg.jpg"));

            return View(newImage);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
