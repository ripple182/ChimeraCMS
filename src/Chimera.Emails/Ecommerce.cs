using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Orders;
using Chimera.Entities.Admin;

namespace Chimera.Emails
{
    public static class Ecommerce
    {
        /// <summary>
        /// Name of the folder to look for in /bin
        /// </summary>
        private const string EMAIL_MODULE_NAME = "Ecommerce";

        /// <summary>
        /// 
        /// </summary>
       /// <param name="purchaseOrderDetails">the order details</param>
        /// <param name="customerEmailSettingName">the name of the email to use the customer should see</param>
        /// <param name="emailSender">the email address that should be listed as the sender</param>
        /// <param name="websiteTitle">The name of the website sending the email</param>
        /// <returns></returns>
        public static bool SendOrderShippedEmail(PurchaseOrderDetails purchaseOrderDetails, string customerEmailSettingName, string emailSender, string websiteTitle)
        {
            string EmailsSent = string.Empty;

            try
            {
                Dictionary<string, string> ValuesToReplace = new Dictionary<string, string>();

                ValuesToReplace.Add("[WEBSITE_TITLE]", websiteTitle);
                ValuesToReplace.Add("[FIRSTNAME]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.FirstName);
                ValuesToReplace.Add("[LASTNAME]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.LastName);
                ValuesToReplace.Add("[ORDER_CONFIRM_NUMBER]", purchaseOrderDetails.Id);
                ValuesToReplace.Add("[ORDER_DATE]", purchaseOrderDetails.PayPalOrderDetails.OrderPlacedDateUtc.ToString("g") + " UTC");
                ValuesToReplace.Add("[ORDER_DETAILS_TABLE]", GenerateOrderDetailsTable(purchaseOrderDetails));
                ValuesToReplace.Add("[SELECTED SHIPPING METHOD]", "<br/><br/>Your order is being shipped by: " + purchaseOrderDetails.PayPalOrderDetails.ShippingMethodType);
                
                string TrackingLink = CompanyCommons.PhysicalShipping.GenerateTrackingLink(purchaseOrderDetails.PayPalOrderDetails.ShippingMethodType, purchaseOrderDetails.PayPalOrderDetails.ShippingTrackingNumber);
                
                if(!string.IsNullOrWhiteSpace(TrackingLink))
                {
                    TrackingLink = "<br/><br/>Click the link below to view shipping progress:<br/><br/><a href=\"" + TrackingLink + "\">" + TrackingLink + "</a>";
                }

                ValuesToReplace.Add("[TRACKING_NUMBER]", "<br/><br/>Tracking Number: " + purchaseOrderDetails.PayPalOrderDetails.ShippingTrackingNumber + TrackingLink);

                CompanyCommons.Email.SendEmailBasedOnFile("CustomerOrderShipped_" + customerEmailSettingName + ".html", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.Email, emailSender, ValuesToReplace, null, EMAIL_MODULE_NAME);
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Emails.Ecommerce.SendNewEcommerceEmails() Unable to send emails: " + e.Message);
            }

            return string.IsNullOrWhiteSpace(EmailsSent);
        }

        /// <summary>
        /// Called when a new PayPal order is finished, we need to email the necessary admin users and the customer.
        /// </summary>
        /// <param name="notifyAdminUserList">the list of admin users that should be notified of the new order</param>
        /// <param name="purchaseOrderDetails">the order details</param>
        /// <param name="customerEmailSettingName">the name of the email to use the customer should see</param>
        /// <param name="emailSender">the email address that should be listed as the sender</param>
        /// <returns>bool if all emails sent</returns>
        public static bool SendNewEcommerceOrderEmails(List<AdminUser> notifyAdminUserList, PurchaseOrderDetails purchaseOrderDetails, string customerEmailSettingName, string emailSender, string websiteTitle)
        {
            string EmailsSent = string.Empty;

            try
            {
                Dictionary<string, string> ValuesToReplace = new Dictionary<string, string>();

                ValuesToReplace.Add("[WEBSITE_TITLE]", websiteTitle);
                ValuesToReplace.Add("[FIRSTNAME]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.FirstName);
                ValuesToReplace.Add("[LASTNAME]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.LastName);
                ValuesToReplace.Add("[ORDER_CONFIRM_NUMBER]", purchaseOrderDetails.Id);
                ValuesToReplace.Add("[ORDER_DATE]", purchaseOrderDetails.PayPalOrderDetails.OrderPlacedDateUtc.ToString("g") + " UTC");
                ValuesToReplace.Add("[ORDER_DETAILS_TABLE]", GenerateOrderDetailsTable(purchaseOrderDetails));
                ValuesToReplace.Add("[EMAIL_ADDRESS]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.Email);
                ValuesToReplace.Add("[SHIPPING_NAME]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.ShipToName);
                ValuesToReplace.Add("[STREET_ADDRESS]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.ShipToStreet);
                ValuesToReplace.Add("[STREET_ADDRESS_TWO]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.ShipToStreet_Two);
                ValuesToReplace.Add("[CITY]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.ShipToCity);
                ValuesToReplace.Add("[STATE]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.ShipToState);
                ValuesToReplace.Add("[POSTAL_CODE]", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.ShipToZip);
                ValuesToReplace.Add("[SELECTED SHIPPING METHOD]", purchaseOrderDetails.PayPalOrderDetails.ShippingMethodType);

                //email the customer
                try
                {
                    if (!CompanyCommons.Email.SendEmailBasedOnFile("CustomerNewOrder_" + customerEmailSettingName + ".html", purchaseOrderDetails.PayPalOrderDetails.CustomerInfo.Email, emailSender, ValuesToReplace, null, EMAIL_MODULE_NAME))
                    {
                        EmailsSent += "FAIL";
                    }
                }
                catch (Exception e)
                {
                    CompanyCommons.Logging.WriteLog("Chimera.Emails.Ecommerce.SendNewEcommerceEmails() Unable to send email: " + customerEmailSettingName + ".html " + e.Message);
                }

                //email admin users
                try
                {
                    if (notifyAdminUserList != null && notifyAdminUserList.Count > 0)
                    {
                        foreach (var AdminUser in notifyAdminUserList)
                        {
                            if (!string.IsNullOrWhiteSpace(AdminUser.Email))
                            {
                                if(!CompanyCommons.Email.SendEmailBasedOnFile("AdminUsersNewOrder.html", AdminUser.Email, emailSender, ValuesToReplace, null, EMAIL_MODULE_NAME))
                                {
                                    EmailsSent += "FAIL";
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    CompanyCommons.Logging.WriteLog("Chimera.Emails.Ecommerce.SendNewEcommerceEmails() Unable to send email: AdminUsersNewOrder.html " + e.Message);
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Emails.Ecommerce.SendNewEcommerceEmails() Unable to send emails: " + e.Message);
            }

            return string.IsNullOrWhiteSpace(EmailsSent);
        }

        /// <summary>
        /// Generate the html table that displays the list of products.
        /// </summary>
        /// <param name="purchaseOrderDetails">the order details</param>
        /// <returns>string containing all the html</returns>
        private static string GenerateOrderDetailsTable(PurchaseOrderDetails purchaseOrderDetails)
        {
            string FinalTableHTML = string.Empty;

            try
            {
                FinalTableHTML = "<table cellpadding='0' width='100%' cellspacing='0' border='0' style='border-left: 1px solid #d6d6d6; border-right: 1px solid #d6d6d6;'>";

                if (purchaseOrderDetails.PurchasedProductList != null && purchaseOrderDetails.PurchasedProductList.Count > 0)
                {
                    FinalTableHTML += "<tr>";
                    FinalTableHTML += "<th style='font-weight: bold; vertical-align: middle; background-color: #f4f4f4; border-top: 1px solid #d6d6d6; border-bottom: 1px solid #d6d6d6; font-size: 11px; text-align: left; padding: 10px 0px; padding-left: 5px;'>ITEM DESCRIPTION</th>";
                    FinalTableHTML += "<th style='font-weight: bold; vertical-align: middle; background-color: #f4f4f4; border-top: 1px solid #d6d6d6; border-bottom: 1px solid #d6d6d6; font-size: 11px; text-align: left; padding: 10px 0px; width: 130px;'>ITEM PRICE</th>";
                    FinalTableHTML += "<th style='font-weight: bold; vertical-align: middle; background-color: #f4f4f4; border-top: 1px solid #d6d6d6; border-bottom: 1px solid #d6d6d6; font-size: 11px; text-align: left; padding: 10px 0px; width: 105px;'>QUANTITY</th>";
                    FinalTableHTML += "<th style='font-weight: bold; vertical-align: middle; background-color: #f4f4f4; border-top: 1px solid #d6d6d6; border-bottom: 1px solid #d6d6d6; font-size: 11px; text-align: center; padding: 10px 0px; width: 105px;'>TOTAL PRICE</th>";
                    FinalTableHTML += "</tr>";

                    int NumberOfItems = 0;

                    foreach (var PurchProd in purchaseOrderDetails.PurchasedProductList)
                    {
                        //html for the item list
                        FinalTableHTML += "<tr>";
                        FinalTableHTML += "<td style='font-size: 11px; text-align: left; border-bottom: 1px solid #d6d6d6; vertical-align: top; padding-top: 20px; padding-bottom: 20px; padding-left: 5px;'>";
                        FinalTableHTML += "<div style='font-weight: bold; text-transform: uppercase; color: #000; text-decoration: none;'>" + PurchProd.Name + "</div>";
                        FinalTableHTML += "<br>";

                        if (PurchProd.SelectedCheckoutProperties != null && PurchProd.SelectedCheckoutProperties.Count > 0)
                        {
                            FinalTableHTML += "<ul style='margin: 0;padding: 0;list-style: none;'>";

                            foreach (var SelectedProp in PurchProd.SelectedCheckoutProperties)
                            {
                                FinalTableHTML += "<li style='list-style: none;'><span style='text-transform: uppercase; font-weight: bold;display: inline;float: left;width: 60px;'>" + SelectedProp.Key + ":</span>" + SelectedProp.Value + "</li>";
                            }

                            FinalTableHTML += "</ul>";
                        }

                        FinalTableHTML += "</td>";
                        FinalTableHTML += "<td style='text-align: left;border-bottom: 1px solid #d6d6d6;vertical-align: top;padding-top: 20px;padding-bottom: 20px;font-size: 12px;'>";
                        FinalTableHTML += PurchProd.PurchasePrice.ToString("C");
                        FinalTableHTML += "</td>";
                        FinalTableHTML += "<td style='text-align: left;border-bottom: 1px solid #d6d6d6;vertical-align: top;padding-top: 20px;padding-bottom: 20px;font-size: 12px;'>";
                        FinalTableHTML += PurchProd.Quantity;
                        FinalTableHTML += "</td>";
                        FinalTableHTML += "<td style='text-align: center;border-bottom: 1px solid #d6d6d6;border-left: 1px solid #d6d6d6;vertical-align: top;padding-top: 20px;color: #000; font-size: 12px;'>";
                        FinalTableHTML += (PurchProd.Quantity * PurchProd.PurchasePrice).ToString("C");
                        FinalTableHTML += "</td>";
                        FinalTableHTML += "</tr>";

                        NumberOfItems += PurchProd.Quantity;
                    }

                    //now html for the subtotal costs
                    FinalTableHTML += "<tr>";

                    FinalTableHTML += "<td style='text-align: right;padding-right: 10px;background-color: #f4f4f4;padding-bottom: 5px;padding-top: 5px; font-size: 12px;' colspan='3'>SUBTOTAL ( " + NumberOfItems;

                    if (NumberOfItems == 1)
                    {
                        FinalTableHTML += " item";
                    }
                    else
                    {
                        FinalTableHTML += " items";
                    }

                    FinalTableHTML += " ):</td>";

                    FinalTableHTML += "<td style='color: #000;text-align: right;padding-right: 20px;width: 105px;background-color: #f4f4f4;padding-bottom: 5px;padding-top: 5px; font-size: 12px;'>" + purchaseOrderDetails.PayPalOrderDetails.BaseAmount.ToString("C") + "</td>";

                    FinalTableHTML += "</tr>";

                    FinalTableHTML += "<tr>";

                    FinalTableHTML += "<td style='text-align: right;padding-right: 10px;background-color: #f4f4f4;padding-bottom: 5px;padding-top: 5px; font-size: 12px;' colspan='3'>TAX:</td>";

                    FinalTableHTML += "<td style='color: #000;text-align: right;padding-right: 20px;width: 105px;background-color: #f4f4f4;padding-bottom: 5px;padding-top: 5px; font-size: 12px;'>" + purchaseOrderDetails.PayPalOrderDetails.TaxAmount.ToString("C") + "</td>";

                    FinalTableHTML += "</tr>";

                    FinalTableHTML += "<tr>";

                    FinalTableHTML += "<td style='text-align: right;padding-right: 10px;background-color: #f4f4f4;padding-bottom: 5px;padding-top: 5px; font-size: 12px;' colspan='3'>S & H:</td>";

                    FinalTableHTML += "<td style='color: #000;text-align: right;padding-right: 20px;width: 105px;background-color: #f4f4f4;padding-bottom: 5px;padding-top: 5px; font-size: 12px;'>" + purchaseOrderDetails.PayPalOrderDetails.ShippingAmount.ToString("C") + "</td>";

                    FinalTableHTML += "</tr>";

                    FinalTableHTML += "<tr>";

                    FinalTableHTML += "<td style='text-align: right;padding-right: 10px;padding-bottom: 15px;padding-top: 15px;border-top: 1px solid #d6d6d6;background: #d9eefa;color: #2c5987;font-weight: bold;border-bottom: 1px solid #d6d6d6;' colspan='3'>GRAND TOTAL:</td>";

                    FinalTableHTML += "<td style='text-align: right;padding-right: 20px;width: 105px;padding-bottom: 15px;padding-top: 15px;border-top: 1px solid #d6d6d6;font: 24px Georgia, \"Times New Roman\", serif;background: #d9eefa;color: #2c5987;border-bottom: 1px solid #d6d6d6;'>" + (purchaseOrderDetails.PayPalOrderDetails.ShippingAmount + purchaseOrderDetails.PayPalOrderDetails.TaxAmount + purchaseOrderDetails.PayPalOrderDetails.BaseAmount).ToString("C") + "</td>";

                    FinalTableHTML += "</tr>";
                }

                FinalTableHTML += "</table>";
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("Chimera.Emails.Ecommerce.GenerateOrderDetailsTable(): " + e.Message);
            }

            return FinalTableHTML;
        }
    }
}
