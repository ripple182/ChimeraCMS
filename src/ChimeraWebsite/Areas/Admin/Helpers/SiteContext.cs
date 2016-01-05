using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chimera.Entities.Admin;
using Chimera.Entities.Admin.Role;

namespace ChimeraWebsite.Areas.Admin.Helpers
{
    public static class SiteContext
    {
        /// <summary>
        /// session key used to access the admin user
        /// </summary>
        private const string ADMIN_USER_SESSION_KEY = "Admin_User";

        /// <summary>
        /// The current logged in admin user
        /// </summary>
        public static AdminUser User
        {
            get
            {
                return (AdminUser) HttpContext.Current.Session[ADMIN_USER_SESSION_KEY] ?? new AdminUser();
            }
            set
            {
                HttpContext.Current.Session[ADMIN_USER_SESSION_KEY] = value;
            }
        }

        public static void Logout()
        {
            User = null;
        }

        /// <summary>
        /// check if the admin user has at least 1 of the passed in roles
        /// </summary>
        /// <param name="semiColonDelimitedRolesString"></param>
        /// <returns></returns>
        public static bool HasAtleastOneRole(string semiColonDelimitedRolesString)
        {
            AdminUser CurrentUser = User;

            //if they have the admin all role 
            if (CurrentUser.RoleList.IndexOf(AdminRoles.ADMIN_ALL) != -1)
            {
                return true;
            }
            else if (!string.IsNullOrWhiteSpace(semiColonDelimitedRolesString))
            {
                string[] RequiredRoles = semiColonDelimitedRolesString.Split(';');

                foreach (var Role in RequiredRoles)
                {
                    if (CurrentUser.RoleList.IndexOf(Role) != -1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// determine if the admin user has all the necessary roles passed in
        /// </summary>
        /// <param name="semiColonDelimitedRolesString"></param>
        /// <returns></returns>
        public static bool CanAdminUserAccess(string semiColonDelimitedRolesString)
        {
            try
            {
                AdminUser CurrentUser = User;

                //if they have the admin all role 
                if (CurrentUser.RoleList.IndexOf(AdminRoles.ADMIN_ALL) != -1)
                {
                    return true;
                }
                else if (!string.IsNullOrWhiteSpace(semiColonDelimitedRolesString))
                {
                    string[] RequiredRoles = semiColonDelimitedRolesString.Split(';');

                    bool HasAllRoles = true;

                    foreach (var Role in RequiredRoles)
                    {
                        if (CurrentUser.RoleList.IndexOf(Role) == -1)
                        {
                            HasAllRoles = false;
                            break;
                        }
                    }

                    return HasAllRoles;
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Helpers.CanAdminUserAccess(semiColonDelimitedRolesString: " + semiColonDelimitedRolesString + ") " + e.Message);
            }

            return false;
        }
    }
}