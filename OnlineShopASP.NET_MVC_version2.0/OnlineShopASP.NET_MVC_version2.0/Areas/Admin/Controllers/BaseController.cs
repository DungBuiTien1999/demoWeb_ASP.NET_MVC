using OnlineShopASP.NET_MVC_version2._0.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index", Areas = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMassage"] = message;
            if(type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }else if(type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }else if(type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}