using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using Chimera.DataAccess;
using Chimera.Entities.Settings.Keys;
using Chimera.Entities.Settings;

namespace ChimeraWebsite.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Index method      
        /// </summary>         
        /// <returns>view</returns>         
        [ValidateInput(false)]   
        public ActionResult Index(string friendlyURL, string previewPageData)    
        {
            friendlyURL = string.IsNullOrWhiteSpace(friendlyURL) ? "Index" : friendlyURL;

            if (friendlyURL.ToUpper().Equals("ADMIN")) 
            { 
                return RedirectToRoute("Admin_Default");
            }

            Models.PageModel PageModel = new Models.PageModel();

            if (!string.IsNullOrWhiteSpace(previewPageData))
            {
                ViewBag.PreviewPageData = previewPageData;
                PageModel.InEditMode = true;
            }
            else
            {
                PageModel = new Models.PageModel(friendlyURL, Request);
            }

            if (!string.IsNullOrWhiteSpace(PageModel.Page.Id) || PageModel.InEditMode)
            {
                ViewBag.PageModel = PageModel;

                return View("Index", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
            }

            //if we got this far this is a 404

            SettingGroup SettingGroup = SettingGroupDAO.LoadSettingGroupByName(SettingGroupKeys.TEMPLATE_CUSTOM_SETTINGS);

            ViewBag.ViewType = SettingGroup.GetSettingVal("PageNotFoundPage");

            return View("PageNotFound", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
        }
    }
}
