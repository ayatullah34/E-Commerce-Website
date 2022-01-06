using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class SecurityController : Controller
    {

        // GET: Security
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            Model1 m = new Model1();
            User u = m.Users.FirstOrDefault(x => x.KullaniciAdi == user.KullaniciAdi && x.Parola == user.Parola);
           
            if (u != null)
            {
                FormsAuthentication.SetAuthCookie(u.KullaniciAdi, false);
                return RedirectToAction("Urunler", "Urun");
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı adı veya parola hatalı";
                return View();
            }

        }

        public ActionResult LogOut()
        {
        
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}