using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CM = System.Configuration.ConfigurationManager;

namespace ChimeraDatabaseInitialize.Processors
{
    public static class IProcessorFactory
    {
        private const string ADMIN_USER_ROLES = "ADMINUSERROLES";

        private const string ADMIN_USERS = "ADMINUSERS";

        private const string STATIC_PROPERTIES = "STATICPROPERTIES";

        private const string SETTING_GROUPS = "SETTINGGROUPS";

        private const string NAVIGATION_MENUS = "NAVIGATIONMENUS";

        /// <summary>
        /// Get the IProcessor needed to process this xml file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IProcessor GetProcessor(string filePath)
        {
            string SpecificProcessors = CM.AppSettings["UseSpecificProcessors"];

            IProcessor Processor = null;

            if (filePath.ToUpper().Contains(ADMIN_USER_ROLES) && string.IsNullOrWhiteSpace(SpecificProcessors) || !string.IsNullOrWhiteSpace(SpecificProcessors) && SpecificProcessors.Contains("AdminUserRolesProcessor"))
            {
                Processor = new AdminUserRolesProcessor(filePath);
            }
            else if (filePath.ToUpper().Contains(ADMIN_USERS) && string.IsNullOrWhiteSpace(SpecificProcessors) || !string.IsNullOrWhiteSpace(SpecificProcessors) && SpecificProcessors.Contains("AdminUserProcessor"))
            {
                Processor = new AdminUserProcessor(filePath);
            }
            else if (filePath.ToUpper().Contains(STATIC_PROPERTIES) && string.IsNullOrWhiteSpace(SpecificProcessors) || !string.IsNullOrWhiteSpace(SpecificProcessors) && SpecificProcessors.Contains("StaticPropertyProcessor"))
            {
                Processor = new StaticPropertyProcessor(filePath);
            }
            else if (filePath.ToUpper().Contains(SETTING_GROUPS) && string.IsNullOrWhiteSpace(SpecificProcessors) || !string.IsNullOrWhiteSpace(SpecificProcessors) && SpecificProcessors.Contains("SettingGroupsProcessor"))
            {
                Processor = new SettingGroupsProcessor(filePath);
            }
            else if (filePath.ToUpper().Contains(NAVIGATION_MENUS) && string.IsNullOrWhiteSpace(SpecificProcessors) || !string.IsNullOrWhiteSpace(SpecificProcessors) && SpecificProcessors.Contains("NavigationMenuProcessor"))
            {
                Processor = new NavigationMenuProcessor(filePath);
            }

            return Processor;
        }
    }
}
