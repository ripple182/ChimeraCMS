using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using ChimeraWebsite.Areas.Admin.Attributes;
using Chimera.DataAccess;
using Chimera.Entities.Property;
using Chimera.Entities.Product.Property;
using Chimera.Entities.Product;
using Chimera.Entities.Utility;
using Newtonsoft.Json;
using CompanyCommons.AbstractClasses;
using Chimera.Entities.Admin.Role;

namespace ChimeraWebsite.Areas.Admin.Controllers
{
    public class ProductController : MasterController
    {
        /// <summary>
        /// Called whenever the admin user wishes to view existing products.
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = ProductRoles.VIEW)]
        public ActionResult Search(string active, string inactive, string searchText = "")
        {
            try
            {
                Dictionary<string, List<string>> SelectedSearchFilters = new Dictionary<string, List<string>>();

                List<Property> ProductSearchProperties = ProductDAO.LoadProductSearchProperties();

                bool? Active = null;

                if (!string.IsNullOrWhiteSpace(active) && string.IsNullOrWhiteSpace(inactive))
                {
                    Active = true;
                }
                else if (string.IsNullOrWhiteSpace(active) && !string.IsNullOrWhiteSpace(inactive))
                {
                    Active = false;
                }

                if (ProductSearchProperties != null && ProductSearchProperties.Count > 0)
                {
                    foreach (var ProductSearchProp in ProductSearchProperties)
                    {
                        if (!string.IsNullOrWhiteSpace(Request["searchProp_" + ProductSearchProp.Name]) && !SelectedSearchFilters.ContainsKey(ProductSearchProp.Name))
                        {
                            SelectedSearchFilters.Add(ProductSearchProp.Name, Request["searchProp_" + ProductSearchProp.Name].Split(',').ToList());
                        }
                    }
                }

                ViewBag.ProductSearchProperties = ProductSearchProperties;

                ViewBag.ProductList = ProductDAO.SearchProducts(SelectedSearchFilters, searchText, Active);

                ViewBag.SelectedSearchFilters = SelectedSearchFilters;

                ViewBag.SearchText = searchText;

                ViewBag.Active = active;
                ViewBag.Inactive = inactive;

            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.ProductController.Search()" + e.Message);

                AddWebUserMessageToSession(Request, String.Format("The server is having some difficulties performing searches, please contact administrator if error continues."), FAILED_MESSAGE_TYPE);
            }

            return View();
        }

        /// <summary>
        /// Called whenever the admin user wishes to add or edit a product.
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = ProductRoles.EDIT)]
        public ActionResult Edit(string id, string productData)
        {
            try
            {
                Product Product = new Product();

                List<StaticProperty> StaticPropertyList = StaticPropertyDAO.LoadAll();

                //edit an existing product
                if (!string.IsNullOrWhiteSpace(id))
                {
                    Product = ProductDAO.LoadByBsonId(id);
                }
                //add a new product
                else if (string.IsNullOrWhiteSpace(productData))
                {
                    //add 4 empty additional images
                    Product.AdditionalImages.Add(new ProductImage());
                    Product.AdditionalImages.Add(new ProductImage());
                    Product.AdditionalImages.Add(new ProductImage());
                    Product.AdditionalImages.Add(new ProductImage());

                    StaticProperty ShippingMethods = StaticPropertyList.Where(e => e.KeyName.Equals(StaticProperty.SHIPPING_METHOD_PROPERTY_KEY)).FirstOrDefault();

                    if (ShippingMethods != null && ShippingMethods.PropertyNameValues != null && ShippingMethods.PropertyNameValues.Count > 0)
                    {
                        foreach (var ShippingMethodName in ShippingMethods.PropertyNameValues)
                        {
                            Product.PurchaseSettings.ShippingMethodList.Add(new ShippingMethodProperty(ShippingMethodName, 0));
                        }
                    }
                }
                //redirected here after attempting to save a product that failed validation
                else
                {
                    Product = JsonConvert.DeserializeObject<Product>(productData);
                }

                ViewBag.AllStaticProperties = StaticPropertyList;

                ViewBag.Product = Product;

                ViewBag.ImageList = Chimera.DataAccess.ImageDAO.LoadAll();

                return View();

            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.ProductController.Edit()" + e.Message);
            }

            AddWebUserMessageToSession(Request, String.Format("Unable to add/update products at this time."), FAILED_MESSAGE_TYPE);

            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// Called whnever the admin user wishes to save product details
        /// </summary>
        /// <returns></returns>
        [AdminUserAccess(Admin_User_Roles = ProductRoles.EDIT)]
        public ActionResult Edit_Post(string productData)
        {
            try
            {
                Product Product = JsonConvert.DeserializeObject<Product>(productData);

                if (!Product.Name.Equals(string.Empty))
                {
                    Product.RemoveCheckoutPropertySettingsDuplicants();

                    if (ProductDAO.Save(Product))
                    {
                        Chimera.Core.Notifications.ProductStock.ProductStockUpdated(Product);

                        AddWebUserMessageToSession(Request, String.Format("Successfully saved/updated product \"{0}\"", Product.Name), SUCCESS_MESSAGE_TYPE);
                    }
                    else
                    {
                        AddWebUserMessageToSession(Request, String.Format("Unable to saved/update product \"{0}\" at this time", Product.Name), FAILED_MESSAGE_TYPE);
                    }

                }
                else
                {
                    AddWebUserMessageToSession(Request, "Product must have a name", FAILED_MESSAGE_TYPE);

                    return RedirectToAction("Edit", "Product", new { productData = productData });
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Areas.Admin.Controllers.ProductController.Edit_Post() " + e.Message);
            }

            AddWebUserMessageToSession(Request, String.Format("Unable to save/update products at this time."), FAILED_MESSAGE_TYPE);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
