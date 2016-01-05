using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.DataAccess;
using ChimeraWebsite.Areas.Admin.Attributes;
using Chimera.Entities.Property;
using Newtonsoft.Json;
using CompanyCommons.AbstractClasses;
using CompanyCommons.Entities;
using Chimera.Entities.Admin.Role;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class PropertiesController : MasterController
    {
        /// <summary>
        /// Called whenever the admin user wants to add a brand new static property to the system
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PropertyRoles.EDIT)]
        public ActionResult AddNew(string staticPropertyData)
        {
            try
            {
                StaticProperty StaticProperty = new StaticProperty();

                if (!string.IsNullOrWhiteSpace(staticPropertyData))
                {
                    StaticProperty = JsonConvert.DeserializeObject<StaticProperty>(staticPropertyData);
                }

                ViewBag.StaticProperty = StaticProperty;
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PropertiesController.AddNew() " + e.Message);
            }

            return View();
        }

        /// <summary>
        /// Add a new static propert form post
        /// </summary>
        /// <param name="staticPropertyData"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PropertyRoles.EDIT)]
        public ActionResult AddNew_Post(string staticPropertyData)
        {
            try
            {
                StaticProperty StaticProperty = JsonConvert.DeserializeObject<StaticProperty>(staticPropertyData);

                List<WebUserMessage> ErrorList = StaticProperty.Validate();

                //if passed validation
                if (ErrorList == null || ErrorList.Count == 0)
                {
                    if (StaticPropertyDAO.Save(StaticProperty))
                    {
                        AddWebUserMessageToSession(Request, String.Format("Successfully saved/updated static property \"{0}\"", StaticProperty.KeyName), SUCCESS_MESSAGE_TYPE);
                    }
                    else
                    {
                        AddWebUserMessageToSession(Request, String.Format("Unable to saved/update static property \"{0}\" at this time", StaticProperty.KeyName), FAILED_MESSAGE_TYPE);
                    }
                }
                //failed validation
                else
                {
                    AddWebUserMessageToSession(Request, ErrorList);

                    return RedirectToAction("AddNew", "Properties", new { staticPropertyData = staticPropertyData });
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PropertiesController.AddNew_Post() " + e.Message);
            }

            AddWebUserMessageToSession(Request, String.Format("Unable to save/update properties at this time."), FAILED_MESSAGE_TYPE);

            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// Called whenever an admin user wishes to add new values to a static property, can't change static property name or edit old values or remove old values
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PropertyRoles.EDIT)]
        public ActionResult Edit(string id)
        {
            try
            {
                ViewBag.StaticProperty = StaticPropertyDAO.LoadByBsonId(id);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PropertiesController.Edit() " + e.Message);
            }

            return View();
        }

        /// <summary>
        /// When adding new values to a static property
        /// </summary>
        /// <param name="staticPropertyData"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PropertyRoles.EDIT)]
        public ActionResult Edit_Post(string staticPropertyData)
        {
            try
            {
                StaticProperty StaticProperty = JsonConvert.DeserializeObject<StaticProperty>(staticPropertyData);

                if (StaticPropertyDAO.Save(StaticProperty))
                {
                    AddWebUserMessageToSession(Request, String.Format("Successfully saved/updated static property \"{0}\"", StaticProperty.KeyName), SUCCESS_MESSAGE_TYPE);
                }
                else
                {
                    AddWebUserMessageToSession(Request, String.Format("Unable to saved/update static property \"{0}\" at this time", StaticProperty.KeyName), FAILED_MESSAGE_TYPE);
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PropertiesController.Edit_Post() " + e.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// View the table of existing static properties
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PropertyRoles.VIEW)]
        public ActionResult ViewAll()
        {
            try
            {
                ViewBag.StaticPropertyList = StaticPropertyDAO.LoadAll();
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PropertiesController.ViewAll() " + e.Message);
            }

            return View();
        }
    }
}
