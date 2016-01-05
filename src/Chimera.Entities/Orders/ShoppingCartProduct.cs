using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Product;
using Chimera.Entities.Product.Property;

namespace Chimera.Entities.Orders
{
    public class ShoppingCartProduct
    {
        /// <summary>
        /// Unique ID of the product
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id created by the product id + the selected checkout properties
        /// </summary>
        public string CartUniqueId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PrivateCartUniqueId))
                {
                    PrivateCartUniqueId = "Id=" + Id + ";";

                    if (SelectedCheckoutProperties != null && SelectedCheckoutProperties.Count > 0)
                    {
                        foreach (var CheckProSettKey in SelectedCheckoutProperties)
                        {
                            PrivateCartUniqueId += CheckProSettKey.Key + "=" + CheckProSettKey.Value + ";";
                        }
                    }
                }

                return PrivateCartUniqueId;
            }
        }

        /// <summary>
        /// Used to hold the actual private cart unique id
        /// </summary>
        private string PrivateCartUniqueId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The path of the main image for this product.
        /// </summary>
        public string MainImagePath { get; set; }

        /// <summary>
        /// The quantity # the customer wishes to buy of this product
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The general product purchase settings, can be overridden if CheckoutPropertySetting exists and set to override price or shipping method.
        /// </summary>
        public PurchaseSettings PurchaseSettings { get; set; }

        /// <summary>
        /// The key/value pair of the checkout properties the user selected
        /// </summary>
        public List<CheckoutPropertySettingKey> SelectedCheckoutProperties { get; set; }

        /// <summary>
        /// When the user is adding an item to the cart, they select checkout properties from the dropdowns, this CheckoutPropertySetting is the logic behind the unique checkout properties selected.
        /// </summary>
        public CheckoutPropertySetting CheckoutPropertySetting { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ShoppingCartProduct(Product.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            MainImagePath = product.MainImage.ImagePath;
            Quantity = 1;
            PurchaseSettings = product.PurchaseSettings;
            SelectedCheckoutProperties = new List<CheckoutPropertySettingKey>();
            CheckoutPropertySetting = new CheckoutPropertySetting();
            PrivateCartUniqueId = string.Empty;
        }

        /// <summary>
        /// Figure out what the price is by checking the custom checkout property settings
        /// </summary>
        /// <returns></returns>
        public decimal GetRealItemPrice()
        {
            if (CheckoutPropertySetting != null && CheckoutPropertySetting.DefineCustomPrice)
            {
                return CheckoutPropertySetting.PurchaseSettings.PurchasePrice;
            }
            else
            {
                return PurchaseSettings.PurchasePrice;
            }
        }

        /// <summary>
        /// Figure out the extra cost of shipping for this product by checking the custom checkout property settings
        /// </summary>
        /// <param name="selectedShippingMethod"></param>
        /// <returns></returns>
        public decimal GetRealShippingCost(string selectedShippingMethod)
        {
            List<ShippingMethodProperty> ShippingMethodPropList = new List<ShippingMethodProperty>();

            if (CheckoutPropertySetting != null && CheckoutPropertySetting.DefineCustomShipping)
            {
                ShippingMethodPropList = CheckoutPropertySetting.PurchaseSettings.ShippingMethodList;
            }
            else
            {
                ShippingMethodPropList = PurchaseSettings.ShippingMethodList;
            }

            ShippingMethodProperty SelectedProp = ShippingMethodPropList.Where(e => e.Name.Equals(selectedShippingMethod)).FirstOrDefault();

            if (SelectedProp != null)
            {
                return SelectedProp.Price;
            }

            return 0.00m;
        }

        /// <summary>
        /// Determine if we can add this to the cart or increase the quantity checking vs the stock level
        /// </summary>
        /// <returns></returns>
        public bool CanAddToCartOrIncreaseQuantity(int newQuantity)
        {
            if (CheckoutPropertySetting.CheckoutPropertySettingKeys.Count > 0)
            {
                if (CheckoutPropertySetting.PurchaseSettings.StockLevel >= newQuantity)
                {
                    return true;
                }
            }
            else if (PurchaseSettings.StockLevel >= newQuantity)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Create a user friendly message for when the product is out of stock
        /// </summary>
        /// <returns></returns>
        public string CreateOutOfStockMessage()
        {
            string ReturnString = string.Empty;

            ReturnString += "We're sorry, the following product is out of stock:<br/><br/>";
            ReturnString += Name + ":<br/>";

            if (SelectedCheckoutProperties != null && SelectedCheckoutProperties.Count > 0)
            {
                ReturnString += "<ul>";

                foreach(var SelectedProp in SelectedCheckoutProperties)
                {
                    ReturnString += "<li>" + SelectedProp.Key + ": " + SelectedProp.Value + "</li>";
                }

                ReturnString += "<ul>";
            }

            return ReturnString;
        }
    }
}
