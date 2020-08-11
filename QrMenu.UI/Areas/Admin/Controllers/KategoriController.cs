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
    public class KategoriController : Controller
    {
        // GET: Admin/Kategori
        [AdminFilter]
        [UserFilter]

        public ActionResult Liste()
        {
            using (KategoriRepository repo = new KategoriRepository())
            {
                var model = repo.GetList();
                return View(model);
            }
            
        }

        [AdminFilter]
        [UserFilter]

        public ActionResult Ekle()
        {
            return View();
        }

        [AdminFilter]
        [HttpPost]
        [UserFilter]

        public ActionResult Ekle(Kategori model,HttpPostedFileBase[] images)
        {
            if (images[0]!=null)
            {
                var saveResult= ImageSaver.SaveImage(images, model.Ad).FirstOrDefault();

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
            
            using (KategoriRepository repo = new KategoriRepository())
            {
                model.IsAktif = true;
                bool result = repo.Create(model);
                TempData["Message"] = result==true? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kategroi eklendi." } }: new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategroi eklenemedi." } };
                return RedirectToAction("Liste");
            }
        }

        [AdminFilter]
        [UserFilter]

        public ActionResult Sil(int id,string path)
        {
            using (KategoriRepository repo = new KategoriRepository())
            {
                bool result;
                try
                {
                    result = repo.Delete(id);
                }
                catch (Exception e)
                {
                    TempData["Message"]  = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategor silinemedi. Kategoriye Bağlı Ürün Olabilir."  } };
                    result = false;

                }
                if (result)
                {
                    var message=ImageSaver.DeleteImage(path);
                    TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kategori silindi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategor silinemedi." +message } };
                    return RedirectToAction("Liste");
                }
                
                return RedirectToAction("Liste");
            }
        }

        [UserFilter]
        [AdminFilter]
        public ActionResult Guncelle(int id)
        {
            using (KategoriRepository repo = new KategoriRepository())
            {
                Kategori model = repo.GetOne(f => f.KategoriId == id);
                return View(model);
            }
            
        }

        [AdminFilter]
        [UserFilter]
        [HttpPost]
        public ActionResult Guncelle(Kategori model,int number,string OldMedia,HttpPostedFileBase[] image)
        {
            if (image[0]==null)
            {
                model.MediaPath = OldMedia;
            }
            else
            {
                var saveResult = ImageSaver.SaveImage(image, model.Ad).FirstOrDefault();
                model.MediaPath = saveResult;
            }
            using (KategoriRepository repo = new KategoriRepository())
            {
                model.KategoriId = number;
                bool result= repo.Update(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kategori eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategori eklenemedi." } };
                return RedirectToAction("Liste");
            }

        }

    }
}