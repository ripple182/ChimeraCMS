using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Chimera.Entities.Product.Property;
using CEP = Chimera.Entities.Property;
using MongoDB.Bson.Serialization.Attributes;
using Chimera.Entities.Utility;
using Chimera.Entities.Orders;

namespace Chimera.Entities.Product
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public PurchaseSettings PurchaseSettings { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public List<CEP.Property> SearchPropertyList { get; set; }

        /// <summary>
        /// optional, allows the client to customize a single product into many products with a combination of different properties, "COLOR", "WIDTH", "SIZE" will create several products.
        /// </summary>
        public List<CEP.Property> CheckoutPropertyList { get; set; }

        /// <summary>
        /// optional, this is where each possible combination is stored and sets the price / stock level.
        /// </summary>
        public List<CheckoutPropertySetting> CheckoutPropertySettingsList { get; set; }

        public ProductImage MainImage { get; set; }

        public List<ProductImage> AdditionalImages { get; set; }

        public Product()
        {
            Id = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Active = false;
            CreatedDateUtc = DateTime.MinValue;
            SearchPropertyList = new List<CEP.Property>();
            CheckoutPropertyList = new List<CEP.Property>();
            CheckoutPropertySettingsList = new List<CheckoutPropertySetting>();
            MainImage = new ProductImage();
            AdditionalImages = new List<ProductImage>();
            PurchaseSettings = new PurchaseSettings();
        }

        public void RemoveCheckoutPropertySettingsDuplicants()
        {
            //remove duplicate checkout setting properties
            if (CheckoutPropertySettingsList != null && CheckoutPropertySettingsList.Count > 0)
            {
                Dictionary<string, CheckoutPropSettingDuplicant> CheckoutPropKeyCount = new Dictionary<string, CheckoutPropSettingDuplicant>();

                for (int index = 0; index < CheckoutPropertySettingsList.Count; index++)
                {
                    var CheckoutPropSetting = CheckoutPropertySettingsList[index];

                    if (CheckoutPropSetting.CheckoutPropertySettingKeys != null && CheckoutPropSetting.CheckoutPropertySettingKeys.Count > 0)
                    {
                        string CombinedValues = string.Empty;

                        foreach (var CheckoutPropSetKey in CheckoutPropSetting.CheckoutPropertySettingKeys)
                        {
                            CombinedValues += CheckoutPropSetKey.Value;
                        }

                        if (CheckoutPropKeyCount.ContainsKey(CombinedValues))
                        {
                            CheckoutPropKeyCount[CombinedValues].NumberProcessed++;
                            CheckoutPropKeyCount[CombinedValues].LastProcessedArrayIndex = index;
                        }
                        else
                        {
                            CheckoutPropKeyCount.Add(CombinedValues, new CheckoutPropSettingDuplicant(1, index));
                        }
                    }
                }

                if (CheckoutPropKeyCount != null && CheckoutPropKeyCount.Count > 0)
                {
                    foreach (var CheckPropKeyDupe in CheckoutPropKeyCount.Values)
                    {
                        if (CheckPropKeyDupe.NumberProcessed > 1)
                        {
                            CheckoutPropertySettingsList.RemoveAt(CheckPropKeyDupe.LastProcessedArrayIndex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update the real product's stock with the quantity selected from the user's purchased product
        /// </summary>
        /// <param name="purchProduct">The product the user purchased</param>
        public void UpdateStock(PurchasedProduct purchProduct)
        {
            bool FoundUniqueCheckoutPropKey = false;

            if (purchProduct.SelectedCheckoutProperties != null && purchProduct.SelectedCheckoutProperties.Count > 0)
            {
                if(CheckoutPropertySettingsList != null && CheckoutPropertySettingsList.Count > 0)
                {
                    foreach (var CheckoutPropSetting in CheckoutPropertySettingsList)
                    {
                        if(purchProduct.GetUniqueCheckoutPropertyKey().Equals(CheckoutPropSetting.GetUniqueCheckoutPropertyKey()))
                        {
                            CheckoutPropSetting.PurchaseSettings.StockLevel -= purchProduct.Quantity;
                            FoundUniqueCheckoutPropKey = true;
                            break;
                        }
                    }
                }
            }

            if (!FoundUniqueCheckoutPropKey)
            {
                PurchaseSettings.StockLevel -= purchProduct.Quantity;
            }
        }
    }
}
