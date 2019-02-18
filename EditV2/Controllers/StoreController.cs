using EditV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EditV2.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult SIndex()
        {
            return View();
        }
        //get list of store
        public JsonResult GetStore()
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                //dbobj.Configuration.ProxyCreationEnabled = false;
                var s = dbobj.Stores.Select(x => new
                {
                    SId = x.SId,
                    StoreName = x.StoreName,
                    StoreAddress = x.StoreAddress,

                }).ToList();
                return Json(new { data = s }, JsonRequestBehavior.AllowGet);
            }
        }
        //to add new store
        [HttpPost]
        public ActionResult AddStore(Store s)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Stores.Add(s);
                dbobj.SaveChanges();
                return RedirectToAction("GetStore");
            }

        }
        //to get product by id
        public ActionResult GetStoreById(int id)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Configuration.ProxyCreationEnabled = false;
                var s = dbobj.Stores.Where(x => x.SId == id).FirstOrDefault();
                return Json(s, JsonRequestBehavior.AllowGet);
            }
        }
        //to update
        [HttpPost]
        public JsonResult UpdateStore(Store s)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                dbobj.Entry(s).State = EntityState.Modified;
                dbobj.SaveChanges();
                return Json(new { success = true, message = "saved" }, JsonRequestBehavior.AllowGet);
            }
        }
        // to delete
        [HttpPost]
        public ActionResult DeleteStore(int id)
        {
            using (ProductSoldEntities dbobj = new ProductSoldEntities())
            {
                var store = dbobj.Stores.Where(x => x.SId == id).FirstOrDefault();
                dbobj.Stores.Remove(store);
                dbobj.SaveChanges();
                return Json(new { success = true, message = "deleted" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}