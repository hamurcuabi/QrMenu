using QrMenu.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.BLL
{
   public class KullaniciRepository:BaseRepository<Kullanici>
    {
        public Kullanici CheckUser(Kullanici model)
        {
            var result = GetOne(f => f.KullaniciAdi == model.KullaniciAdi && f.Sifre == model.Sifre);
            return result;
        }
    }
}
