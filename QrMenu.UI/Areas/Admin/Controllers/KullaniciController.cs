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
        [UserFilter]

        public ActionResult Liste()
        {
            using (KullaniciRepository repo = new KullaniciRepository())
            {
                return View(repo.GetList());

            }
        }


        [UserFilter]
        [AdminFilter]
        public ActionResult Ekle()
        {
          
                return View();

           
        }
        [AdminFilter]
        [UserFilter]
        [HttpPost]
        public ActionResult Ekle(Kullanici model)
        {
            using (KullaniciRepository repo = new KullaniciRepository())
            {
                
                model.Sifre = Guid.NewGuid().ToString();
                bool result = repo.Create(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kullanıcı eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kullanıcı eklenemedi." } };
                return RedirectToAction("Liste");
            }
        }


        [UserFilter]
        [AdminFilter]
        public ActionResult Sil(int id, string path)
        {
            using (KullaniciRepository repo = new KullaniciRepository())
            {
               
                bool result = repo.Delete(id);
               
                    TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kullanıcı silindi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kullanıcı silinemedi"} };
                    return RedirectToAction("Liste");
               
              
            }
        }


        
        [AdminFilter]
        public ActionResult Guncelle(int id)
        {
            Kullanici kullanici = Session["loginSU"] as Kullanici;
            if (id==kullanici.KullaniciId)
            {
                using (KullaniciRepository repo = new KullaniciRepository())
                {
                    Kullanici model = repo.GetOne(f => f.KullaniciId == id);

                    return View(model);
                }
            }
            return RedirectToAction("Index", "Main");



        }

        [HttpPost]
        [AdminFilter]
        public ActionResult Guncelle(Kullanici model,string yeniSifre)
        {
            Kullanici kullanici = Session["loginSU"] as Kullanici;
            if (model.Sifre != kullanici.Sifre)
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