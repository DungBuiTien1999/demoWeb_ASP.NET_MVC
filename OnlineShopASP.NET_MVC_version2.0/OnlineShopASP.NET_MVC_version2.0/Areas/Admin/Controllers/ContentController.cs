using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace OnlineShopASP.NET_MVC_version2._0.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPage(searchString, page, pageSize);
            ViewBag.SearchString = searchString;//de giu lai gia tri search, ViewBag gui gia tri tu controllor xuong view
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                long id = dao.Insert(model);
                if(id > 0)
                    return RedirectToAction("Index", "Content");
                else
                    ModelState.AddModelError("", "Add failure");
            }

            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);
            SetViewBag(content.CategoryID);
            return View(content);
        }
        [HttpPost]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                var result = dao.Update(model);
                if (result)
                {
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Update user failure");
                }
            }

            SetViewBag(model.CategoryID);
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new ContentDao().DeleteByID(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }

        }

        public void SetViewBag(long? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
    }
}