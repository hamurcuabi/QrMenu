using QrMenu.MODEL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.BLL
{
   public class IsletmeUrunRepository:BaseRepository<IsletmeUrun>
    {
        public ICollection<IsletmeUrun> IsletmeninUrunleri(int id)
        {
            return GetList(f => f.KullaniciId == id);
        }

        public List<int> UrunleriEkle(IsletmeUrunEkleModel model,int id)
        {
            List<int> eklenemeyenler = new List<int>();
            foreach (var item in model.IsletmeUrunEkleModelList)
            {
               bool result= Create(new IsletmeUrun { KullaniciId = id, UrunId = item.Id,IsletmeFiyat=item.Fiyat });
                if (!result)
                {
                    eklenemeyenler.Add(item.Id);
                }
            }
            return eklenemeyenler;
        }

        public bool IsletmeUrunSil(int urunId,int isletmeId)
        {
            IsletmeUrun isletmeUrun = GetOne(f => f.UrunId == urunId && f.KullaniciId == isletmeId);
            return Delete(isletmeUrun.KullaniciUrunId);
        }
    }
}
