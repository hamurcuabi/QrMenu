using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.MODEL
{
    public class IsletmeUrun
    {
        [Key]
        public int KullaniciUrunId { get; set; }

        public int KullaniciId { get; set; }

        public int UrunId { get; set; }

        public decimal IsletmeFiyat { get; set; }

        [ForeignKey("KullaniciId")]
        public virtual Kullanici Kullanici { get; set; } 
        [ForeignKey("UrunId")]
        public virtual Urun Urun { get; set; }
    }
}
