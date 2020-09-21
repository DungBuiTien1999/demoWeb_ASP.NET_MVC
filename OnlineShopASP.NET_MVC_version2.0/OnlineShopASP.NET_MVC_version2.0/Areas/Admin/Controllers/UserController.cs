using Models.Dao;
using Models.EF;
using OnlineShopASP.NET_MVC_version2._0.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPage(searchString, page, pageSize);
            ViewBag.SearchString = searchString;//de giu lai gia tri search, ViewBag gui gia tri tu controllor xuong view
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                user.Password = Encryptor.MD5Hash(user.Password);
                user.CreateDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                long id = dao.Insert(user);
                if(id > 0)
                {
                    SetAlert("Add user successfully", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Add user failure", "danger");
                    ModelState.AddModelError("", "Add failure");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new UserDao();
            var user = dao.GetByID(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //user.Password = Encryptor.MD5Hash(user.Password);
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Add user successfully", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Update user failure", "danger");
                    ModelState.AddModelError("", "Update user failure");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new UserDao().DeleteByID(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }
            
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}