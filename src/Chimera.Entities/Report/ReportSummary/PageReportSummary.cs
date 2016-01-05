using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Report.ReportSummary
{
    public class PageReportSummary
    {
        public int TotalVisits { get; set; }

        public int TotalUniqueVistors { get; set; }

        public int TotalPageVisits { get; set; }

        public double PageViewsPerVisit { get; set; }

        public TimeSpan AverageVisitDuration { get; set; }

        public HashSet<string> MasterCategoryList { get; set; }

        public List<PageReportSeries> PageReportSeriesList { get; set; }

        public Dictionary<string, int> BrowserSummary { get; set; }

        public Dictionary<string, int> OperatingSystemSummary { get; set; }

        public PageReportSummary()
        {
            TotalVisits = 0;
            TotalUniqueVistors = 0;
            TotalPageVisits = 0;
            PageViewsPerVisit = 0;
            AverageVisitDuration = new TimeSpan(0,0,0);
            MasterCategoryList = new HashSet<string>();
            PageReportSeriesList = new List<PageReportSeries>();
            BrowserSummary = new Dictionary<string, int>();
            OperatingSystemSummary = new Dictionary<string, int>();
        }

        public string AverageVisitDurationFormatted()
        {
            string Hours = AverageVisitDuration.Hours.ToString();
            string Minutes = AverageVisitDuration.Minutes.ToString();
            string Seconds = AverageVisitDuration.Seconds.ToString();

            if (Hours.Length == 1)
            {
                Hours = "0" + Hours;
            }

            if (Minutes.Length == 1)
            {
                Minutes = "0" + Minutes;
            }

            if (Seconds.Length == 1)
            {
                Seconds = "0" + Seconds;
            }

            return Hours + ":" + Minutes + ":" + Seconds;
        }
    }
}
