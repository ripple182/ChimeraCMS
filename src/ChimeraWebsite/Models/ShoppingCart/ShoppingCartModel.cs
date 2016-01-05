using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chimera.Entities.Orders;
using ChimeraWebsite.Helpers;
using Chimera.Entities.Property;
using Chimera.Entities.Settings;

namespace ChimeraWebsite.Models.ShoppingCart
{
    public class ShoppingCartModel
    {
        public SettingGroup PaypalPurchaseSettings { get; set; }

        public List<ShoppingCartProduct> ShoppingCartProductList { get; set; }

        public string ViewType { get; set; }

        public StaticProperty ShippingMethods { get; set; } 

        public ShoppingCartModel(SettingGroup paypalPurchaseSettings, string viewType, StaticProperty shippingMethods)
        {
            PaypalPurchaseSettings = paypalPurchaseSettings;
            ShippingMethods = shippingMethods;
            ShoppingCartProductList = SiteContext.ShoppingCartProductList;
            ViewType = viewType;
        }

        /// <summary>
        /// Create a dictionary that has the global shipping price value for each possible shipping method
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, decimal> GetGlobalShippingMethodDictionary()
        {
            return Helpers.ShippingMethod.GetGlobalShippingMethodDictionary(ShippingMethods, PaypalPurchaseSettings);
        }
    }
}