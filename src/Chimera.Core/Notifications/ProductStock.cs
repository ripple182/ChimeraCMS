using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Dashboard;
using Chimera.Entities.Product;
using Chimera.Entities.Product.Property;
using Chimera.DataAccess;
using Chimera.Entities.Admin.Role;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;

namespace Chimera.Core.Notifications
{
    public static class ProductStock
    {
        /// <summary>
        /// Generate a new notification for a product stock level warning
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="stockLevel"></param>
        /// <returns></returns>
        private static Notification GenerateNewNotification(string name, string entityId, int stockLevel, List<CheckoutPropertySettingKey> checkoutPropSettingKeys = null)
        {
            Notification NewNotification = new Notification();

            NewNotification.EntityId = entityId;
            NewNotification.MvcUrl.Action = "Edit";
            NewNotification.MvcUrl.Controller = "Product";
            NewNotification.NotificationType = NotificationType.STOCK_LEVEL;
            NewNotification.WarningLevel = WarningLevelType.DANGER;
            NewNotification.ViewAdminUserRolesRequired.Add(ProductRoles.VIEW);
            NewNotification.ViewAdminUserRolesRequired.Add(ProductRoles.EDIT);

            NewNotification.ActionList.Insert(0, new NotificationAction("Edit", "glyphicon glyphicon-pencil", "Edit", "Product"));

            if (checkoutPropSettingKeys != null && checkoutPropSettingKeys.Count > 0)
            {
                NewNotification.Description = String.Format("The following product \"{0}\" has a stock level of \"{1}\" with these checkout settings:<br/>", name, stockLevel);
                NewNotification.Description += "<ul>";

                foreach (var CheckPropKey in checkoutPropSettingKeys)
                {
                    NewNotification.Description += String.Format("<li>{0}: {1}</li>", CheckPropKey.Key, CheckPropKey.Value);
                }

                NewNotification.Description += "</ul>";
            }
            else
            {
                NewNotification.Description = String.Format("The following product \"{0}\" has a stock level of \"{1}\".", name, stockLevel);
            }

            return NewNotification;
        }

        /// <summary>
        /// Called whenever the admin user updates the product to remove the notification
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool ProductStockUpdated(Product product)
        {
            try
            {
                return DashboardNotificationDAO.Delete(product.Id);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Core.Notifications.ProductStock.ProductStockUpdated()" + e.Message);
            }

            return false;
        }

        /// <summary>
        /// Process a purchased product after the stock level has been altered from the purchase order
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool ProcessPurchasedProduct(SettingGroup paypalSettings, Product product)
        {
            try
            {
                string StockLevelWarningString = paypalSettings.GetSettingVal(PayPalSettingKeys.StockLevelWarning);

                int StockLevelWarning = Int32.Parse(!string.IsNullOrWhiteSpace(StockLevelWarningString) ? StockLevelWarningString : "0");

                if (product.PurchaseSettings.StockLevel <= StockLevelWarning)
                {
                    Notification NewNotification = GenerateNewNotification(product.Name, product.Id, product.PurchaseSettings.StockLevel);

                    DashboardNotificationDAO.Save(NewNotification);
                }

                if (product.CheckoutPropertySettingsList != null && product.CheckoutPropertySettingsList.Count > 0)
                {
                    foreach (var CheckPropSetting in product.CheckoutPropertySettingsList)
                    {
                        if (CheckPropSetting.PurchaseSettings.StockLevel <= StockLevelWarning)
                        {
                            Notification NewNotification = GenerateNewNotification(product.Name, product.Id, product.PurchaseSettings.StockLevel, CheckPropSetting.CheckoutPropertySettingKeys);

                            DashboardNotificationDAO.Save(NewNotification);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Core.Notifications.ProductStock.ProcessPurchasedProduct()" + e.Message);
            }

            return false;
        }
    }
}
