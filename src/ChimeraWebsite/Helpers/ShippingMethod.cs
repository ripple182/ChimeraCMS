using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chimera.Entities.Property;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;
using Chimera.DataAccess;

namespace ChimeraWebsite.Helpers
{
    public static class ShippingMethod
    {
        /// <summary>
        /// Get a dictionary where the shipping method type is the key and the value is the global price when selecting that shipping method.
        /// </summary>
        /// <param name="shippingMethods"></param>
        /// <param name="paypalPurchaseSettings"></param>
        /// <returns></returns>
        public static Dictionary<string, decimal> GetGlobalShippingMethodDictionary(StaticProperty shippingMethods = null, SettingGroup paypalPurchaseSettings = null)
        {
            if (shippingMethods == null)
            {
                shippingMethods = StaticPropertyDAO.LoadByKeyName(StaticProperty.SHIPPING_METHOD_PROPERTY_KEY);
            }

            if (paypalPurchaseSettings == null)
            {
                paypalPurchaseSettings = SettingGroupDAO.LoadSettingGroupByName(SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS);
            }

            Dictionary<string, decimal> GlobalShippingMethodDictionary = new Dictionary<string, decimal>();

            if (shippingMethods != null && shippingMethods.PropertyNameValues != null && shippingMethods.PropertyNameValues.Count > 0)
            {
                foreach (var ShippValueKey in shippingMethods.PropertyNameValues)
                {
                    Setting GlobalShippingPriceSetting = paypalPurchaseSettings.SettingsList.Where(e => e.Key.Equals("GlobalBaseShippingAmt_" + ShippValueKey)).FirstOrDefault();

                    decimal GlobalShippingPrice = 0.00m;

                    if (GlobalShippingPriceSetting != null && !string.IsNullOrWhiteSpace(GlobalShippingPriceSetting.Value))
                    {
                        GlobalShippingPrice = Decimal.Parse(GlobalShippingPriceSetting.Value);
                    }

                    GlobalShippingMethodDictionary.Add(ShippValueKey, GlobalShippingPrice);
                }
            }

            return GlobalShippingMethodDictionary;
        }
    }
}