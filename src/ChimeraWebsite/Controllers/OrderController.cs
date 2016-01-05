using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.DataAccess;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;  
using Chimera.Entities.Product;
using Chimera.Entities.Product.Property;
using Chimera.Entities.Orders;
using ChimeraWebsite.Helpers;
using CompanyCommons.Ecommerce.PayPal.Functions;
using CompanyCommons.Ecommerce.PayPal.Entities; 
using CompanyCommons.Ecommerce.PayPal.Entities.Order;  
using CM = System.Configuration.ConfigurationManager;
using ChimeraWebsite.Models.Order;
using Chimera.Entities.Admin;  
using MongoDB.Bson;
using Chimera.Entities.Report;

namespace ChimeraWebsite.Controllers
{
    public class OrderController : Controller
    {
        /// <summary>
        /// Called after the user reviews their order on our side right and wishes to finalize the purchase
        /// </summary>
        /// <returns></returns>
        public ActionResult FinalCheckout()
        {
            SiteContext.RecordPageView("Ecommerce_FinalCheckout");

            try
            {
                PurchaseOrderDetails PayPalPurchaseOrder = SiteContext.PayPalPurchaseOrder;

                if (PayPalPurchaseOrder != null && !string.IsNullOrWhiteSpace(PayPalPurchaseOrder.PayPalOrderDetails.PaypalInfo.Token))
                {
                    List<SettingGroup> SettingGroupList = SettingGroupDAO.LoadByMultipleGroupNames(new List<string> { SettingGroupKeys.TEMPLATE_CUSTOM_SETTINGS, SettingGroupKeys.EMAIL_SETTINGS, SettingGroupKeys.ECOMMERCE_SETTINGS, SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS });

                    SettingGroup PayPalSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS)).FirstOrDefault();

                    //create auth header obj
                    AuthHeader PayPalAuthHeader = Chimera.Core.PurchaseOrders.PayPalAuthHeader.GetAuthHeaderFromSetting(PayPalSettings);

                    OrderDetails FinalOrderDetails = CompanyCommons.Ecommerce.PayPal.Functions.Execute.FinalConfirmedAuthorization(PayPalAuthHeader, PayPalPurchaseOrder.PayPalOrderDetails);

                    if (FinalOrderDetails != null)
                    {
                        PayPalPurchaseOrder.PayPalOrderDetails = FinalOrderDetails;

                        //generate a new id so we can give the user a confirmation #
                        PayPalPurchaseOrder.Id = ObjectId.GenerateNewId().ToString();

                        //save to the database as an order
                        PurchaseOrderDetailsDAO.Save(PayPalPurchaseOrder);

                        //clear session cart
                        SiteContext.ClearShoppingCart();

                        //subtract stock from current products
                        Chimera.Core.PurchaseOrders.ProductStock.ProcessNewOrderStockLevels(PayPalSettings, PayPalPurchaseOrder);

                        //send emails
                        Chimera.Core.PurchaseOrders.Email.SendNewEcommerceOrderEmails(SettingGroupList, PayPalPurchaseOrder);

                        //add admin notification
                        Chimera.Core.Notifications.PurchaseOrder.ProcessNewPurchaseOrder(PayPalPurchaseOrder);

                        //return view
                        SettingGroup EcommerceSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.ECOMMERCE_SETTINGS)).FirstOrDefault();

                        ViewBag.ConfirmationNumber = PayPalPurchaseOrder.Id.ToString();
                        ViewBag.ViewType = EcommerceSettings.GetSettingVal(ECommerceSettingKeys.CheckoutFinishedPage);

