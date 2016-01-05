<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%ShoppingCartModel ShopCartModel = Model; %>
<%string GlobalTaxAmount = ShopCartModel.PaypalPurchaseSettings.GetSettingVal(PayPalSettingKeys.GlobalTaxAmount); %>
<%Html.RenderPartial(Url.Content("~/Views/PartialViews/Products/ShoppingCartCustomShippingHandling.ascx"), ShopCartModel);%>
<div class="row">
    <div class="col-md-12">

        <%if (ShopCartModel.ShoppingCartProductList != null && ShopCartModel.ShoppingCartProductList.Count > 0)
          {%>
        <%int TotalNumItems = 0; %>
        <%decimal SubTotalPrice = 0.00m; %>
        <div class="well">
            <table class="table viewCartTable_basicView">
                <thead>
                    <tr>
                        <th class="width-20 center"></th>
                        <th class="width-40">ITEM DESCRIPTION</th>
                        <th class="width-10 center">ITEM PRICE</th>
                        <th class="width-15 center">QUANTITY</th>
                        <th class="width-15 center">TOTAL PRICE</th>
                    </tr>
                </thead>
                <tbody>
                <form method="post" action="<%=Url.Content("~/ShoppingCart/UpdateQuantities") %>">
                    <%foreach (var ShopCartProd in ShopCartModel.ShoppingCartProductList)
                      {%>
                    <tr>
                        <td class="center">
                            <img class="img-thumbnail" src="<%=ShopCartProd.MainImagePath %>" title="<%=ShopCartProd.Name %>" alt="<%=ShopCartProd.Name %>" />
                        </td>
                        <td>
                            <b><%=ShopCartProd.Name %></b>
                            <br />
                            <%if (ShopCartProd.SelectedCheckoutProperties != null && ShopCartProd.SelectedCheckoutProperties.Count > 0)
                              {%>
                            <ul>
                                <%foreach (var SelectedCheckProp in ShopCartProd.SelectedCheckoutProperties)
                                  {%>
                                <li><b><%=SelectedCheckProp.Key %>:</b>&nbsp;&nbsp;<%=SelectedCheckProp.Value %></li>
                                <%}%>
                            </ul>
                            <%}%>
                        </td>
                        <td class="center"><%=ShopCartProd.GetRealItemPrice().ToString("C") %></td>
                        <td class="center">
                            <input class="form-control" value="<%=ShopCartProd.Quantity %>" name="<%=ShopCartProd.CartUniqueId %>_quantity" />
                            <a href="<%=Url.Content("~/ShoppingCart/RemoveItemFromCart?idprops=" + ShopCartProd.CartUniqueId) %>">remove item</a>
                        </td>
                        <td class="leftHandBorder center">
                            <%=(ShopCartProd.GetRealItemPrice() * ShopCartProd.Quantity).ToString("C") %>
                        </td>
                    </tr>
                    <%TotalNumItems += ShopCartProd.Quantity; %>
                    <%SubTotalPrice += ShopCartProd.GetRealItemPrice() * ShopCartProd.Quantity; %>
                    <%}%>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="5">
                            <button type="submit" class="btn btn-default btn-sm pull-right">Update Item Quantity</button>
                            </form>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <br />
                        </td>
                    </tr>
                    <form id="checkout-init-form" method="post" action="<%=Url.Content("~/Order/InitCheckout") %>">
                    <%if (ShopCartModel.ShippingMethods != null && ShopCartModel.ShippingMethods.PropertyNameValues != null && ShopCartModel.ShippingMethods.PropertyNameValues.Count > 0)
                      {%>
                    <tr>
                        <td colspan="3">
                            <div class="shippingMethod pull-right"><b>Select Shipping Method:</b></div>
                        </td>
                        <td colspan="2">
                            <select name="shippingMethod" id="checkout-shipping-method-dropdown" class="form-control">
                                <%foreach (var ShipMethVal in ShopCartModel.ShippingMethods.PropertyNameValues)
                                  {%>
                                <option value="<%=ShipMethVal %>"><%=ShipMethVal %></option>
                                <%}%>
                            </select>
                        </td>
                    </tr>
                    <%}%>
                    <tr>
                        <td  class="subtotal-label" colspan="4">
                            Subtotal ( <%=TotalNumItems %> <%=TotalNumItems == 1 ? "item" : "items" %> ):
                        </td>
                        <td class="center">$<span id="checkout-item-total"><%=SubTotalPrice.ToString("0.00") %></span></td>
                    </tr>
                    <%if (Decimal.Parse(GlobalTaxAmount) > 0)
                      {%>
                    <tr>
                        <td class="subtotal-label" colspan="4">
                            TAX:
                        </td>
                        <td class="center">$<span id="checkout-tax-total"><%=(SubTotalPrice * Decimal.Parse(GlobalTaxAmount)).ToString("0.00") %></span></td>
                    </tr>
                    <%}%>
                    <tr>
                        <td class="subtotal-label" colspan="4">
                            S & H:
                        </td>
                        <td class="center">$<span id="checkout-shipping-method-total"></span></td>
                    </tr>
                    <tr>
                        <td class="grand-total-text" colspan="4">
                            GRAND TOTAL:
                        </td>
                        <td class="grand-total-total center">$<span id="checkout-grand-total"></span></td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <br />
                            <img id="checkout-submit-button" class="pull-right" src="<%=ShopCartModel.PaypalPurchaseSettings.GetSettingVal(PayPalSettingKeys.PayPal_CheckoutBtn_Img)%>" title="Checkout with PayPal" alt="Checkout with PayPal" />
                        </td>
                    </tr>
                    </form>
                </tfoot>
            </table>
        </div>
        <%}
          else
          {%>
        <div class="error">
            <div class="error-page">
                <p class="error-med">Your shopping cart has no items yet!</p>
            </div>
        </div>
        <%}%>
    </div>
</div>
