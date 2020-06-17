
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QrMenu.UI.Helpers
{
    public class AdminFilter : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session["loginSU"];
            if (session == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Giris");
            }

        }
    }
}