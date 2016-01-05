using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCommons.Ecommerce.PayPal.Entities;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;

namespace Chimera.Core.PurchaseOrders
{
    public static class PayPalAuthHeader
    {
        /// <summary>
        /// Get the necessary paypal API auth header from our setting group.
        /// </summary>
        /// <param name="paypalSettingGroup"></param>
        /// <returns></returns>
        public static AuthHeader GetAuthHeaderFromSetting(SettingGroup paypalSettingGroup)
        {
            AuthHeader PayPalAuthHeader = new AuthHeader();

            PayPalAuthHeader.BaseApiURL = paypalSettingGroup.GetSettingVal(PayPalSettingKeys.PayPal_API);
            PayPalAuthHeader.Username = paypalSettingGroup.GetSettingVal(PayPalSettingKeys.PayPal_Username);
            PayPalAuthHeader.Password = paypalSettingGroup.GetSettingVal(PayPalSettingKeys.PayPal_Password);
            PayPalAuthHeader.Signature = paypalSettingGroup.GetSettingVal(PayPalSettingKeys.PayPal_Signature);

            return PayPalAuthHeader;
        }
    }
}
