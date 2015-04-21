using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhereDoesItAllGo.Models;

namespace WhereDoesItAllGo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public UserBL _userBL = new UserBL();

        public ActionResult Dashboard()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login");
            int userId = Convert.ToInt32(Session["UserID"]);
            //get the user by userId and load their page
            var currentUser = _userBL.GetUser(userId);
            var dashboardVM = new DashboardVM();

            dashboardVM.UserFirstName = currentUser.FirstName;
            dashboardVM.UserLastName = currentUser.LastName;




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
            }

            try
            {
                var userID = _userBL.AddUser(FirstName, LastName, Email, Password, InitialBalance);
                if (userID != -1)
                {
                    statusMessage = "Successfully registered. <a href='../Login.cshtml'>Click here</a> to login to your account.";
                    Session["UserID"] = userID;
                }
                else
                {
                    statusMessage = "There was a problem registering this user.";
                }
            }
            catch (Exception ex)
            {
                statusMessage = "There was a problem registering this user.";
            }
            
            ViewBag.StatusMessage = statusMessage;
            return View();
        }

    }
}
