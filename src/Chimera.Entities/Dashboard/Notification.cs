using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chimera.Entities.Dashboard
{
    public class Notification
    {
        /// <summary>
        /// The PK for this notification
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The id of the entity this notification was created for, when stock level then this is the product id, when order details then this is the order id, etc.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The description of this notification.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The type of notification, used to create identifiying icons as well.
        /// </summary>
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// The warning level, used to change text color so admin user knows weight of notification.
        /// </summary>
        public WarningLevelType WarningLevel { get; set; }

        /// <summary>
        /// The mvc url object used to figure out where to redirect the admin user when they click
        /// </summary>
        public MvcUrl MvcUrl { get; set; }

        /// <summary>
        /// The list of admin user roles required to even see this notification.
        /// </summary>
        public List<string> ViewAdminUserRolesRequired { get; set; }

        /// <summary>
        /// The list of actions to create an additional "Actions" dropdown button for quick actions, like "Mark As Approved", "Mark As Read", etc.
        /// </summary>
        public List<NotificationAction> ActionList { get; set; }

        /// <summary>
        /// The date this notification was created
        /// </summary>
        public DateTime CreatedDateUtc { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Notification()
        {
            Id = string.Empty;
            EntityId = string.Empty;
            Description = string.Empty;
            NotificationType = NotificationType.STOCK_LEVEL;
            WarningLevel = WarningLevelType.NEUTRAL;
            MvcUrl = new MvcUrl();
            ViewAdminUserRolesRequired = new List<string>();
            ActionList = new List<NotificationAction>();
            ActionList.Add(new NotificationAction("Dismiss", "glyphicon glyphicon-remove", "DismissNotification", "Dashboard"));
            CreatedDateUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Get the bootstrap icon class from the notification type
        /// </summary>
        /// <returns></returns>
        public string GetTypeIconClass()
        {
            switch (NotificationType)
            {
                case Dashboard.NotificationType.NEW_PURCHASE_ORDER:
                    return "glyphicon glyphicon-shopping-cart";

                case Dashboard.NotificationType.STOCK_LEVEL:
                    return "glyphicon glyphicon-warning-sign";

                default:
                    return "";
            }
        }

        /// <summary>
        /// Get the warning level class from the warning level
        /// </summary>
        /// <returns></returns>
        public string GetWarningLevelClass()
        {
            switch (WarningLevel)
            {
                case WarningLevelType.SUCCESS:
                    return "alert alert-success";

                case WarningLevelType.NEUTRAL:
                    return "alert alert-info";

                case WarningLevelType.WARNING:
                    return "alert alert-warning";

                case WarningLevelType.DANGER:
                    return "alert alert-danger";

                default:
                    return "";
            }
        }

    }
}
