using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using E_Ticaret.App_Classes;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "A,T,M")]
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Security");
            //return View();
        }
        [Authorize(Roles = "A,T,M")]
        public ActionResult Anasayfa()
        {

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "A")]
        public ActionResult SliderResimleri()
        {
            return View();
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult SliderResimEkle(HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                Bitmap bp = new Bitmap(img, Settings.SliderResimBoyut);

                string yol = "/Content/SliderResim/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                bp.Save(Server.MapPath(yol));

                Resim rsm = new Resim();
                rsm.BuyukYol = yol;
                Contex.Baglanti.Resims.Add(rsm);
                Contex.Baglanti.SaveChanges();
            }

            return RedirectToAction("SliderResimleri");

        }


       


        //----------------------------DÜZENLEME İŞLEMLERİ--------------------------------

        ///Content/UrunResim/Buyuk6ea22a75-a6a3-4e38-9f9e-3101503f36b8.png

        
    }
}
