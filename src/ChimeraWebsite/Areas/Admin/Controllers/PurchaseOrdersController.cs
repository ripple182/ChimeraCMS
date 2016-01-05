using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using ChimeraWebsite.Areas.Admin.Attributes;
using Chimera.DataAccess;
using CompanyCommons.AbstractClasses;
using CompanyCommons.Ecommerce.PayPal.Functions;
using CompanyCommons.Ecommerce.PayPal.Entities;
using CompanyCommons.Ecommerce.PayPal.Entities.Order;
using Chimera.Entities.Orders;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;
using Chimera.Entities.Admin.Role;


namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class PurchaseOrdersController : MasterController
    {
        /// <summary>
        /// Called when the admin user wishes to edit the order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PurchaseOrderRoles.EDIT)]
        public ActionResult Edit(string id)
        {
            try
            {
                ViewBag.PurchaseOrderDetail = PurchaseOrderDetailsDAO.LoadByBsonId(id);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PurchaseOrdersController.Edit()" + e.Message);
            }

            return View();
        }

        /// <summary>
        /// Called whenever the admin user wishes to ship the order, this triggers an email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trackingNumber"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PurchaseOrderRoles.EDIT)]
        public ActionResult ShipOrder(string id, string trackingNumber)
        {
            try
            {
                PurchaseOrderDetails PurchaseOrderDetail = PurchaseOrderDetailsDAO.LoadByBsonId(id);

                PurchaseOrderDetail.PayPalOrderDetails.ShippingTrackingNumber = trackingNumber;
                PurchaseOrderDetail.PayPalOrderDetails.OrderShippedDateUtc = DateTime.UtcNow;

                if (PurchaseOrderDetailsDAO.Save(PurchaseOrderDetail))
                {
                    Chimera.Core.Notifications.PurchaseOrder.ProcessPurchaseOrderShipped(PurchaseOrderDetail);

                    try
                    {
                        List<SettingGroup> SettingGroupList = SettingGroupDAO.LoadByMultipleGroupNames(new List<string> { SettingGroupKeys.TEMPLATE_CUSTOM_SETTINGS, SettingGroupKeys.EMAIL_SETTINGS });

                        SettingGroup EmailSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.EMAIL_SETTINGS)).FirstOrDefault();

                        SettingGroup TemplateSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.TEMPLATE_CUSTOM_SETTINGS)).FirstOrDefault();

                        Chimera.Emails.Ecommerce.SendOrderShippedEmail(PurchaseOrderDetail, EmailSettings.GetSettingVal(EmailSettingKeys.CustomerOrderShippedEmail), EmailSettings.GetSettingVal(EmailSettingKeys.SenderEmailAddress), TemplateSettings.GetSettingVal("WebsiteTitle"));
                    }
                    catch (Exception e)
                    {
                        CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PurchaseOrdersController.ShipOrder.SendEmail()" + e.Message);
                    }

                    AddWebUserMessageToSession(Request, String.Format("Tracking # added and user notified order shipped!"), SUCCESS_MESSAGE_TYPE);
                }
                else
                {
                    AddWebUserMessageToSession(Request, String.Format("Unable to add tracking # and email user of shipped order."), FAILED_MESSAGE_TYPE);
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PurchaseOrdersController.ShipOrder()" + e.Message);
            }

            return RedirectToAction("Search", "PurchaseOrders");
        }

        /// <summary>
        /// Called whenever the admin user wishes to finalize the paypal payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PurchaseOrderRoles.EDIT)]
        public ActionResult CapturePayment(string id)
        {
            try
            {
                PurchaseOrderDetails PurchaseOrderDetail = PurchaseOrderDetailsDAO.LoadByBsonId(id);

                SettingGroup PayPalSettings = SettingGroupDAO.LoadSettingGroupByName(SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS);

                //create auth header obj
                AuthHeader PayPalAuthHeader = Chimera.Core.PurchaseOrders.PayPalAuthHeader.GetAuthHeaderFromSetting(PayPalSettings);

                OrderDetails CapturedOrderDetails = CompanyCommons.Ecommerce.PayPal.Functions.Execute.CapturePayment(PayPalAuthHeader, PurchaseOrderDetail.PayPalOrderDetails);

                if (CapturedOrderDetails != null)
                {
                    PurchaseOrderDetail.PayPalOrderDetails = CapturedOrderDetails;

                    if (PurchaseOrderDetailsDAO.Save(PurchaseOrderDetail))
                    {
                        Chimera.Core.Notifications.PurchaseOrder.ProcessPurchaseOrderPaymentCaptured(PurchaseOrderDetail);

                        AddWebUserMessageToSession(Request, String.Format("PayPal payment successfully captured!"), SUCCESS_MESSAGE_TYPE);
                    }
                    else
                    {
                        AddWebUserMessageToSession(Request, String.Format("Unable to capture PayPal payment at this time."), FAILED_MESSAGE_TYPE);
                    }
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PurchaseOrdersController.CapturePayment()" + e.Message);
            }

            return RedirectToAction("Search", "PurchaseOrders");
        }

        /// <summary>
        /// Called whenever the admin user wishes to search thru the existing orders made by customers
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = PurchaseOrderRoles.VIEW)]
        public ActionResult Search(string paymentCapturedTrue, string paymentCapturedFalse, string orderShippedTrue, string orderShippedFalse, DateTime? orderPlacedFrom, DateTime? orderPlacedTo, string email, string firstName, string lastName, int? numberToQuery)
        {
            try
            {
                if (numberToQuery != null)
                {
                    CustomerInfo CustInfo = new CustomerInfo();

                    CustInfo.Email = email;
                    CustInfo.FirstName = firstName;
                    CustInfo.LastName = lastName;

                    bool? PaymentCaptured = null;
                    bool? OrderShipped = null;

                    if(!string.IsNullOrWhiteSpace(paymentCapturedTrue) && string.IsNullOrWhiteSpace(paymentCapturedFalse))
                    {
                        PaymentCaptured = true;
                    }
                    else if(string.IsNullOrWhiteSpace(paymentCapturedTrue) && !string.IsNullOrWhiteSpace(paymentCapturedFalse))
                    {
                        PaymentCaptured = false;
                    }

                    if(!string.IsNullOrWhiteSpace(orderShippedTrue) && string.IsNullOrWhiteSpace(orderShippedFalse))
                    {
                        OrderShipped = true;
                    }
                    else if(string.IsNullOrWhiteSpace(orderShippedTrue) && !string.IsNullOrWhiteSpace(orderShippedFalse))
                    {
                        OrderShipped = false;
                    }

                    ViewBag.paymentCapturedTrue = paymentCapturedTrue;
                    ViewBag.paymentCapturedFalse = paymentCapturedFalse;
                    ViewBag.orderShippedTrue = orderShippedTrue;
                    ViewBag.orderShippedFalse = orderShippedFalse;
                    ViewBag.orderPlacedFrom = orderPlacedFrom;
                    ViewBag.orderPlacedTo = orderPlacedTo;
                    ViewBag.email = email;
                    ViewBag.firstName = firstName;
                    ViewBag.lastName = lastName;
                    ViewBag.numberToQuery = numberToQuery;

                    ViewBag.PurchasedOrderList = PurchaseOrderDetailsDAO.Search(PaymentCaptured != null ? PaymentCaptured.Value.ToString() : "", OrderShipped != null ? OrderShipped.Value.ToString() : "", orderPlacedFrom != null ? orderPlacedFrom.Value : DateTime.MinValue, orderPlacedTo != null ? orderPlacedTo.Value : DateTime.MinValue, CustInfo, numberToQuery != null ? numberToQuery.Value : 0);
                }
                else
                {
                    ViewBag.PurchasedOrderList = PurchaseOrderDetailsDAO.Search("", "", DateTime.MinValue, DateTime.MinValue, new CustomerInfo(), 10);
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.PurchaseOrdersController.Search()" + e.Message);

                AddWebUserMessageToSession(Request, String.Format("The server is having some difficulties performing searches, please contact administrator if error continues."), FAILED_MESSAGE_TYPE);
            }

            return View();
        }

    }
}
