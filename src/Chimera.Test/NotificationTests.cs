using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chimera.Entities.Admin;
using Chimera.DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;
using Chimera.Entities.Dashboard;
using Chimera.Entities.Admin.Role;

namespace Chimera.Test
{
    [TestClass]
    public class NotificationTests
    {
        [TestMethod]
        public void AddTestNotification()
        {
            Notification NewNotification = new Notification();

            NewNotification.EntityId = "";
            NewNotification.MvcUrl.Action = "Edit";
            NewNotification.MvcUrl.Controller = "PurchaseOrders";
            NewNotification.Description = "New Notification";
            NewNotification.NotificationType = NotificationType.NEW_PURCHASE_ORDER;
            NewNotification.WarningLevel = WarningLevelType.NEUTRAL;
            NewNotification.ViewAdminUserRolesRequired.Add(PurchaseOrderRoles.VIEW);
            NewNotification.ViewAdminUserRolesRequired.Add(PurchaseOrderRoles.EDIT);

            NewNotification.ActionList.Insert(0, new NotificationAction("Edit", "glyphicon glyphicon-pencil", "Edit", "PurchaseOrders"));

            DashboardNotificationDAO.Save(NewNotification);
        }
    }
}
