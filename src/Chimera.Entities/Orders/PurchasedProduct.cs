using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Product.Property;

namespace Chimera.Entities.Orders
{
    public class PurchasedProduct
    {
        /// <summary>
        /// MongoDB ID of the product
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Main image path of the product
        /// </summary>
        public string MainImagePath { get; set; }

        /// <summary>
        /// # the user ordered
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The price for the product
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// The extra shipping for the product
        /// </summary>
        public decimal ShippingPrice { get; set; }

        /// <summary>
        /// The key/value pair of the checkout properties the user selected
        /// </summary>
        public List<CheckoutPropertySettingKey> SelectedCheckoutProperties { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PurchasedProduct()
        {
            Id = string.Empty;
            Name = string.Empty;
            MainImagePath = string.Empty;
            Quantity = 0;
            PurchasePrice = 0;
            ShippingPrice = 0;
            SelectedCheckoutProperties = new List<CheckoutPropertySettingKey>();
        }

        /// <summary>
        /// Constructor called to create the purchased product off of the shopping cart product
        /// </summary>
        /// <param name="shopCartProd"></param>
        public PurchasedProduct(string shippingMethodName, ShoppingCartProduct shopCartProd) : this()
        {
            Id = shopCartProd.Id;
            Name = shopCartProd.Name;
            MainImagePath = shopCartProd.MainImagePath;
            Quantity = shopCartProd.Quantity;
            PurchasePrice = shopCartProd.GetRealItemPrice();
            ShippingPrice = shopCartProd.GetRealShippingCost(shippingMethodName);
            SelectedCheckoutProperties = shopCartProd.SelectedCheckoutProperties;
        }

        /// <summary>
        /// Get the unique string for the selected checkout props
        /// </summary>
        /// <returns></returns>
        public string GetUniqueCheckoutPropertyKey()
        {
            string ReturnString = string.Empty;

            if (SelectedCheckoutProperties != null && SelectedCheckoutProperties.Count > 0)
            {
                foreach (var CheckPropSet in SelectedCheckoutProperties)
                {
                    ReturnString += CheckPropSet.Key + ":" + CheckPropSet.Value + ",";
                }
            }

            return ReturnString;
        }

        /// <summary>
        /// Convert the purchased prod into a string of params for paypal description
        /// </summary>
        /// <returns></returns>
        public string CreatePayPalItemDescriptions(int index)
        {
            string ReturnString = string.Empty;

            string Description = string.Empty;

            ReturnString += String.Format("&L_PAYMENTREQUEST_0_NAME{0}={1}", index, Uri.EscapeDataString(Name));

            if (SelectedCheckoutProperties != null && SelectedCheckoutProperties.Count > 0)
            {
                for (int myIndex = 0; myIndex < SelectedCheckoutProperties.Count; myIndex++)
                {
                    CheckoutPropertySettingKey SelectedProp = SelectedCheckoutProperties[myIndex];

                    Description += SelectedProp.Key + ": " + SelectedProp.Value;

                    if (myIndex != SelectedCheckoutProperties.Count - 1)
                    {
                        Description += ", ";
                    }
                }
            }

            ReturnString += String.Format("&L_PAYMENTREQUEST_0_DESC{0}={1}", index, Description);
            ReturnString += String.Format("&L_PAYMENTREQUEST_0_AMT{0}={1}", index, Uri.EscapeDataString(PurchasePrice.ToString("0.00")));
            ReturnString += String.Format("&L_PAYMENTREQUEST_0_QTY{0}={1}", index, Uri.EscapeDataString(Quantity.ToString()));

            return ReturnString;
        }
    }
}
