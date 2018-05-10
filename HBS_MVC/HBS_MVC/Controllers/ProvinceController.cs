using HBS_MVC.DAL;
using HBS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HBS_MVC.Controllers
{
    public class ProvinceController : Controller
    {
        private HbsContext db = new HbsContext();

        // GET: Province
        public ActionResult Index()
        {
            if (Session["SKEY"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public JsonResult GetProvinces()
        {
            //var jsonData = new
            //{
            //    data = from p in db.Provinces.ToList()
            //           where p.ExpiredFlag == "N"
            //           select p
            //};
            var jsonData = new
            {
                data = db.Provinces.ToList()
                .Where(p => p.ExpiredFlag.Equals("N"))
                .Select(x => new { x.ProvinceID, x.Name, x.DateCreated, x.CreatedBy, x.DateModified, x.ModifiedBy, x.DateExpired, x.ExpiredFlag })
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            if (Session["SKEY"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,DateCreated,CreatedBy,ExpiredFlag")] Province province)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    province.Name = province.Name.ToUpper();
                    province.CreatedBy = Convert.ToInt32(Session["SKEY"].ToString());
                    province.DateCreated = DateTime.Now;
                    province.ExpiredFlag = "N";
                    db.Provinces.Add(province);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(province);
        }

        public ActionResult Details(int? id)
        {
            if (Session["SKEY"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Province province = db.Provinces.Find(id);
                if (province == null)
                {
                    return HttpNotFound();
                }
                return View(province);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (Session["SKEY"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Province province = db.Provinces.Find(id);
                if (province == null)
                {
                    return HttpNotFound();
                }
                return View(province);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProvince(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var provinceToUpdate = db.Provinces.Find(id);
            if (TryUpdateModel(provinceToUpdate, "",
               new string[] { "Name", "DateModified", "ModifiedBy", "ExpiredFlag" }))
            {
                try
                {
                    provinceToUpdate.Name = provinceToUpdate.Name.ToUpper();
                    provinceToUpdate.DateModified = DateTime.Now;
                    provinceToUpdate.ModifiedBy = Convert.ToInt32(Session["SKEY"].ToString());
                    provinceToUpdate.ExpiredFlag = "N";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(provinceToUpdate);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (Session["SKEY"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
                }
                Province province = db.Provinces.Find(id);
                if (province == null)
                {
                    return HttpNotFound();
                }
                return View(province);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var provinceToDelete = db.Provinces.Find(id);
            if (TryUpdateModel(provinceToDelete, "",
               new string[] { "Name", "DateModified", "ModifiedBy", "ExpiredFlag" }))
            {
                try
                {
                    provinceToDelete.DateExpired = DateTime.Now;
                    provinceToDelete.ModifiedBy = Convert.ToInt32(Session["SKEY"].ToString());
                    provinceToDelete.ExpiredFlag = "Y";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    return RedirectToAction("Delete", new { id = id, saveChangesError = true });
                }
            }
            return View(provinceToDelete);
            /*try
            {
                Province pro = db.Provinces.Find(id);
                db.Provinces.Remove(pro);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");*/
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