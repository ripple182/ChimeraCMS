<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%PurchaseOrderDetails PurchOrder = ViewBag.PurchaseOrderDetail;%>

    <div class="hero">
        <h3><span>Edit Purchase Order</span></h3>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="well">
                <h5>Order Details</h5>
                <div class="form-horizontal chimera-edit-purch-order-form">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Order Time:</label>
                        <div class="col-sm-10">
                            <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.OrderPlacedDateUtc.ToString("g") %> UTC</p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Customer's Name:</label>
                        <div class="col-sm-10">
                            <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.FirstName + " " + PurchOrder.PayPalOrderDetails.CustomerInfo.MiddleName + " " + PurchOrder.PayPalOrderDetails.CustomerInfo.LastName + " " + PurchOrder.PayPalOrderDetails.CustomerInfo.Suffix %></p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Customer's Phone #:</label>
                        <div class="col-sm-10">
                            <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToPhoneNum %></p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Shipping Address:</label>
                        <div class="col-sm-10">
                            <p class="form-control-static">
                                <%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToName %>
                                <br />
                                <%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet %>
                                <br />
                                <%if (!string.IsNullOrWhiteSpace(PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet_Two))
                                  {%>
                                <%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet_Two%>
                                <br />
                                <%}%>
                                <%=PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToCity + ", " + PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToState + " " + PurchOrder.PayPalOrderDetails.CustomerInfo.ShipToZip %>
                            </p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Shipping Method Type:</label>
                        <div class="col-sm-10">
                            <p class="form-control-static"><%=PurchOrder.PayPalOrderDetails.ShippingMethodType %></p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">PayPal Payment Completed:</label>
                        <div class="col-sm-10">
                            <p class="form-control-static">
                                <%if (!PurchOrder.PayPalOrderDetails.PaymentCapturedDateUtc.Equals(DateTime.MinValue))
                                  {%>
                                <div class="alert alert-success">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <%= PurchOrder.PayPalOrderDetails.PaymentCapturedDateUtc.ToString("g")%> UTC
                                </div>
                                <%}
                                  else
                                  {%>
                                <div class="alert alert-danger">
                                    <span class="glyphicon glyphicon-remove"></span>
                                    <span>Payment Required!</span>
                                    <br />
                                    <a class="btn btn-danger btn-md" href="<%=Url.Content("~/Admin/PurchaseOrders/CapturePayment?id=" + PurchOrder.Id) %>">Complete Payment Now</a>
                                </div>
                                <%}%>
                            </p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Order Shipped:</label>
                        <div class="col-sm-10">
                            <p class="form-control-static">
                                <%if (!PurchOrder.PayPalOrderDetails.OrderShippedDateUtc.Equals(DateTime.MinValue))
                                  {%>
                                <div class="alert alert-success">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <%= PurchOrder.PayPalOrderDetails.OrderShippedDateUtc.ToString("g")%> UTC
                                    <br />
                                    Tracking #: <%=PurchOrder.PayPalOrderDetails.ShippingTrackingNumber %>
                                </div>
                                <%}
                                  else if (PurchOrder.PayPalOrderDetails.PaymentCapturedDateUtc.Equals(DateTime.MinValue))
                                  {%>
                                <div class="alert alert-danger">
                                    <span class="glyphicon glyphicon-remove"></span>
                                    Capture PayPal payment before shipping!
                                </div>
                                <%}
                                  else
                                  {%>
                                <div class="alert alert-danger">
                                    <span class="glyphicon glyphicon-remove"></span>
                                    Ship This Order!
                                    <br />
                                    <form method="post" action="<%=Url.Content("~/Admin/PurchaseOrders/ShipOrder?id=" + PurchOrder.Id) %>">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Enter Tracking #:</label>
                                            <div class="col-sm-10">
                                                <input class="form-control" type="text" name="trackingNumber" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-md-10">
                                                <button type="submit" class="btn btn-primary btn-md">Save tracking # and notify customer</button>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                                <%}%>
                            </p>
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
                <h5>Ordered Products</h5>
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
                    </tfoot>
                </table>
            </div>
            <%}%>
        </div>
    </div>
</asp:Content>
