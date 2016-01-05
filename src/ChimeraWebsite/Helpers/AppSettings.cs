using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CM = System.Configuration.ConfigurationManager;

namespace ChimeraWebsite.Helpers
{
    public static class AppSettings 
    {
        public static string BaseWebsiteURL { get; set; }

        public static bool AllowEcommerce { get; set; }

        public static bool AllowPageReportRecording { get; set; }

        public static bool InDeveloperEditMode { get; set; }

        public static bool InProductionMode { get; set; }

        public static string ChimeraTemplate { get; set; }

        public static string PRODUCTION_EDITOR_CDN_URL { get; set; }

        public static string PRODUCTION_ADMIN_CDN_URL { get; set; }

        public static string PRODUCTION_TEMPLATE_CDN_URL { get; set; }

        public static string PRODUCTION_GLOBAL_CDN_URL { get; set; }

        static AppSettings()
        {
            PRODUCTION_EDITOR_CDN_URL = CM.AppSettings["PRODUCTION_EDITOR_CDN_URL"];
            PRODUCTION_ADMIN_CDN_URL = CM.AppSettings["PRODUCTION_ADMIN_CDN_URL"];
            PRODUCTION_TEMPLATE_CDN_URL = CM.AppSettings["PRODUCTION_TEMPLATE_CDN_URL"];
            PRODUCTION_GLOBAL_CDN_URL = CM.AppSettings["PRODUCTION_GLOBAL_CDN_URL"];
            BaseWebsiteURL = CM.AppSettings["BaseWebsiteURL"];
            ChimeraTemplate = CM.AppSettings["Chimera_Template"];
            InProductionMode = !string.IsNullOrWhiteSpace(CM.AppSettings["InProductionMode"]) ? Boolean.Parse(CM.AppSettings["InProductionMode"]) : false;
            AllowEcommerce = !string.IsNullOrWhiteSpace(CM.AppSettings["ALLOW_ECOMMERCE"]) ? Boolean.Parse(CM.AppSettings["ALLOW_ECOMMERCE"]) : false;
            AllowPageReportRecording = !string.IsNullOrWhiteSpace(CM.AppSettings["ALLOW_PAGE_REPORT_RECORDING"]) ? Boolean.Parse(CM.AppSettings["ALLOW_PAGE_REPORT_RECORDING"]) : false;
            InDeveloperEditMode = !string.IsNullOrWhiteSpace(CM.AppSettings["InDeveloperEditMode"]) ? Boolean.Parse(CM.AppSettings["InDeveloperEditMode"]) : false;
        }
    }
}