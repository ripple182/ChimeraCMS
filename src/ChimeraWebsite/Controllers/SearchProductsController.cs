using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.DataAccess;
using Chimera.Entities.Property;
using Chimera.Entities.Product;
using ChimeraWebsite.Models.Product;

namespace ChimeraWebsite.Controllers
{
    public class SearchProductsController : Controller
    {
        private string GetPartialViewPath(string viewType)
        {
            return String.Format(@"~\Templates\{0}\Views\ProductViews\Lists\{1}.ascx", Models.ChimeraTemplate.TemplateName, viewType);
        }

        /// <summary>
        /// Called to load a preview of the unique product list module
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public ActionResult Preview(string viewType, int numFakeProds)
        {
            try
            {
                List<Product> ProductList = new List<Product>();

                for (int i = 1; i <= numFakeProds; i++)
                {
                    Product Prod = new Product();

                    Prod.Name = "Fake Product " + i;
                    Prod.Description = "Praesent at tellus porttitor nisl porttitor sagittis. Mauris in massa ligula, a tempor nulla. Ut tempus interdum mauris vel vehicula. Nulla ullamcorper tortor commodo in sagittis est accumsan.";
                    Prod.PurchaseSettings.PurchasePrice = i * 5;

                    ProductList.Add(Prod);

                }

                return Content(CompanyCommons.Utility.RenderPartialViewToString(ControllerContext, GetPartialViewPath(viewType), new ProductListModel(viewType, ProductList), false));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.SearchProductsController.Active() " + e.Message);
            }

            return Content("");
        }

        /// <summary>
        /// Called to load all active products into a specific view and returns the raw html
        /// </summary>
        /// <returns></returns>
        public ActionResult Active(string viewType)
        {
            try
            {
                List<Product> ProductList = Chimera.DataAccess.ProductDAO.SearchProducts(null, string.Empty, true);

                return Content(CompanyCommons.Utility.RenderPartialViewToString(ControllerContext, GetPartialViewPath(viewType), new ProductListModel(viewType, ProductList), false));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.SearchProductsController.Active() " + e.Message);
            }

            return Content("");
        }

        /// <summary>
        /// Called to load specific products by their Ids into a specific view and returns the raw html
        /// </summary>
        /// <returns></returns>
        public ActionResult Specific(string viewType, string ids)
        {
            try
            {
                List<Product> ProductList = Chimera.DataAccess.ProductDAO.LoadSpecificProducts(ids.Split(',').ToList());

                return Content(CompanyCommons.Utility.RenderPartialViewToString(ControllerContext, GetPartialViewPath(viewType), new ProductListModel(viewType, ProductList), false));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.SearchProductsController.Specific() " + e.Message);
            }

            return Content("");
        }

        /// <summary>
        /// Called to load products filtered by search properties into a specific view and returns the raw html
        /// </summary>
        /// <returns></returns>
        public ActionResult Filtered(string viewType)
        {
            try
            {
                Dictionary<string, List<string>> SelectedSearchFilters = new Dictionary<string, List<string>>();

                List<Property> ProductSearchProperties = ProductDAO.LoadProductSearchProperties();

                if (ProductSearchProperties != null && ProductSearchProperties.Count > 0)
                {
                    foreach (var SearchProp in ProductSearchProperties)
                    {
                        if (!string.IsNullOrWhiteSpace(Request[SearchProp.Name]))
                        {
                            SelectedSearchFilters.Add(SearchProp.Name, Request[SearchProp.Name].Split(',').ToList());
                        }
                    }
                }

                List<Product> ProductList = Chimera.DataAccess.ProductDAO.SearchProducts(SelectedSearchFilters);

                return Content(CompanyCommons.Utility.RenderPartialViewToString(ControllerContext, GetPartialViewPath(viewType), new ProductListModel(viewType, ProductList), false));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.SearchProductsController.Filtered() " + e.Message);
            }

            return Content("");
        }
    }
}
