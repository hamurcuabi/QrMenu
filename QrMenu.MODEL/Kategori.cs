using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrMenu.MODEL
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        [StringLength(200)]
        public String Ad { get; set; }
        public String MediaPath { get; set; }
        public String Aciklama { get; set; }
        [DefaultValue(true)]
        public Boolean IsAktif { get; set; }
    }
}
