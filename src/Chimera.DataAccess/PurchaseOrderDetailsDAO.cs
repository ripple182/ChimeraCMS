using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;
using Chimera.Entities.Orders;
using CompanyCommons.Ecommerce.PayPal.Entities.Order;

namespace Chimera.DataAccess
{
    public static class PurchaseOrderDetailsDAO
    {
        private const string COLLECTION_NAME = "Order";

        /// <summary>
        /// Add a new customer order
        /// </summary>
        /// <param name="order">The order to save or update</param>
        /// <returns>bool if successful</returns>
        public static bool Save(PurchaseOrderDetails order)
        {
            return Execute.Save<PurchaseOrderDetails>(COLLECTION_NAME, order);
        }

        /// <summary>
        /// Load a single purchase order by its unique MongoDB ID
        /// </summary>
        /// <param name="bsonId">The ID</param>
        /// <returns></returns>
        public static PurchaseOrderDetails LoadByBsonId(string bsonId)
        {
            MongoCollection<PurchaseOrderDetails> Collection = Execute.GetCollection<PurchaseOrderDetails>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<PurchaseOrderDetails>() where e.Id == bsonId select e).FirstOrDefault();
        }

        /// <summary>
        /// Load a list of purchase orders by filtering by the order date and the customer info passed in
        /// </summary>
        /// <param name="paymentCaptured">if the payment from paypal has been captured</param>
        /// <param name="orderShipped">if the order has been shipped</param>
        /// <param name="orderPlacedFrom"></param>
        /// <param name="orderPlaceTo"></param>
        /// <param name="customerInfo">Relevant customer info to search by</param>
        /// <returns>list of purchase orders</returns>
        public static List<PurchaseOrderDetails> Search(string paymentCaptured, string orderShipped, DateTime orderPlacedFrom, DateTime orderPlaceTo, CustomerInfo customerInfo, int numberToQuery)
        {
            paymentCaptured = paymentCaptured.ToUpper();
            orderShipped = orderShipped.ToUpper();

            MongoCollection<PurchaseOrderDetails> Collection = Execute.GetCollection<PurchaseOrderDetails>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<PurchaseOrderDetails>()

                    where e.PayPalOrderDetails.OrderPlacedDateUtc >= orderPlacedFrom

                    && (orderPlaceTo == DateTime.MinValue || e.PayPalOrderDetails.OrderPlacedDateUtc <= orderPlaceTo)
                    && (string.IsNullOrWhiteSpace(orderShipped) || ("FALSE" == orderShipped && e.PayPalOrderDetails.OrderShippedDateUtc == DateTime.MinValue) || ("TRUE" == orderShipped && e.PayPalOrderDetails.OrderShippedDateUtc != DateTime.MinValue))
                    && (string.IsNullOrWhiteSpace(paymentCaptured) || ("FALSE" == paymentCaptured && e.PayPalOrderDetails.PaymentCapturedDateUtc == DateTime.MinValue) || ("TRUE" == paymentCaptured && e.PayPalOrderDetails.PaymentCapturedDateUtc != DateTime.MinValue))
                    && ("" == customerInfo.Email || e.PayPalOrderDetails.CustomerInfo.Email.Contains(customerInfo.Email))
                    && ("" == customerInfo.FirstName || e.PayPalOrderDetails.CustomerInfo.FirstName.Contains(customerInfo.FirstName))
                    && ("" == customerInfo.LastName || e.PayPalOrderDetails.CustomerInfo.LastName.Contains(customerInfo.LastName))

                    orderby e.PayPalOrderDetails.OrderPlacedDateUtc descending

                    select e).Take(numberToQuery).ToList();
        }
    }
}
