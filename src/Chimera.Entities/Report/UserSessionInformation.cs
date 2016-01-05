using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Report
{
    public class UserSessionInformation
    {
        public string IpAddress { get; set; }

        public string BrowserNameAndVersion { get; set; }

        public string OperatingSystem { get; set; }

        public Guid SessionId { get; set; }

        public bool IsBot { get; set; }

        public DateTime LastDatePageRecordedUTC { get; set; }

        public UserSessionInformation()
        {
            IpAddress = string.Empty;
            BrowserNameAndVersion = string.Empty;
            OperatingSystem = string.Empty;
            SessionId = Guid.Empty;
            IsBot = false;
            LastDatePageRecordedUTC = DateTime.MinValue;
        }
    }
}
