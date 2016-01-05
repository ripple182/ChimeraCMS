using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Page
{
    /// <summary>
    /// Class used to summarize page revisions.
    /// </summary>
    public class PageRevision
    {
        /// <summary>
        /// The MongoDB Bson ObjectId of the page revision record in the pages collection.
        /// </summary>
        public string Id { get; set; }

        public string PageTitle { get; set; }

        public string PageFriendlyURL { get; set; }

        public bool Published { get; set; }

        /// <summary>
        /// The time this page revision was created.
        /// </summary>
        public DateTime ModifiedDateUTC { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PageRevision()
        {
            Id = string.Empty;
            PageTitle = string.Empty;
            PageFriendlyURL = string.Empty;
            Published = false;
            ModifiedDateUTC = DateTime.MinValue;
        }
    }
}
