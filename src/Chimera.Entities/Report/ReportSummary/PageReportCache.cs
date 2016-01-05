using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Report.ReportSummary
{
    public class PageReportCache
    {
        public string CacheKey { get; set; }

        public DateTime LastAccessedUtc { get; set; }

        public PageReportSummary ReportSummary { get; set; }

        public string SelectedPageType { get; set; }

        public PageReportType PageReportType { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public PageReportCache()
        {
            SelectedPageType = string.Empty;
            PageReportType = Report.ReportSummary.PageReportType.DAY;
            DateFrom = DateTime.MinValue;
            DateTo = DateTime.MinValue;
            CacheKey = string.Empty;
            LastAccessedUtc = DateTime.MinValue;
            ReportSummary = new PageReportSummary();
        }

        public PageReportCache(string cacheKey, DateTime lastAccessedUtc, PageReportSummary pageReport, string selectedPageType, PageReportType pageReportType, DateTime dateFrom, DateTime dateTo)
        {
            CacheKey = cacheKey;
            LastAccessedUtc = lastAccessedUtc;
            ReportSummary = pageReport;
            SelectedPageType = selectedPageType;
            PageReportType = pageReportType;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
