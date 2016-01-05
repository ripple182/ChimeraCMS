using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChimeraWebsite.Models.Editor
{
    public class ColumnModuleType
    {
        /// <summary>
        /// key used to access the list of module types from app cahce.
        /// </summary>
        private const string APP_CACHE_KEY = "ColumnModuleTypes";

        /// <summary>
        /// The display name the client will see when selection the module.
        /// </summary>
        public string DisplayName { get; set; } 

        /// <summary>
        /// A description of the module type.
        /// </summary>
        public string DisplayDescription { get; set; }

        /// <summary>
        /// The list of categories this module belongs to.
        /// </summary>
        public HashSet<string> Categories { get; set; }

        /// <summary>
        /// The default column module model.
        /// </summary>
        public Models.ColumnModuleModel ColumnModuleModel = new Models.ColumnModuleModel();

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ColumnModuleType()
        {
            DisplayName = string.Empty;
            DisplayDescription = string.Empty;
            Categories = new HashSet<string>();
            ColumnModuleModel = new Models.ColumnModuleModel();
        }

        /// <summary>
        /// Create a new column module type based on the 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="developmentName"></param>
        public ColumnModuleType(ControllerContext controllerContext, HttpContextBase context, string developmentName) : this()
        {
            List<ColumnModuleType> ColumnModuleTypeList = GetList(controllerContext, context);

            ColumnModuleType ColumnModuleType = ColumnModuleTypeList.Where(e => e.ColumnModuleModel.ColumnModule.ModuleTypeName.Equals(developmentName)).FirstOrDefault();

            DisplayName = ColumnModuleType.DisplayDescription;
            DisplayDescription = ColumnModuleType.DisplayDescription;
            Categories = ColumnModuleType.Categories;
            ColumnModuleModel = ColumnModuleType.ColumnModuleModel;
        }

        /// <summary>
        /// Load the list of possible modules from the app cache, if the app cache does not contain the module types then it will load and store them.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<ColumnModuleType> GetList(ControllerContext controllerContext, HttpContextBase context)
        {
            if (context.Application[APP_CACHE_KEY] == null)
            {
                CacheInitializer.InitializeEditorCache(controllerContext, context);
            }

            return (List<ColumnModuleType>) context.Application[APP_CACHE_KEY];
        }

        /// <summary>
        /// Add a new column module type to the app cache, should only be called from the CacheInitializer function.
        /// </summary>
        /// <param name="context"></param>
        public static void AddNew(HttpContextBase context, ColumnModuleType columnModuleType)
        {
            if (context.Application[APP_CACHE_KEY] == null)
            {
                context.Application[APP_CACHE_KEY] = new List<ColumnModuleType>();
            }

            ((List<ColumnModuleType>)context.Application[APP_CACHE_KEY]).Add(columnModuleType);
        }
    }
}