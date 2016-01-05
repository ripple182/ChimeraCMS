using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ChimeraWebsite.Models
{
    public class PartialColumnModuleModel  
    {
        public string UniqueIdentifier { get; set; }

        public ColumnModuleModel ColumnModuleModel { get; set; }

        public PartialColumnModuleModel() 
        {
            UniqueIdentifier = string.Empty;
            ColumnModuleModel = new ColumnModuleModel();
        }
    }
}