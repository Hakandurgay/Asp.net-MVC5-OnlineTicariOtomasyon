using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;

namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context tablolar=new Context();
        public ActionResult Index()
        {
            var degerler = tablolar.SatisHarekets.ToList();

            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
          SatisGetirListeme();
            return View();
        }

        private void SatisGetirListeme()
        {
            List<SelectListItem> deger1 = (from x in tablolar.Uruns.ToList()
                select new SelectListItem()
                {
                    Text = x.UrunAd,
                    Value = x.UrunId.ToString()
                }).ToList();
            List<SelectListItem> deger2 = (from x in tablolar.Carilers.ToList()
                select new SelectListItem()
                {
                    Text = x.CariAd + " " + x.CariSoyad,
                    Value = x.CariId.ToString()
                }).ToList();
            List<SelectListItem> deger3 = (from x in tablolar.Personels.ToList()
                select new SelectListItem()
                {
                    Text = x.PersonelAd + " " + x.PersonelSoyad,
                    Value = x.PersonelId.ToString()
                }).ToList();
            ViewBag.urun = deger1;
            ViewBag.cari = deger2;
            ViewBag.personel = deger3;
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            //fiyatı ürünler tablosundan çekiyor
            s.Fiyat=Convert.ToDecimal(tablolar.Uruns.Where(x => x.UrunId == s.UrunId).Select(y=>y.SatisFiyati).FirstOrDefault());
            
            s.ToplamTutar = Convert.ToDecimal(s.Fiyat * s.Adet);
            
            tablolar.SatisHarekets.Add(s);
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir(int id) //buradaki id indexte hrefin yanındaki @satis.satisid den geliyor
        {
            SatisGetirListeme();
            var deger = tablolar.SatisHarekets.Find(id);
            return View("SatisGetir",deger);  //virgülden sonra değer, textboxlara yazdırıyor
        }

        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var deger = tablolar.SatisHarekets.Find(p.SatisId);
            deger.CariId = p.CariId;
            deger.Adet = p.Adet;
            deger.Fiyat = p.Fiyat;
            deger.PersonelId = p.PersonelId;
            deger.Tarih = p.Tarih;
            deger.ToplamTutar = p.ToplamTutar;
            deger.UrunId = p.UrunId;
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }
        //datatable için projeye sağ tık managemendnupackages
    }
}