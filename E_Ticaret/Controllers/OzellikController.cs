using E_Ticaret.App_Classes;
using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    
    public class OzellikController : Controller
    {
        [Authorize(Roles = "A,T,M")]
        public ActionResult OzellikTipleri()
        {
            return View(Contex.Baglanti.OzellikTips.ToList());
        }

        public ActionResult OzellikTipEkle()
        {
            return View(Contex.Baglanti.Kategoris.ToList());
        }

        [HttpPost]
        public ActionResult OzellikTipEkle(OzellikTip ot)
        {
            Contex.Baglanti.OzellikTips.Add(ot);
            Contex.Baglanti.SaveChanges();
            return RedirectToAction("OzellikTipleri");
        }

        public ActionResult OzellikDegerleri()
        {
            return View(Contex.Baglanti.OzellikDegers.ToList());
        }

        public ActionResult OzellikDegerEkle()
        {
            return View(Contex.Baglanti.OzellikTips.ToList());
        }

        [HttpPost]
        public ActionResult OzellikDegerEkle(OzellikDeger od)
        {
            Contex.Baglanti.OzellikDegers.Add(od);
            Contex.Baglanti.SaveChanges();
            return RedirectToAction("OzellikDegerleri");
        }

        public ActionResult UrunOzellikleri()
        {
            return View(Contex.Baglanti.UrunOzelliks.ToList());
        }

       
        public ActionResult UrunOzellikEkle()
        {
            return View(Contex.Baglanti.Uruns.ToList());
        }

        public PartialViewResult UrunOzellikTipWidget(int? katId)
        {
            if (katId != null)
            {
                var data = Contex.Baglanti.OzellikTips.Where(x => x.KategoriID == katId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Contex.Baglanti.OzellikTips.ToList();
                return PartialView(data);
            }
        }

        public PartialViewResult UrunOzellikDegerWidget(int? tipId)
        {
            if (tipId != null)
            {
                var data = Contex.Baglanti.OzellikDegers.Where(x => x.OzellikTipId == tipId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Contex.Baglanti.OzellikDegers.ToList();
                return PartialView(data);
            }
        }



        [HttpPost]
        public ActionResult UrunOzellikEkle(UrunOzellik uo)
        {
            Contex.Baglanti.UrunOzelliks.Add(uo);
            Contex.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }






        //--------------------- SİLME İŞLEMLERİ ------------------------------------


        public ActionResult UrunOzellikSil(int urunId, int tipId, int degerId)
        {
            UrunOzellik uo = Contex.Baglanti.UrunOzelliks.FirstOrDefault(x => x.UrunID == urunId && x.OzellikTipID == tipId && x.OzellikDegerID == degerId);

            Contex.Baglanti.UrunOzelliks.Remove(uo);
            Contex.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }


        [HttpGet]
        public ActionResult OzellikTipSil(int? id)
        {
            Model1 m = new Model1();
            OzellikTip u = m.OzellikTips.FirstOrDefault(x => x.Id == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult OzellikTipSil(OzellikTip u, OzellikDeger od)
        {
            Model1 m = new Model1();

            List<OzellikDeger> od1 = m.OzellikDegers.Where(x => x.OzellikTipId == u.Id).ToList();

            if (od1 != null)
            {
                foreach (var item in od1)
                {
                    m.OzellikDegers.Remove(item);
                }
                m.SaveChanges();
            }

            u = m.OzellikTips.FirstOrDefault(x => x.Id == u.Id);
            m.OzellikTips.Remove(u);
            m.SaveChanges();

            return RedirectToAction("OzellikTipleri");
        }



        [HttpGet]
        public ActionResult OzellikDegerSil(int id)
        {
            Model1 m = new Model1();
            OzellikDeger u = m.OzellikDegers.FirstOrDefault(x => x.Id == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult OzellikDegerSil(OzellikDeger u)
        {
            Model1 m = new Model1();
            u = m.OzellikDegers.FirstOrDefault(x => x.Id == u.Id);
            m.OzellikDegers.Remove(u);
            m.SaveChanges();

            return RedirectToAction("OzellikDegerleri");
        }



        //--------------------------GÜNCELLEME İŞLEMLERİ-----------------------

        public ActionResult OzellikTipGuncelle(int? id)
        {
            Model1 m = new Model1();

            OzellikTip guncelle = m.OzellikTips.Where(k => k.Id == id).FirstOrDefault();

            List<SelectListItem> deger1 = (from x in m.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Value = x.Id.ToString(),
                                               Text = x.Adi,

                                           }).ToList();

            ViewBag.ktgr = deger1;
            return View(guncelle);
        }

        [HttpPost]
        public ActionResult OzellikTipGuncelle(OzellikTip model)
        {
            Model1 m = new Model1();
            var ot = m.OzellikTips.Find(model.Id);

            ot.Adi = model.Adi;
            ot.Aciklama = model.Aciklama;
            ot.KategoriID = model.KategoriID;

            m.SaveChanges();

            return RedirectToAction("OzellikTipleri");

        }


        public ActionResult OzellikDegerGuncelle(int? id)
        {
            Model1 m = new Model1();

            OzellikDeger guncelle = m.OzellikDegers.Where(k => k.Id == id).FirstOrDefault();

            List<SelectListItem> deger1 = (from x in m.OzellikTips.ToList()
                                           select new SelectListItem
                                           {
                                               Value = x.Id.ToString(),
                                               Text = x.Adi,

                                           }).ToList();

            ViewBag.ktgr = deger1;
            return View(guncelle);
        }

        [HttpPost]
        public ActionResult OzellikDegerGuncelle(OzellikDeger model)
        {
            Model1 m = new Model1();
            var od = m.OzellikDegers.Find(model.Id);

            od.Adi = model.Adi;
            od.Aciklama = model.Aciklama;
            od.OzellikTipId = model.OzellikTipId;
  
            m.SaveChanges();

            return RedirectToAction("OzellikDegerleri");

        }



       

    }
}