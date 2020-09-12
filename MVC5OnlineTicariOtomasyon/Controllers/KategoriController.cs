using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;  //bu eklenir
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context tablolar=new Context();
        public ActionResult Index()
        {
            var degerler = tablolar.Kategoris.ToList();
            return View(degerler);
        }
        [HttpGet]   //sayfa yüklendiği zaman çalışacak
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]   //butona tıklandığı zaman çalışacak
         public ActionResult KategoriEkle(Kategori k)
         {
             tablolar.Kategoris.Add(k);//k dan gelen değeri ekler.  k da view tarafında gönderilen parametreleri tutar yani kategori adını tutacak
             tablolar.SaveChanges();
             return RedirectToAction("Index");
         }

         public ActionResult KategoriSil(int id)
         {
             var kategori = tablolar.Kategoris.Find(id); //gönderilen k de kategoriyi buluyor ve değişkene atıyor
             tablolar.Kategoris.Remove(kategori);  //bulunanı siliyor
             tablolar.SaveChanges();
             return RedirectToAction("Index");
         }

         public ActionResult KategoriGetir(int id)
         {
             var kategori = tablolar.Kategoris.Find(id);
             return View("KategoriGetir", kategori);
         }

         public ActionResult KategoriGuncelle(Kategori k)
         {
             var kategori = tablolar.Kategoris.Find(k.KategoriId);
            kategori.KategoriAd = k.KategoriAd;
            tablolar.SaveChanges();
            return RedirectToAction("Index");
         }
    }
}