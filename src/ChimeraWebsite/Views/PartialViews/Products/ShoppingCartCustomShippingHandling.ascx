<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%ShoppingCartModel ShopCartModel = Model; %>
<%Dictionary<string, decimal> GlobalShippingMethodDictionary = ShopCartModel.GetGlobalShippingMethodDictionary(); %>
<%if (ShopCartModel.ShoppingCartProductList != null && ShopCartModel.ShoppingCartProductList.Count > 0)
  {
      foreach (var ShopCartProd in ShopCartModel.ShoppingCartProductList)
      {
          if (ShopCartModel.ShippingMethods != null && ShopCartModel.ShippingMethods.PropertyNameValues != null && ShopCartModel.ShippingMethods.PropertyNameValues.Count > 0)
          {
                foreach (var ShippValueKey in ShopCartModel.ShippingMethods.PropertyNameValues)
                {
                    %><input class="checkout-prod-shipping-price" type="hidden" shipping-property-name="<%=ShippValueKey %>" value="<%=(ShopCartProd.GetRealShippingCost(ShippValueKey) * ShopCartProd.Quantity).ToString("0.00") %>" /><%         
                }
              
          }
      }
  }%>
<%
if (ShopCartModel.ShippingMethods != null && ShopCartModel.ShippingMethods.PropertyNameValues != null && ShopCartModel.ShippingMethods.PropertyNameValues.Count > 0)
{
    foreach (var ShippValueKey in ShopCartModel.ShippingMethods.PropertyNameValues)
    {
        %><input class="checkout-prod-shipping-price" type="hidden" shipping-property-name="<%=ShippValueKey %>" value="<%=GlobalShippingMethodDictionary[ShippValueKey].ToString("0.00") %>" /><%         
    }
              
} %>