using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Dashboard;
using Chimera.Entities.Orders;
using Chimera.DataAccess;
using Chimera.Entities.Admin.Role;

namespace Chimera.Core.Notifications
{
    public static class PurchaseOrder
    {
        /// <summary>
        /// Create a new notification customized for purchase orders.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private static Notification GenerateNewNotification(string entityId, string description)
        {
            Notification NewNotification = new Notification();

            NewNotification.EntityId = entityId;
            NewNotification.MvcUrl.Action = "Edit";
            NewNotification.MvcUrl.Controller = "PurchaseOrders";
            NewNotification.Description = description;
            NewNotification.NotificationType = NotificationType.NEW_PURCHASE_ORDER;
            NewNotification.WarningLevel = WarningLevelType.NEUTRAL;
            NewNotification.ViewAdminUserRolesRequired.Add(PurchaseOrderRoles.VIEW);
            NewNotification.ViewAdminUserRolesRequired.Add(PurchaseOrderRoles.EDIT);

            NewNotification.ActionList.Insert(0, new NotificationAction("Edit", "glyphicon glyphicon-pencil", "Edit", "PurchaseOrders"));

            return NewNotification;
        }

        /// <summary>
        /// The purchase order has shipped, delete the notification.
        /// </summary>
        /// <param name="purchOrder"></param>
        /// <returns></returns>
        public static bool ProcessPurchaseOrderShipped(PurchaseOrderDetails purchOrder)
        {
            try
            {
                return DashboardNotificationDAO.Delete(purchOrder.Id);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Core.Notifications.PurchaseOrder.ProcessPurchaseOrderShipped()" + e.Message);
            }

            return false;
        }

        /// <summary>
        /// Called when the purchase order payment is captured, we want to update the notification description and created date
        /// </summary>
        /// <param name="purchOrder"></param>
        /// <returns></returns>
        public static bool ProcessPurchaseOrderPaymentCaptured(PurchaseOrderDetails purchOrder)
        {
            try
            {
                Notification Notif = DashboardNotificationDAO.Load(purchOrder.Id);

                string UpdateText = String.Format("Purchase order shipment needs updated in order to notify customer.   Order placed on {0} UTC with a total of {1} spent.", purchOrder.PayPalOrderDetails.OrderPlacedDateUtc.ToString("g"), (purchOrder.PayPalOrderDetails.BaseAmount + purchOrder.PayPalOrderDetails.TaxAmount + purchOrder.PayPalOrderDetails.ShippingAmount).ToString("C"));

                //arleady exists
                if (Notif != null && !string.IsNullOrWhiteSpace(Notif.Id))
                {
                    return DashboardNotificationDAO.Update(purchOrder.Id, UpdateText);
                }
                //else save a new one
                else
                {
                    Notification NewNotification = GenerateNewNotification(purchOrder.Id, UpdateText);

                    return DashboardNotificationDAO.Save(NewNotification);
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Core.Notifications.PurchaseOrder.ProcessPurchaseOrderPaymentCaptured()" + e.Message);
            }

            return false;
        }

        /// <summary>
        /// Called when a new purchase order is added to the system
        /// </summary>
        /// <param name="purchOrder"></param>
        /// <returns></returns>
        public static bool ProcessNewPurchaseOrder(PurchaseOrderDetails purchOrder)
        {
            try
            {
                Notification NewNotification = GenerateNewNotification(purchOrder.Id, String.Format("New purchase order requires PayPal payment captured.  Order placed on {0} UTC with a total of {1} spent.", purchOrder.PayPalOrderDetails.OrderPlacedDateUtc.ToString("g"), (purchOrder.PayPalOrderDetails.BaseAmount + purchOrder.PayPalOrderDetails.TaxAmount + purchOrder.PayPalOrderDetails.ShippingAmount).ToString("C")));

                return DashboardNotificationDAO.Save(NewNotification);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Core.Notifications.PurchaseOrder.ProcessNewPurchaseOrder()" + e.Message);
            }

            return false;
        }
    }
}
