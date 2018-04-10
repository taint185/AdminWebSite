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
    public class AdminBrandsController : Controller
    {
        private DBWatchEntities db = new DBWatchEntities();

        // GET: AdminBrands
        public ActionResult Index()
        {
            return View(db.Brand.ToList());
        }

        // GET: AdminBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: AdminBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminBrands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {

            var br = (from Brand in db.Brand where Brand.BrandName == brand.BrandName.ToString() select new { Brand.BrandID}).FirstOrDefault();
            if (br != null)
            {
                ViewBag.Error = "This brand already exists.";
                return View(brand);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Brand.Add(brand);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(brand);
        }

        // GET: AdminBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: AdminBrands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            var br = (from Brand in db.Brand where Brand.BrandName == brand.BrandName.ToString() select new { Brand.BrandID }).FirstOrDefault();
            if (br != null)
            {
                ViewBag.Error = "This brand already exists.";
                return View(brand);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
              
            return View(brand);
        }

        // GET: AdminBrands/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: AdminBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var query = "SELECT * FROM Product P Where P.BrandID =" + id + "";
            var a = db.Product.SqlQuery(query).ToList();
            if (a.Count() >0)
            {
                ViewBag.Error = "This brand is being used in the product. You can not delete. ";
                Brand brand = db.Brand.Find(id);
                return View(brand);
            }
            else
            {
                Brand brand = db.Brand.Find(id);
                db.Brand.Remove(brand);
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
