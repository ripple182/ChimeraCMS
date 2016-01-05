using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChimeraWebsite.Areas.Admin.Attributes;
using Chimera.Entities.Website;
using Chimera.DataAccess;
using Newtonsoft.Json;
using CompanyCommons.AbstractClasses;
using CompanyCommons.Entities;
using Chimera.Entities.Admin.Role;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class NavMenuController : MasterController
    {
        /// <summary>
        /// The edit screen for the nav menu
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = NavMenuRoles.EDIT)]
        public ActionResult Edit(string id, string navigationMenuData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(navigationMenuData) && !string.IsNullOrWhiteSpace(id))
                {
                    ViewBag.NavigationMenu = NavigationMenuDAO.LoadById(id);
                }
                else if (string.IsNullOrWhiteSpace(navigationMenuData) && string.IsNullOrWhiteSpace(id))
                {
                    ViewBag.NavigationMenu = new NavigationMenu();
                }
                else
                {
                    ViewBag.NavigationMenu = JsonConvert.DeserializeObject<NavigationMenu>(navigationMenuData);
                }

                ViewBag.PageTypeList = PageDAO.LoadPageTypes();
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.NavMenuController.Edit() " + e.Message);
            }

            return View();
        }

        /// <summary>
        /// Save the edited data
        /// </summary>
        /// <param name="navigationMenuData"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = NavMenuRoles.EDIT)]
        public ActionResult Edit_Post(string navigationMenuData)
        {
            try
            {
                NavigationMenu NavigationMenu = JsonConvert.DeserializeObject<NavigationMenu>(navigationMenuData);

                List<WebUserMessage> ErrorList = NavigationMenu.Validate();

                //if passed validation
                if (ErrorList == null || ErrorList.Count == 0)
                {
                    if (NavigationMenuDAO.Save(NavigationMenu))
                    {
                        AddWebUserMessageToSession(Request, String.Format("Successfully saved/updated nav menu \"{0}\"", NavigationMenu.KeyName), SUCCESS_MESSAGE_TYPE);
                    }
                    else
                    {
                        AddWebUserMessageToSession(Request, String.Format("Unable to saved/update nav menu \"{0}\" at this time", NavigationMenu.KeyName), FAILED_MESSAGE_TYPE);
                    }
                }
                //failed validation
                else
                {
                    AddWebUserMessageToSession(Request, ErrorList);

                    return RedirectToAction("Edit", "NavMenu", new { navigationMenuData = navigationMenuData });
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.NavMenuController.Edit_Post() " + e.Message);
            }

            AddWebUserMessageToSession(Request, String.Format("Unable to save/update navigation menus at this time."), FAILED_MESSAGE_TYPE);

            return RedirectToAction("Index", "Dashboard");
        }

    }
}
