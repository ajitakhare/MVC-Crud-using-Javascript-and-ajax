using EditV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EditV2.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult PIndex()
        {
            return View();
        }
        //get list of product
        public JsonResult GetProduct()
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                // dbobj.Configuration.ProxyCreationEnabled = false;
                var p = dbobj.Products.Select(x => new
                {
                    PId = x.PId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,

                }).ToList();
                return Json(new { data = p }, JsonRequestBehavior.AllowGet);
            }
        }
        //to add new product
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Products.Add(p);
                dbobj.SaveChanges();
                return RedirectToAction("GetProduct");
            }

        }
        //to get product by id
        public ActionResult GetProductById(int id)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Configuration.ProxyCreationEnabled = false;
                var p = dbobj.Products.Where(x => x.PId == id).FirstOrDefault();
                return Json(p, JsonRequestBehavior.AllowGet);
            }
        }
        //to update
        [HttpPost]
        public JsonResult UpdateProduct(Product p)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Entry(p).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json(new { success = true, message = "saved" }, JsonRequestBehavior.AllowGet);
            }
        }
        // to delete
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                var product = dbobj.Products.Where(x => x.PId == id).FirstOrDefault();
                dbobj.Products.Remove(product);
                dbobj.SaveChanges();
                return Json(new { success = true, message = "deleted" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}