using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyCommons.Ecommerce.PayPal.Entities.Order;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chimera.Entities.Orders
{
    public class PurchaseOrderDetails
    {
        /// <summary>
        /// MongoDB ID for this purchase order
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The information necessary to interact with paypal along with the customer's information.
        /// </summary>
        public OrderDetails PayPalOrderDetails { get; set; }

        /// <summary>
        /// The base/global shipping price for all orders at the time this order was placed.
        /// </summary>
        public decimal GlobalShippingPrice { get; set; }

        /// <summary>
        /// The percentage used for tax when the order was taken place
        /// </summary>
        public decimal GlobalTaxAmount { get; set; }

        /// <summary>
        /// The summary of the selected purchase products.
        /// </summary>
        public List<PurchasedProduct> PurchasedProductList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PurchaseOrderDetails()
        {
            Id = string.Empty;
            PayPalOrderDetails = new OrderDetails();
            GlobalShippingPrice = 0;
            GlobalTaxAmount = 0;
            PurchasedProductList = new List<PurchasedProduct>();
        }

        /// <summary>
        /// Constructor for a new order
        /// </summary>
        /// <param name="shippingMethodName">The selected shipping method</param>
        /// <param name="shoppingCartList">The customers current shopping cart</param>
        /// <param name="globalShippingPrice">The global shipping price for tghe selected shipping method</param>
        /// <param name="globalTaxAmount">The global tax amount</param>
        public PurchaseOrderDetails(string shippingMethodName, List<ShoppingCartProduct> shoppingCartList, decimal globalShippingPrice, string globalTaxAmount) : this()
        {
            PayPalOrderDetails.ShippingMethodType = shippingMethodName;
            PayPalOrderDetails.ShippingAmount = globalShippingPrice;
            GlobalShippingPrice = globalShippingPrice;
            GlobalTaxAmount = Decimal.Parse(globalTaxAmount);

            if (shoppingCartList != null && shoppingCartList.Count > 0)
            {
                foreach (var ShopCartProd in shoppingCartList)
                {
                    PurchasedProduct PurchasedProd = new PurchasedProduct(shippingMethodName, ShopCartProd);

                    PayPalOrderDetails.BaseAmount += PurchasedProd.PurchasePrice * PurchasedProd.Quantity;
                    PayPalOrderDetails.ShippingAmount += PurchasedProd.ShippingPrice;

                    PurchasedProductList.Add(PurchasedProd);
                }
            }

            PayPalOrderDetails.TaxAmount = PayPalOrderDetails.BaseAmount * GlobalTaxAmount;
        }

        /// <summary>
        /// Called in order to convert the purchased product list into a string of params for the paypal side item description
        /// </summary>
        /// <returns>string to append to the paypal api call</returns>
        public string CreatePayPalItemDescriptions()
        {
            string ReturnString = string.Empty;

            if (PurchasedProductList != null && PurchasedProductList.Count > 0)
            {
                for (int index = 0; index < PurchasedProductList.Count; index++)
                {
                    ReturnString += PurchasedProductList[index].CreatePayPalItemDescriptions(index);
                }
            }

            return ReturnString;
        }
    }
}
