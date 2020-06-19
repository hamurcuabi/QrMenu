
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QrMenu.MODEL
{
    public class Urun
    {
        [Key]
        public int UrunId { get; set; }
        
        public int? KategoriId { get; set; }
        [StringLength(200)]
        public String Ad { get; set; }
        [StringLength(500)]
        public String Aciklama { get; set; }
        public String MediaPath { get; set; }
        public String Kodu { get; set; }
        [DefaultValue(true)]
        public Boolean IsAktif { get; set; }
        public decimal Fiyat { get; set; }

        
        [ForeignKey("KategoriId")]
        public virtual Kategori Kategori { get; set; }
    }


}
