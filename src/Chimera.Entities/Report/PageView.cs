using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chimera.Entities.Report
{
    public class PageView
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string PageFriendlyURL { get; set; }

        public DateTime PageOpenedDateUTC { get; set; }

        public string IpAddress { get; set; }

        public string BrowserNameAndVersion { get; set; }

        public string OperatingSystem { get; set; }

        public Guid SessionId { get; set; }

        public DateTime PageExitDateUTC { get; set; }

        public PageView()
        {
            Id = string.Empty;
            PageFriendlyURL = string.Empty;
            PageOpenedDateUTC = DateTime.MinValue;
            IpAddress = string.Empty;
            BrowserNameAndVersion = string.Empty;
            OperatingSystem = string.Empty;
            SessionId = Guid.Empty;
            PageExitDateUTC = DateTime.MinValue;
        }

        public PageView(UserSessionInformation userInfo, string pageFriendlyURL)
        {
            PageFriendlyURL = pageFriendlyURL;
            IpAddress = userInfo.IpAddress;
            BrowserNameAndVersion = userInfo.BrowserNameAndVersion;
            OperatingSystem = userInfo.OperatingSystem;
            SessionId = userInfo.SessionId;
        }
    }
}
