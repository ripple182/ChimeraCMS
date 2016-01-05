using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chimera.DataAccess;
using Chimera.Entities.Product;
using ChimeraWebsite.Models.Product;
using Chimera.Entities.Settings;
using Chimera.Entities.Settings.Keys;
using Chimera.Entities.Report;

namespace ChimeraWebsite.Controllers
{
    public class ViewProductController : Controller
    {
        /// <summary>
        /// View the product
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            try
            {
                Product Prod = ProductDAO.LoadByBsonId(id);

                try
                {
                    if (Prod != null && !string.IsNullOrWhiteSpace(Prod.Name))
                    {
                        ChimeraWebsite.Helpers.SiteContext.RecordPageView("Ecommerce_ViewProduct=" + Prod.Name);
                    }
                }
                catch (Exception e)
                {
                    CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.ViewProduct.Details.RecordPageVisit()", e);
                }

                SettingGroup EcommerceSettings = SettingGroupDAO.LoadSettingGroupByName(SettingGroupKeys.ECOMMERCE_SETTINGS);

                ViewBag.ProductModel = new ProductModel(EcommerceSettings.GetSettingVal(ECommerceSettingKeys.ViewProductDetailPage), Prod);

                return View("Details", String.Format("~/Templates/{0}/Views/Shared/Template.Master", Models.ChimeraTemplate.TemplateName));
            }
            catch (Exception e)
            {
                CompanyCommons.Logging.WriteLog("ChimeraWebsite.Controllers.ViewProductController.Details() " + e.Message);
            }

            //TODO: return 404 page instead?
            return RedirectToAction("Index", "Home");
        }

    }
}
