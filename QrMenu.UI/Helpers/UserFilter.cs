using QrMenu.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QrMenu.UI.Constants;

namespace QrMenu.UI.Helpers
{
    public class UserFilter : FilterAttribute, IActionFilter
    {
        public int LoginSession { get; private set; }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session[Sessions.LoginSession] as Kullanici;
            if (session == null || session.Yetki==(int)EnumYetki.Isletme)
            {
                filterContext.Result = new RedirectResult("/Admin/Giris");
            }

        }
    }
}