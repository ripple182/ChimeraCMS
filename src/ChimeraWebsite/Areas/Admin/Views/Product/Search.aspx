<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<CEP.Property> ProductSearchProperties = ViewBag.ProductSearchProperties ?? new List<CEP.Property>(); %>
    <%List<Product> ProductList = ViewBag.ProductList ?? new List<Product>(); %>
    <%string SearchText = ViewBag.SearchText ?? string.Empty; %>
    <%string Active = ViewBag.Active; %>
    <%string Inactive = ViewBag.Inactive; %>
    <%Dictionary<string, List<string>> SelectedSearchFilters = ViewBag.SelectedSearchFilters ?? new Dictionary<string, List<string>>(); %>
    <div class="hero">
        <h3><span>Search Existing Products</span></h3>
    </div>
    <form class="form-horizontal" method="post" action="<%=Url.Content("~/Admin/Product/Search") %>">
        <div class="form">
            <div class="row">
                <div class="col-md-12">
                    <div class="well">
                        <h5>Search Filters</h5>
                        <div class="form-group">
                            <label class="control-label col-md-2">Name / Description</label>
                            <div class="col-md-10">
                                <input type="text" class="form-control" name="searchText" value="<%=SearchText %>">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Product Status</label>
                            <div class="col-md-10">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="active" value="active" <%if (!string.IsNullOrWhiteSpace(Active))
                                                                                              {%>checked<%}%>>
                                        Active
                                    </label>
                                </div>
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="inactive" value="inactive" <%if (!string.IsNullOrWhiteSpace(Inactive))
                                                                                                  {%>checked<%}%>>
                                        Inactive
                                    </label>
                                </div>
                            </div>
                        </div>
                        <%if (ProductSearchProperties != null && ProductSearchProperties.Count > 0)
                          {%>
                        <%for (int index = 0; index < ProductSearchProperties.Count; index++)
                          {
                              var SearchProp = ProductSearchProperties[index];

                              if (SearchProp.Values != null && SearchProp.Values.Count > 0)
                              {%>
                        <%if (index % 4 == 0 && index != 0)
                          {%>
                    </div>
                    <%}%>
                    <%if (index % 4 == 0)
                      {%>
                    <div class="form-group">
                        <%}%>
                        <label class="control-label col-md-1"><%=SearchProp.Name%></label>
                        <div class="col-md-2">
                            <%foreach (var Option in SearchProp.Values)
                              {%>
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" name="searchProp_<%=SearchProp.Name%>" value="<%=Option.Value %>" <%if (SelectedSearchFilters.ContainsKey(SearchProp.Name) && SelectedSearchFilters[SearchProp.Name].Contains(Option.Value))
                                                                                                                               { %>checked<%}%>>
                                    <%=Option.Value %>
                                </label>
                            </div>
                            <%}%>
                        </div>
                        <%if (ProductSearchProperties.Count - 1 == index)
                          {%>
                    </div>
                    <%}%>
                    <%}%>
                    <%}%>
                    <%}%>
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
    <%if (ProductList != null && ProductList.Count > 0)
      {%>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <%foreach (var Product in ProductList)
                  {%>
                <div class="col-md-6 chimera-search-product-result">
                    <div class="well">
                        <div class="row">
                            <div class="col-md-8">
                                <h5><%=Product.Name %></h5>
                            </div>
                            <div class="col-md-4">
                                <div class="pull-right">
                                    <%if (Product.Active)
                                      {%>
                                    <span class="glyphicon glyphicon-ok"></span>
                                    <div>Active</div>
                                    <%}
                                      else
                                      {%>
                                    <span class="glyphicon glyphicon-remove"></span>
                                    <div>Inactive</div>
                                    <%}%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <img class="img-thumbnail" src="<%=!string.IsNullOrWhiteSpace(Product.MainImage.ImagePath) ?  Product.MainImage.ImagePath : ConfigurationManager.AppSettings["[CHIMERA_VALUE_DEFAULT_IMAGE]"] %>" />
                                <%if(AAH.SiteContext.CanAdminUserAccess(ProductRoles.EDIT))
                                  {%>
                                <br />
                                <a role="button" href="<%=Url.Content("~/Admin/Product/Edit?id=" + Product.Id) %>" class="btn btn-success btn-md">Edit Product</a>
                                <%}%> 
                            </div>
                            <div class="col-md-8">
                                <%=Product.Description.Length >= 260 ? Product.Description.Substring(0, 259) + "..." : Product.Description %>
                            </div>
                        </div>
                        <%if ((Product.SearchPropertyList != null && Product.SearchPropertyList.Count > 0) || (Product.CheckoutPropertyList != null && Product.CheckoutPropertyList.Count > 0))
                          {%>
                        <div class="row">
                            <%if (Product.SearchPropertyList != null && Product.SearchPropertyList.Count > 0)
                              {%>
                            <div class="col-md-6">
                                <div class="search-property-header">Search Properties</div>
                                <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/SearchProductPropertyTable.ascx"), Product.SearchPropertyList); %>
                            </div>
                            <%}%>
                            <%if (Product.CheckoutPropertyList != null && Product.CheckoutPropertyList.Count > 0)
                              {%>
                            <div class="col-md-6">
                                <div class="search-property-header">Checkout Properties</div>
                                <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/SearchProductPropertyTable.ascx"), Product.CheckoutPropertyList); %>
                            </div>
                            <%}%>
                        </div>
                        <%}%>
                    </div>
                </div>
                <%}%>
            </div>
        </div>
    </div>
    <%}%>
</asp:Content>
