using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chimera.Entities.Orders;
using Chimera.Entities.Report;

namespace ChimeraWebsite.Helpers
{
    public static class SiteContext
    {
        /// <summary>
        /// session key used to access the admin user
        /// </summary>
        private const string SHOPPING_CART_LIST_SESSION_KEY = "Shopping_Cart_List";

        /// <summary>
        /// session key for the paypal purchase order thats in progress
        /// </summary>
        private const string PURCHASE_ORDER_SESSION_KEY = "PurchaseOrderInProgress";

        /// <summary>
        /// Session key for the users session info, like their IP / session id / browser / OS
        /// </summary>
        public const string USER_SESSION_INFO = "User_Session_Info";

        /// <summary>
        /// The current user's session info created on Session New
        /// </summary>
        public static UserSessionInformation UserSessionInfo
        {
            get
            {
                return (UserSessionInformation) HttpContext.Current.Session[USER_SESSION_INFO] ?? new UserSessionInformation();
            }
            set
            {
                HttpContext.Current.Session[USER_SESSION_INFO] = value;
            }
        }

        /// <summary>
        /// The current purchase order, only valid when user is in process of finalizing an order
        /// </summary>
        public static PurchaseOrderDetails PayPalPurchaseOrder
        {
            get
            {
                return (PurchaseOrderDetails)HttpContext.Current.Session[PURCHASE_ORDER_SESSION_KEY] ?? new PurchaseOrderDetails();
            }
            set
            {
                HttpContext.Current.Session[PURCHASE_ORDER_SESSION_KEY] = value;
            }
        }

        /// <summary>
        /// The customers current shopping cart list
        /// </summary>
        public static List<ShoppingCartProduct> ShoppingCartProductList
        {
            get
            {
                return (List<ShoppingCartProduct>)HttpContext.Current.Session[SHOPPING_CART_LIST_SESSION_KEY] ?? new List<ShoppingCartProduct>();
            }
            set
            {
                HttpContext.Current.Session[SHOPPING_CART_LIST_SESSION_KEY] = value;
            }
        }

        /// <summary>
        /// Record a new page view
        /// </summary>
        /// <param name="pageName"></param>
        public static void RecordPageView(string pageName)
        {
            try
            {
                UserSessionInformation UserInfo = UserSessionInfo;

                //record the page view
                Chimera.DataAccess.ReportDAO.SaveNew(new PageView(UserInfo, pageName.ToUpper()), AppSettings.AllowPageReportRecording, UserInfo);

                UserInfo.LastDatePageRecordedUTC = DateTime.UtcNow;

                UserSessionInfo = UserInfo;
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog(String.Format("ChimeraWebsite.Helpers.SiteContextRecordPageView(pageName: {0})", pageName), e);
            }
        }

        /// <summary>
        /// Get the total count of the quantities in the shopping cart prod list
        /// </summary>
        /// <returns></returns>
        public static int GetShoppingCartProductListCount()
        {
            int ReturnCount = 0;

            var ShopCartProdList = ShoppingCartProductList;

            if (ShopCartProdList != null && ShopCartProdList.Count > 0)
            {
                foreach (var ShopCartProd in ShopCartProdList)
                {
                    ReturnCount += ShopCartProd.Quantity;
                }
            }

            return ReturnCount;
        }

        /// <summary>
        /// Add a new shopping cart product
        /// </summary>
        /// <param name="shoppingCartProduct"></param>
        public static void AddShoppingCartProduct(ShoppingCartProduct shoppingCartProduct)
        {
            List<ShoppingCartProduct> NewShoppingCartProductList = ShoppingCartProductList;

            NewShoppingCartProductList.Add(shoppingCartProduct);

            ShoppingCartProductList = NewShoppingCartProductList;
        }

        /// <summary>
        /// Remove a product from the shopping cart.
        /// </summary>
        /// <param name="id"></param>
        public static void RemoveShoppingCartProduct(string uniqueCartId)
        {
            List<ShoppingCartProduct> NewShoppingCartProductList = ShoppingCartProductList.Where(e => !e.CartUniqueId.Equals(uniqueCartId)).ToList();

            ShoppingCartProductList = NewShoppingCartProductList;
        }

        /// <summary>
        /// Clear the shopping cart and anything in session that deals with purchase orders
        /// </summary>
        public static void ClearShoppingCart()
        {
            PayPalPurchaseOrder = null;
            ShoppingCartProductList = null;
        }
    }
}