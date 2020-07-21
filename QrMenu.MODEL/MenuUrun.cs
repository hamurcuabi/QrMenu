using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using QrMenu.MODEL;

namespace QrMenu.MODEL
{
    [Serializable]
    public class MenuUrun
    {
        [Key]
        public int MenuUrunId { get; set; }
        public int MenuId { get; set; }
        public int UrunId { get; set; }
        public decimal UrunMiktar { get; set; }

        [ForeignKey("UrunId")]
        public virtual Urun Urun { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
    }
}
