using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CompanyCommons.Entities;

namespace Chimera.Entities.Settings
{
    public class SettingGroup
    {
        /// <summary>
        /// MongoDB ID
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The DB key to identify the setting group, i.e. "OVERALL_WEBSITE_SETTINGS", "PAYPAL_SETTINGS", "MASTER_SEO", etc.
        /// </summary>
        public string GroupKey { get; set; }

        /// <summary>
        /// friendly name to appear to the client using the admin console
        /// </summary>
        public string UserFriendlyName { get; set; }

        /// <summary>
        /// friendly description of what the setting group means
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The category this setting group belongs to.
        /// </summary>
        public ParentCategoryType ParentCategory { get; set; }

        /// <summary>
        /// The actual list of settings that belong to this group
        /// </summary>
        public List<Setting> SettingsList { get; set; }

        public SettingGroup()
        {
            Id = string.Empty;
            GroupKey = string.Empty;
            UserFriendlyName = string.Empty;
            Description = string.Empty;
            ParentCategory = ParentCategoryType.OTHER;
            SettingsList = new List<Setting>();
        }

        /// <summary>
        /// Called whenever the developer wishes to grab a setting value by its setting key.
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public string GetSettingVal(string settingKey)
        {
            string ReturnValue = string.Empty;

            try
            {

                if (SettingsList != null && SettingsList.Count > 0)
                {
                    Setting Sett = SettingsList.Where(e => e.Key.ToUpper().Equals(settingKey.ToUpper())).FirstOrDefault();

                    if (Sett != null)
                    {
                        ReturnValue = Sett.Value;
                    }
                }
            }
            catch (Exception e)
            {
                //do nothing
            }

            return ReturnValue;
        }

        /// <summary>
        /// Validate this setting group before saving, if return list of user messages is empty then can save
        /// </summary>
        /// <returns></returns>
        public List<WebUserMessage> Validate()
        {
            List<WebUserMessage> WebUserMessageList = new List<WebUserMessage>();

            string FailedType = WebUserMessage.WebUserMessageType.FAILED_MESSAGE_TYPE;

            //make sure key is not empty
            if (string.IsNullOrWhiteSpace(GroupKey))
            {
                WebUserMessageList.Add(new WebUserMessage("Group Key field can't be empty or whitespace.", FailedType));
            }

            //make sure friendly name is not empty
            if (string.IsNullOrWhiteSpace(UserFriendlyName))
            {
                WebUserMessageList.Add(new WebUserMessage("Friendly Name field can't by empty or whitespace.", FailedType));
            }

            //make sure has at least 1 setting
            if (SettingsList != null && SettingsList.Count > 0)
            {
                bool SettingKeyErrorMessageSet = false;
                bool FriendlyNameErrorMessageSet = false;

                Dictionary<string, int> ProcessedSettingKeys = new Dictionary<string, int>();

                foreach (var Setting in SettingsList)
                {
                    //make sure all settings have a key
                    if (string.IsNullOrWhiteSpace(Setting.Key) && !SettingKeyErrorMessageSet)
                    {
                        SettingKeyErrorMessageSet = true;
                        WebUserMessageList.Add(new WebUserMessage("Editable Setting 'Key' field can't be empty or whitespace.", FailedType));
                    }

                    //make sure all settings have a friendly name
                    if (string.IsNullOrWhiteSpace(Setting.UserFriendlyName) && !FriendlyNameErrorMessageSet)
                    {
                        FriendlyNameErrorMessageSet = true;
                        WebUserMessageList.Add(new WebUserMessage("Editable Setting 'Friendly Name' field can't be empty or whitespace.", FailedType));
                    }

                    //make sure no duplicate settings
                    if (ProcessedSettingKeys.ContainsKey(Setting.Key))
                    {
                        ProcessedSettingKeys[Setting.Key]++;
                    }
                    else
                    {
                        ProcessedSettingKeys.Add(Setting.Key, 1);
                    }
                }

                //make sure no duplicate settings
                if (ProcessedSettingKeys != null && ProcessedSettingKeys.Count > 0)
                {
                    foreach (var Value in ProcessedSettingKeys.Values)
                    {
                        if (Value > 1)
                        {
                            WebUserMessageList.Add(new WebUserMessage("Editable Setting 'Key' must be unique, no duplicants.", FailedType));
                            break;
                        }
                    }
                }
            }
            else
            {
                WebUserMessageList.Add(new WebUserMessage("At least 1 'Editable Setting' is required.", FailedType));
            }

            return WebUserMessageList;
        }
    }
}
