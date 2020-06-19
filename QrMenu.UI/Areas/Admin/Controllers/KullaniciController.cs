using QrMenu.BLL;

using QrMenu.MODEL;
using QrMenu.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QrMenu.UI.Areas.Admin.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Admin/Kullanici
        [AdminFilter]
        public ActionResult Liste()
        {
            using (KullaniciRepository repo = new KullaniciRepository())
            {
                return View(repo.GetList());

            }
        }


        [AdminFilter]
        public ActionResult Ekle()
        {
          
                return View();

           
        }
        [AdminFilter]
        [HttpPost]
        public ActionResult Ekle(Kullanici model)
        {
            using (KullaniciRepository repo = new KullaniciRepository())
            {
                bool result = repo.Create(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Ürün eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Ürün eklenemedi." } };
                return RedirectToAction("Liste");
            }
        }


        [AdminFilter]
        public ActionResult Sil(int id, string path)
        {
            using (UrunRepository repo = new UrunRepository())
            {

                bool result = repo.Delete(id);
               
                    TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kullanıcı silindi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kullanıcı silinemedi"} };
                    return RedirectToAction("Liste");
               
              
            }
        }


        [AdminFilter]
        public ActionResult Guncelle(int id)
        {
            using (KullaniciRepository repo = new KullaniciRepository())
            {
                Kullanici model = repo.GetOne(f => f.KullaniciId == id);
                
                return View(model);
            }



        }

        [HttpPost]
        [AdminFilter]
        public ActionResult Guncelle(Kullanici model,string yeniSifre)
        {
            if (model.Sifre!=yeniSifre)
            {
                TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Şifre Hatalı" } };
                return RedirectToAction("Guncelle",new {id=model.KullaniciId });
            }
            else
            {
                using (KullaniciRepository repo = new KullaniciRepository())
                {

                    bool result = repo.Create(model);
                    TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kullanıcı Güncellendi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kullanıcı Güncellenemedi" } };
                    return RedirectToAction("Liste");
                }
            }
            



        }
    }
}