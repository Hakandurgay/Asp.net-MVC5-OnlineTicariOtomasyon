using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Context tablo=new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Partial1(Cariler p)
        {
            tablo.Carilers.Add(p);
            tablo.SaveChanges();
            return PartialView();
        }
        [HttpGet]

        public ActionResult CariLogin()
        {
            return View();
        }
        [HttpPost]

        public ActionResult CariLogin(Cariler c)
        {
            var bilgiler = tablo.Carilers.FirstOrDefault(x => x.CariMail == c.CariMail && x.CariSifre == c.CariSifre);
            if (bilgiler != null)
            {

                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"] = bilgiler.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            return RedirectToAction("Index","Login");
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View("Index");
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var bilgiler = tablo.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
                Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}