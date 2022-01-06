using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.App_Classes;
using E_Ticaret.Models;
using static E_Ticaret.App_Classes.Sepet;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


        public PartialViewResult Sepet()
        {
            return PartialView();
        }

        public PartialViewResult Slider()
        {
            var data = Contex.Baglanti.Resims.Where(x => x.BuyukYol.Contains("Slider")).ToList();

            return PartialView(data);
        }

        public PartialViewResult YeniUrunler()
        {
            Model1 m = new Model1();
            var data =m.Uruns.ToList();
            return PartialView(data);
        }

        public PartialViewResult Servisler()
        {
            return PartialView();
        }



        public PartialViewResult Markalar()
        {
            var data = Contex.Baglanti.Markas.ToList();
            return PartialView(data);
        }

        public void SepeteEkle(int id)
        {
            SepetItem si = new SepetItem();
            Urun u = Contex.Baglanti.Uruns.FirstOrDefault(x => x.Id == id);
            si.Urun = u;
            si.Adet = 1;
            si.Indirim = 0;
            Sepet s = new Sepet();
            s.SepeteEkle(si);

        }

        public void SepetUrunAdetDusur(int id)
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Session["AktifSepet"];

                if (s.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet > 1)
                {
                    s.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet--;
                }
                else
                {
                    SepetItem si = s.Urunler.FirstOrDefault(x => x.Urun.Id == id);

                    s.Urunler.Remove(si);
                }
            }
        }

        public PartialViewResult MiniSepetWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
            }
            else
            {
                return PartialView();
            }
        }

        public ActionResult UrunDetay(string id)
        {
            Urun u = Contex.Baglanti.Uruns.FirstOrDefault(x => x.Adi == id);

            List<UrunOzellik> uos = Contex.Baglanti.UrunOzelliks.Where(x => x.UrunID == u.Id).ToList();
            Dictionary<string, List<OzellikDeger>> ozellik = new Dictionary<string, List<OzellikDeger>>();

            List<OzellikDeger> degers = new List<OzellikDeger>();
            foreach (UrunOzellik uo in uos)
            {
                OzellikTip ot = Contex.Baglanti.OzellikTips.FirstOrDefault(x => x.Id == uo.OzellikTipID);

                bool p = false;
                foreach (var item in ozellik)
                {
                    // OzellikDeger od = Context.Baglanti.OzellikDeger.FirstOrDefault(x => x.OzellikTipID == ot.Id && x.Id == uo.OzellikDegerID);
                    if (item.Key != ot.Adi)
                    {
                        p = true;
                    }
                    else
                    {
                        p = false;
                    }
                }
                if (p)
                {
                    degers = new List<OzellikDeger>();
                }

                foreach (OzellikDeger deger in Contex.Baglanti.OzellikDegers.Where(x =>x.OzellikTipId == ot.Id).ToList())
                {
                    OzellikDeger od = Contex.Baglanti.OzellikDegers.FirstOrDefault(x => x.OzellikTipId == ot.Id && x.Id == uo.OzellikDegerID);
                    if (!degers.Any(x => x.Id == od.Id))
                    {
                        degers.Add(od);
                    }
                }
                if (ozellik.Any(x => x.Key == ot.Adi))
                {
                    ozellik[ot.Adi] = degers;
                }
                else
                {
                    ozellik.Add(ot.Adi, degers);
                }
                /* ozellik.Add(ot.Adi, degers);
                 degers = new List<OzellikDeger>();*/
            }


            ViewBag.Ozellikler = ozellik;

            return View(u);
        }
    }
}