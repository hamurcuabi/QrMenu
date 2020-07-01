

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
    public class UrunController : Controller
    {
        // GET: Admin/Urun
        [UserFilter]
        [AdminFilter]
        public ActionResult Liste()
        {
            using (UrunRepository repo = new UrunRepository())
            {
                var model = repo.GetList();
                return View(model);
            }
            
        }

       
        [HttpPost]
        public JsonResult ListByCategory(int id)
        {
            using (UrunRepository repo = new UrunRepository())
            {
                
                List<Urun> model = repo.GetList(f => f.KategoriId == id).ToList() ;
                
                return Json(model,JsonRequestBehavior.AllowGet);
            }
        }


        [UserFilter]
        [AdminFilter]
        public ActionResult Ekle()
        {
            using (KategoriRepository repo = new KategoriRepository())
            {
                ViewBag.KategoryList = repo.GetList();
                return View();

            }
        }



        [AdminFilter]
        [UserFilter]
        [HttpPost]
        public ActionResult Ekle(Urun model, HttpPostedFileBase[] images)
        {
            if (images != null)
            {
                var saveResult = ImageSaver.SaveImage(images, model.Ad).FirstOrDefault();

                if (!saveResult.Contains("Dosya Kaydedilemedi"))
                {
                    model.MediaPath = saveResult;
                }
                else
                {
                    TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", $"Görsel Kaydedilemedi </br>{saveResult[1]} " } };
                    return RedirectToAction("Ekle");
                }

            }

            using (UrunRepository repo = new UrunRepository())
            {
                model.IsAktif = true;
                bool result = repo.Create(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Ürün eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Ürün eklenemedi." } };
                return RedirectToAction("Liste");
            }
        }


        [UserFilter]
        [AdminFilter]
        public ActionResult Sil(int id, string path)
        {
            using (UrunRepository repo = new UrunRepository())
            {

                bool result = repo.Delete(id);
                if (result)
                {
                    var message = ImageSaver.DeleteImage(path);
                    TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Ürün silindi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Ürün silinemedi. </br>" + message } };
                    return RedirectToAction("Liste");
                }
                TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Ürün silinemedi. </br>" } };
                return RedirectToAction("Liste");
            }
        }



        [UserFilter]
        [AdminFilter]
        public ActionResult Guncelle(int id)
        {
            using (UrunRepository repo = new UrunRepository())
            {
                Urun model = repo.GetOne(f => f.UrunId == id);
                using (KategoriRepository repo2 = new KategoriRepository())
                {
                    ViewBag.KategoriList = repo2.GetList();
                }
                return View(model);
            }
          
          

        }

        [UserFilter]
        [AdminFilter]
        [HttpPost]
        public ActionResult Guncelle(Urun model, int number, string OldMedia,HttpPostedFileBase[] media)
        {
            model.UrunId = number;
            if (media == null)
            {
                model.MediaPath = OldMedia;
            }
            //ImageSaver.DeleteImage(OldMedia);
            var saveResult = ImageSaver.SaveImage(media, model.Ad).Last();
            if (!saveResult.Contains("Dosya Kaydedilemedi"))
            {
                model.MediaPath = saveResult;
            }
            else
            {
                TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", $"Görsel Kaydedilemedi </br>{saveResult[1]} " } };
                return RedirectToAction("Ekle");
            }
            using (UrunRepository repo = new UrunRepository())
            {
                //model.KategoriId = number;
                bool result = repo.Update(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kategroi eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategroi eklenemedi." } };
                return RedirectToAction("Liste");
            }

        }
    }
}