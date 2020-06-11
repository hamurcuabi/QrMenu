using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QrMenu.MODEL
{
    public class Menu
    {

        [Key]
        public int MenuId { get; set; }
        public int MenuSpecialId { get; set; }
        public int UrunId { get; set; }
        public String MediaPath { get; set; }
        public decimal Fiyat { get; set; }
        [DefaultValue(true)]
        public Boolean IsAktif { get; set; }
        public decimal Miktar { get; set; }

        [StringLength(200)]
        public String Ad { get; set; }

        [ForeignKey("UrunId")]
        public virtual Urun Urunler { get; set; }
    }
}
