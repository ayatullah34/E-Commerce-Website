using E_Ticaret.App_Classes;
using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    [Authorize(Roles = "A,T,M")]
    public class UrunController:Controller
    {

        public ActionResult Urunler()
        {
            return View(Contex.Baglanti.Uruns.ToList());
        }

        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = Contex.Baglanti.Kategoris.ToList();
            ViewBag.Markalar = Contex.Baglanti.Markas.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun urun)
        {
            Contex.Baglanti.Uruns.Add(urun);
            Contex.Baglanti.SaveChanges();
            return RedirectToAction("Urunler");
        }

       
        public ActionResult UrunResimEkle(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult UrunResimEkle(int urunId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                Bitmap ortaResim = new Bitmap(img, Settings.UrunOrtaBoyut);
                Bitmap buyukResim = new Bitmap(img, Settings.UrunBuyukBoyut);

                string ortaYol = "/Content/UrunResim/Orta" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string buyukYol = "/Content/UrunResim/Buyuk" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                ortaResim.Save(Server.MapPath(ortaYol));
                buyukResim.Save(Server.MapPath(buyukYol));

                Resim rsm = new Resim();
                rsm.BuyukYol = buyukYol;
                rsm.OrtaYol = ortaYol;
                rsm.UrunID = urunId;

                if (Contex.Baglanti.Resims.FirstOrDefault(x => x.UrunID == urunId && x.Varsayilan == false) != null)
                    rsm.Varsayilan = true;
                else
                    rsm.Varsayilan = false;

                Contex.Baglanti.Resims.Add(rsm);
                Contex.Baglanti.SaveChanges();
                return View(urunId);
            }

            return View(urunId);

        }

        //--------------------- SİLME İŞLEMLERİ ------------------------------------
        [HttpGet]
        public ActionResult UrunSil(int id)
        {
            Model1 m = new Model1();
            Urun u = m.Uruns.FirstOrDefault(x => x.Id == id);

            return View(u);
        }
        [HttpPost]
        public ActionResult UrunSil(Urun u, Resim rs, UrunOzellik uo)
        {
            Model1 m = new Model1();

            List<Resim> rs1 = m.Resims.Where(x => x.UrunID == u.Id).ToList();

            if (rs1 != null)
            {
                foreach (var item in rs1)
                {
                    m.Resims.Remove(item);
                }
                m.SaveChanges();
            }

            u = m.Uruns.FirstOrDefault(x => x.Id == u.Id);
            m.Uruns.Remove(u);
            m.SaveChanges();

            return RedirectToAction("Urunler");
        }


        //---------------GÜNCELLEME İŞLEMİ--------------------


        public ActionResult UrunGuncelle(int? id)
        {
            Model1 m = new Model1();

            Urun guncelle = m.Uruns.Where(k => k.Id == id).FirstOrDefault();

            List<SelectListItem> deger1 = (from x in m.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Value = x.Id.ToString(),
                                               Text = x.Adi,

                                           }).ToList();

            List<Resim> resimler = m.Resims.Where(x => x.UrunID == id).ToList();
            ViewBag.resim = resimler;

            ViewBag.ktgr = deger1;
            return View(guncelle);
        }

        [HttpPost]
        public ActionResult UrunGuncelle(Urun model, HttpPostedFileBase file,int resimId = -1)
        {
            Model1 m = new Model1();
            var urun = m.Uruns.Find(model.Id);
            


            {
                urun.Adi = model.Adi;
                urun.Aciklama = model.Aciklama;
                urun.AlisFiyat = model.AlisFiyat;
                urun.SatisFiyat = model.SatisFiyat;
                urun.KategoriID = model.KategoriID;
                /*if (urun.Kategori != null)
                    urun.Kategori = model.Kategori;*/

                if (file != null)
                {
                   
                    Image img = Image.FromStream(file.InputStream);

                    Bitmap ortaResim = new Bitmap(img, Settings.UrunOrtaBoyut);
                    Bitmap buyukResim = new Bitmap(img, Settings.UrunBuyukBoyut);

                    string ortaYol = "/Content/UrunResim/Orta" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string buyukYol = "/Content/UrunResim/Buyuk" + Guid.NewGuid() + Path.GetExtension(file.FileName);

                    ortaResim.Save(Server.MapPath(ortaYol));
                    buyukResim.Save(Server.MapPath(buyukYol));

                    Resim rsm = new Resim();
                    rsm.BuyukYol = buyukYol;
                    rsm.OrtaYol = ortaYol;
                    rsm.UrunID = model.Id;

                    if (m.Resims.FirstOrDefault(x => x.UrunID == model.Id && x.Varsayilan == false) != null)
                        rsm.Varsayilan = true;
                    else
                        rsm.Varsayilan = false;

                    m.Resims.Add(rsm);
                    m.SaveChanges();
                    //return View(model.Id);
                }

               
                
                List<Resim> i = m.Resims.Where(x => x.UrunID == model.Id).ToList();
                
                if( resimId > 0)
                {
                    m.Resims.Remove(i[resimId - 1]);
                }
               
                
                m.SaveChanges();

                return RedirectToAction("Urunler");
            }
        }





    }
}