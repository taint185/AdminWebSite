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

        //report
        public ActionResult XuatReport(String id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "Report.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View();
            }


            List<OrderDetail> cm = new List<OrderDetail>();


            using (DBWatchEntities db = new DBWatchEntities())
            {

                //var list = (from I in db.OrderDetail
                //            where I.Order.Customer.CusName == id
                //            orderby I.Order.Date
                //            select new { I.Order.OrderID, I.Product.ProName, I.Quantity, I.Price }).FirstOrDefault();


                // var a = list.ToString();
                cm = db.OrderDetail.ToList();
                ReportDataSource rd = new ReportDataSource("MyDataSet", cm);
                lr.DataSources.Add(rd);
                string reportType = id;
                string mineType;
                string encoding;
                string fileNameExtension;

                string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>" + id + "</OutputFormat>" +
                    "<PageWidth>8.5in</PageWidth>" +
                    "<PageHeight>11in</PageHeight>" +
                    "<MarginTop>0.5in</MarginTop>" +
                    "<MarginLeft>1in</MarginLeft>" +
                    "<MarginRight>1in</MarginRight>" +
                    "<MarginBottom>0.5in</MarginBottom>" +
                    "</DeviceInfo>"
                    ;
                Warning[] warnings;
                string[] streams;
                byte[] rendereBytes;

                rendereBytes = lr.Render
                    (
                        reportType,
                        deviceInfo,
                        out mineType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings
                    );
                return File(rendereBytes, mineType);
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
