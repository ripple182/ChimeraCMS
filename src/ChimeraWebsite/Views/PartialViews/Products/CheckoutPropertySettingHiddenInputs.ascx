<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%Product Prod = Model; %>
<%if(Prod.CheckoutPropertySettingsList != null && Prod.CheckoutPropertySettingsList.Count > 0)
  {
      foreach (var CheckPropSet in Prod.CheckoutPropertySettingsList)
      {
          if (CheckPropSet.DefineCustomPrice)
          {
              if (CheckPropSet.CheckoutPropertySettingKeys != null && CheckPropSet.CheckoutPropertySettingKeys.Count > 0)
              {
                  string JoinedDictionary = string.Join(",", CheckPropSet.CheckoutPropertySettingKeys.Select(x => x.Key + ":" + x.Value));
                  %>
                        <input class="checkout-prod-setting-price" type="hidden" id="<%=JoinedDictionary %>" value="<%=CheckPropSet.PurchaseSettings.PurchasePrice.ToString("C") %>" />
                  <%
              }
          }
      }
  }%>
