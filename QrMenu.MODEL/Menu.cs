using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QrMenu.MODEL
{
    [Serializable]
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [StringLength(30)]
        public string Ad { get; set; }
        [Required]
        public decimal Fiyat { get; set; }
        public string MediaPath { get; set; }
        [DefaultValue(false)]
        public bool isAktive { get; set; }

    }
}
