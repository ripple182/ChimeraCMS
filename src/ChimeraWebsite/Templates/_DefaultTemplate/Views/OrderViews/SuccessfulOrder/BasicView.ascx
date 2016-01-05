<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PurchaseOrderDetails PurchOrder = Model; %>
<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <h5>Shipping Information</h5>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Email:</label>
                            <div class="col-sm-10">
                                <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.Email %></p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Name:</label>
                            <div class="col-sm-10">
                                <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.FirstName +  " " + PurchOrder.PayPalOrderDetails.CustomerInfo.MiddleName + " " + PurchOrder.PayPalOrderDetails.CustomerInfo.LastName %></p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Street Address:</label>
                            <div class="col-sm-10">
                                <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet %></p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Street Address Two:</label>
                            <div class="col-sm-10">
                                <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet_Two %></p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">City:</label>
                            <div class="col-sm-10">
                                <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToCity %></p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">State:</label>
                            <div class="col-sm-10">
                                <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToState %></p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Postal Code:</label>
                            <div class="col-sm-10">
                                <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToZip %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <%if (PurchOrder.PurchasedProductList != null && PurchOrder.PurchasedProductList.Count > 0)
                  {%>
                <%int TotalNumItems = 0; %>
                <div class="well">
                    <h5>Order Information</h5>
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
                            <%foreach (var ShopCartProd in PurchOrder.PurchasedProductList)
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
                                <td class="center"><%=ShopCartProd.PurchasePrice.ToString("C") %></td>
                                <td class="center">
                                    <%=ShopCartProd.Quantity %>
                                </td>
                                <td class="leftHandBorder center">
                                    <%=(ShopCartProd.PurchasePrice * ShopCartProd.Quantity).ToString("C") %>
                                </td>
                            </tr>
                            <%TotalNumItems += ShopCartProd.Quantity; %>
                            <%}%>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="4" class="subtotal-label">
                                    Shipping Method:
                                </td>
                                <td class="center">
                                    <%=PurchOrder.PayPalOrderDetails.ShippingMethodType %>
                                </td>
                            </tr>
                            <tr>
                                <td class="subtotal-label" colspan="4">Subtotal ( <%=TotalNumItems %> <%=TotalNumItems == 1 ? "item" : "items" %> ):
                                </td>
                                <td class="center"><%=PurchOrder.PayPalOrderDetails.BaseAmount.ToString("C") %></td>
                            </tr>
                            <%if (PurchOrder.PayPalOrderDetails.TaxAmount > 0)
                              {%>
                            <tr>
                                <td class="subtotal-label" colspan="4">TAX:
                                </td>
                                <td class="center"><%=PurchOrder.PayPalOrderDetails.TaxAmount.ToString("C") %></td>
                            </tr>
                            <%}%>
                            <tr>
                                <td class="subtotal-label" colspan="4">S & H:
                                </td>
                                <td class="center"><%=PurchOrder.PayPalOrderDetails.ShippingAmount.ToString("C") %></td>
                            </tr>
                            <tr>
                                <td class="grand-total-text" colspan="4">GRAND TOTAL:
                                </td>
                                <td class="grand-total-total center"><%=(PurchOrder.PayPalOrderDetails.BaseAmount + PurchOrder.PayPalOrderDetails.TaxAmount + PurchOrder.PayPalOrderDetails.ShippingAmount).ToString("C") %></td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <br />
                                    <a href="<%=Url.Content("~/Order/FinalCheckout") %>" class="btn btn-primary btn-lg pull-right">Confirm Order</a>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <%}%>
            </div>
        </div>
    </div>
</div>
