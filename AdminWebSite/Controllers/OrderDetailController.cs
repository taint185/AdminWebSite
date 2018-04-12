using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminWebSite.Models;
using Microsoft.Reporting.WebForms;


namespace AdminWebSite.Controllers
{
    public class OrderDetailController : Controller
    {
        private DBWatchEntities db = new DBWatchEntities();
        // GET: OrderDetail
        public ActionResult Reports()
        {
            var orderDetail = db.OrderDetail.Include(o => o.Order).Include(o => o.Product);
            return View(orderDetail.ToList());
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
