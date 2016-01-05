using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChimeraWebsite.Areas.Admin.Attributes;
using MongoDB.Bson;
using CompanyCommons.Entities;
using CompanyCommons.Extensions;
using Chimera.Entities.Admin.Role;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class AdminUserController : CompanyCommons.AbstractClasses.MasterController
    {
        /// <summary>
        /// View a list of all the admin users.
        /// </summary>
        /// <returns>return the view.</returns>
        [AdminUserAccess(Admin_User_Roles = AdminUserRoles.VIEW)]
        public ActionResult ViewAll()
        {
            try
            {
                ViewBag.AdminUserList = Chimera.DataAccess.AdminUserDAO.LoadAll();
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.AdminUserController.ViewAll()" + e.Message);
            }

            return View();
        }

        /// <summary>
        /// Add New / Edit page for admin users.
        /// </summary>
        /// <returns>edit view</returns>
        [AdminUserAccess(Admin_User_Roles = AdminUserRoles.EDIT)]
        public ActionResult Edit()
        {
            try
            {
                string id = !string.IsNullOrWhiteSpace(Request["id"]) ? Request["id"] : string.Empty;

                if (!string.Empty.Equals(id))
                {
                    ViewBag.AdminUser = Chimera.DataAccess.AdminUserDAO.LoadByBsonId(new MongoDB.Bson.ObjectId(id));
                }

                ViewBag.AdminUserRoleList = Chimera.DataAccess.AdminUserRoleDAO.LoadAll();

                return View();
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.AdminUserController.Edit()" + e.Message);
            }

            return RedirectToAction("ViewAll", "AdminUser");
        }

        /// <summary>
        /// Posted to whenever saving details about an admin user.
        /// </summary>
        /// <returns>View All page</returns>
        [HttpPost]
        [AdminUserAccess(Admin_User_Roles = AdminUserRoles.EDIT)]
        public ActionResult Edit_Post()
        {
            try
            {
                string id = !string.IsNullOrWhiteSpace(Request["id"]) ? Request["id"] : string.Empty;
                string username = !string.IsNullOrWhiteSpace(Request["username"]) ? Request["username"] : string.Empty;
                string email = !string.IsNullOrWhiteSpace(Request["email"]) ? Request["email"] : string.Empty;
                string password = !string.IsNullOrWhiteSpace(Request["password"]) ? Request["password"] : string.Empty;
                string passwordrepeat = !string.IsNullOrWhiteSpace(Request["passwordrepeat"]) ? Request["passwordrepeat"] : string.Empty;
                bool active = (!string.IsNullOrWhiteSpace(Request["active"]) && Request["active"].Equals("yes")) ? true : false;

                List<WebUserMessage> ErrorUserMessageList = new List<WebUserMessage>();

                //Add New User - Require Username
                ErrorUserMessageList.AddIfNotNull(Models.AdminUser.SaveValidation.AddNewRequireUsername(Request, id, username));

                if (password.Equals(passwordrepeat))
                {
                    //Add New User - Require Password
                    ErrorUserMessageList.AddIfNotNull(Models.AdminUser.SaveValidation.AddNewRequirePassword(Request, id, password));

                    //only continue if no errors
                    if (ErrorUserMessageList.Count == 0)
                    {
                        //if adding new password requires >= 8 characters, capital and lower, and numbers
                        ErrorUserMessageList.AddIfNotNull(Models.AdminUser.SaveValidation.AddNewCheckPasswordStrength(Request, id, password));

                        //if we reached this point its time to save.
                        if (ErrorUserMessageList.Count == 0)
                        {
                            Chimera.Entities.Admin.AdminUser AdminUser = Models.AdminUser.LoadAdminUserForSaving(id, username, active);

                            string OnSuccessUserMessage = Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Edit_Saved_Success.Replace("[USERNAME]", AdminUser.Username);

                            //if adding new and no errors set password.
                            if (AdminUser.Id.Equals(ObjectId.Empty))
                            {
                                AdminUser.Hashed_Password = password;
                                OnSuccessUserMessage = Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Add_New_Saved_Success.Replace("[USERNAME]", AdminUser.Username);
                            }

                            //setup admin users new role list from the request.
                            AdminUser.RoleList = Models.AdminUser.SaveValidation.SetupAdminUserRolesOnSave(Request, AdminUser.RoleList);

                            //if successfully saved.
                            if (Chimera.DataAccess.AdminUserDAO.Save(AdminUser))
                            {
                                AddWebUserMessageToSession(Request, OnSuccessUserMessage, SUCCESS_MESSAGE_TYPE);
                            }
                            else
                            {
                                ErrorUserMessageList.Add(new WebUserMessage(Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Unable_To_Complete_Save_Default_Fail, FAILED_MESSAGE_TYPE));
                            }
                        }

                    }
                }
                else
                {
                    //add user message to tell them that passwords must repeat
                    ErrorUserMessageList.Add(new WebUserMessage(Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Add_New_Passwords_Dont_Match_Fail, FAILED_MESSAGE_TYPE));
                }

                if (ErrorUserMessageList.Count > 0)
                {
                    AddWebUserMessageToSession(Request, ErrorUserMessageList);
                    return RedirectToAction("Edit", "AdminUser", new { id = id });
                }
            }
            catch (Exception e)
            {
                AddWebUserMessageToSession(Request, Chimera.Resources.Admin.Website.Controllers.AdminUser.UserMessages.Unable_To_Complete_Save_Default_Fail, FAILED_MESSAGE_TYPE);
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.AdminUserController.Edit_Post()" + e.Message);
            }

            return RedirectToAction("ViewAll", "AdminUser");
        }

    }
}