                        return View("FinalCheckout", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
                    }
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.OrderController.FinalCheckout() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Called whenever a customer cancels an order
        /// </summary>
        /// <returns></returns>
        public ActionResult PayPalCancel()
        {
            SiteContext.PayPalPurchaseOrder = null;

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// User redirected back here from paypal after successfully filling out all the necessary form data from paypal
        /// </summary>
        /// <returns></returns>
        public ActionResult PayPalSuccess()
        {

            try
            {

                if (!string.IsNullOrWhiteSpace(SiteContext.PayPalPurchaseOrder.PayPalOrderDetails.PaypalInfo.Token))
                {
                    SiteContext.RecordPageView("Ecommerce_PayPalSuccess");

                    List<SettingGroup> SettingGroupList = SettingGroupDAO.LoadByMultipleGroupNames(new List<string> { SettingGroupKeys.ECOMMERCE_SETTINGS, SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS });

                    SettingGroup PayPalSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS)).FirstOrDefault();

                    //create auth header obj
                    AuthHeader PayPalAuthHeader = Chimera.Core.PurchaseOrders.PayPalAuthHeader.GetAuthHeaderFromSetting(PayPalSettings);

                    PurchaseOrderDetails PayPalPurchaseOrder = SiteContext.PayPalPurchaseOrder;

                    OrderDetails CheckoutOrderDetails = CompanyCommons.Ecommerce.PayPal.Functions.Execute.ExpressCheckout(PayPalAuthHeader, PayPalPurchaseOrder.PayPalOrderDetails);

                    if (CheckoutOrderDetails != null)
                    {
                        PayPalPurchaseOrder.PayPalOrderDetails = CheckoutOrderDetails;

                        SiteContext.PayPalPurchaseOrder = PayPalPurchaseOrder;

                        SettingGroup EcommerceSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.ECOMMERCE_SETTINGS)).FirstOrDefault();

                        ViewBag.PayPalSuccessModel = new PayPalSuccessModel(EcommerceSettings.GetSettingVal(ECommerceSettingKeys.FinalizeCheckoutPage), PayPalPurchaseOrder);

                        return View("PayPalSuccess", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
                    }
                }

            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.OrderController.PayPalSuccess() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// User redirect to whenever they click the checkout button from the view shopping cart page to start their paypal checkout.
        /// </summary>
        /// <param name="shippingMethod"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InitCheckout(string shippingMethod)
        {
            try
            {
                List<ShoppingCartProduct> ShoppingCartList = SiteContext.ShoppingCartProductList;

                SettingGroup PayPalSettings = SettingGroupDAO.LoadSettingGroupByName(SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS);

                //create auth header obj
                AuthHeader PayPalAuthHeader = Chimera.Core.PurchaseOrders.PayPalAuthHeader.GetAuthHeaderFromSetting(PayPalSettings);

                //create authorization obj
                Authorization PayPalAuthorization = new Authorization();

                string BaseWebsiteURL = CM.AppSettings["BaseWebsiteURL"];

                PayPalAuthorization.StoreImageURL = PayPalSettings.GetSettingVal(PayPalSettingKeys.PayPal_HDRIMG);
                PayPalAuthorization.SuccessOrderURL = BaseWebsiteURL + "Order/PayPalSuccess";
                PayPalAuthorization.CancelOrderURL = BaseWebsiteURL + "Order/PayPalCancel";

                //create purchase order details obj
                PurchaseOrderDetails PurchaseOrder = new PurchaseOrderDetails(shippingMethod, ShoppingCartList, Helpers.ShippingMethod.GetGlobalShippingMethodDictionary(null, PayPalSettings)[shippingMethod], PayPalSettings.GetSettingVal(PayPalSettingKeys.GlobalTaxAmount));

                //call paypal API to get new order details
                OrderDetails AuthOrderDetails = CompanyCommons.Ecommerce.PayPal.Functions.Execute.Authorization(PayPalAuthHeader, PayPalAuthorization, PurchaseOrder.PayPalOrderDetails, PurchaseOrder.CreatePayPalItemDescriptions());

                if (AuthOrderDetails != null)
                {
                    //store purchase order object into session
                    PurchaseOrder.PayPalOrderDetails = AuthOrderDetails;

                    //add updated info to session
                    SiteContext.PayPalPurchaseOrder = PurchaseOrder;

                    //redirect to paypal
                    return Redirect(CompanyCommons.Ecommerce.PayPal.Functions.Execute.GetAuthorizationRedirectURL(PayPalSettings.GetSettingVal(PayPalSettingKeys.PayPal_REDIRECT), AuthOrderDetails));
                }

                //if we got this far the call the paypal's API failed
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.OrderController.InitCheckout() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }
    }
}
