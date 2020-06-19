using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrMenu.MODEL
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }
        [StringLength(30)]
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        
    }
}
