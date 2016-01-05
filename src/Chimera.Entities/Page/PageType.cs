using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Page
{
    /// <summary>
    /// Class used to load a lite of existing page types, so pretty much load the most recent page revision for each page type.
    /// </summary>
    public class PageType
    {
        /// <summary>
        /// Unique Page Id that ties page revisions together.
        /// </summary>
        public Guid PageId { get; set; }

        /// <summary>
        /// The actual user friendly page title
        /// </summary>
        public string PageTitle { get; set; }

        public string PageFriendlyURL { get; set; }

        public bool Published { get; set; }

        public bool LatestVersionPublished { get; set; }

        /// <summary>
        /// Last time this page type was altered.
        /// </summary>
        public DateTime ModifiedDateUTC { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PageType()
        {
            PageId = Guid.Empty;
            PageTitle = string.Empty;
            Published = false;
            LatestVersionPublished = false;
            ModifiedDateUTC = DateTime.MinValue;
            PageFriendlyURL = string.Empty;
        }
    }
}
