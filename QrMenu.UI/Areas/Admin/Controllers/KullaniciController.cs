using QrMenu.BLL;

using QrMenu.MODEL;
using QrMenu.UI.Constants;
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
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "İşletme eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "İşletme eklenemedi." } };
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
               
                    TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "İşletme silindi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "İşletme silinemedi"} };
                    return RedirectToAction("Liste");
               
              
            }
        }

        
        //public ActionResult IsletmeSifreGuncelle()
        //{
        //    Kullanici kullanici = Session[Sessions.LoginSession] as Kullanici;

           

        //}


        
        [AdminFilter]
        public ActionResult Guncelle(int? id)
        {
            Kullanici kullanici = Session[Sessions.LoginSession] as Kullanici;
            if (kullanici.Yetki==(int)EnumYetki.Admin)
            {
                using (KullaniciRepository repo = new KullaniciRepository())
                {
                    Kullanici model = repo.GetOne(f => f.KullaniciId == id);

                    return View(model);
                }
            }
            else
            {
                using (KullaniciRepository repo = new KullaniciRepository())
                {
                    Kullanici model = repo.GetOne(f => f.KullaniciId == kullanici.KullaniciId);

                    return View(model);
                }
            }

            //return RedirectToAction("Index", "Main");



        }

        [HttpPost]
       
        [AdminFilter]
        public ActionResult Guncelle(Kullanici model,string yeniSifre,int number,int number2)
        {
            Kullanici kullanici = Session[Sessions.LoginSession] as Kullanici;
            
                using (KullaniciRepository repo = new KullaniciRepository())
                {
                    model.Sifre = yeniSifre;
                    model.KullaniciId = number;
                   
               
                    if (model.KullaniciId==kullanici.KullaniciId)
                    {
                        kullanici.Sifre = yeniSifre;

                        Session[Sessions.LoginSession] = kullanici;
                }
                if (number2!=(int)EnumYetki.Admin)
                {
                    model.Yetki = (int)EnumYetki.Isletme;
                }
                bool result = repo.Update(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "İşletme Güncellendi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "İşletme Güncellenemedi" } };
                  
                    return RedirectToAction("Liste");
                }
            
            



        }
    }
}