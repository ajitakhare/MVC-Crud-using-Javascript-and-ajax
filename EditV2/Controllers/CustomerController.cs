using EditV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EditV2.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Customer
        public ActionResult CIndex()
        {
            return View();
        }
        //list of all customers
        public JsonResult GetCustomer()
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                //dbobj.Configuration.ProxyCreationEnabled = false;
                var c = dbobj.Customers.Select(x => new
                {
                    CId = x.CId,
                    CustomerName = x.CustomerName,
                    CustomerAddress = x.CustomerAddress,

                }).ToList();
                return Json(new { data = c }, JsonRequestBehavior.AllowGet);
            }
        }
        //to add customer
        [HttpPost]
        public ActionResult AddCustomer(Customer c)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Customers.Add(c);
                dbobj.SaveChanges();
                return RedirectToAction("GetCustomer");
            }

        }
        //to get customer by id
        public ActionResult GetCustomerById(int id)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Configuration.ProxyCreationEnabled = false;
                var c = dbobj.Customers.Where(x => x.CId == id).FirstOrDefault();
                return Json(c, JsonRequestBehavior.AllowGet);
            }
        }
        //to update
        [HttpPost]
        public JsonResult UpdateCustomer(Customer c)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Entry(c).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json(new { success = true, message = "saved" }, JsonRequestBehavior.AllowGet);
            }
        }
        // to delete
        [HttpPost]
        public ActionResult DeleteCustomer(int id)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                var customer = dbobj.Customers.Where(x => x.CId == id).FirstOrDefault();
                dbobj.Customers.Remove(customer);
                dbobj.SaveChanges();
                return Json(new { success = true, message = "deleted" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}