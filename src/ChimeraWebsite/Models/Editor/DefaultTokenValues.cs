using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChimeraWebsite.Models.Editor
{
    public static class DefaultTokenValues
    {
        private const string APP_CACHE_KEY = "ChimeraDefaultTokenValueDictionary";

        private const string APP_START_FILE_PATH = "~/App_Data/DefaultChimeraModuleValues.txt";
          
        /// <summary>
        /// Get the dictionary of key and values for the default token/value from the app cache.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(ControllerContext controllerContext, HttpContextBase context)
        {
            if (context.Application[APP_CACHE_KEY] == null)
            {
                Initialize(controllerContext, context);
            }

            return (Dictionary<string, string>) context.Application[APP_CACHE_KEY] ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Load all the key value pairs of the default values into the app cache
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="context"></param>
        private static void Initialize(ControllerContext controllerContext, HttpContextBase context)
        {
            try
            {
                Dictionary<string, string> AppCacheDictionary = new Dictionary<string, string>();
                
                List<string> ListOfLines = CompanyCommons.FileManagement.Disk.ReadEachFileLineIntoList(context.Request.RequestContext.HttpContext.Server.MapPath(APP_START_FILE_PATH));

                foreach (var Line in ListOfLines)
                {
                    AddKeyAndValueFromLine(AppCacheDictionary, Line);
                }

                context.Application[APP_CACHE_KEY] = AppCacheDictionary;
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Models.Editor.DefaultTokenValues.Initialize(): ", e);
            }
        }

        /// <summary>
        /// Add the key and value from the line into the dictionary.
        /// </summary>
        /// <param name="appCacheDictionary"></param>
        /// <param name="line"></param>
        private static void AddKeyAndValueFromLine(Dictionary<string, string> appCacheDictionary, string line)
        {
            int idx = line.IndexOf("=");

            if (idx > 0)
            {
                appCacheDictionary.Add(line.Substring(0, idx), line.Substring(idx + 1, line.Length - idx - 1).Trim());
            }
        }
    }
}