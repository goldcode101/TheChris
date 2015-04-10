using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhereDoesItAllGo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Dashboard()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login");
            int userId = Convert.ToInt32(Session["UserID"]);
            //get the user by userId and load their page

            return View();
        }

        public ActionResult Login()
        {
            if (Session["UserID"] != null) return RedirectToAction("Dashboard");



            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string FirstName, string LastName, string Email, string Password, string InitialBalance)
        {
            var statusMessage = "";
            Decimal initialBalance;
            try
            {
                initialBalance = Convert.ToDecimal(InitialBalance);
            }
            catch(Exception ex)
            {
                statusMessage = "There was a problem with your initial balance.";
                ViewBag.StatusMessage = statusMessage;
                return View();
            }

            var success = new UserBL().Insert(FirstName, LastName, Email, Password, InitialBalance);

            return View();
        }

    }
}
