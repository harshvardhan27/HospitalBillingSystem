using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HBS_MVC.DAL;
using HBS_MVC.Models;

namespace HBS_MVC.Controllers
{
    public class DoctorController : Controller
    {
        private HbsContext db = new HbsContext();

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
        public JsonResult GetDoctors()
        {
            var jsonData = new
            {
                data = db.Doctors.ToList()
               .Where(d => d.ExpiredFlag.Equals("N"))
               .Select(x => new { x.DoctorID, x.Firstname, x.Lastname, x.FullName, x.Contactno, x.Email, x.Dob, x.Appartment, x.City, x.Country, x.Postalcode, x.DateCreated, x.CreatedBy, x.DateModified, x.ModifiedBy, x.DateExpired, x.ExpiredFlag })
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Details(int? id)
        {
            if (Session["SKEY"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Doctor doctor = db.Doctors.Find(id);
                if (doctor == null)
                {
                    return HttpNotFound();
                }
                return View(doctor);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Create()
        {
            if (Session["SKEY"] != null)
            {
                ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "Name");
                //var data = new SelectList(db.Provinces, "ProvinceID", "Name");
                //ViewData["DBProvince"] = data;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Firstname,Lastname,Appartment,Street,City,Postalcode,Country,Dob,Email,Contactno,ProvinceID,CreatedBy,DateCreated,ExpiredFlag")] Doctor doctor, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int selectedProvince = Convert.ToInt32(form["ProvinceID"].ToString());
                    doctor.Firstname = doctor.Firstname.ToUpper();
                    doctor.Lastname = doctor.Lastname.ToUpper();
                    doctor.Appartment = doctor.Appartment;
                    doctor.Street = doctor.Street;
                    doctor.City = doctor.City;
                    doctor.Postalcode = doctor.Postalcode;
                    doctor.Country = doctor.Country;
                    doctor.Dob = doctor.Dob;
                    doctor.Email = doctor.Email;
                    doctor.Contactno = doctor.Contactno;
                    doctor.ProvinceID = selectedProvince;
                    doctor.CreatedBy = Convert.ToInt32(Session["SKEY"].ToString());
                    doctor.DateCreated = DateTime.Now;
                    doctor.ExpiredFlag = "N";
                    db.Doctors.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "Name");
            return View(doctor);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["SKEY"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "Name");
                Doctor doctor = db.Doctors.Find(id);
                if (doctor == null)
                {
                    return HttpNotFound();
                }
                return View(doctor);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctor(int? id, FormCollection form)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctorToUpdate = db.Doctors.Find(id);
            if (TryUpdateModel(doctorToUpdate, "", new string[] { "Firstname", "Lastname", "Appartment", "Street", "City", "Postalcode", "Country", "Dob", "Email", "Contactno", "ProvinceID", "DateModified", "ModifiedBy", "ExpiredFlag" }))
            {
                try
                {
                    int selectedProvince = Convert.ToInt32(form["ProvinceID"].ToString());
                    doctorToUpdate.Firstname = doctorToUpdate.Firstname.ToUpper();
                    doctorToUpdate.Lastname = doctorToUpdate.Lastname.ToUpper();
                    doctorToUpdate.Appartment = doctorToUpdate.Appartment;
                    doctorToUpdate.Street = doctorToUpdate.Street;
                    doctorToUpdate.City = doctorToUpdate.City;
                    doctorToUpdate.Postalcode = doctorToUpdate.Postalcode;
                    doctorToUpdate.Country = doctorToUpdate.Country;
                    doctorToUpdate.Dob = doctorToUpdate.Dob;
                    doctorToUpdate.Email = doctorToUpdate.Email;
                    doctorToUpdate.Contactno = doctorToUpdate.Contactno;
                    doctorToUpdate.ProvinceID = selectedProvince;
                    doctorToUpdate.DateModified = DateTime.Now;
                    doctorToUpdate.ModifiedBy = Convert.ToInt32(Session["SKEY"].ToString());
                    doctorToUpdate.ExpiredFlag = "N";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "Name");
            return View(doctorToUpdate);
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
                ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "Name");
                Doctor doctor = db.Doctors.Find(id);
                if (doctor == null)
                {
                    return HttpNotFound();
                }
                return View(doctor);
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
            var doctorToDelete = db.Doctors.Find(id);
            if (TryUpdateModel(doctorToDelete, "", new string[] { "Firstname", "Lastname", "Appartment", "Street", "City", "Postalcode", "Country", "Dob", "Email", "Contactno", "ProvinceID", "DateModified", "ModifiedBy", "ExpiredFlag" }))
            {
                try
                {
                    doctorToDelete.Firstname = doctorToDelete.Firstname.ToUpper();
                    doctorToDelete.Lastname = doctorToDelete.Lastname.ToUpper();
                    doctorToDelete.Appartment = doctorToDelete.Appartment;
                    doctorToDelete.Street = doctorToDelete.Street;
                    doctorToDelete.City = doctorToDelete.City;
                    doctorToDelete.Postalcode = doctorToDelete.Postalcode;
                    doctorToDelete.Country = doctorToDelete.Country;
                    doctorToDelete.Dob = doctorToDelete.Dob;
                    doctorToDelete.Email = doctorToDelete.Email;
                    doctorToDelete.Contactno = doctorToDelete.Contactno;
                    doctorToDelete.ProvinceID = doctorToDelete.ProvinceID;
                    doctorToDelete.DateExpired = DateTime.Now;
                    doctorToDelete.ModifiedBy = Convert.ToInt32(Session["SKEY"].ToString());
                    doctorToDelete.ExpiredFlag = "Y";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    return RedirectToAction("Delete", new { id = id, saveChangesError = true });
                }
            }
            return View(doctorToDelete);
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
