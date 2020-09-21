using Models.Dao;
using OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Models;
using OnlineShopASP.NET_MVC_version2._0.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByUsername(model.Username);
                    var userSession = new UserLogin();
                    userSession.UserID = user.ID;
                    userSession.Username = user.Username;

                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account do not exist");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Account has locked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Password wrong");
                }
                else
                {
                    ModelState.AddModelError("", "Login failure");
                }
            }
            return View("Index");
        }
    }
}