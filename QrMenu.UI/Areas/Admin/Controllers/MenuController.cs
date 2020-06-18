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
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        [AdminFilter]
        public ActionResult Liste()
        {
            using (MenuRepository repo = new MenuRepository())
            {
                var model = repo.GetList();
                return View(model);
            }
        }


        [AdminFilter]
        public ActionResult Ekle()
        {
            using (UrunRepository repo = new UrunRepository())
            {
                ViewBag.UrunList = repo.GetList();

                return View();
            }
        }

        [AdminFilter]
        [HttpPost]
        public ActionResult Ekle(Menu model, HttpPostedFileBase[] images, int[] urunler)
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

            using (MenuRepository repo = new MenuRepository())
            {
                model.isAktive = true;
                model.MediaPath = saveResult;
                bool result = repo.Create(model);
                if (result)
                {
                    if (urunler != null)
                    {

                        using (MenuUrunRepository repo2 = new MenuUrunRepository())
                        {
                            List<MenuUrun> menuUrunModel = new List<MenuUrun>();
                            foreach (var item in urunler)
                            {
                                menuUrunModel.Add(new MenuUrun { MenuId = model.MenuId, UrunId = item, UrunMiktar = 1 });
                            }
                            bool result2 = repo2.CokluIcerikEkle(menuUrunModel);
                            if (!result2)
                            {
                                TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Menü içeriği eklenemedi. </br> Menü oluşturuldu fakat içerikler eklenirken bir hata oluştu menü listesinden içerik eklemeyi deneyin." } };
                            }
                        }
                    }
                }
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Menü eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Menü eklenemedi." } };
                return RedirectToAction("Liste");
            }
        }

        [AdminFilter]
        public ActionResult Sil(int id, string path)
        {
            using (MenuRepository repo = new MenuRepository())
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

        [AdminFilter]
        public ActionResult Guncelle(int id)
        {
            using (MenuRepository repo = new MenuRepository())
            {
                using (UrunRepository repo2 = new UrunRepository())
                {
                    ViewBag.UrunList = repo2.GetList();
                }
                var model = repo.GetOne(f => f.MenuId == id);
                return View(model);
            }
        }


        [AdminFilter]
        public ActionResult Guncelle(int number,Menu model, HttpPostedFileBase[] images, int[] urunler,string oldImage)
        {
            if (images==null)
            {
                model.MediaPath = oldImage;
            }
            else
            {
               string newImage = ImageSaver.SaveImage(images, model.Ad).FirstOrDefault();
                model.MediaPath = newImage;
            }
            using (MenuRepository repo = new MenuRepository())
            {
                model.MenuId = number;
                bool result = repo.Update(model);
                if (result)
                {
                    if (urunler != null)
                    {

                        using (MenuUrunRepository repo2 = new MenuUrunRepository())
                        {
                            
                            List<MenuUrun> menuUrunModel = new List<MenuUrun>();
                            foreach (var item in urunler)
                            {
                                menuUrunModel.Add(new MenuUrun { MenuId = model.MenuId, UrunId = item, UrunMiktar = 1 });
                            }
                            bool result2 = repo2.CokluIcerikGuncelle(menuUrunModel);
                            if (!result2)
                            {
                                TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Menü içeriği eklenemedi. </br> Menü oluşturuldu fakat içerikler eklenirken bir hata oluştu menü listesinden içerik eklemeyi deneyin." } };
                            }

                        }
                    }
                }
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Menü eklendi." } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Menü eklenemedi." } };
                return RedirectToAction("Liste");
            }
           
        }


        [AdminFilter]
        public ActionResult IcerikListe(int id)
        {
            using (MenuUrunRepository repo = new MenuUrunRepository())
            {
                var model = repo.MenuyeGoreIcerikGetir(id);
                if (model.Count()==0)
                {
                    TempData["Message"] = new TempDataDictionary { { "class", "alert alert-warning" }, { "msg", "Menüde ürün bulunmuyor" } };
                    return RedirectToAction("Liste");
                }
                
                    return View(model);

            }
        }

        [AdminFilter]
        public ActionResult IcerikSil(int id,int number)
        {
            using (MenuUrunRepository repo = new MenuUrunRepository())
            {
                var found = repo.GetOne(f => f.MenuId == number && f.UrunId == id);
                bool result = repo.Delete(found.MenuUrunId);
                if (repo.GetOne(f=>f.MenuId==found.MenuId)==null)
                {
                    return RedirectToAction("Liste");
                }
                return RedirectToAction("IcerikListe", new { id = found.MenuId });
            }
        }


        [AdminFilter]
        public ActionResult IcerikEkle()
        {
            using (KategoriRepository repo = new KategoriRepository())
            {
                ViewBag.KategoriList = repo.GetList();
            }
            return View();
        }
    }


}
