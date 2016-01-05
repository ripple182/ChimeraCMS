using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Report;
using Chimera.Entities.Report.ReportSummary;
using Chimera.DataAccess;
using System.Globalization;

namespace Chimera.Core.Report
{
    public static class PageReport
    {
        /// <summary>
        /// Generate the all page report summary to create a line chart
        /// </summary>
        /// <param name="pageReportType"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static PageReportSummary GeneratePageReport(PageReportType pageReportType, DateTime dateFrom, DateTime dateTo, string selectedPage = "")
        {
            dateFrom = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 0, 0, 1);
            dateTo = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 23, 59, 59);

            PageReportSummary PageReportSummary = new PageReportSummary();

            //add 3 lists to represent the all categories
            if (string.IsNullOrWhiteSpace(selectedPage) || "all".Equals(selectedPage))
            {
                PageReportSummary.PageReportSeriesList.Add(new PageReportSeries { name = "Page Views", data = new List<int>() });
                PageReportSummary.PageReportSeriesList.Add(new PageReportSeries { name = "Website Visits", data = new List<int>() });
                PageReportSummary.PageReportSeriesList.Add(new PageReportSeries { name = "Unique Website Visits", data = new List<int>() });
            }
            else
            {
                PageReportSummary.PageReportSeriesList.Add(new PageReportSeries { name = "Page Views", data = new List<int>() });
            }

            List<PageView> PageViewList = ReportDAO.LoadPageViews(dateFrom, dateTo, selectedPage);

