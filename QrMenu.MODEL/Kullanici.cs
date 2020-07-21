using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QrMenu.MODEL
{
    [Serializable]
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }
        [StringLength(30)]
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public int? IsletmeAyarId { get; set; }

        public int Yetki { get; set; }

        [ForeignKey("IsletmeAyarId")]
        public IsletmeAyar IsletmeAyar { get; set; }

    }
}
