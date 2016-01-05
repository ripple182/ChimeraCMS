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
    public static class ImageDAO
    {
        /// <summary>
        /// collection name of the images table
        /// </summary>
        private const string COLLECTION_NAME = "Images";

        /// <summary>
        /// save image to database.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>bool if successfully saved to DB</returns>
        public static bool Save(Image image)
        {
            return Execute.Save<Image>(COLLECTION_NAME, image);
        }

        /// <summary>
        /// Load all of the previously uploaded images
        /// </summary>
        /// <returns></returns>
        public static List<Image> LoadAll()
        {
            MongoCollection<Image> Collection = Execute.GetCollection<Image>(COLLECTION_NAME);

            return (List<Image>)(from e in Collection.AsQueryable<Image>() orderby e.ModifiedDateUTC descending select e).ToList();
        }

        /// <summary>
        /// Load a list of images that were uploaded after the passed in date object
        /// </summary>
        /// <param name="lastKnownUploadDate"></param>
        /// <returns></returns>
        public static List<Image> LoadRecentlyUploaded(DateTime lastKnownUploadDate)
        {
            MongoCollection<Image> Collection = Execute.GetCollection<Image>(COLLECTION_NAME);

            return (List<Image>)(from e in Collection.AsQueryable<Image>() where e.ModifiedDateUTC >= lastKnownUploadDate orderby e.ModifiedDateUTC descending select e).ToList();
        }
    }
}
