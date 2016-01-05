using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;
using Chimera.DataAccess;
using Chimera.Entities.Admin;
using Chimera.Entities.Orders;

namespace Chimera.Core.PurchaseOrders
{
    public static class Email
    {
        /// <summary>
        /// Called to send the emails whenever a new order is processed.
        /// </summary>
        /// <param name="settingGroupList"></param>
        /// <param name="paypalPurchaseOrder"></param>
        public static void SendNewEcommerceOrderEmails(List<SettingGroup> settingGroupList, PurchaseOrderDetails paypalPurchaseOrder)
        {
            try
            {
                SettingGroup EmailSettings = settingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.EMAIL_SETTINGS)).FirstOrDefault();

                SettingGroup TemplateSettings = settingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.TEMPLATE_CUSTOM_SETTINGS)).FirstOrDefault();

                List<AdminUser> AdminUserList = new List<AdminUser>();

                try
                {

                    if (!string.IsNullOrWhiteSpace(EmailSettings.GetSettingVal(EmailSettingKeys.NewOrderEmailAdminUsers)))
                    {
                        AdminUserList = AdminUserDAO.LoadByMultipleIds(EmailSettings.GetSettingVal(EmailSettingKeys.NewOrderEmailAdminUsers).Split(',').ToList());
                    }
                }
                catch (Exception e)
                {
                    //do nothing, just in case the setting got jacked up somehow we still want to send the customer's email.
                }

                Chimera.Emails.Ecommerce.SendNewEcommerceOrderEmails(AdminUserList, paypalPurchaseOrder, EmailSettings.GetSettingVal(EmailSettingKeys.CustomerOrderFinishedEmail), EmailSettings.GetSettingVal(EmailSettingKeys.SenderEmailAddress), TemplateSettings.GetSettingVal("WebsiteTitle"));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Core.PurchaseOrders.Email.SendNewEcommerceOrderEmails()" + e.Message);
            }
        }
    }
}
