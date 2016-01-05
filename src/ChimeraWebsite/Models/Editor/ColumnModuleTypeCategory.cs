using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace ChimeraWebsite.Models.Editor
{
    public class ColumnModuleTypeCategory
    {
        /// <summary>
        /// key use to access list of the possible categories.
        /// </summary>
        private const string APP_CACHE_KEY = "ColumnModuleTypeCategories";

        /// <summary>
        /// Load a list of all possible categories.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static HashSet<string> GetList(ControllerContext controllerContext, HttpContextBase context)
        {
            if (context.Application[APP_CACHE_KEY] == null)
            {
                CacheInitializer.InitializeEditorCache(controllerContext, context);
            }

            return (HashSet<string>)context.Application[APP_CACHE_KEY];
        }

        /// <summary>
        /// Add a new column module type to the app cache, should only be called from the CacheInitializer function.
        /// </summary>
        /// <param name="context"></param>
        public static void AddNew(HttpContextBase context, string category)
        {
            if (context.Application[APP_CACHE_KEY] == null)
            {
                context.Application[APP_CACHE_KEY] = new HashSet<string>();
            }

            ((HashSet<string>)context.Application[APP_CACHE_KEY]).Add(category);
        }


    }
}