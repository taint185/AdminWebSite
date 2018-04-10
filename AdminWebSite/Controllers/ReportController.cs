using AdminWebSite.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

using WebSite.Models;

namespace AdminWebSite.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Reports()
        {
            using (DBWatchEntities db = new DBWatchEntities())
            {
                var orderDetails = db.OrderDetail.Include(o => o.Order).Include(o => o.Product);
                return View();
            }
        }

        public ActionResult ViewOrder(string cusName)
        {

            using (DBWatchEntities db = new DBWatchEntities())
            {
                var list = (from I in db.OrderDetail
                            where I.Order.Customer.CusName == cusName
                            orderby I.Order.Date
                            select new { I.Order.OrderID, I.Product.ProName, I.Quantity, I.Price }).ToList();
                //string query = "SELECT * FROM [Order] O JOIN OrderDetail I ON O.OrderID = I.OrderID JOIN Product P ON P.ProID = I.ProID JOIN Customer C ON C.CusName = O.CusName WHERE C.[CusName] ='" + cusName.ToString() + "' ORDER BY O.[Date]";
                //string query = "SELECT * FROM [Order] O Where O.CusName = '"+cusName+"'";

                //var historyOrder = db.Orders.SqlQuery(query).ToList();
                //ViewBag.list = list;
                return View(list);
            }

           
        }

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
    }
}