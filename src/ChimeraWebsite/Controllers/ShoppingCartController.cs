using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.DataAccess;
using ChimeraWebsite.Models.ShoppingCart;
using Chimera.Entities.Property;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;
using Chimera.Entities.Product;
using Chimera.Entities.Product.Property;
using Chimera.Entities.Orders;
using ChimeraWebsite.Helpers;
using CompanyCommons.AbstractClasses;
using Chimera.Entities.Report;

namespace ChimeraWebsite.Controllers
{
    public class ShoppingCartController : MasterController
    {
        /// <summary>
        /// Get the model for the view cart view
        /// </summary>
        /// <returns></returns>
        private ShoppingCartModel GetModel()
        {
            List<SettingGroup> SettingGroupList = SettingGroupDAO.LoadByMultipleGroupNames(new List<string> { SettingGroupKeys.ECOMMERCE_SETTINGS, SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS });

            SettingGroup EcommerceSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.ECOMMERCE_SETTINGS)).FirstOrDefault();

            SettingGroup PaypalPurchaseSettings = SettingGroupList.Where(e => e.GroupKey.Equals(SettingGroupKeys.PAYPAL_PURCHASE_SETTINGS)).FirstOrDefault();

            StaticProperty ShippingMethods = StaticPropertyDAO.LoadByKeyName(StaticProperty.SHIPPING_METHOD_PROPERTY_KEY);

