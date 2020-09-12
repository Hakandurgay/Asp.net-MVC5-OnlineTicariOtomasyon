using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context tablolar=new Context();
        public ActionResult Index()
        {
            UrunDetayGetir dg=new UrunDetayGetir();
            dg.Deger1 = tablolar.Uruns.Where(x => x.UrunId == 4).ToList();
            dg.Deger2 = tablolar.Detays.Where(y => y.DetayID == 1).ToList();
          //  var degerler = tablolar.Uruns.Where(x => x.UrunId == 4);
            return View(dg);
        }
    }
}