using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Uploads;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class ColorHexDAO
    {
        /// <summary>
        /// collection name of the images table
        /// </summary>
        private const string COLLECTION_NAME = "Colors";

        /// <summary>
        /// save ColorHex to database.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>bool if successfully saved to DB</returns>
        public static bool Save(ColorHex colorHex)
        {
            return Execute.Save<ColorHex>(COLLECTION_NAME, colorHex);
        }

        /// <summary>
        /// Load all of the previously uploaded ColorHex
        /// </summary>
        /// <returns></returns>
        public static List<ColorHex> LoadAll()
        {
            MongoCollection<ColorHex> Collection = Execute.GetCollection<ColorHex>(COLLECTION_NAME);

            return (List<ColorHex>)(from e in Collection.AsQueryable<ColorHex>() orderby e.ModifiedDateUTC descending select e).ToList();
        }
    }
}
