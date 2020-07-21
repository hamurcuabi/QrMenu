using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QrMenu.MODEL
{
    [Serializable]
    public class IsletmeUrunEkleModel
    {
       public List<IsletmeUrunEkleModelData> IsletmeUrunEkleModelList { get; set; }
    }
    [Serializable]
    public class IsletmeUrunEkleModelData
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
    }
}