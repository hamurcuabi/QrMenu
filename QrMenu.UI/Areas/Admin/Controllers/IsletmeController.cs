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
    public class IsletmeController : Controller
    {
        // GET: Admin/Isletme
        [AdminFilter]
        public ActionResult Ayar()
        {
            using (IsletmeAyarRepository repo = new IsletmeAyarRepository())
            {
                IsletmeAyar ayar = repo.GetOne(f => f.isActive == true);
                return View(ayar);
            }

        }

        [HttpPost]
        [AdminFilter]
        public ActionResult Ayar(HttpPostedFileBase[] images, IsletmeAyar model)
        {
            if (images[0] != null)
            {
                var saveResult = ImageSaver.SaveImage(images, model.Ad).FirstOrDefault();
                if (!saveResult.Contains("Dosya Kaydedilemedi"))
                {
                    model.LogoUri = saveResult;
                }
                else
                {
                    TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", $"Görsel Kaydedilemedi </br>{saveResult[1]} " } };
                    return RedirectToAction("Ekle");
                }

            }

            using (IsletmeAyarRepository repo = new IsletmeAyarRepository())
            {
                model.isActive = true;
                bool result = repo.Create(model);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Ayar eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Ayar eklenemedi." } };
                return RedirectToAction("Liste");
            }
        }
    }
}
