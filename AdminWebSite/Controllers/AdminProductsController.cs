using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminWebSite.Models;

namespace AdminWebSite.Controllers
{
    public class AdminProductsController : Controller
    {
        private DBWatchEntities db = new DBWatchEntities();

        // GET: AdminProducts
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.Brand).Include(p => p.Categories);
            return View(product.ToList());
        }

        // GET: AdminProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: AdminProducts/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brand, "BrandID", "BrandName");
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "CateName");
            return View();
        }

        // POST: AdminProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase Image)
        {
            
            if (ModelState.IsValid)
            {
                product.Image = System.IO.Path.GetFileName(Image.FileName);
                db.Product.Add(product);
                db.SaveChanges();
                //Image.SaveAs("~/Content/images/hinh/"+product.Image);
                Image.SaveAs(Server.MapPath(Url.Content("~/Content/images/hinh/" + product.Image)));

                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(db.Brand, "BrandID", "BrandName", product.BrandID);
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "CateName", product.CateID);
            return View(product);
        }

        // GET: AdminProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brand, "BrandID", "BrandName", product.BrandID);
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "CateName", product.CateID);
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var model = db.Product.Find(product.ProID);
                string oldfilePath = model.Image;
                if (Image != null && Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/hinh/"), fileName);
                    Image.SaveAs(path);
                    model.Image =Image.FileName;
                    string fullPath = Request.MapPath(oldfilePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                 product.Image = model.Image;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Error = "Edit product information failed.";
            }
            ViewBag.BrandID = new SelectList(db.Brand, "BrandID", "BrandName", product.BrandID);
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "CateName", product.CateID);
            return View(product);
        }

        // GET: AdminProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
