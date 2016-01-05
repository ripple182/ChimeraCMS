using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChimeraWebsite.Areas.Admin.Attributes
{
    public class AdminUserAccess: AuthorizeAttribute
    {
        /// <summary>
        /// Semi Colon delimited string of the roles required to execute this ActionResult.
        /// </summary>
        public string Admin_User_Roles { get; set; }

        /// <summary>
        /// Simply determine if the user has the necessary roles to execute the ActionResult that inherits this attribute.
        /// </summary>
        /// <param name="httpContext">http context</param>
        /// <returns>bool, if false redirects user to login page.</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string RedirectController = "Dashboard";

            bool HashAllRoles = true;

            Chimera.Entities.Admin.AdminUser AdminUser = Models.AdminUser.GetFromSession(httpContext.Request);

            //only allow people to view these pages if they are in the process of ordering IDOS stuff
            if (AdminUser != null)
            {
                //if they don't contain the admin all role then check their roles vs the required role from the action result.
                if (!AdminUser.RoleList.Contains("admin-all"))
                {
                    //check to make sure the user has all the necessary roles to continue.
                    string[] RoleArray = !string.IsNullOrWhiteSpace(Admin_User_Roles) ? Admin_User_Roles.Split(';') : null;

                    if (RoleArray != null && RoleArray.Length > 0)
                    {
                        foreach (var Role in RoleArray)
                        {
                            if (!AdminUser.RoleList.Contains(Role))
                            {
                                HashAllRoles = false;

                                break;
                            }
                        }
                    }
                }
                //if true then no special roles were required or they had all the roles.
                if (HashAllRoles)
                {
                    return true;
                }
            }
            else
            {
                RedirectController = "Home";
            }

            System.Web.Mvc.UrlHelper UrlHelper = new UrlHelper(((System.Web.Mvc.MvcHandler) HttpContext.Current.CurrentHandler).RequestContext);

            String RedirectUrl = UrlHelper.Action("Index", RedirectController, null, UrlHelper.RequestContext.HttpContext.Request.Url.Scheme);

            httpContext.Response.Redirect(RedirectUrl, true);

            return false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AdminUserAccess()
        {
            Admin_User_Roles = string.Empty;
        }
 
    }
}