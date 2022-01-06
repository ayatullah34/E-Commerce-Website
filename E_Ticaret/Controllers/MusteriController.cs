using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class MusteriController:Controller
    {

        // GET: Müşteri
        [Authorize(Roles = "A,T")]
        public ActionResult Index()
            {
                Model1 m = new Model1();
                List<Musteri> ted = m.Musteris.ToList();
                return View(ted);
            }

            public ActionResult Ekle()
            {
                Musteri k = new Musteri();
                return View(k);

            }

            [HttpPost]
            public ActionResult Ekle(Musteri kat)
            {
                Model1 m = new Model1();

   
            Musteri k = m.Musteris.FirstOrDefault(x =>x.Id == kat.Id);


                if (k == null)
                {
                    m.Musteris.Add(kat);
                }
                else
                {
                k.Adi = kat.Adi;
                k.Soyadi = kat.Soyadi;
                k.KullaniciAdi = kat.KullaniciAdi;
                k.Email = kat.Email;
                k.Telefon = kat.Telefon;

                }
           
                m.SaveChanges();
         
            return RedirectToAction("Index");
            }

            public ActionResult Guncelle(int id)
            {

                Model1 m = new Model1();
                Musteri kat = m.Musteris.FirstOrDefault(x => x.Id == id);
                return View("Ekle", kat);
            }

            public int Sil(int id)
            {
                Model1 m = new Model1();
                Musteri ted = m.Musteris.FirstOrDefault(x => x.Id == id);
                m.Musteris.Remove(ted);
                try
                {
                    m.SaveChanges();
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
            public ActionResult KayitOl(Musteri data)
        {
            Model1 m = new Model1();
            m.Musteris.Add(data);
            m.SaveChanges();
            return RedirectToAction("Index", "Home");


        }




        }
  
}