using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Chimera.DataAccess
{
    public static class Execute
    {
        /// <summary>
        /// AppSetting key used to look for inside the executing program's configuration file for the ConnectionString for MongoDB.
        /// </summary>
        private const string CONNECTION_STRING_APP_SETTING = "MongoDB_ConnectionString";

        /// <summary>
        /// AppSetting key used to look for inside the executing program's configuration file for the DatabaseName for MongoDB.
        /// </summary>
        private const string DATABASE_NAME_APP_SETTING = "MongoDB_Database";

        /// <summary>
        /// Delete multiple items from a collection based on the passed in query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="deleteQuery"></param>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static bool Delete<T>(string collectionName, IMongoQueryableable deleteQuery, string connectionString = "", string databaseName = "")
        {
            

            try
            {
                return ReturnNonQueryResult(GetCollection<T>(collectionName, connectionString, databaseName).Remove(deleteQuery));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog(String.Format("CompanyCommons.DataAccess.MongoDB.Delete(collectionName: '{0}') ExceptionMessage: {1}", collectionName, e.Message));
            }

            return false;
        }

        /// <summary>
        /// Simply save the entity to the desired collection.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="collectionName">The collection name, (i.e. table name in MongoDB).</param>
        /// <param name="entityToSave">entity to save</param>
        /// <param name="connectionString">Will use AppSetting "MongoDB_ConnectionString" if parameter not supplied.</param>
        /// <param name="databaseName">Will use AppSetting "MongoDB_Database" if parameter not supplied.</param>
        /// <returns>bool</returns>
        public static bool Save<T>(string collectionName, T entityToSave, string connectionString = "", string databaseName = "")
        {
            try
            {
                return ReturnNonQueryResult(GetCollection<T>(collectionName, connectionString, databaseName).Save(entityToSave));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog(String.Format("CompanyCommons.DataAccess.MongoDB.Save(collectionName: '{0}') ExceptionMessage: {1}", collectionName, e.Message));
            }

            return false;
        }

        /// <summary>
        /// Update a set of rows.
        /// </summary>
        /// <typeparam name="T">Object type we are using.</typeparam>
        /// <param name="collectionName">Name of the table.</param>
        /// <param name="updateQuery">The query used to select what records to update</param>
        /// <param name="updateSet">update set statement of what to change for each record</param>
        /// <param name="connectionString">Will use AppSetting "MongoDB_ConnectionString" if parameter not supplied.</param>
        /// <param name="databaseName">Will use AppSetting "MongoDB_Database" if parameter not supplied.</param>
        /// <returns>bool</returns>
        public static bool Update<T>(string collectionName, IMongoQueryable updateQuery, IMongoUpdate updateSet, string connectionString = "", string databaseName = "")
        {
            try
            {
                return ReturnNonQueryResult(GetCollection<T>(collectionName, connectionString, databaseName).Update(updateQuery, updateSet, UpdateFlags.Multi));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog(String.Format("CompanyCommons.DataAccess.MongoDB.Update(collectionName: '{0}') ExceptionMessage: {1}", collectionName, e.Message));
            }

            return false;
        }

        /// <summary>
        /// Get a collection reference so that we are able to perform Save / Update / Remove functions.
        /// </summary>
        /// <typeparam name="T">Entity type to look for in the collection.</typeparam>
        /// <param name="collectionName">The collection name, (i.e. table name in MongoDB).</param>
        /// <param name="connectionString">Will use AppSetting "MongoDB_ConnectionString" if parameter not supplied.</param>
        /// <param name="databaseName">Will use AppSetting "MongoDB_Database" if parameter not supplied.</param>
        /// <returns>MongoCollection</returns>
        public static MongoCollection<T> GetCollection<T>(string collectionName, string connectionString = "", string databaseName = "")
        {
            try
            {
                return GetMongoDatabase(connectionString, databaseName).GetCollection<T>(collectionName);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog(String.Format("CompanyCommons.DataAccess.MongoDB.Execute.GetCollection(collectionName: '{0}') ExceptionMessage: {1}", collectionName, e.Message));
            }

            return null;
        }

        /// <summary>
        /// Handles Save / Update / Delete, writes errors to log and returns bool.
        /// </summary>
        /// <param name="writeConcernResult">The write concern result from a save / update / delete.</param>
        /// <returns>bool</returns>
        private static bool ReturnNonQueryResult(WriteConcernResult writeConcernResult)
        {
            if (writeConcernResult != null)
            {
                //if no error message just return true
                if (string.IsNullOrWhiteSpace(writeConcernResult.ErrorMessage))
                {
                    return true;
                }

                //if we got this far we should log the error message and return false;
                CompanyCommons.Logging.WriteLog(String.Format("CompanyCommons.DataAccess.MongoDB.Execute.ReturnNonQueryResult: {0}", writeConcernResult.ErrorMessage));
            }
            else
            {
                CompanyCommons.Logging.WriteLog(String.Format("CompanyCommons.DataAccess.MongoDB.Execute.ReturnNonQueryResult: {0}", "WriteConcernResult is NULL"));
            }

            return false;
        }

        /// <summary>
        /// Simple private method to obtain the MongoDatabase object with our connection string and database name.
        /// </summary>
        /// <param name="connectionString">Will use AppSetting "MongoDB_ConnectionString" if parameter not supplied.</param>
        /// <param name="databaseName">Will use AppSetting "MongoDB_Database" if parameter not supplied.</param>
        /// <returns>MongoDatabase</returns>
        private static MongoDatabase GetMongoDatabase(string connectionString = "", string databaseName = "")
        {
            string ConnectionString = string.Empty;

            string DatabaseName = string.Empty;

            try
            {
                ConnectionString = string.IsNullOrWhiteSpace(connectionString) ? System.Configuration.ConfigurationManager.AppSettings[CONNECTION_STRING_APP_SETTING] : connectionString;

                DatabaseName = string.IsNullOrWhiteSpace(databaseName) ? System.Configuration.ConfigurationManager.AppSettings[DATABASE_NAME_APP_SETTING] : databaseName;

                return new MongoClient(ConnectionString).GetServer().GetDatabase(DatabaseName);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog(String.Format("CompanyCommons.DataAccess.MongoDB.Execute.GetMongoDatabase(ConnectionString: '{0}', DatabaseName: '{1}') ExceptionMessage: {2}", ConnectionString, DatabaseName, e.Message));
            }

            return null;
        }
    }
}
