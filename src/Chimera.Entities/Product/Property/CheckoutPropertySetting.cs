using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Product.Property
{
    public class CheckoutPropertySetting
    {
        /// <summary>
        /// dictionary of what checkout properties key / value make up this setting.  I.E. "Color","Red" is a key/value pair in the dictionary.
        /// </summary>
        public List<CheckoutPropertySettingKey> CheckoutPropertySettingKeys { get; set; }

        public bool DefineCustomPrice { get; set; }

        public bool DefineCustomShipping { get; set; }

        public PurchaseSettings PurchaseSettings { get; set; }
       
        public CheckoutPropertySetting()
        {
            CheckoutPropertySettingKeys = new List<CheckoutPropertySettingKey>();
            PurchaseSettings = new PurchaseSettings();
        }

        /// <summary>
        /// Get the unique string for the selected checkout props
        /// </summary>
        /// <returns></returns>
        public string GetUniqueCheckoutPropertyKey()
        {
            string ReturnString = string.Empty;

            if (CheckoutPropertySettingKeys != null && CheckoutPropertySettingKeys.Count > 0)
            {
                foreach (var CheckPropSet in CheckoutPropertySettingKeys)
                {
                    ReturnString += CheckPropSet.Key + ":" + CheckPropSet.Value + ",";
                }
            }

            return ReturnString;
        }
    }
}
