using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Product.Property
{
    public class CheckoutPropertySettingKey
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public CheckoutPropertySettingKey()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        public CheckoutPropertySettingKey(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
