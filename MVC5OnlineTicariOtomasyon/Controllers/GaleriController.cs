using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context tablolar=new Context();
        public ActionResult Index()
        {
            var degerler = tablolar.Uruns.ToList();

            return View(degerler);
        }
    }
}