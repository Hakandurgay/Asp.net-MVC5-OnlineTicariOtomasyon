using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context tablolar=new Context();
        public ActionResult Index()
        {
            //carilerin değerini sayarak stringe çeviriyior
            var deger1 = tablolar.Carilers.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = tablolar.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = tablolar.Personels.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = tablolar.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;
            var deger5 = tablolar.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = deger5;
            //ürünler tablosundan markayı tekrarsız olarak seçip sayıyor.
            var deger6 = (from x in tablolar.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;
            var deger7 = tablolar.Uruns.Count(x => x.Stok <= 20).ToString();//stok sayısı 20den az olanları getiriyor
            ViewBag.d7 = deger7;
            var deger8 = (from x in tablolar.Uruns orderby x.SatisFiyati descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;
            var deger9 = (from x in tablolar.Uruns orderby x.SatisFiyati ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;
            var deger10 = tablolar.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10 = deger10;
            var deger11 = tablolar.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;
            var deger12 = tablolar.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key)
                .FirstOrDefault();  //markaya göre gruplandır, z den a ya göre bir gruplandırma yap, sayına göre gruplandırma yap, key isim oluyor
            ViewBag.d12 = deger12;

            //en fazla satış yapılan ürünün id sini buluyor ve bu id yi urunler tablosunda bularak adı yazdırıyor
            var deger13 = tablolar.Uruns.Where(u=>u.UrunId==tablolar.SatisHarekets.GroupBy(x => x.UrunId).OrderByDescending(z => z.Count())
                .Select(y => y.Key).FirstOrDefault()).Select(k=>k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;
            //sum değerlerin içini topluyor. count ise satır sayınısın yani o veriden kaç tane var onu getiriyor
            var deger14 = tablolar.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;
            DateTime bugun = DateTime.Today;
            var deger15 = tablolar.SatisHarekets.Count(x => x.Tarih == bugun).ToString(); //tarihi bugüne eşit olan satışların sayısını getir
            ViewBag.d15 = deger15;
     //bugün yapılan satış olmazsa hata verir
            /*    var deger16 = tablolar.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => y.ToplamTutar).ToString();
            ViewBag.d16 = deger16; */  //tarihi bugüne eşit olanların toplam tutarını topla
            return View();
        }

        public ActionResult KolayTablolar()
        {
            var sorgu = from x in tablolar.Carilers  //cariler tablosundaki farklı olan şehirleri alıyor
                group x by x.CariSehir
                into soyutDeger
                select new BasitTabloSinif
                {
                    Sehir = soyutDeger.Key,
                    Sayi = soyutDeger.Count()
                };
            return View(sorgu.ToList());
        }

        public PartialViewResult Partial1()  //viewın içine farklı bir parça atamayı sağlar
                                             //@Html.Action("Partial1","Istatistik") partialın adını ve bulunduuğu yeri yazarak partial çağrılır
        {
            var sorgu = from x in tablolar.Personels
                group x by x.Departman.DepartmanAd
                into g
                select new BasitTabloSinif2
                {
                    Departman = g.Key,
                    Sayi = g.Count()
                };
                    return PartialView(sorgu.ToList());
        }

        public PartialViewResult Partial2()
        {
            var deger = tablolar.Carilers.ToList();
            return PartialView(deger);
        }
        public PartialViewResult Partial3()
        {
            var sorgu = from x in tablolar.Uruns
                group x by x.Marka
                into g
                select new Partial3()
                {
                    Marka = g.Key,
                    Sayi = g.Count()
                };
            return PartialView(sorgu.ToList());
        }
    }
}