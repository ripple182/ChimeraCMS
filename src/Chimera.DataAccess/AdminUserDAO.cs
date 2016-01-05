using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Admin;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;
using CompanyCommons;

namespace Chimera.DataAccess
{
    public static class AdminUserDAO
    {
        private const string COLLECTION_NAME = "AdminUsers";

        /// <summary>
        /// Add or update an admin user.  Username is unique, password is hashed based on mongo object id.
        /// Will return false if attempting to save with new username of a user that already exists.
        /// </summary>
        /// <param name="adminUser">The new or existing admin user</param>
        /// <returns>bool if successful.</returns>
        public static bool Save(AdminUser adminUser)
        {
            AdminUser AdminUser = LoadByUsername(adminUser.Username);

            //if no users in the database exist with that username, or the only user that exists in the one we are trying to save.
            if (AdminUser == null || string.IsNullOrWhiteSpace(AdminUser.Id) || (AdminUser.Id.Equals(adminUser.Id)))
            {
                //if true we are saving a brand new user
                if (string.IsNullOrWhiteSpace(adminUser.Id))
                {
                    //generate a brand new mongo id
                    adminUser.Id = ObjectId.GenerateNewId().ToString();

                    //hash the password by using the user's mongo id
                    adminUser.Hashed_Password = Hashing.GetSaltedHash(adminUser.Hashed_Password, adminUser.Id.ToString());
                }

                //Timestamp of when the page was saved.
                adminUser.ModifiedDateUTC = DateTime.UtcNow;

                return Execute.Save<AdminUser>(COLLECTION_NAME, adminUser);
            }

            return false;
        }

        /// <summary>
        /// Load an admin user by comparing their username and hashed password.
        /// </summary>
        /// <param name="username">The unique username.</param>
        /// <param name="password">Unhashed password used to compare with password in database.</param>
        /// <returns>A single admin user object.</returns>
        public static AdminUser LoadByAttemptLogin(string username, string password)
        {
            AdminUser AdminUser = LoadByUsername(username);

            //compare username and hashed password
            if (AdminUser.Username.ToUpper().Equals(username.ToUpper()) && AdminUser.Hashed_Password.Equals(Hashing.GetSaltedHash(password, AdminUser.Id.ToString())))
            {
                if (AdminUser.Active)
                {
                    //record the login date
                    DateTime LastLogin = AdminUser.LastLoginDateUTC;

                    AdminUser.LastLoginDateUTC = DateTime.UtcNow;

                    Execute.Save<AdminUser>(COLLECTION_NAME, AdminUser);

                    AdminUser.LastLoginDateUTC = LastLogin;

                    return AdminUser;
                }
            }

            return null;
        }

        /// <summary>
        /// Load a admin user by its unique login username.
        /// </summary>
        /// <param name="username">The unique username.</param>
        /// <returns>A single admin user object.</returns>
        public static AdminUser LoadByUsername(string username)
        {
            MongoCollection<AdminUser> Collection = Execute.GetCollection<AdminUser>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<AdminUser>() where e.Username == username select e).FirstOrDefault();
        }

        /// <summary>
        /// Simply load a admin user by its unique Mongo Bson Id.
        /// </summary>
        /// <param name="bsonId">The unqiue Mongo Bson Id.</param>
        /// <returns>A single admin user object.</returns>
        public static AdminUser LoadByBsonId(ObjectId bsonId)
        {
            MongoCollection<AdminUser> Collection = Execute.GetCollection<AdminUser>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<AdminUser>() where e.Id.Equals(bsonId) select e).FirstOrDefault();
        }

        /// <summary>
        /// Load a list of all the admin users.
        /// </summary>
        /// <returns>list of admin user objects.</returns>
        public static List<AdminUser> LoadAll()
        {
            MongoCollection<AdminUser> Collection = Execute.GetCollection<AdminUser>(COLLECTION_NAME);

            return (List<AdminUser>) (from e in Collection.AsQueryable<AdminUser>() orderby e.Username select e).ToList();
        }

        /// <summary>
        /// Load a list of admin users by using a list of ids
        /// </summary>
        /// <param name="adminUserIds">The admin user id list</param>
        /// <returns>list of admin users</returns>
        public static List<AdminUser> LoadByMultipleIds(List<string> adminUserIds)
        {
            MongoCollection<AdminUser> Collection = Execute.GetCollection<AdminUser>(COLLECTION_NAME);

            return (List<AdminUser>)(from e in Collection.AsQueryable<AdminUser>() orderby e.Username where adminUserIds.Contains(e.Id) select e).ToList();
        }
    }
}