            if (PageViewList != null && PageViewList.Count > 0)
            {
                while (dateFrom <= dateTo)
                {
                    string DateTimeFormat = GetDateTimeCategoryToString(pageReportType);

                    string Category = dateFrom.ToString(DateTimeFormat);

                    PageReportSummary.MasterCategoryList.Add(Category);

                    if (string.IsNullOrWhiteSpace(selectedPage) || "all".Equals(selectedPage))
                    {
                        ProcessAllPageReportRow(PageReportSummary, PageViewList, DateTimeFormat, Category);
                    }
                    else
                    {
                        ProcessSpecificPageReportRow(PageReportSummary, PageViewList, DateTimeFormat, Category);
                    }

                    dateFrom = IncreaseDateTime(pageReportType, dateFrom);
                }

                ProcessBasicStatistics(PageReportSummary, PageViewList);
                ProcessBrowserSummary(PageReportSummary, PageViewList);
                ProcessOperatingSystemSummary(PageReportSummary, PageViewList);
            }
            return PageReportSummary;
        }

        /// <summary>
        /// # of sessions created per browser, user can switch browsers on the same ip address
        /// </summary>
        /// <param name="pageReportSummary"></param>
        /// <param name="pageViewList"></param>
        private static void ProcessBrowserSummary(PageReportSummary pageReportSummary, List<PageView> pageViewList)
        {
            List<string> EntityList = pageViewList.Select(e => e.BrowserNameAndVersion).Distinct().ToList();

            foreach (var Entity in EntityList.OrderBy(e => e))
            {
                if (!pageReportSummary.BrowserSummary.ContainsKey(Entity))
                {
                    pageReportSummary.BrowserSummary.Add(Entity, pageViewList.Where(e => e.BrowserNameAndVersion.Equals(Entity)).Select(e => e.SessionId).Distinct().Count());
                }
            }
        }

        /// <summary>
        /// # of different ip addresses per operating system, user is locked into an operating system per ip address
        /// </summary>
        /// <param name="pageReportSummary"></param>
        /// <param name="pageViewList"></param>
        private static void ProcessOperatingSystemSummary(PageReportSummary pageReportSummary, List<PageView> pageViewList)
        {
            List<string> EntityList = pageViewList.Select(e => e.OperatingSystem).Distinct().ToList();

            foreach (var Entity in EntityList.OrderBy(e => e))
            {
                if (!pageReportSummary.OperatingSystemSummary.ContainsKey(Entity))
                {
                    pageReportSummary.OperatingSystemSummary.Add(Entity, pageViewList.Where(e => e.OperatingSystem.Equals(Entity)).Select(e => e.IpAddress).Distinct().Count());
                }
            }
        }

        /// <summary>
        /// Get the total visits, total page views, total unique visitors, avg pages per session, avg session time
        /// </summary>
        /// <param name="pageReportSummary"></param>
        /// <param name="pageViewList"></param>
        private static void ProcessBasicStatistics(PageReportSummary pageReportSummary, List<PageView> pageViewList, string selectedPage = "")
        {
            pageReportSummary.TotalPageVisits = pageViewList.Count;
            pageReportSummary.TotalVisits = pageViewList.Select(e => e.SessionId).Distinct().Count();
            pageReportSummary.TotalUniqueVistors = pageViewList.Select(e => e.IpAddress).Distinct().Count();

            if (string.IsNullOrWhiteSpace(selectedPage) || "all".Equals(selectedPage))
            {
                ProcessAverageSessionTime(pageReportSummary, pageViewList);
            }
            else
            {
                ProcessAveragePageViewTime(pageReportSummary, pageViewList);
            }
        }

        /// <summary>
        /// When viewing an "all" page report process the average session time and pages per visit
        /// </summary>
        /// <param name="pageReportSummary"></param>
        /// <param name="pageViewList"></param>
        private static void ProcessAverageSessionTime(PageReportSummary pageReportSummary, List<PageView> pageViewList)
        {
            Dictionary<Guid, int> PageVisitsPerSession = new Dictionary<Guid, int>();

            foreach (var PageV in pageViewList)
            {
                if (!PageVisitsPerSession.ContainsKey(PageV.SessionId))
                {
                    PageVisitsPerSession.Add(PageV.SessionId, 0);
                }

                PageVisitsPerSession[PageV.SessionId]++;
            }

            pageReportSummary.PageViewsPerVisit = PageVisitsPerSession.Values.Average();

            List<TimeSpan> TimeSpanList = new List<TimeSpan>();

            foreach (var SessionId in PageVisitsPerSession.Keys)
            {
                DateTime StartDate = pageViewList.Where(e => e.SessionId.Equals(SessionId)).OrderBy(e => e.PageOpenedDateUTC).Select(e => e.PageOpenedDateUTC).FirstOrDefault();
                DateTime EndDate = pageViewList.Where(e => e.SessionId.Equals(SessionId) && !e.PageExitDateUTC.Equals(DateTime.MinValue)).OrderByDescending(e => e.PageExitDateUTC).Select(e => e.PageExitDateUTC).FirstOrDefault();

                if (!StartDate.Equals(DateTime.MinValue) && !EndDate.Equals(DateTime.MinValue))
                {
                    TimeSpanList.Add(EndDate.Subtract(StartDate));
                }
            }

            pageReportSummary.AverageVisitDuration = new TimeSpan(Convert.ToInt64(TimeSpanList.Average(timeSpan => timeSpan.Ticks)));
        }

        /// <summary>
        /// When a specific page is selected process its average page view time
        /// </summary>
        private static void ProcessAveragePageViewTime(PageReportSummary pageReportSummary, List<PageView> pageViewList)
        {
            List<TimeSpan> TimeSpanList = new List<TimeSpan>();

            foreach(var PageV in pageViewList)
            {
                if (!PageV.PageOpenedDateUTC.Equals(DateTime.MinValue) && !PageV.PageExitDateUTC.Equals(DateTime.MinValue))
                {
                    TimeSpanList.Add(PageV.PageExitDateUTC.Subtract(PageV.PageOpenedDateUTC));
                }
            }

            pageReportSummary.AverageVisitDuration = new TimeSpan(Convert.ToInt64(TimeSpanList.Average(timeSpan => timeSpan.Ticks)));
        }

        /// <summary>
        /// Only process page views for a specific page report
        /// </summary>
        /// <param name="pageReportSummary"></param>
        /// <param name="pageViewList"></param>
        /// <param name="dateTimeFormat"></param>
        /// <param name="category"></param>
        private static void ProcessSpecificPageReportRow(PageReportSummary pageReportSummary, List<PageView> pageViewList, string dateTimeFormat, string category)
        {
            //total count of users this day
            int PageViewCount = pageViewList.Count(e => e.PageOpenedDateUTC.ToString(dateTimeFormat).Equals(category));

            pageReportSummary.PageReportSeriesList[0].data.Add(PageViewCount);
        }

        /// <summary>
        /// Process a category row for the all page report
        /// </summary>
        /// <param name="pageReportSummary"></param>
        /// <param name="pageViewList"></param>
        /// <param name="dateTimeFormat"></param>
        /// <param name="category"></param>
        private static void ProcessAllPageReportRow(PageReportSummary pageReportSummary, List<PageView> pageViewList, string dateTimeFormat, string category)
        {
            //total count of users this day
            int PageViewCount = pageViewList.Count(e => e.PageOpenedDateUTC.ToString(dateTimeFormat).Equals(category));

            //website visits (count # sessions on that day)
            int WebsiteVisitCount = pageViewList.Where(e => e.PageOpenedDateUTC.ToString(dateTimeFormat).Equals(category)).Select(e => e.SessionId).Distinct().Count();

            //unique website vists (count # distnct sessions by ip on that day)
            int UniqueWebsiteVisitCount = pageViewList.Where(e => e.PageOpenedDateUTC.ToString(dateTimeFormat).Equals(category)).Select(e => e.IpAddress).Distinct().Count();

            pageReportSummary.PageReportSeriesList[0].data.Add(PageViewCount);
            pageReportSummary.PageReportSeriesList[1].data.Add(WebsiteVisitCount);
            pageReportSummary.PageReportSeriesList[2].data.Add(UniqueWebsiteVisitCount);
        }

        /// <summary>
        /// Increase the date from according to the page report type
        /// </summary>
        /// <param name="pageReportType"></param>
        /// <param name="dateFrom"></param>
        /// <returns></returns>
        private static DateTime IncreaseDateTime(PageReportType pageReportType, DateTime dateFrom)
        {
            switch (pageReportType)
            {
                case PageReportType.HOURLY:
                    return dateFrom.AddHours(1);
                case PageReportType.DAY:
                    return dateFrom.AddDays(1);
                case PageReportType.MONTH:
                    return dateFrom.AddMonths(1);
            }

            return dateFrom;
        }

        /// <summary>
        /// Get the to string used for date time to string
        /// </summary>
        /// <param name="pageReportType"></param>
        /// <returns></returns>
        private static string GetDateTimeCategoryToString(PageReportType pageReportType)
        {
            switch (pageReportType)
            {
                case PageReportType.HOURLY:
                    return "h tt";
                case PageReportType.DAY:
                    return "MMM dd";
                case PageReportType.MONTH:
                    return "MMM yyyy";
            }

            return "";
        }
    }
}
