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

        public List<int> UrunleriEkle(List<int> model,int id)
        {
            List<int> eklenemeyenler = new List<int>();
            foreach (var item in model)
            {
               bool result= Create(new IsletmeUrun { KullaniciId = id, UrunId = item });
                if (!result)
                {
                    eklenemeyenler.Add(item);
                }
            }
            return eklenemeyenler;
        }
    }
}
