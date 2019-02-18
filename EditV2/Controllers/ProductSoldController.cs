using EditV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EditV2.Controllers
{
    public class ProductSoldViewModel
    {

        public int PSId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

    }
    public class ProductSoldController : Controller
    {
        // GET: ProductSold
        public ActionResult PSIndex()
        {
            ProductSoldEntities dbobj = new ProductSoldEntities() ;
            var psold = new ProductSold();
            ViewBag.CustomerId = new SelectList(dbobj.Customers, "CId", "CustomerName", psold.CustomerId);
            ViewBag.ProductId = new SelectList(dbobj.Products, "PId", "ProductName", psold.ProductId);
            ViewBag.StoreId = new SelectList(dbobj.Stores, "SId", "StoreName", psold.StoreId);
            return View();
        }
        // to get list 
        public JsonResult GetProductSold()
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                var productsold = dbobj.ProductSolds.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Store).Select(x => new
                {
                    PSId = x.PSId,
                    ProductId = x.Product.ProductName,
                    CustomerId = x.Customer.CustomerName,
                    StoreId = x.Store.StoreName

                }).ToList();

                return Json(productsold, JsonRequestBehavior.AllowGet);

            }
        }
        // to add
        public JsonResult Add(ProductSold psold)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.ProductSolds.Add(psold);

                return Json(dbobj.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetbyID(int id)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                var ProductSold = dbobj.ProductSolds.Where(x => x.PSId == id).Select(x => new ProductSoldViewModel
                {
                    PSId = x.PSId,
                    CustomerId = x.CustomerId,
                    StoreId = x.StoreId,
                    ProductId = x.ProductId
                }).FirstOrDefault();
                return Json(ProductSold, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Update(ProductSold psold)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Entry(psold).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json(dbobj.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RemoveProduct(int ID)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                ProductSold psold = dbobj.ProductSolds.Find(ID);
                dbobj.ProductSolds.Remove(psold);
                return Json(dbobj.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}