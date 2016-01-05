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
using Chimera.Entities.Website;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class NavigationMenuDAO
    {
        private const string COLLECTION_NAME = "NavigationMenus";

        /// <summary>
        /// Save a navigation link
        /// </summary>
        /// <param name="navigationLink"></param>
        /// <returns></returns>
        public static bool Save(NavigationMenu masterNavigationLink)
        {
            return Execute.Save<NavigationMenu>(COLLECTION_NAME, masterNavigationLink);
        }

        /// <summary>
        /// Load a single nav menu
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static NavigationMenu LoadByKeyName(string keyName)
        {
            MongoCollection<NavigationMenu> Collection = Execute.GetCollection<NavigationMenu>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<NavigationMenu>() where e.KeyName == keyName select e).FirstOrDefault();
        }

        /// <summary>
        /// Load a single nav menu
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static NavigationMenu LoadById(string id)
        {
            MongoCollection<NavigationMenu> Collection = Execute.GetCollection<NavigationMenu>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<NavigationMenu>() where e.Id == id select e).FirstOrDefault();
        }

        /// <summary>
        /// load all the nav menus
        /// </summary>
        /// <returns></returns>
        public static List<NavigationMenu> LoadAll()
        {
            MongoCollection<NavigationMenu> Collection = Execute.GetCollection<NavigationMenu>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<NavigationMenu>() orderby e.UserFriendlyName select e).ToList();
        }
    }
}
