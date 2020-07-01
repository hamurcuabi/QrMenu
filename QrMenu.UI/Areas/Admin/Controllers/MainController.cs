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
    [RouteArea("Admin")]
    [Route("{action=AdminIndex}")]
    public class MainController : Controller
    {
        // GET: Admin/Main
        [Route]
        [Route("anasayfa")]
        [AdminFilter]
        public ActionResult Index()
        {
            Kullanici user = Session["loginSU"] as Kullanici;
            KategoriMenuUrunKullaniciModel model = new KategoriMenuUrunKullaniciModel();
            if (user.Yetki==(int)EnumYetki.Admin)
            {
                using (KategoriRepository repo = new KategoriRepository())
                {
                    model.KategoriList = repo.GetList();
                }
                using (MenuRepository repo = new MenuRepository())
                {
                    model.MenuList = repo.GetList();
                }
                using (UrunRepository repo = new UrunRepository())
                {
                    model.UrunList = repo.GetList();
                }
                using (KullaniciRepository repo = new KullaniciRepository())
                {
                    model.KullaniciList = repo.GetList();
                }
                return View(model);
            }
            else if (user.Yetki==(int)EnumYetki.Isletme)
            {
                using (IsletmeUrunRepository repo = new IsletmeUrunRepository())
                {
                    ViewBag.IsletmeUrunList = repo.IsletmeninUrunleri(user.KullaniciId);
                }
                
            }
            return View();
        }

       
        [Route("Giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Giris")]
        public ActionResult Login(Kullanici model)
        {
            using (KullaniciRepository repo = new KullaniciRepository())
            {
                Kullanici user = repo.CheckUser(model);
                if (user != null)
                {
                    Session["loginSU"] = user;
                    return RedirectToAction("Index");
                }
                TempData["Message"] = new TempDataDictionary { { "class", "alert alert-danger" }, { "msg", "Hatalı kullanıcı Adı/Şifre" } };
                return RedirectToAction("Login");
            }
        }

        [AdminFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}