using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminWebSite.Models;

namespace AdminWebSite.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private DBWatchEntities db = new DBWatchEntities();

        // GET: AdminCategories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: AdminCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: AdminCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories)
        {
            var br = (from Categories in db.Categories where Categories.CateName == categories.CateName.ToString() select new { Categories.CateID }).FirstOrDefault();
            if (br != null)
            {
                ViewBag.Error = "This category already exists.";
                return View(categories);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(categories);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
                

            return View(categories);
        }

        // GET: AdminCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: AdminCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            var br = (from Categories in db.Categories where Categories.CateName == categories.CateName.ToString() select new { Categories.CateID }).FirstOrDefault();
            if (br != null)
            {
                ViewBag.Error = "This category already exists.";
                return View(categories);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(categories).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
                
            return View(categories);
        }

        // GET: AdminCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var query = "SELECT * FROM Product P Where P.CateID =" + id + "";
            var a = db.Product.SqlQuery(query).ToList();
            if (a.Count() > 0)
            {

                ViewBag.Error = "This category is being used in the product. You can not delete. ";
                Categories categories = db.Categories.Find(id);
                return View(categories);
            }
            else
            {
                Categories categories = db.Categories.Find(id);
                db.Categories.Remove(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
