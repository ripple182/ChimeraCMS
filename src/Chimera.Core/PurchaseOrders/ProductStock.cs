using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Orders;
using Chimera.DataAccess;
using Chimera.Entities.Product;
using Chimera.Entities.Settings;

namespace Chimera.Core.PurchaseOrders
{
    public static class ProductStock
    {
        /// <summary>
        /// After a new purchase order is final and saved, we need to substract the stock levels from the actual product.
        /// </summary>
        /// <param name="purchOrder"></param>
        public static void ProcessNewOrderStockLevels(SettingGroup paypalSettings, PurchaseOrderDetails purchOrder)
        {
            try
            {
                if (purchOrder != null && purchOrder.PurchasedProductList != null && purchOrder.PurchasedProductList.Count > 0)
                {
                    foreach (var PurchProd in purchOrder.PurchasedProductList)
                    {
                        Product Prod = ProductDAO.LoadByBsonId(PurchProd.Id);

                        if (Prod != null)
                        {
                            Prod.UpdateStock(PurchProd);

                            ProductDAO.Save(Prod);

                            Chimera.Core.Notifications.ProductStock.ProcessPurchasedProduct(paypalSettings, Prod);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Core.PurchaseOrders.ProductStock.ProcessNewOrderStockLevels()" + e.Message);
            }
        }
    }
}
