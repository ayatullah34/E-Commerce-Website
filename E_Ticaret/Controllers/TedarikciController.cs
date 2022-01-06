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
    [Authorize(Roles = "A")]
    public class TedarikciController : Controller
    {
        // GET: Tedarikci

        public ActionResult Index()
        {
            Model1 m = new Model1();
            List<Tedarikci> ted = m.Tedarikcis.ToList();
            return View(ted);
        }

        public ActionResult Ekle()
        {
            Tedarikci k = new Tedarikci();
            return View(k);

        }

        [HttpPost]
        public ActionResult Ekle(Tedarikci kat)
        {
            Model1 m = new Model1();
            Tedarikci k = m.Tedarikcis.FirstOrDefault(x => x.TedarikciID == kat.TedarikciID);

            if (k == null)
            {
                m.Tedarikcis.Add(kat);
            }
            else
            {
                k.SirketAdi= kat.SirketAdi;
                k.MusteriAdi = kat.MusteriAdi;
                k.MusteriUnvani = kat.MusteriUnvani;
                k.Adres = kat.Adres;
             
            }

            m.SaveChanges();

            
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {

            Model1 m = new Model1();
            Tedarikci kat = m.Tedarikcis.FirstOrDefault(x => x.TedarikciID == id);
            return View("Ekle", kat);
        }

        public int Sil(int id)
        {
            Model1 m = new Model1();
            Tedarikci ted = m.Tedarikcis.FirstOrDefault(x => x.TedarikciID == id);
            m.Tedarikcis.Remove(ted);
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
    }

}