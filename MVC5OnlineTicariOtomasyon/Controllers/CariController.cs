using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar; //ilk bunu eklemekle başla
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context tablolar=new Context(); //sonra bu
        public ActionResult Index()
        {
            var degerler = tablolar.Carilers.Where(x=>x.Durum==true).ToList();

            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniCariEkle()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult YeniCariEkle(Cariler p)
        {
            p.Durum = true;
            tablolar.Carilers.Add(p);
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var cari = tablolar.Carilers.Find(id);
            cari.Durum = false;
            
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var cari = tablolar.Carilers.Find(id);
            return View("CariGetir", cari);
        }

        public ActionResult CariGuncelle(Cariler p)
        {
            if (!ModelState.IsValid)
                return View("CariGetir");
            var cari = tablolar.Carilers.Find(p.CariId);
            cari.CariAd = p.CariAd;
            cari.CariSoyad = p.CariSoyad;
            cari.CariSehir = p.CariSehir;
            cari.CariMail = p.CariMail;
            tablolar.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult MusteriSatisGetir(int id)
        {
            var deger = tablolar.SatisHarekets.Where(x => x.CariId == id).ToList();
            var adSoyad = tablolar.Carilers.Where(x => x.CariId == id).Select(y => y.CariAd + " " + y.CariSoyad)
                .FirstOrDefault();
            ViewBag.degisken = adSoyad;
            return View(deger);
        }
    }
}