using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CompanyCommons.Entities;

namespace Chimera.Entities.Property
{
    public class StaticProperty
    {
        /// <summary>
        /// database key name for seach property list
        /// </summary>
        public const string SEARCH_PROPERTY_KEY = "SearchProperties";

        /// <summary>
        /// database key name for checkout property list
        /// </summary>
        public const string CHECKOUT_PROPERTY_KEY = "CheckoutProperties";

        /// <summary>
        /// database key name for shipping method property list
        /// </summary>
        public const string SHIPPING_METHOD_PROPERTY_KEY = "ShippingMethodProperties";

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// "SearchProperty", "CheckoutProperty", "ShippingMethod", etc.
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// "Color", "Width", "Size", "ISBN", etc.
        /// "UPS", "USPS", "FedEx", etc.
        /// </summary>
        public HashSet<string> PropertyNameValues { get; set; }

        public StaticProperty()
        {
            Id = string.Empty;
            KeyName = string.Empty;
            PropertyNameValues = new HashSet<string>();
        }

        /// <summary>
        /// Validate when adding a new one
        /// </summary>
        /// <returns></returns>
        public List<WebUserMessage> Validate()
        {
            List<WebUserMessage> WebUserMessageList = new List<WebUserMessage>();

            string FailedType = WebUserMessage.WebUserMessageType.FAILED_MESSAGE_TYPE;

            if (string.IsNullOrWhiteSpace(KeyName))
            {
                WebUserMessageList.Add(new WebUserMessage("Property Name is required.", FailedType));
            }

            if (PropertyNameValues == null || PropertyNameValues.Count == 0)
            {
                WebUserMessageList.Add(new WebUserMessage("At least a single property value is required.", FailedType));
            }

            return WebUserMessageList;
        }
    }
}
