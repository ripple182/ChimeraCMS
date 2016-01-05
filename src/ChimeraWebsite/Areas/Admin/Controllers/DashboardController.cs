using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChimeraWebsite.Areas.Admin.Attributes;
using CompanyCommons.Entities;
using Chimera.Entities.Dashboard;
using Chimera.DataAccess;
using ChimeraWebsite.Areas.Admin.Helpers;
using Chimera.Core.Report;
using Chimera.Entities.Report.ReportSummary;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class DashboardController : CompanyCommons.AbstractClasses.MasterController
    {
        /// <summary>
        /// Index of dashboard controller.
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess] 
        public ActionResult Index(string selectedPageType, PageReportType? pageReportType, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                if (selectedPageType == null)
                {
                    selectedPageType = string.Empty;
                }

                if (pageReportType == null)
                {
                    pageReportType = PageReportType.DAY;
                }

                if (dateFrom == null)
                {
                    dateFrom = DateTime.UtcNow.AddDays(-7);
                }

                if (dateTo == null)
                {
                    dateTo = DateTime.UtcNow;
                }

                if (ChimeraWebsite.Helpers.AppSettings.AllowPageReportRecording)
                {
                    ViewBag.PageReportSummary = ChimeraWebsite.Helpers.AppCache.GetPageReportSummary(selectedPageType, pageReportType.Value, dateFrom.Value, dateTo.Value);

                    ViewBag.UniquePageTypes = Chimera.DataAccess.ReportDAO.LoadUniquePageTypes();
                }

                ViewBag.NotificationList = DashboardNotificationDAO.LoadDashboardList(SiteContext.User.RoleList);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.Dashboard() ", e);
            }

            ViewBag.selectedPageType = selectedPageType;
            ViewBag.pageReportType = pageReportType.Value;
            ViewBag.dateFrom = dateFrom.Value;
            ViewBag.dateTo = dateTo.Value;

            return View();
        }

        /// <summary>
        /// User wants to force refresh the report
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        [AdminUserAccess]
        public ActionResult RefreshReport(string cacheKey)
        {
            PageReportCache ReportCache = ChimeraWebsite.Helpers.AppCache.RefreshPageReportSummary(cacheKey);

            if (ReportCache != null)
            {
                return RedirectToAction("Index", "Dashboard", new { selectedPageType = ReportCache.SelectedPageType, pageReportType = ReportCache.PageReportType, dateFrom = ReportCache.DateFrom, dateTo = ReportCache.DateTo });
            }

            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// Called to dismiss a notification
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess]
        public ActionResult DismissNotification(string id)
        {
            DashboardNotificationDAO.Delete(id);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
