using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using Chimera.DataAccess;
using Chimera.Entities;
using CompanyCommons.Entities;

namespace ChimeraWebsite.Areas.Admin.Models
{
    public static class AdminUser
    {
        /// <summary>
        /// The session key for storing / loading the admin user.
        /// </summary>
        private const string SESSION_KEY = "Admin_User";

        /// <summary>
        /// Called when the user attempts to login with their admin credentials.
        /// </summary>
        /// <param name="request">The request so we can access the session & get the variables.</param>
        /// <param name="username">username from controller.</param>
        /// <param name="password">password from controller.</param>
        /// <returns>bool.</returns>
        public static bool AttemptLogin(HttpRequestBase request, string username, string password)
        {
            Chimera.Entities.Admin.AdminUser AdminUser = AdminUserDAO.LoadByAttemptLogin(username, password);

            //if true then this is a valid login.
            if (AdminUser != null && !AdminUser.Id.Equals(ObjectId.Empty) && username.ToUpper().Equals(AdminUser.Username.ToUpper()))
            {
                //add user to session.
                AddToSession(request, AdminUser);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Simply grab the currently logged in admin user from the session.
        /// </summary>
        /// <param name="request">the httprequest</param>
        /// <returns>admin user object.</returns>
        public static Chimera.Entities.Admin.AdminUser GetFromSession(HttpRequestBase request)
        {
            if (request.RequestContext.HttpContext.Session[SESSION_KEY] != null)
            {
                return (Chimera.Entities.Admin.AdminUser)request.RequestContext.HttpContext.Session[SESSION_KEY];
            }

            return null;
        }

        /// <summary>
        /// Simply add an admin user to the session.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="adminuser">The admin user to add.</param>
        private static void AddToSession(HttpRequestBase request, Chimera.Entities.Admin.AdminUser adminuser)
        {
            request.RequestContext.HttpContext.Session[SESSION_KEY] = adminuser;
        }

        /// <summary>
        /// Load either the admin user from database or a new admin user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public static Chimera.Entities.Admin.AdminUser LoadAdminUserForSaving(string id, string username, bool active)
        {
            Chimera.Entities.Admin.AdminUser AdminUser = new Chimera.Entities.Admin.AdminUser();

            //if id field not empty load the user we are editing
            if (!id.Equals(string.Empty))
            {
                AdminUser = Chimera.DataAccess.AdminUserDAO.LoadByBsonId(new MongoDB.Bson.ObjectId(id));
            }
            //else set the username of the new user
            else
            {
                AdminUser.Username = username;
            }

            AdminUser.Active = active;

            return AdminUser;
        }

        /// <summary>
        /// Inner classes to split of the validation when adding or updating an admin user.  Will automaticially add user messages.
        /// </summary>
        public static class SaveValidation
        {
            /// <summary>
            /// If we are adding a new user and the password field is empty then error.
            /// </summary>
            public static WebUserMessage AddNewRequirePassword(HttpRequestBase request, string id, string password)
            {
                if (id.Equals(string.Empty) && password.Equals(string.Empty))
                {
                    return new WebUserMessage(Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Add_New_Password_Required_Fail, WebUserMessage.WebUserMessageType.FAILED_MESSAGE_TYPE);
                }

                return null;
            }

            /// <summary>
            /// If we are adding a new user and the username field is empty then error.
            /// </summary>
            public static WebUserMessage AddNewRequireUsername(HttpRequestBase request, string id, string username)
            {
                if (id.Equals(string.Empty) && username.Equals(string.Empty))
                {
                    return new WebUserMessage(Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Add_New_Username_Required_Fail, WebUserMessage.WebUserMessageType.FAILED_MESSAGE_TYPE);
                }

                return null;
            }

            /// <summary>
            /// If we are adding a new user check password strength. requires >= 8 characters, capital and lower, and numbers.
            /// </summary>
            public static WebUserMessage AddNewCheckPasswordStrength(HttpRequestBase request, string id, string password)
            {
                CompanyCommons.Logging.WriteLog("pass: " + CompanyCommons.PasswordAdvisor.CheckStrength(password));
                if (id.Equals(string.Empty) && !password.Equals(string.Empty) && CompanyCommons.PasswordAdvisor.CheckStrength(password) < 3)
                {
                    return new WebUserMessage(Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Add_New_Password_Strength_Failed, WebUserMessage.WebUserMessageType.FAILED_MESSAGE_TYPE);
                }

                return null;
            }

            /// <summary>
            /// Looks for role changes on the save and adds/removes roles as necessary.  admin-all role cant be added or removed.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="currentAdminUserRoles"></param>
            /// <returns></returns>
            public static List<string> SetupAdminUserRolesOnSave(HttpRequestBase request, List<string> currentAdminUserRoles)
            {
                List<Chimera.Entities.Admin.AdminUserRole> AdminUserRoleList = Chimera.DataAccess.AdminUserRoleDAO.LoadAll();

                if (AdminUserRoleList != null && AdminUserRoleList.Count > 0)
                {
                    foreach (var AdminUserRole in AdminUserRoleList)
                    {
                        //never allow the admin-all role to be set using this form
                        if (!AdminUserRole.Name.Equals("admin-all"))
                        {
                            string roleEnabled = !string.IsNullOrWhiteSpace(request[AdminUserRole.Id.ToString()]) ? request[AdminUserRole.Id.ToString()] : string.Empty;

                            if ("on".Equals(roleEnabled))
                            {
                                //if they don't contain this role already then add it
                                if (!currentAdminUserRoles.Contains(AdminUserRole.Name))
                                {
                                    currentAdminUserRoles.Add(AdminUserRole.Name);
                                }
                            }
                            else if (string.Empty.Equals(roleEnabled))
                            {
                                //if they contain this role then remove it
                                if (currentAdminUserRoles.Contains(AdminUserRole.Name))
                                {
                                    currentAdminUserRoles.Remove(AdminUserRole.Name);
                                }
                            }
                        }
                    }
                }

                return currentAdminUserRoles;
            }
        }
    }
}