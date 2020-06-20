using QrMenu.BLL;
using QrMenu.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QrMenu.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int id)
        {
            //KategoriMenuUrunModel allList = new KategoriMenuUrunModel();
            //using (UrunRepository repo = new UrunRepository())
            //{
            //    allList.UrunList = repo.GetList();

            //}
            //using (KategoriRepository repo = new KategoriRepository())
            //{
            //    allList.KategoriList = repo.GetList();
            //}
            //using (MenuRepository repo = new MenuRepository())
            //{
            //    allList.MenuList = repo.GetList();
            //}
            //using (MenuUrunRepository repo = new MenuUrunRepository())
            //{
            //    ViewBag.MenuUrunList = repo.GetList();
            //}
            //using (IsletmeAyarRepository repo = new IsletmeAyarRepository())
            //{
            //    ViewBag.IsletmeAyar = repo.GetOne(f => f.isActive == true);
            //}


            return View();
        }

        public ActionResult Landing()
        {
            return View();
        }
    }
}