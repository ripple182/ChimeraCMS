using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chimera.Entities.Website
{
    public class NavigationMenuLink
    {
        
        /// <summary>
        /// Actual text of the link
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Whether the link is to a chimera page or not
        /// </summary>
        public string ChimeraPageUrl { get; set; }

        /// <summary>
        /// Actual url of the link
        /// </summary>
        public string RealUrl { get; set; }

        /// <summary>
        /// Whether the link opens in the current window or a new tab
        /// </summary>
        public string LinkAction { get; set; }

        /// <summary>
        /// The child links of exist
        /// </summary>
        public List<NavigationMenuLink> ChildNavLinks { get; set; }

        /// <summary>
        /// Default Construcor.
        /// </summary>
        public NavigationMenuLink()
        {
            Text = string.Empty;
            ChimeraPageUrl = string.Empty;
            RealUrl = string.Empty;
            LinkAction = "";
            ChildNavLinks = new List<NavigationMenuLink>();
        }
    }
}
