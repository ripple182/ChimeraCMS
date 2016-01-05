using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities;

namespace Chimera.Entities.Settings
{
    public class Setting
    {
        /// <summary>
        /// The actual key for the setting.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// A user friendly name for the setting
        /// </summary>
        public string UserFriendlyName { get; set; }

        /// <summary>
        /// Description of what the setting actually does.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The string value, will be comma delimted if multiple values possible
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The data entry type, text input, textarea, dropdown menu, checkboxes, etc.
        /// </summary>
        public DataEntryType EntryType { get; set; }

        /// <summary>
        /// The key of StaticProperties we need to gather before loading the edit settings page
        /// </summary>
        public string DataEntryStaticPropertyKey { get; set; }

        /// <summary>
        /// List of settings for the "setting", mostly used only for SEO relevant settings
        /// </summary>
        public List<SettingAttribute> SettingAttributeList { get; set; }

        public Setting()
        {
            Key = string.Empty;
            UserFriendlyName = string.Empty;
            Description = string.Empty;
            Value = string.Empty;
            EntryType = DataEntryType.SmallText;
            DataEntryStaticPropertyKey = string.Empty;
            SettingAttributeList = new List<SettingAttribute>();
        }
    }
}
