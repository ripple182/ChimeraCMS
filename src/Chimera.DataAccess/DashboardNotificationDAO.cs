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
using Chimera.Entities.Dashboard;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class DashboardNotificationDAO
    {
        private const string COLLECTION_NAME = "DashboardNotifications";

      
        /// <summary>
        /// Add a new dashboard notification.
        /// </summary>
        /// <param name="notification">The notification to add.</param>
        /// <returns>bool</returns>
        public static bool Save(Notification notification)
        {
            return Execute.Save<Notification>(COLLECTION_NAME, notification);
        }

        /// <summary>
        /// Update a notification with a new description, usually only for purchase orders after payment captured
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static bool Update(string entityId, string description)
        {
            var UpdateQuery = Query<Notification>.EQ(e => e.EntityId, entityId);
            var UpdateSetStatement = Update<Notification>.Set(e => e.Description, description).Set(e => e.CreatedDateUtc, DateTime.UtcNow);

            return Execute.Update<Notification>(COLLECTION_NAME, UpdateQuery, UpdateSetStatement);
        }

        /// <summary>
        /// Delete all notifications that are assoicated when the entity id (call after the user updates or performs a specific action)
        /// </summary>
        /// <param name="entityId">the product id / order id / form submission id, etc.</param>
        /// <returns>bool</returns>
        public static bool Delete(string entityId)
        {
            var DeleteQuery = Query<Notification>.EQ(e => e.EntityId, entityId);

            return Execute.Delete<Notification>(COLLECTION_NAME, DeleteQuery);
        }

        /// <summary>
        /// Delete all notifications that are associated with any of the entity ids in the list
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns>bool</returns>
        public static bool Delete(List<string> entityIdList)
        {
            var DeleteQuery = Query<Notification>.In(e => e.EntityId, entityIdList);

            return Execute.Delete<Notification>(COLLECTION_NAME, DeleteQuery);
        }

        /// <summary>
        /// Load a single notification by its entity id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public static Notification Load(string entityId)
        {
            MongoCollection<Notification> Collection = Execute.GetCollection<Notification>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<Notification>() orderby e.CreatedDateUtc descending select e).FirstOrDefault();
        }

        /// <summary>
        /// Get the count of total notifications
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            MongoCollection<Notification> Collection = Execute.GetCollection<Notification>(COLLECTION_NAME);

            return Collection.AsQueryable<Notification>().Count();
        }

        /// <summary>
        /// Load a list of all the dashboard notifications that the admin user can see
        /// </summary>
        /// <returns></returns>
        public static List<Notification> LoadDashboardList(List<string> adminUserRoles)
        {
            MongoCollection<Notification> Collection = Execute.GetCollection<Notification>(COLLECTION_NAME);

            List<Notification> NotificationList = (from e in Collection.AsQueryable<Notification>() orderby e.CreatedDateUtc descending select e).ToList();

            List<Notification> ReturnList = new List<Notification>();

            if (adminUserRoles.Contains(Chimera.Entities.Admin.Role.AdminRoles.ADMIN_ALL))
            {
                return NotificationList;
            }
            else if (NotificationList != null && NotificationList.Count > 0)
            {
                foreach (var Notif in NotificationList)
                {
                    int NumMatchRoles = 0;

                    if (Notif.ViewAdminUserRolesRequired != null && Notif.ViewAdminUserRolesRequired.Count > 0)
                    {
                        foreach (var NotifAdminRole in Notif.ViewAdminUserRolesRequired)
                        {
                            if(adminUserRoles.Contains(NotifAdminRole))
                            {
                                NumMatchRoles++;
                            }
                        }
                    }

                    if(NumMatchRoles == Notif.ViewAdminUserRolesRequired.Count)
                    {
                        ReturnList.Add(Notif);
                    }
                }
            }

            return ReturnList;
        }
    }
}
