using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using Chimera.Entities.Page;
using Chimera.Entities.Admin;
using Chimera.DataAccess;
using ChimeraWebsite.Areas.Admin.Helpers;
using Chimera.Entities.Report;

namespace ChimeraWebsite.Models
{
    public class PageModel
    {
        /// <summary>
        /// Whether we are in edit mode or not.
        /// </summary>
        public bool InEditMode { get; set; }
         
        /// <summary>
        /// The actual page object.
        /// </summary>
        public Page Page { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageModel()
        {
            InEditMode = false;
            Page = new Page();
        }

        /// <summary>
        /// Create a new page model.
        /// </summary>
        /// <param name="friendlyURL"></param>
        /// <param name="request"></param>
        public PageModel(string friendlyURL, HttpRequestBase request)
        {
            string id = !string.IsNullOrWhiteSpace(request["id"]) ? request["id"] : string.Empty;

            AdminUser AdminUser = SiteContext.User;

            //if a bson id was passed, and the admin user is not null, and they have the page edit role
            if (!string.IsNullOrWhiteSpace(id) && AdminUser != null && SiteContext.CanAdminUserAccess("page-edit"))
            {
                InEditMode = true;
                Page = PageDAO.LoadByBsonId(id) ?? new Page();
            }
            else
            {
                InEditMode = Helpers.AppSettings.InDeveloperEditMode;

                //only continue if this is a regular page view outside of edit mode
                if (Page != null && !string.IsNullOrWhiteSpace(Page.Id) && !InEditMode)
                {
                    ChimeraWebsite.Helpers.SiteContext.RecordPageView(friendlyURL);
                }

                Page = ChimeraWebsite.Helpers.AppCache.GetPageFromCache(friendlyURL);
            }
        }
    }
}