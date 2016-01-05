using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.Entities;
using Chimera.DataAccess;
using MongoDB.Bson;
using ChimeraWebsite.Areas.Admin.Attributes;
using ChimeraWebsite.Areas.Admin.Helpers;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class HomeController : CompanyCommons.AbstractClasses.MasterController
    {
        [AdminUserAccess]
        public ActionResult Logout()
        {
            SiteContext.Logout();

            return View("Index");
        }

        /// <summary>
        /// Index method
        /// </summary>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult Index()
        {
            if (!Request.UserAgent.Contains("Chrome"))
            {
                AddWebUserMessageToSession(Request, "The latest version of Google Chrome is required for site administration.  Please <a href=\"https://www.google.com/chrome\" target=\"_blank\">click here</a> to download.", FAILED_MESSAGE_TYPE);
            }

            return View();
        }

        /// <summary>
        /// Called when a user attempts to log into the admin area.
        /// </summary>
        /// <returns>either back to login page on success or the index of the dashboard controller.</returns>
        [HttpPost]
        public ActionResult Attempt_Login()
        {
            try
            {
                string email = !string.IsNullOrWhiteSpace(Request["email"]) ? Request["email"] : string.Empty;

                //stops bots because they will fill this field out?
                if (string.Empty.Equals(email))
                {

                    string username = !string.IsNullOrWhiteSpace(Request["username"]) ? CompanyCommons.Utility.TrimToMaxSize(Request["username"], 250) : string.Empty;
                    string password = !string.IsNullOrWhiteSpace(Request["password"]) ? CompanyCommons.Utility.TrimToMaxSize(Request["password"], 250) : string.Empty;

                    username = username.Trim();
                    password = password.Trim();

                    if (!string.Empty.Equals(username) && !string.Empty.Equals(password))
                    {
                        if (Models.AdminUser.AttemptLogin(Request, username, password))
                        {
                            //successful login
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }

                }

            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.HomeController.Attempt_Login()" + e.Message);
            }

            AddWebUserMessageToSession(Request, Chimera.Resources.Admin.Website.Controllers.Home.UserMessages.Invalid_Login_Fail, FAILED_MESSAGE_TYPE);

            //if we got this far the login failed
            return RedirectToAction("Index", "Home");
        }
    }
}
