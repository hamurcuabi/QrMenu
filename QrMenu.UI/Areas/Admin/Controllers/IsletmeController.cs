using QrMenu.BLL;
using QrMenu.MODEL;
using QrMenu.UI.Areas.Admin.Models;
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
        [UserFilter]
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
        [UserFilter]

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

        [AdminFilter]
        [UserFilter]
        public ActionResult Liste()
        {
            using (IsletmeAyarRepository repo = new IsletmeAyarRepository())
            {
                var model = repo.GetList();
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult ToggleActivity(int id)
        {
            using (IsletmeAyarRepository repo = new IsletmeAyarRepository())
            {
                return Json(new { model = repo.Toggle(id) });
            }
        }

        [AdminFilter]
        public ActionResult IsletmeUrunListe(int? id)
        {
            if (id == null)
            {
                Kullanici kullanici = Session["loginSU"] as Kullanici;
                using (IsletmeUrunRepository repo = new IsletmeUrunRepository())
                {
                    ICollection<IsletmeUrun> model = repo.IsletmeninUrunleri(kullanici.KullaniciId);
                    return View(model);
                }
            }
            else
            {
                using (IsletmeUrunRepository repo = new IsletmeUrunRepository())
                {
                    ICollection<IsletmeUrun> model = repo.IsletmeninUrunleri((int)id);
                    return View(model);
                }


            }
        }

        
        [AdminFilter]
        public ActionResult Guncelle()
        {
            Kullanici kullanici = Session["loginSU"] as Kullanici;

            using (KullaniciRepository repo = new KullaniciRepository())
            {
                Kullanici model = repo.GetOne(f => f.KullaniciId == kullanici.KullaniciId);

                return View(model);
            }

        }


        [HttpPost]

        [AdminFilter]
        public ActionResult Guncelle(Kullanici model, string yeniSifre)
        {
            Kullanici kullanici = Session["loginSU"] as Kullanici;

            using (KullaniciRepository repo = new KullaniciRepository())
            {
                model.KullaniciAdi = kullanici.KullaniciAdi;
                model.Sifre = yeniSifre;
                model.KullaniciId = kullanici.KullaniciId;
                model.Yetki = kullanici.Yetki;

                bool result = repo.Update(model);
               
                    kullanici.Sifre = yeniSifre;

                    Session["loginSu"] = kullanici;
                

                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "İşletme Güncellendi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "İşletme Güncellenemedi" } };
               
                    return RedirectToAction("Index", "Main");
                
                
            }





        }





        [AdminFilter]
        public ActionResult IsletmeUrunSil(int id)
        {
            Kullanici kullanici = Session["loginSU"] as Kullanici;
            using (IsletmeUrunRepository repo = new IsletmeUrunRepository())
            {
                bool result = repo.Delete(id);
                TempData["Message"] = result == true ? new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Ürün Silindi" } } : new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Ürün Silinemedi" } };
                return RedirectToAction("IsletmeUrunListe");
            }
        }

        [AdminFilter]
        public ActionResult IsletmeUrunEkle()
        {
            Kullanici kullanici = Session["loginSU"] as Kullanici;
            using (UrunRepository repo = new UrunRepository())
            {
                var model = repo.GetList();
                return View(model);
            }
        }


        //[AdminFilter]
        [HttpPost]
        [AdminFilter]
        public void IsletmeUrunEkle(/*int[] model,string[] obj*/ string model)
        {
            IsletmeUrunEkleModel b = (IsletmeUrunEkleModel)Newtonsoft.Json.JsonConvert.DeserializeObject(model, typeof(IsletmeUrunEkleModel));
            Kullanici kullanici = Session["loginSU"] as Kullanici;
            //List<int> eklenecekler = new List<int>();
            using (IsletmeUrunRepository repo = new IsletmeUrunRepository())
            {
                var olanlar = repo.GetList(f => f.KullaniciId == kullanici.KullaniciId);
                foreach (var item in b.IsletmeUrunEkleModelList)
                {
                    if (olanlar.Any(f => f.UrunId == item.Id))
                    {
                        b.IsletmeUrunEkleModelList.Remove(item);
                    }
                }
                List<int> result = repo.UrunleriEkle(b, kullanici.KullaniciId);
                if (result.Count() == 0)
                {
                    TempData["Message"] = new TempDataDictionary { { "class", "alert alert-success" }, { "msg", "Ürünler eklendi." } };
                }
                else
                {
                    List<Urun> eklenemeyenUrunler = new List<Urun>();
                    using (UrunRepository repo2 = new UrunRepository())
                    {
                        foreach (var item in result)
                        {
                            eklenemeyenUrunler = repo2.GetList(f => f.UrunId == item).ToList();
                        }

                    }
                    TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", $"{result.Count()} ürün eklenemedi" } };
                    ViewBag.EklenemeyenUrunler = eklenemeyenUrunler;
                }
                //return RedirectToAction("IsletmeUrunEkle");
            }
        }
    }

}