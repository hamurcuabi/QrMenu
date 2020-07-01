using QrMenu.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.BLL
{
   public class IsletmeAyarRepository:BaseRepository<IsletmeAyar>
    {
        public bool Deactive(int id)
        {
            var model = GetOne(f => f.IsletmeAyarId == id);
            model.isActive = false;
           
            return Update(model);
        }

        public bool Toggle(int id)
        {
            var model = GetOne(f => f.IsletmeAyarId == id);
            switch (model.isActive)
            {
                case true:
                    model.isActive = false;
                    break;
                case false:
                    model.isActive = true;
                    break;
                default:
                    break;
            }
            return Update(model);
        }
    }
}
