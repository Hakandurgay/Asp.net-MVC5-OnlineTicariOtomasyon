using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context tablolar=new Context();
        public ActionResult Index()
        {
            var degerler = tablolar.Personels.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in tablolar.Departmans.ToList()
                select new SelectListItem
                {
                    Text = x.DepartmanAd, //bize gözükecek kıısm
                    Value = x.DepartmanId.ToString() //arka paln
                }).ToList();

            ViewBag.dgr1 = deger1; //değer biri viewa taşıyor   
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            tablolar.Personels.Add(p);
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            var personel = tablolar.Personels.Find(id);
            List<SelectListItem> deger1 = (from x in tablolar.Departmans.ToList()
                select new SelectListItem
                {
                    Text = x.DepartmanAd, //bize gözükecek kıısm
                    Value = x.DepartmanId.ToString() //arka paln
                }).ToList();

            ViewBag.dgr1 = deger1; //değer biri viewa taşıyor   
            return View("PersonelGetir",personel);  //viewın adı
        }

        public ActionResult PersonelGuncelle(Personel p)
        {
            var personel = tablolar.Personels.Find(p.PersonelId);
            personel.PersonelAd = p.PersonelAd;
            personel.PersonelSoyad = p.PersonelSoyad;
            personel.PersonelGorsel = p.PersonelGorsel;
            personel.Departmanid = p.Departmanid;
            tablolar.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}