            return new ShoppingCartModel(PaypalPurchaseSettings, EcommerceSettings.GetSettingVal(ECommerceSettingKeys.ViewShoppingCartPage), ShippingMethods);
        }

        /// <summary>
        /// Called whenever the user wishes to view their shopping cart.
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewCart()
        {
            try
            {
                SiteContext.RecordPageView("Ecommerce_ViewShoppingCart");

                ViewBag.ShoppingCartModel = GetModel();

                return View("ViewCart", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.ShoppingCartController.ViewCart() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Called whenever the customer wishes to update the quantity
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateQuantities()
        {
            try
            {
                List<ShoppingCartProduct> ShoppingCartList = SiteContext.ShoppingCartProductList;

                List<ShoppingCartProduct> FinalShoppingCartList = new List<ShoppingCartProduct>();

                if (ShoppingCartList != null && ShoppingCartList.Count > 0)
                {
                    foreach (var ShopCartProd in ShoppingCartList)
                    {
                        if (!string.IsNullOrWhiteSpace(Request[ShopCartProd.CartUniqueId + "_quantity"]))
                        {
                            int NewQuantity = Int32.Parse(Request[ShopCartProd.CartUniqueId + "_quantity"]);

                            if (ShopCartProd.CanAddToCartOrIncreaseQuantity(NewQuantity))
                            {
                                ShopCartProd.Quantity = NewQuantity;
                            }
                            else
                            {
                                //add user message
                                AddWebUserMessageToSession(Request, ShopCartProd.CreateOutOfStockMessage(), NEUTRAL_MESSAGE_TYPE);
                            }

                            if (ShopCartProd.Quantity > 0)
                            {
                                FinalShoppingCartList.Add(ShopCartProd);
                            }
                        }
                    }
                }

                SiteContext.ShoppingCartProductList = FinalShoppingCartList;

                if (FinalShoppingCartList.Count > 0)
                {
                    ViewBag.ShoppingCartModel = GetModel();

                    return View("ViewCart", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.ShoppingCartController.RemoveItemFromCart() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Remove an item from the shopping cart
        /// </summary>
        /// <param name="idprops">the custom identify for this shopping cart item</param>
        /// <returns></returns>
        public ActionResult RemoveItemFromCart(string idprops)
        {
            try
            {
                SiteContext.RemoveShoppingCartProduct(idprops);

                if (SiteContext.ShoppingCartProductList.Count > 0)
                {
                    ViewBag.ShoppingCartModel = GetModel();

                    return View("ViewCart", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.ShoppingCartController.RemoveItemFromCart() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Called whenever the user wishes to add a new item to their shopping cart.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddItemToCart(string id)
        {
            try
            {
                SiteContext.RecordPageView("Ecommerce_ViewShoppingCart");

                Product Prod = ProductDAO.LoadByBsonId(id);

                if (Prod != null && !Prod.Id.Equals(string.Empty))
                {
                    ShoppingCartProduct ShopCartProd = new ShoppingCartProduct(Prod);


                    //first build the key/value pairs that the user selected in the view product form
                    List<CheckoutPropertySettingKey> CheckoutPropertySettingKeys = new List<CheckoutPropertySettingKey>();

                    if (Prod.CheckoutPropertyList != null && Prod.CheckoutPropertyList.Count > 0)
                    {
                        foreach (var CheckProp in Prod.CheckoutPropertyList)
                        {
                            if (!string.IsNullOrWhiteSpace(Request["checkProd_" + CheckProp.Name]))
                            {
                                CheckoutPropertySettingKeys.Add(new CheckoutPropertySettingKey(CheckProp.Name, Request["checkProd_" + CheckProp.Name]));
                            }
                        }
                    }

                    //if there are even custom settings
                    if (Prod.CheckoutPropertySettingsList != null && Prod.CheckoutPropertySettingsList.Count > 0)
                    {
                        //now find the custom setting with the customers selected key/values
                        foreach (var CheckPropSetting in Prod.CheckoutPropertySettingsList)
                        {
                            int NumberOfMatches = 0;

                            if (CheckPropSetting.CheckoutPropertySettingKeys != null && CheckPropSetting.CheckoutPropertySettingKeys.Count > 0)
                            {
                                foreach (var CheckPropSettingKey in CheckPropSetting.CheckoutPropertySettingKeys)
                                {
                                    CheckoutPropertySettingKey MatchedCheckPropSetKey = CheckoutPropertySettingKeys.Where(e => e.Key.Equals(CheckPropSettingKey.Key) && e.Value.Equals(CheckPropSettingKey.Value)).FirstOrDefault();

                                    if (MatchedCheckPropSetKey != null && !string.IsNullOrWhiteSpace(MatchedCheckPropSetKey.Key) && !string.IsNullOrWhiteSpace(MatchedCheckPropSetKey.Value))
                                    {
                                        NumberOfMatches++;
                                    }
                                }
                            }

                            //if this checkout property setting matched the same key/value pairs the user selected
                            if (NumberOfMatches > 0 && NumberOfMatches == CheckoutPropertySettingKeys.Count)
                            {
                                ShopCartProd.CheckoutPropertySetting = CheckPropSetting;

                                break;
                            }
                        }
                    }

                    ShopCartProd.SelectedCheckoutProperties = CheckoutPropertySettingKeys;

                    ShoppingCartProduct AlreadyExistsShopCartProd = SiteContext.ShoppingCartProductList.Where(e => e.CartUniqueId.Equals(ShopCartProd.CartUniqueId)).FirstOrDefault();

                    //if not null then this user already has this exact prod/setting match, just increase quantity
                    if (AlreadyExistsShopCartProd != null)
                    {
                        if (AlreadyExistsShopCartProd.CanAddToCartOrIncreaseQuantity(AlreadyExistsShopCartProd.Quantity + 1))
                        {
                            AlreadyExistsShopCartProd.Quantity++;
                            SiteContext.RemoveShoppingCartProduct(AlreadyExistsShopCartProd.CartUniqueId);
                            SiteContext.AddShoppingCartProduct(AlreadyExistsShopCartProd);
                        }
                        else
                        {
                            //add user message
                            AddWebUserMessageToSession(Request, AlreadyExistsShopCartProd.CreateOutOfStockMessage(), NEUTRAL_MESSAGE_TYPE);
                        }
                    }
                    //else add a brand new product to the shopping cart
                    else
                    {
                        if (ShopCartProd.CanAddToCartOrIncreaseQuantity(1))
                        {
                            SiteContext.AddShoppingCartProduct(ShopCartProd);
                        }
                        else
                        {
                            //add user message
                            AddWebUserMessageToSession(Request, ShopCartProd.CreateOutOfStockMessage(), NEUTRAL_MESSAGE_TYPE);
                        }
                    }

                    ViewBag.ShoppingCartModel = GetModel();

                    return View("ViewCart", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
                }
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.ShoppingCartController.AddItemToCart() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }
    }
}
