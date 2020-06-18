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
        public ActionResult Liste()
        {
            using (KategoriRepository repo = new KategoriRepository())
            {
                var model = repo.GetList();
                return View(model);
            }
            
        }

        [AdminFilter]
        public ActionResult Ekle()
        {
            return View();
        }

        [AdminFilter]
        [HttpPost]
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
        public ActionResult Sil(int id,string path)
        {
            using (KategoriRepository repo = new KategoriRepository())
            {

                bool result=repo.Delete(id);
                if (result)
                {
                    var message=ImageSaver.DeleteImage(path);
                    TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kategori silindi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategor silinemedi. </br>" +message } };
                    return RedirectToAction("Liste");
                }
                TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategori silinemedi. </br>" } };
                return RedirectToAction("Liste");
            }
        }

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
        [HttpPost]
        public ActionResult Guncelle(Kategori model,int number,string OldMedia)
        {
            if (model.MediaPath==null)
            {
                model.MediaPath = OldMedia;
            }
            using (KategoriRepository repo = new KategoriRepository())
            {
                model.KategoriId = number;
                bool result= repo.Update(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Kategroi eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Kategroi eklenemedi." } };
                return RedirectToAction("Liste");
            }

        }

    }
}