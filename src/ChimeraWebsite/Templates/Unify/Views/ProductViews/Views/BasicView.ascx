<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%Product Prod = Model; %>
<%Html.RenderPartial(Url.Content("~/Views/PartialViews/Products/CheckoutPropertySettingHiddenInputs.ascx"), Prod);%>
<br />
<div class="row">
    <div class="col-md-offset-2 col-md-10">
        <h2><%=Prod.Name %></h2>
    </div>
</div>
<div class="row prodViewMaster">
    <div class="col-md-2">
        <div class="thumbnail">
            <img class="preview-product-img" src="<%=!Prod.MainImage.ImagePath.Equals(string.Empty) ? Prod.MainImage.ImagePath : System.Configuration.ConfigurationManager.AppSettings["[CHIMERA_VALUE_DEFAULT_IMAGE]"] %>" title="<%=Prod.Name %>" alt="<%=Prod.Name %>" />
        </div>
        <%if (Prod.AdditionalImages != null && Prod.AdditionalImages.Count > 0)
          {%>
            <%foreach (var AdditImage in Prod.AdditionalImages)
              {%>
                <%if (!string.IsNullOrWhiteSpace(AdditImage.ImagePath))
                  {%>
                <div class="thumbnail">
                    <img class="preview-product-img" src="<%=AdditImage.ImagePath %>" title="<%=Prod.Name %>" alt="<%=Prod.Name %>" />
                </div>
                <%}%>
            <%}%>
        <%}%>
    </div>
    <div class="col-md-6">
        <img class="main-product-img img-thumbnail" src="<%=!Prod.MainImage.ImagePath.Equals(string.Empty) ? Prod.MainImage.ImagePath : System.Configuration.ConfigurationManager.AppSettings["[CHIMERA_VALUE_DEFAULT_IMAGE]"] %>" title="<%=Prod.Name %>" alt="<%=Prod.Name %>" />
    </div>
    <div class="col-md-4">
        <form class="form-horizontal" method="post" action="<%=Url.Content("~/ShoppingCart/AddItemToCart?id=" + Prod.Id) %>">
            <div class="well">
                <h1 id="checkout-custom-price" checkout-default-price="<%=Prod.PurchaseSettings.PurchasePrice.ToString("C")%>"></h1>
                <%if (Prod.CheckoutPropertyList != null && Prod.CheckoutPropertyList.Count > 0)
                  {%>
                    <%foreach (var CheckoutProd in Prod.CheckoutPropertyList)
                      {%>
                        <%if (CheckoutProd.Values != null && CheckoutProd.Values.Count > 0)
                          {%>
                        <div class="form-group">
                            <label class="control-label col-md-2"><%=CheckoutProd.Name %></label>
                            <div class="col-md-10">
                                <select checkout-property-name="<%=CheckoutProd.Name %>" class="checkout-prop-select form-control" name="checkProd_<%=CheckoutProd.Name %>">
                                    <%foreach (var Val in CheckoutProd.Values)
                                      {%>
                                    <option value="<%=Val.Value %>"><%=Val.Value %></option>
                                    <%}%>
                                </select>
                            </div>
                        </div>
                        <%}%>
                    <%}%>
                <%}%>
                <button class="btn btn-primary btn-lg"><span class="glyphicon glyphicon-shopping-cart"></span>&nbsp;&nbsp;Add To Cart</button>
            </div>
        </form>
    </div>
</div>
<div class="row prodViewMaster">
    <div class="col-md-offset-2 col-md-10">
        <p><%=Prod.Description %></p>
    </div>
</div>
