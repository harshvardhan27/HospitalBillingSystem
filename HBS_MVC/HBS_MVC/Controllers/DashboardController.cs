using HBS_MVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBS_MVC.Controllers
{
    public class DashboardController : Controller
    {
        private HbsContext db = new HbsContext();

        // GET: Dashboard
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