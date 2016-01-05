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
using Chimera.Entities.Admin;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class AdminUserRoleDAO
    {
        private const string COLLECTION_NAME = "AdminUserRoles";

        public static bool Save(AdminUserRole adminUserRole)
        {
            return Execute.Save<AdminUserRole>(COLLECTION_NAME, adminUserRole);
        }

        /// <summary>
        /// Load a list of all available admin user roles.
        /// </summary>
        /// <returns>list of strings.</returns>
        public static List<AdminUserRole> LoadAll()
        {
            MongoCollection<AdminUserRole> Collection = Execute.GetCollection<AdminUserRole>(COLLECTION_NAME);

            return (List<AdminUserRole>)(from e in Collection.AsQueryable<AdminUserRole>() select e).ToList();
        }
    }
}
