using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChimeraWebsite.Areas.Admin.Attributes;
using Chimera.Entities.Settings;
using Chimera.Entities.Property;
using Chimera.Entities;
using Chimera.DataAccess;
using Newtonsoft.Json;
using CompanyCommons.AbstractClasses;
using CompanyCommons.Entities;
using Chimera.Entities.Admin.Role;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class SettingsController : MasterController
    {
        /// <summary>
        /// Should really only be visible to the developer of the template, called to add or edit a setting schema.
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = SettingRoles.EDIT_SCHEMA)]
        public ActionResult EditSchema(string id, string settingGroupData)
        {
            try
            {
                SettingGroup SettingGroup = new SettingGroup();

                if (!string.IsNullOrWhiteSpace(id))
                {
                    SettingGroup = SettingGroupDAO.LoadSettingGroupById(id);
                }
                else if(string.IsNullOrWhiteSpace(settingGroupData))
                {
                    //add an empty setting for new setting groups
                    SettingGroup.SettingsList.Add(new Setting());
                }
                else
                {
                    SettingGroup = JsonConvert.DeserializeObject<SettingGroup>(settingGroupData);
                }

                ViewBag.SettingGroup = SettingGroup;
                ViewBag.StaticPropertyKeyList = StaticPropertyDAO.LoadAllKeyNames();
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.SettingsController.EditSchema() " + e.Message);
            }

            return View();
        }

        /// <summary>
        /// Add/Update a setting schema
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = SettingRoles.EDIT_SCHEMA)]
        public ActionResult EditSchema_Post(string settingGroupData)
        {
            try
            {
                SettingGroup SettingGroup = JsonConvert.DeserializeObject<SettingGroup>(settingGroupData);

                List<WebUserMessage> ErrorList = SettingGroup.Validate();

                //if passed validation
                if (ErrorList == null || ErrorList.Count == 0)
                {
                    //remove data entry property key if data entry does not require just to be safe
                    foreach (var Sett in SettingGroup.SettingsList)
                    {
                        if (!DataEntryTypeProperty.DataTypesRequireProperties.Contains(Sett.EntryType))
                        {
                            Sett.DataEntryStaticPropertyKey = string.Empty;
                        }
                    }

                    if (SettingGroupDAO.Save(SettingGroup))
                    {
                        AddWebUserMessageToSession(Request, String.Format("Successfully saved/updated setting group \"{0}\"", SettingGroup.GroupKey), SUCCESS_MESSAGE_TYPE);
                    }
                    else
                    {
                        AddWebUserMessageToSession(Request, String.Format("Unable to saved/update setting group \"{0}\" at this time", SettingGroup.GroupKey), FAILED_MESSAGE_TYPE);
                    }
                }
                //failed validation
                else
                {
                    AddWebUserMessageToSession(Request, ErrorList);

                    return RedirectToAction("EditSchema", "Settings", new { settingGroupData = settingGroupData });
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.SettingsController.EditSchema_Post() " + e.Message);
            }

            AddWebUserMessageToSession(Request, String.Format("Unable to save/update setting schemas at this time."), FAILED_MESSAGE_TYPE);

            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// View the table of all the existing setting groups
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = SettingRoles.VIEW)]
        public ActionResult ViewAll()
        {
            ViewBag.SettingGroupList = SettingGroupDAO.LoadAll();

            return View();
        }

        /// <summary>
        /// Edit the values of of a setting group
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = SettingRoles.EDIT_VALUES)]
        public ActionResult EditValues(string id)
        {
            try
            {
                SettingGroup SettingGroup = SettingGroupDAO.LoadSettingGroupById(id);

                List<string> StaticPropertyKeys = (from e in SettingGroup.SettingsList.AsQueryable() where DataEntryTypeProperty.DataTypesRequireProperties.Contains(e.EntryType) && !string.IsNullOrWhiteSpace(e.DataEntryStaticPropertyKey) select e.DataEntryStaticPropertyKey).ToList();

                ViewBag.SettingGroup = SettingGroup;

                ViewBag.StaticPropertyList = StaticPropertyKeys != null && StaticPropertyKeys.Count > 0 ?StaticPropertyDAO.LoadByMultipleKeyNames(StaticPropertyKeys) : new List<StaticProperty>();

                ViewBag.ImageList = ImageDAO.LoadAll();
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.SettingsController.EditValues() " + e.Message);
            }

            return View();
        }

        /// <summary>
        /// save the new values for a setting group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = SettingRoles.EDIT_VALUES)]
        public ActionResult EditValues_Post(string id)
        {
            try
            {
                SettingGroup SettingGroup = SettingGroupDAO.LoadSettingGroupById(id);

                foreach (var Sett in SettingGroup.SettingsList)
                {
                    if(!string.IsNullOrWhiteSpace(Request["setting_" + Sett.Key]))
                    {
                        Sett.Value = Request["setting_" + Sett.Key];
                    }
                    else
                    {
                        Sett.Value = string.Empty;
                    }
                }

                if (SettingGroupDAO.Save(SettingGroup))
                {
                    AddWebUserMessageToSession(Request, String.Format("Successfully saved/updated setting group \"{0}\"", SettingGroup.GroupKey), SUCCESS_MESSAGE_TYPE);
                }
                else
                {
                    AddWebUserMessageToSession(Request, String.Format("Unable to saved/update setting group \"{0}\" at this time", SettingGroup.GroupKey), FAILED_MESSAGE_TYPE);
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.SettingsController.EditValues_Post() " + e.Message);
            }

            AddWebUserMessageToSession(Request, String.Format("Unable to save/update setting values at this time."), FAILED_MESSAGE_TYPE);

            return RedirectToAction("Index", "Dashboard");
        }

    }
}
