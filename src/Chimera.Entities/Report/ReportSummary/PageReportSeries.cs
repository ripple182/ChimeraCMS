using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Report.ReportSummary
{
    public class PageReportSeries
    {
        public string name { get; set; }

        public List<int> data { get; set; }

        public PageReportSeries() 
        { 
            name = string.Empty;
            data = new List<int>();
        }
    }
}
