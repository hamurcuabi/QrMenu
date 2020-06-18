using QrMenu.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.BLL
{
    public class MenuUrunRepository:BaseRepository<MenuUrun>
    {
        public bool CokluIcerikEkle(List<MenuUrun> model)
        {
            if (model!=null)
            {
                foreach (var item in model)
                {
                    bool result=Create(item);
                    if (!result)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public ICollection<MenuUrun>MenuyeGoreIcerikGetir(int id)
        {
            return GetList(f => f.MenuId==id);
        }

        public bool CokluIcerikGuncelle (List<MenuUrun> model)
        {
            if (model!=null)
            {
                foreach (var item in model)
                {
                    var found = GetOne(f => f.MenuId == item.MenuId);
                    bool resultdelete = Delete(found.MenuId);
                    if (!resultdelete)
                    {
                        return false;
                    }
                    bool resultUpdate = Create(item);
                    if (!resultUpdate)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
