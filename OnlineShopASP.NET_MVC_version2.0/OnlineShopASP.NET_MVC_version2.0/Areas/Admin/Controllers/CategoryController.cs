using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var model = new CategoryDao().ListAllPage(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                long id = dao.Insert(model);
                if (id > 0)
                {
                    SetAlert("Add category successfully", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Add category failure", "danger");
                    ModelState.AddModelError("", "Add failure");
                }
            }
            return View("Index");
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CategoryDao().GetByID(id);
            return View(model);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            var dao = new CategoryDao();
            var result = dao.Update(model);
            if (result)
            {
                SetAlert("Add category successfully", "success");
                return RedirectToAction("Index", "Category");
            }
            else
            {
                SetAlert("Update category failure", "danger");
                ModelState.AddModelError("", "Update category failure");
            }
            return View("Index");
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Category/Delete/5
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
            var result = new CategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
