using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new AboutDao();
            var model = dao.ListAllPage(searchString, page, pageSize);
            ViewBag.SearchString = searchString;//de giu lai gia tri search, ViewBag gui gia tri tu controllor xuong view
            return View(model);
        }

        // GET: Admin/About/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/About/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/About/Create
        [HttpPost]
        public ActionResult Create(About about)
        {
            if (ModelState.IsValid)
            {
                var dao = new AboutDao();
                about.CreateDate = DateTime.Now;
                about.ModifiedDate = DateTime.Now;
                long id = dao.Insert(about);
                if (id > 0)
                {
                    SetAlert("Add user successfully", "success");
                    return RedirectToAction("Index", "About");
                }
                else
                {
                    SetAlert("Add user failure", "danger");
                    ModelState.AddModelError("", "Add failure");
                }
            }
            return View("Index");
        }

        // GET: Admin/About/Edit/5
        public ActionResult Edit(int id)
        {
            var dao = new AboutDao();
            var about = dao.GetByID(id);
            return View(about);
        }

        // POST: Admin/About/Edit/5
        [HttpPost]
        public ActionResult Edit(About about)
        {
            var dao = new AboutDao();
            var result = dao.Update(about);
            if (result)
            {
                SetAlert("Add user successfully", "success");
                return RedirectToAction("Index", "About");
            }
            else
            {
                SetAlert("Update user failure", "danger");
                ModelState.AddModelError("", "Update user failure");
            }
            return View("Index");
        }
            

        // GET: Admin/About/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/About/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var result = new AboutDao().DeleteByID(id);
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
            var result = new AboutDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
