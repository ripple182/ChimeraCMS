<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<PurchaseOrderDetails> PurcOrderList = ViewBag.PurchasedOrderList;
      string paymentCapturedTrue = ViewBag.paymentCapturedTrue;
      string paymentCapturedFalse = ViewBag.paymentCapturedFalse;
      string orderShippedTrue = ViewBag.orderShippedTrue;
      string orderShippedFalse = ViewBag.orderShippedFalse;
      DateTime? orderPlacedFrom = ViewBag.orderPlacedFrom;
      DateTime? orderPlacedTo = ViewBag.orderPlacedTo;
      string email = ViewBag.email;
      string firstName = ViewBag.firstName;
      string lastName = ViewBag.lastName;
      int numberToQuery = ViewBag.numberToQuery ?? 10;
    %>

    <div class="hero">
        <h3><span>Search Purchase Orders</span></h3>
    </div>
    <form class="form-horizontal" method="get" action="<%=Url.Content("~/Admin/PurchaseOrders/Search") %>">
        <div class="form">
            <div class="row">
                <div class="col-md-12">
                    <div class="well">
                        <h5>Search Filters</h5>
                        <div class="form-group">
                            <label class="control-label col-md-2">Customer Email</label>
                            <div class="col-md-10">
                                <input type="text" class="form-control" name="email" value="<%=email %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">First Name</label>
                            <div class="col-md-4">
                                <input type="text" class="form-control" name="firstName" value="<%=firstName%>">
                            </div>
                            <label class="control-label col-md-2">Last Name</label>
                            <div class="col-md-4">
                                <input type="text" class="form-control" name="lastName" value="<%=lastName %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Order Placed From</label>
                            <div class="col-md-4">
                                <input type="text" class="form-control date-picker" name="orderPlacedFrom" value="<%=orderPlacedFrom != null ? orderPlacedFrom.Value.ToShortDateString() : "" %>">
                            </div>
                            <label class="control-label col-md-2">Order Place To</label>
                            <div class="col-md-4">
                                <input type="text" class="form-control date-picker" name="orderPlacedTo" value="<%=orderPlacedTo != null ? orderPlacedTo.Value.ToShortDateString() : "" %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">PayPal Payment Captured</label>
                            <div class="col-md-4">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="paymentCapturedTrue" value="true" <%if (!string.IsNullOrWhiteSpace(paymentCapturedTrue))
                                                                                                         {%>checked<%}%>>
                                        True
                                    </label>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="paymentCapturedFalse" value="false" <%if (!string.IsNullOrWhiteSpace(paymentCapturedFalse))
                                                                                                           {%>checked<%}%>>
                                        False
                                    </label>
                                </div>
                            </div>
                            <label class="control-label col-md-2">Has Order Shipped</label>
                            <div class="col-md-4">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="orderShippedTrue" value="true" <%if (!string.IsNullOrWhiteSpace(orderShippedTrue))
                                                                                                      {%>checked<%}%>>
                                        True
                                    </label>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="orderShippedFalse" value="false" <%if (!string.IsNullOrWhiteSpace(orderShippedFalse))
                                                                                                        {%>checked<%}%>>
                                        False
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2"># Orders To View</label>
                            <div class="col-md-10">
                                <select name="numberToQuery" class="form-control">
                                    <option <%if (numberToQuery == 10)
                                              {%>selected<%}%>>10</option>
                                    <option <%if (numberToQuery == 25)
                                              {%>selected<%}%>>25</option>
                                    <option <%if (numberToQuery == 50)
                                              {%>selected<%}%>>50</option>
                                    <option <%if (numberToQuery == 100)
                                              {%>selected<%}%>>100</option>
                                    <option <%if (numberToQuery == 500)
                                              {%>selected<%}%>>500</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <button type="submit" class="btn btn-primary btn-lg pull-right">Search</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <%if (PurcOrderList != null && PurcOrderList.Count > 0)
      {%>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <table class="table table-striped">
                    <thead>
                        <tr>
                             <%if(AAH.SiteContext.CanAdminUserAccess(PurchaseOrderRoles.EDIT))
                          {%>
                            <th></th>
                            <%}%>
                            <th>Order Time</th>
                            <th>Customer Details</th>
                            <th>Shipping Address</th>
                            <th>Order Cost</th>
                            <th>Paypal Complete</th>
                            <th>Order Shipped</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%foreach (var PurcOrder in PurcOrderList)
                          {%>
                        <tr>
                             <%if(AAH.SiteContext.CanAdminUserAccess(PurchaseOrderRoles.EDIT))
                          {%>
                            <td>
                                <a href="<%=Url.Content("~/Admin/PurchaseOrders/Edit?id=" + PurcOrder.Id) %>" class="btn btn-primary btn-sm">Edit Order</a>
                            </td>
                            <%}%>
                            <td><%=PurcOrder.PayPalOrderDetails.OrderPlacedDateUtc.ToString("g") %> UTC</td>
                            <td>
                                <%=PurcOrder.PayPalOrderDetails.CustomerInfo.Email %>
                                <br />
                                <%=PurcOrder.PayPalOrderDetails.CustomerInfo.FirstName + " " + PurcOrder.PayPalOrderDetails.CustomerInfo.MiddleName + " " + PurcOrder.PayPalOrderDetails.CustomerInfo.LastName + " " + PurcOrder.PayPalOrderDetails.CustomerInfo.Suffix %>
                                <br />
                                <%=PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToPhoneNum%>
                            </td>
                            <td>
                                <%=PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToName %>
                                <br />
                                <%=PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet %>
                                <br />
                                <%if (!string.IsNullOrWhiteSpace(PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet_Two))
                                  {%>
                                <%=PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToStreet_Two%>
                                <br />
                                <%}%>
                                <%=PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToCity + ", " + PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToState + " " + PurcOrder.PayPalOrderDetails.CustomerInfo.ShipToZip %>
                            </td>
                            <td>
                                <%=(PurcOrder.PayPalOrderDetails.BaseAmount + PurcOrder.PayPalOrderDetails.TaxAmount + PurcOrder.PayPalOrderDetails.ShippingAmount).ToString("C") %>
                            </td>
                            <td>
                                <%if (!PurcOrder.PayPalOrderDetails.PaymentCapturedDateUtc.Equals(DateTime.MinValue))
                                  {%>
                                <div class="alert alert-success">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <%= PurcOrder.PayPalOrderDetails.PaymentCapturedDateUtc.ToString("g")%> UTC
                                </div>

                                <%}
                                  else
                                  {%>
                                <div class="alert alert-danger">
                                    <span class="glyphicon glyphicon-remove"></span>
                                    Payment Required!
                                </div>
                                <%}%>
                            </td>
                            <td>
                                <%if (!PurcOrder.PayPalOrderDetails.OrderShippedDateUtc.Equals(DateTime.MinValue))
                                  {%>
                                <div class="alert alert-success">
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <%= PurcOrder.PayPalOrderDetails.OrderShippedDateUtc.ToString("g")%> UTC
                                    <br />
                                    Tracking #: <%=PurcOrder.PayPalOrderDetails.ShippingTrackingNumber %>
                                </div>
                                <%}
                                  else
                                  {%>
                                <div class="alert alert-danger">
                                    <span class="glyphicon glyphicon-remove"></span>
                                    Ship This Order!
                                </div>
                                <%}%>
                            </td>
                        </tr>
                        <%}%>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <%}%>
    <script type="text/javascript">
        $(function ()
        {
            $(".date-picker").datepicker();
        });
    </script>
</asp:Content>
