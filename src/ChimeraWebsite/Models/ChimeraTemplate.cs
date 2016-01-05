using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CM = System.Configuration.ConfigurationManager;

namespace ChimeraWebsite.Models
{
    public static class ChimeraTemplate
    {
        public static string TemplateName { get; set; }

        static ChimeraTemplate() 
        {
            TemplateName = CM.AppSettings["Chimera_Template"];
        }  
    }
}