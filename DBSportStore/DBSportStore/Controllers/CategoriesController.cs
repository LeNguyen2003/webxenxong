using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBSportStore.Models;
namespace DBSportStore.Controllers
{
    public class CategoriesController : Controller
    {
        DBSportStoreEntities database = new DBSportStoreEntities();
        // GET: Categories
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(database.Categories.ToList());
            else
                return View(database.Categories.Where(s => s.NameCate.Contains(_name)).ToList());

        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                database.Categories.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Trùng khóa");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = database.Categories.Where(s => s.Id == id).FirstOrDefault();
                database.Categories.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using orther table, Error Delete! ");
            }

        }
        // Action PartialViewResult
        [ChildActionOnly]
        public PartialViewResult CategoryPartial()
        {
            var cateList = database.Categories.ToList();
            return PartialView(cateList);
        }

    }
}

