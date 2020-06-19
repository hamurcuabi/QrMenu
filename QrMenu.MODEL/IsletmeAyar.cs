using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.MODEL
{
   public class IsletmeAyar
    {
        [Key]
        public int IsletmeAyarId { get; set; }
        [StringLength(50)]
        public string Ad { get; set; }
     
        public string LogoUri { get; set; }
        [StringLength(30)]
        public string TelNo { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Adres { get; set; }
        [StringLength(200)]
        public string Facebook { get; set; }
        [StringLength(200)]
        public string Instagram { get; set; }
        [StringLength(200)]
        public string Twitter { get; set; }
        [DefaultValue(false)]
        public bool isActive { get; set; }



    }
}
