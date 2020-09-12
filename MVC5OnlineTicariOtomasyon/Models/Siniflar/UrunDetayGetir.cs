using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5OnlineTicariOtomasyon.Models.Siniflar
{
    public class UrunDetayGetir
    {
        public IEnumerable<Urun> Deger1 { get; set; }
        public IEnumerable<Detay> Deger2 { get; set; }

        //tek sayfa üzerine birden fazla tablodan veri çekerken ienumerable kullanılıyor

    }
}