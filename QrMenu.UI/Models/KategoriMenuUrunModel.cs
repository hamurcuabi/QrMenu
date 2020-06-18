using QrMenu.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QrMenu.UI.Models
{
    public class KategoriMenuUrunModel
    {
       public ICollection<Kategori> KategoriList { get; set; }
        public ICollection<Menu> MenuList { get; set; }
        public ICollection<Urun> UrunList { get; set; }
    }
}