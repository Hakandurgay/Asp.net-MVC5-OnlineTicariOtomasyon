using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;  //bu eklenir

namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context tablolar=new Context();
        public ActionResult Index()
        {
            var degerler = tablolar.Departmans.Where(degisken=>degisken.Durum==false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            tablolar.Departmans.Add(d);

            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            var dep = tablolar.Departmans.Find(id); //id yi buluyor
            dep.Durum = true;
            tablolar.SaveChanges();
            return RedirectToAction("Index"); //index yazınca hangi sayfaya gideceğini belirliyor
        }

        public ActionResult DepartmanGetir(int id)
        {
            var dep = tablolar.Departmans.Find(id);
            return View("DepartmanGetir",dep);//departmandan gelen değer ile birlikte yönlendir
        }

        public ActionResult DepartmanGuncelle(Departman d)
        {
            var dep = tablolar.Departmans.Find(d.DepartmanId); //silmede neden direkt parametre veriyoruz da departmanda departman classından erişiyoruz
            dep.DepartmanAd = d.DepartmanAd;
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int id)
        {
            var degerler = tablolar.Personels.Where(x => x.Departmanid == id).ToList(); //personeller tablosundan id ye göre personel bilgilerini dödürüyor
            var departmanAd = tablolar.Departmans.Where(x => x.DepartmanId == id).Select(y => y.DepartmanAd)
                .FirstOrDefault();//tek bir değer çekeceği için tolist kullanmadı onun yerine bunu kullandı
            ViewBag.sanalDegiskenAdiOnemsiz = departmanAd; //view tarafında tablo ile, foreach kullanarak veri çekilmediği için view bag kullandı.
            //viewbag conttrollerdan veri taşır. //kullanmak için view tarafında @viewbag.sanaldegiskenadionemsiz yazılır
            return View(degerler);
        }

        public ActionResult DepartmanSatisPersonel(int id)
        {
            var degerler = tablolar.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            var personelAd = tablolar.Personels.Where(x => x.PersonelId == id)
                .Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.degisken = personelAd;
            return View(degerler);
        }
    }
}