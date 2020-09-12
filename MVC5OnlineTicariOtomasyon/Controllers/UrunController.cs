using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun


        Context tablolar=new Context();

        public ActionResult Index()
        {
         //var urunler = tablolar.Uruns.ToList();  bunun yerine true olanları listelicek
         var urunler = tablolar.Uruns.Where(x => x.Durum == true).ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in tablolar.Kategoris.ToList()
                select new SelectListItem
                {
                    Text = x.KategoriAd, //bize gözükecek kıısm
                    Value = x.KategoriId.ToString() //arka paln
                }).ToList();

            ViewBag.dgr1 = deger1; //değer biri viewa taşıyor   
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun urun)
        {
            tablolar.Uruns.Add(urun);
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger = tablolar.Uruns.Find(id);
            deger.Durum = false;
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in tablolar.Kategoris.ToList()
                select new SelectListItem
                {
                    Text = x.KategoriAd, //bize gözükecek kıısm
                    Value = x.KategoriId.ToString() //arka paln
                }).ToList();

            ViewBag.dgr1 = deger1; //değer biri viewa taşıyor   
            var urunDeger = tablolar.Uruns.Find(id);
            return View("UrunGetir", urunDeger);
        }

        public ActionResult UrunGuncelle(Urun p)
        {
            var urun = tablolar.Uruns.Find(p.UrunId);
            urun.AlisFiyati = p.AlisFiyati;
            urun.Durum = p.Durum;
            urun.KategoriId = p.KategoriId;
            urun.Marka = p.Marka;
            urun.SatisFiyati = p.SatisFiyati;
            urun.Stok = p.Stok;
            urun.UrunAd = p.UrunAd;
            urun.UrunGorsel = p.UrunGorsel;
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}