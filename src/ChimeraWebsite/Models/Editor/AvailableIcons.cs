using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.Entities.Uploads;

namespace ChimeraWebsite.Models.Editor
{
    public static class AvailableIcons
    {
        private const string APP_CACHE_KEY = "AvailableIcons";

        private const string APP_START_FILE_PATH = "GlyphiconHalflings.txt";

        /// <summary>
        /// Called whenenver we wish to display the list of available icons, will auto initialize into app cache if does not exist.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<Icon> GetList(ControllerContext controllerContext, HttpContextBase context)
        {
            if (context.Application[APP_CACHE_KEY] == null)
            {
                 Initialize(controllerContext, context);
            }

            return (List<Icon>)context.Application[APP_CACHE_KEY] ?? new List<Icon>();
        }

        /// <summary>
        /// Load all the available icons into a list of strings into app cache
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="context"></param> 
        private static void Initialize(ControllerContext controllerContext, HttpContextBase context)
        {
            try
            {
                List<Icon> AvailableIcons = new List<Icon>();

                string FullFilePath = "~/Templates/" + ChimeraWebsite.Helpers.AppSettings.ChimeraTemplate  + "/App_Data/" + APP_START_FILE_PATH;

                List<string> RawIconList = CompanyCommons.FileManagement.Disk.ReadEachFileLineIntoList(context.Request.RequestContext.HttpContext.Server.MapPath(FullFilePath));

                foreach (var rawIcon in RawIconList)  
                {
                    string DisplayName = rawIcon.Replace("glyphicon-", "").Replace("glyphicon", "").Replace("icon-", "");

                    AvailableIcons.Add(new Icon { ClassValue = rawIcon, DisplayName = DisplayName });
                }

                context.Application[APP_CACHE_KEY] = AvailableIcons;
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Models.Editor.AvailableIcons.Initialize(): ", e);
            }
        }
    }
}