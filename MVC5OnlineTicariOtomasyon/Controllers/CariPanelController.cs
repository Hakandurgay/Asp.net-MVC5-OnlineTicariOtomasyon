using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5OnlineTicariOtomasyon.Models.Siniflar;
namespace MVC5OnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        private Context tablo = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string) Session["CariMail"];
            var degerler = tablo.Carilers.FirstOrDefault(x => x.CariMail == mail);
            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var mail = (string) Session["CariMail"];
            var id = tablo.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = tablo.SatisHarekets.Where(x => x.CariId == id).ToList();
            return View(degerler);
        }
    }
}