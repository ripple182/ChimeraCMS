using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;
using Chimera.Entities.Settings;

namespace Chimera.DataAccess
{
    public static class SettingGroupDAO
    {
        private const string COLLECTION_NAME = "SettingGroups";

        /// <summary>
        /// Save or update a setting group
        /// </summary>
        /// <param name="staticProperty"></param>
        /// <returns></returns>
        public static bool Save(SettingGroup settingGroup)
        {
            return Execute.Save<SettingGroup>(COLLECTION_NAME, settingGroup);
        }

        /// <summary>
        /// Load all the setting groups for the admin nav bar so users can see which settings to edit
        /// </summary>
        /// <returns></returns>
        public static List<SettingGroup> LoadAll()
        {
            MongoCollection<SettingGroup> Collection = Execute.GetCollection<SettingGroup>(COLLECTION_NAME);

            return (List<SettingGroup>) (from e in Collection.AsQueryable<SettingGroup>() orderby e.UserFriendlyName select e).ToList();
        }

        /// <summary>
        /// Load a single setting group object by its unique group name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static SettingGroup LoadSettingGroupByName(string groupName)
        {
            MongoCollection<SettingGroup> Collection = Execute.GetCollection<SettingGroup>(COLLECTION_NAME);

            SettingGroup SettGroup = (from e in Collection.AsQueryable<SettingGroup>() where e.GroupKey == groupName select e).FirstOrDefault();

            if (SettGroup == null)
            {
                SettGroup = new SettingGroup();
            }

            return SettGroup;
        }

        /// <summary>
        /// Load a list of setting groups by passing in multiple group names
        /// </summary>
        /// <param name="groupNames"></param>
        /// <returns></returns>
        public static List<SettingGroup> LoadByMultipleGroupNames(List<string> groupNames)
        {
            MongoCollection<SettingGroup> Collection = Execute.GetCollection<SettingGroup>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<SettingGroup>() orderby e.GroupKey select e).Where(e => groupNames.Contains(e.GroupKey)).ToList();
        }

        public static SettingGroup LoadSettingGroupById(string id)
        {
            MongoCollection<SettingGroup> Collection = Execute.GetCollection<SettingGroup>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<SettingGroup>() where e.Id == id select e).FirstOrDefault();
        }
    }
}
