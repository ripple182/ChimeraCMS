using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities
{
    public static class ProductOverrideSEO
    {
        
        /// <summary>
        /// Dictionary, the key is the SettingGroupKey, the value is the SettingKey.  Contains a list of SEO settings to override whenever viewing a product
        /// </summary>
        public static readonly HashSet<string> OverrideGlobalSEOList = new HashSet<string> 
        {  
            "CommonSEOSettings;Description",
            "TwitterSEOSettings;Card",
            "TwitterSEOSettings;Title",
            "TwitterSEOSettings;Description",
            "TwitterSEOSettings;Image",
            "GooglePlusSEOSettings;Name",
            "GooglePlusSEOSettings;Description",
            "GooglePlusSEOSettings;Image",
            "FacebookSEOSettings;Title",
            "FacebookSEOSettings;Url",
            "FacebookSEOSettings;Image",
            "FacebookSEOSettings;Description",
            "FacebookSEOSettings;ModifiedTime"
        };
    }
}
