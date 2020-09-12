using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context tablolar=new Context();
        public ActionResult Index()
        {
            var liste = tablolar.Faturalars.ToList();

            return View(liste);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();

        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            tablolar.Faturalars.Find(f);
            tablolar.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult FaturaGetir(int id)
        {
            var fatura = tablolar.Faturalars.Find(id);
            return View("FaturaEkle", fatura);
        }
    }
}