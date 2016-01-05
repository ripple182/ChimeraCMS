using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chimera.Entities.Property;
using Chimera.Entities.Settings;

namespace ChimeraWebsite.Areas.Admin.Models.PartialViews
{
    public class DataEntryFormField
    {
        public Setting Setting { get; set; }

        public StaticProperty StaticProperty { get; set; }
     
        public DataEntryFormField(Setting setting, StaticProperty staticProperty)
        {
            Setting = setting;
            StaticProperty = staticProperty;
        }
    }
}