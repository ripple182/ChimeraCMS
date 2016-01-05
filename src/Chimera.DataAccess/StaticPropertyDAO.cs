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
using Chimera.Entities.Property;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class StaticPropertyDAO
    {
        private const string COLLECTION_NAME = "StaticProperties";

        /// <summary>
        /// Save or update a static property with new values
        /// </summary>
        /// <param name="staticProperty"></param>
        /// <returns></returns>
        public static bool Save(StaticProperty staticProperty)
        {
            return Execute.Save<StaticProperty>(COLLECTION_NAME, staticProperty);
        }

        /// <summary>
        /// Load a single static property by its id
        /// </summary>
        /// <param name="bsonId"></param>
        /// <returns></returns>
        public static StaticProperty LoadByBsonId(string bsonId)
        {
            MongoCollection<StaticProperty> Collection = Execute.GetCollection<StaticProperty>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<StaticProperty>() where e.Id == bsonId select e).FirstOrDefault();
        }

        /// <summary>
        /// Load only the keys
        /// </summary>
        /// <returns></returns>
        public static List<string> LoadAllKeyNames()
        {
            List<string> ReturnList = new List<string>();

            MongoCollection<StaticProperty> Collection = Execute.GetCollection<StaticProperty>(COLLECTION_NAME);

            List<StaticProperty> StaticPropList = (from e in Collection.AsQueryable<StaticProperty>() orderby e.KeyName select e).ToList();

            if (StaticPropList != null && StaticPropList.Count > 0)
            {
                foreach(var StaticProp in StaticPropList)
                {
                    ReturnList.Add(StaticProp.KeyName);
                }
            }

            return ReturnList;
        }

        /// <summary>
        /// Load all the static properties.
        /// </summary>
        /// <returns></returns>
        public static List<StaticProperty> LoadAll()
        {
            MongoCollection<StaticProperty> Collection = Execute.GetCollection<StaticProperty>(COLLECTION_NAME);

            return (List<StaticProperty>)(from e in Collection.AsQueryable<StaticProperty>() orderby e.KeyName select e).ToList();
        }

        /// <summary>
        /// Load a single static property object by its unique key name.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static StaticProperty LoadByKeyName(string keyName)
        {
            MongoCollection<StaticProperty> Collection = Execute.GetCollection<StaticProperty>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<StaticProperty>() where e.KeyName.Equals(keyName) select e).FirstOrDefault();
        }

        /// <summary>
        /// Load a list of static properties wwith a list of key names
        /// </summary>
        /// <param name="keyNames"></param>
        /// <returns></returns>
        public static List<StaticProperty> LoadByMultipleKeyNames(List<string> keyNames)
        {
            MongoCollection<StaticProperty> Collection = Execute.GetCollection<StaticProperty>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<StaticProperty>() orderby e.KeyName select e).Where(e => keyNames.Contains(e.KeyName)).ToList();
        }
    }
}
