using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chimera.Entities.Report;
using Chimera.Entities.Report.ReportSummary;
using Chimera.Core.Report;
using Chimera.Entities.Page;
using Chimera.DataAccess;

namespace ChimeraWebsite.Helpers
{
    public static class AppCache
    {
        /// <summary>
        /// App cache key for stored reports
        /// </summary>
        private const string PAGE_REPORT_APP_CACHE = "Page_Report_App_Cache_Key";

        /// <summary>
        /// App cache key for pages
        /// </summary>
        private const string PAGE_APP_CACHE = "PAGE_APP_CACHE_KEY";

        /// <summary>
        /// The page dictionary in the app cache
        /// </summary>
        private static Dictionary<string, Page> PageDictionary
        {
            get
            {
                return (Dictionary<string, Page>) HttpContext.Current.Application[PAGE_APP_CACHE] ?? new Dictionary<string, Page>();
            }
            set
            {
                HttpContext.Current.Application[PAGE_APP_CACHE] = value;
            }
        }

        /// <summary>
        /// The page report dictionary in the app cache
        /// </summary>
        private static Dictionary<string, PageReportCache> PageReportDictionary
        {
            get
            {
                return (Dictionary<string, PageReportCache>)HttpContext.Current.Application[PAGE_REPORT_APP_CACHE] ?? new Dictionary<string, PageReportCache>();
            }
            set
            {
                HttpContext.Current.Application[PAGE_REPORT_APP_CACHE] = value;
            }
        }

        /// <summary>
        /// Update the page in the cache if it exists in there post publish
        /// </summary>
        /// <param name="page"></param>
        public static void UpdatePageInCache(Page page)
        {
            Dictionary<string, Page> CurrentPageDictionary = PageDictionary;

            if (PageDictionary.ContainsKey(page.PageFriendlyURL))
            {
                CurrentPageDictionary[page.PageFriendlyURL] = page;

                PageDictionary = CurrentPageDictionary;
            }
        }

        /// <summary>
        /// Load the requested page from the app cache
        /// </summary>
        /// <param name="friendlyURL"></param>
        /// <returns></returns>
        public static Page GetPageFromCache(string friendlyURL)
        {
            Page Page = new Page();

            Dictionary<string, Page> CurrentPageDictionary = PageDictionary;

            if (!PageDictionary.ContainsKey(friendlyURL))
            {
                Page = PageDAO.LoadByURL(friendlyURL);

                if (!string.IsNullOrWhiteSpace(Page.Id))
                {
                    CurrentPageDictionary.Add(Page.PageFriendlyURL, Page);

                    PageDictionary = CurrentPageDictionary;
                }
            }
            else
            {
                Page = CurrentPageDictionary[friendlyURL];
            }

            return Page;
        }

        /// <summary>
        /// Force a refresh on a report in the cache
        /// </summary>
        /// <param name="cacheKey"></param>
        public static PageReportCache RefreshPageReportSummary(string cacheKey)
        {
            Dictionary<string, PageReportCache> CurrentPageReportDictionary = PageReportDictionary;

            if (CurrentPageReportDictionary.ContainsKey(cacheKey))
            {
                PageReportCache ReportCache = CurrentPageReportDictionary[cacheKey];

                ReportCache.LastAccessedUtc = DateTime.UtcNow.AddHours(1);

                ReportCache.ReportSummary = PageReport.GeneratePageReport(ReportCache.PageReportType, ReportCache.DateFrom, ReportCache.DateTo, ReportCache.SelectedPageType);

                CurrentPageReportDictionary[cacheKey] = ReportCache;

                PageReportDictionary = CurrentPageReportDictionary;

                return ReportCache;
            }

            return null;
        }

        /// <summary>
        /// Check the app cache and see if this report cache has expired.
        /// </summary>
        /// <param name="selectedPageType"></param>
        /// <param name="pageReportType"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static PageReportCache GetPageReportSummary(string selectedPageType, PageReportType pageReportType, DateTime dateFrom, DateTime dateTo)
        {
            string CacheKey = String.Format("{0}|{1}|{2}|{3}", selectedPageType.ToUpper(), pageReportType, dateFrom.ToString("MM/dd/yyyy"), dateTo.ToString("MM/dd/yyyy"));

            PageReportSummary ReportSummary = new PageReportSummary();

            Dictionary<string, PageReportCache> PageReportDictionary = (Dictionary<string, PageReportCache>)HttpContext.Current.Application[PAGE_REPORT_APP_CACHE] ?? new Dictionary<string, PageReportCache>();

            bool RunNewReport = false;

            if (PageReportDictionary.ContainsKey(CacheKey))
            {
                if (PageReportDictionary[CacheKey].LastAccessedUtc <= DateTime.UtcNow)
                {
                    RunNewReport = true;
                }
                else
                {
                    ReportSummary = PageReportDictionary[CacheKey].ReportSummary;
                }
            }
            else
            {
                RunNewReport = true;
            }

            if (RunNewReport)
            {
                ReportSummary = PageReport.GeneratePageReport(pageReportType, dateFrom, dateTo, selectedPageType);

                PageReportDictionary[CacheKey] = new PageReportCache(CacheKey, DateTime.UtcNow.AddHours(1), ReportSummary, selectedPageType, pageReportType, dateFrom, dateTo);

                HttpContext.Current.Application[PAGE_REPORT_APP_CACHE] = PageReportDictionary;
            }

            return PageReportDictionary[CacheKey];
        }
    }
}