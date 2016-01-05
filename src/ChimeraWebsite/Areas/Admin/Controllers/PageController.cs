using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChimeraWebsite.Areas.Admin.Attributes;
using MongoDB.Bson;
using CompanyCommons.Entities;
using CompanyCommons.Extensions;
using CompanyCommons;
using Chimera.DataAccess;
using Chimera.Entities.Page;
using Newtonsoft.Json;
using Chimera.Entities.Admin.Role;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class PageController : CompanyCommons.AbstractClasses.MasterController
    {
        /// <summary>
        /// Simply load a list of all available page types.
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PageRoles.VIEW)]
        public ActionResult ViewAllPageTypes()
        {
            try
            {
                ViewBag.PageTypeList = PageDAO.LoadPageTypes();
            }
            catch (Exception e)
            {
                Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PageController.ViewAllPageTypes()" + e.Message);
            }

            return View();
        }

        [AdminUserAccess(Admin_User_Roles = PageRoles.VIEW)]
        public ActionResult ViewPageHistory(string pageId)
        {
            try
            {
                ViewBag.PageRevisionList = PageDAO.LoadRevisionHistory(new Guid(pageId));
            }
            catch (Exception e)
            {
                Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PageController.ViewPageHistory()" + e.Message);
            }

            return View();
        }

        /// <summary>
        /// Called to add a new page or edit an existing one
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PageRoles.EDIT)]
        public ActionResult Edit(string id)
        {
            try
            {
                Page Page = new Page();

                if (!string.IsNullOrWhiteSpace(id))
                {
                    Page = PageDAO.LoadByBsonId(id);
                }

                ViewBag.Page = Page;
            }
            catch (Exception e)
            {
                Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PageController.Edit()" + e.Message);
            }

            return View();
        }

        /// <summary>
        /// Update an existing page or add a new page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageTitle"></param>
        /// <param name="pageFriendlyURL"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PageRoles.EDIT)]
        public ActionResult Edit_Post(string id, string pageTitle, string pageFriendlyURL, string published)
        {
            Page Page = new Page();

            bool SavedIt = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    Page = PageDAO.LoadByBsonId(id);
                }
                else
                {
                    Page.CreateDefaultNewPage();
                }

                Page.PageTitle = pageTitle;
                Page.PageFriendlyURL = pageFriendlyURL;
                Page.Published = Boolean.Parse(published);

                Page PageFriendlyURLExists = PageDAO.LoadByURL(Page.PageFriendlyURL);

                //can only save if another page type does not have the same page friendly url
                if (PageFriendlyURLExists == null || PageFriendlyURLExists.PageId.Equals(Page.PageId))
                {
                    SavedIt = true;

                    AddWebUserMessageToSession(Request, String.Format("Successfully saved/updated page."), SUCCESS_MESSAGE_TYPE);

                    PageDAO.Save(Page);
                }
                else
                {
                    AddWebUserMessageToSession(Request, String.Format("Unable to save page, there is already a page type published with the friendly URL \"{0}\"", Page.PageFriendlyURL), FAILED_MESSAGE_TYPE);
                }
            }
            catch (Exception e)
            {
                Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PageController.Edit_Post()" + e.Message);
            }

            //if we were trying to add a new page and were unable to save it
            if (string.IsNullOrWhiteSpace(id) && !SavedIt)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return RedirectToAction("ViewPageHistory", "Page", new { pageId = Page.PageId });
        }


        /// <summary>
        /// User redirected from the chimera editor when they wish to save w/o publishing
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PageRoles.EDIT)]
        public ActionResult Editor_Save(string pageData)
        {
            Page Page = new Page();

            try
            {
                Page = JsonConvert.DeserializeObject<Page>(pageData);

                Page.Published = false;

                PageDAO.Save(Page);
            }
            catch (Exception e)
            {
                Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PageController.Editor_Save()" + e.Message);
            }

            return RedirectToAction("ViewPageHistory", "Page", new { pageId = Page.PageId });
        }

        /// <summary>
        /// User redirected from the chimera editor when they wish to save and publish
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PageRoles.EDIT)]
        public ActionResult Editor_Publish(string pageData)
        {
            Page Page = new Page();

            try
            {
                Page = JsonConvert.DeserializeObject<Page>(pageData);

                Page.Published = true;

                if (PageDAO.Save(Page))
                {
                    ChimeraWebsite.Helpers.AppCache.UpdatePageInCache(Page);
                }
            }
            catch (Exception e)
            {
                Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PageController.Editor_Publish()" + e.Message);
            }

            return RedirectToAction("ViewPageHistory", "Page", new { pageId = Page.PageId });
        }
    }
}
