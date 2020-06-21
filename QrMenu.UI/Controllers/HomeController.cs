using QrMenu.BLL;
using QrMenu.MODEL;
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
            List<Kategori> kategoriList =new List<Kategori>();
            using (IsletmeUrunRepository repo = new IsletmeUrunRepository())
            {
                var model = repo.IsletmeninUrunleri(id);
                using (KategoriRepository repo2=new KategoriRepository())
                {
                    //kategoriList = repo2.GetList();
                    foreach (var item in model)
                    {
                        var deneme = repo2.GetOne(f => f.KategoriId == item.Urun.KategoriId);
                        if (!kategoriList.Contains(deneme))
                        {
                            kategoriList.Add(deneme);

                        }
                    }
                }
                ViewBag.KategoriList = kategoriList;
                return View(model);
            }
            
        }

        public ActionResult Landing()
        {
            return View();
        }
    }
}