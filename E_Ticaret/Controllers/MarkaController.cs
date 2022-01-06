using E_Ticaret.App_Classes;
using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    [Authorize(Roles = "A,T")]
    public class MarkaController : Controller
    {

        public ActionResult Markalar()
        {
            return View(Contex.Baglanti.Markas.ToList());
        }

        public ActionResult MarkaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarkaEkle(Marka mrk, HttpPostedFileBase fileUpload)
        {
            int resimId = -1;
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                int width = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaWidth"].ToString());

                int height = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaHeight"].ToString());

                string name = "/Content/MarkaResim/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));

                Resim rsm = new Resim();
                rsm.OrtaYol = name;
                Contex.Baglanti.Resims.Add(rsm);
                Contex.Baglanti.SaveChanges();

                if (rsm.Id != 0)
                {
                    resimId = rsm.Id;
                }


            }

            if (resimId != -1)
            {
                mrk.ResimID = resimId;
            }

            Contex.Baglanti.Markas.Add(mrk);
            Contex.Baglanti.SaveChanges();
            return RedirectToAction("Markalar");
        }


        //-------------------------SİLME İŞLEMİ---------------------


        [HttpGet]
        public ActionResult MarkaSil(int id)
        {
            Model1 m = new Model1();
            Marka u = m.Markas.FirstOrDefault(x => x.Id == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult MarkaSil(Marka u, Urun urun, Resim rs,UrunOzellik uo)
        {
            Model1 m = new Model1();

            List<UrunOzellik> rs2 = m.UrunOzelliks.Where(x => x.UrunID == u.Id).ToList();

            if (rs2 != null)
            {
                foreach (var item in rs2)
                {
                    m.UrunOzelliks.Remove(item);
                }
                m.SaveChanges();
            }


            List<Resim> rs1 = m.Resims.Where(x => x.UrunID == urun.Id).ToList();

            if (rs1 != null)
            {
                foreach (var item in rs1)
                {
                    m.Resims.Remove(item);
                }
                m.SaveChanges();
            }

            List<Urun> urun1 = m.Uruns.Where(x => x.MarkaID == urun.Id).ToList();

            if (urun1 != null)
            {
                foreach (var item in urun1)
                {
                    m.Uruns.Remove(item);
                }
                m.SaveChanges();
            }


            u = m.Markas.FirstOrDefault(x => x.Id == u.Id);
            m.Markas.Remove(u);
            m.SaveChanges();

            return RedirectToAction("Markalar");
        }


        //-----------------------------GÜNCELLEME İŞLEMİ----------------------

        public ActionResult MarkaGuncelle(int id)
        {
            Model1 m = new Model1();
            var data = m.Markas.Where(x => x.Id == id).FirstOrDefault();
            return View(data);

        }


        [HttpPost]

        public ActionResult MarkaGuncelle(Marka model, HttpPostedFileBase file,int rsmId=-1)
        {
            Model1 m = new Model1();


            var marka = m.Markas.Find(model.Id);


            marka.Adi = model.Adi;
            marka.Aciklama = model.Aciklama;

            
            m.SaveChanges();
            return RedirectToAction("Markalar");


        }



    }
}