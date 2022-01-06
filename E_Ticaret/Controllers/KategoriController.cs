using E_Ticaret.App_Classes;
using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    [Authorize(Roles = "A,T,M")]
    public class KategoriController:Controller
    {

        public ActionResult Kategoriler()
        {
            return View(Contex.Baglanti.Kategoris.ToList());
        }

        public ActionResult KategoriEkle()

        {
            Kategori k = new Kategori();


            return View(k);
        }



        [HttpPost]
        public ActionResult KategoriEkle(Kategori ktg)
        {
            Kategori k = Contex.Baglanti.Kategoris.FirstOrDefault(x => x.Id == ktg.Id);
            if (k != null)
            {
                k.Adi = ktg.Adi;
                k.Aciklama = ktg.Aciklama;
            }
            else
            {
                Contex.Baglanti.Kategoris.Add(ktg);
            }

            Contex.Baglanti.SaveChanges();
            return RedirectToAction("Kategoriler");
        }

        //--------------------SİLME-------------------------
        [HttpGet]
        public ActionResult KategoriSil(int id)
        {
            Model1 m = new Model1();
            Kategori u = m.Kategoris.FirstOrDefault(x => x.Id == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult KategoriSil(Kategori u, OzellikTip tip, Urun urun, OzellikDeger od)
        {
            Model1 m = new Model1();

            List<OzellikTip> tip1 = m.OzellikTips.Where(x => x.KategoriID == u.Id).ToList();



            List<Urun> urun1 = m.Uruns.Where(x => x.KategoriID == u.Id).ToList();

            int i;

            List<OzellikDeger> od1 = new List<OzellikDeger>();


            foreach (var item in tip1)
            {
                i = item.Id;
                List<OzellikDeger> tmp = m.OzellikDegers.Where(x => x.OzellikTipId == i).ToList();
                od1.AddRange(tmp);
            }

            if (od1 != null)
            {
                foreach (var item in od1)
                {
                    m.OzellikDegers.Remove(item);

                }
                m.SaveChanges();
            }


            try
            {
                if (tip1 != null)
                {
                    foreach (var item in tip1)
                    {
                        m.OzellikTips.Remove(item);

                    }
                    m.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (urun1 != null)
            {
                foreach (var item in urun1)
                {
                    m.Uruns.Remove(item);
                }
                m.SaveChanges();
            }

            u = m.Kategoris.FirstOrDefault(x => x.Id == u.Id);
            m.Kategoris.Remove(u);
            m.SaveChanges();

            return RedirectToAction("Kategoriler");
        }

        //-------------------------GÜNCELLE---------------------------
        public ActionResult KategoriGuncelle(int id)
        {
            Model1 m = new Model1();
            Kategori kat = m.Kategoris.FirstOrDefault(x => x.Id == id);


            return View("KategoriEkle", kat);

        }


    }
}