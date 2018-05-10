using HBS_MVC.DAL;
using HBS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBS_MVC.Controllers
{
    public class AccountController : Controller
    {
        private HbsContext db = new HbsContext();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                var userAccount = db.Accounts.Where(a => a.Email.Equals(account.Email) && a.Password.Equals(account.Password)).FirstOrDefault();
                if (userAccount != null)
                {
                    Session["SKEY"] = userAccount.AccountID.ToString();
                    Session["USER"] = userAccount.Email.ToString();
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Email,Password,DateCreated,CreatedBy,ExpiredFlag")] Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    account.Email = account.Email;
                    account.Password = account.Password;
                    account.CreatedBy = Convert.ToInt32(1);
                    account.DateCreated = DateTime.Now;
                    account.ExpiredFlag = "N";
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(account);
        }
    }
}