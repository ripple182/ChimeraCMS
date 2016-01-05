using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.DataAccess;
using ChimeraWebsite.Helpers;

namespace ChimeraWebsite.Controllers
{
    public class PageExitController : Controller
    {
        /// <summary>
        /// Record the users page exit time
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit()
        {
            ReportDAO.UpdateLastPageUserWasOn(AppSettings.AllowPageReportRecording, SiteContext.UserSessionInfo);

            return Content("");
        }

    }
}
