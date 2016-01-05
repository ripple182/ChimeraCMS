<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<CEP.StaticProperty> AllStaticProperties = ViewBag.AllStaticProperties; %>
    <%List<Chimera.Entities.Uploads.Image> ImageList = ViewBag.ImageList; %>
    <%Product Product = ViewBag.Product;%>
    <div class="hero">
        <h3>
            <span>
                <%if (!string.IsNullOrWhiteSpace(Product.Id))
                  {%>
                    Edit Product <span class="color"><%=Product.Name %></span>
                <%}
                  else
                  {%>
                    Add New Product
                <%}%>
            </span>
        </h3>
    </div>
    <div class="form form-horizontal chimera-admin-edit-product-form">
        <div class="row">
            <div class="col-md-9">
                <div class="well">
                    <h5>Description Details</h5>
                    <div class="form-group">
                        <label class="control-label col-md-2">Name</label>
                        <div class="col-md-10">
                            <input type="text" class="form-control" data-bind="value: product().Product.Name" placeholder="Enter the product name">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Description</label>
                        <div class="col-md-10">
                            <textarea class="form-control" data-bind="value: product().Product.Description" placeholder="Enter a description the customers will view and can search."></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Product Status</label>
                        <div class="col-md-10">
                            <button type="button" class="btn" data-bind="css: { 'btn-success': product().Product.Active() }, click: function (data, event) { product().Product.Active(true) }">Active</button>
                            <button type="button" class="btn" data-bind="css: { 'btn-danger': !product().Product.Active() }, click: function (data, event) { product().Product.Active(false) }">Inactive</button>
                            <br />
                            <span class="help-block">Customers are onle able to search for and add "Active" products to their checkout shopping cart.</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="well">
                    <div class="row center">
                        <div class="col-md-12">
                            <h5>Main Product Image</h5>
                        </div>
                    </div>
                    <div class="row center">
                        <div class="col-md-12">
                            <img class="img-thumbnail" data-bind="click: function (data, event) { openProductImageDialog(-1) }, attr: { src: getProductImageSource(product().Product.MainImage) }">
                        </div>
                    </div>
                    <div class="row">
                        <br />
                    </div>
                    <div class="row center">
                        <div class="col-md-12">
                            <h5>Additional Thumbnails</h5>
                        </div>
                    </div>
                    <div class="row center">
                        <div class="col-md-12" data-bind="foreach: product().Product.AdditionalImages">
                            <img class="img-thumbnail chimera-thumbnail-smaller" data-bind="click: function (data, event) { openProductImageDialog($index() + 1) }, attr: { src: getProductImageSource($data) }">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <h5>Purchase Settings</h5>
                    All checkout properties will automatically inherit these purchase settings unless manually added.
                        <!-- ko template: { data: product().Product.PurchaseSettings , name: 'product-edit-purchase-settings-template' } -->
                    <!-- /ko -->
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/EditProductPropertyTable.ascx"), new EditProductPropertyTable("Search Properties", "This is a description", "product().Product.SearchPropertyList", "SEARCH_PROPERTY_KEY")); %>
            </div>
            <div class="col-md-6">
                <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/EditProductPropertyTable.ascx"), new EditProductPropertyTable("Checkout Properties", "This is a description", "product().Product.CheckoutPropertyList", "CHECKOUT_PROPERTY_KEY")); %>
            </div>
        </div>
        <div class="row" data-bind="visible: viewModel.product().Product.CheckoutPropertyList().length > 0">
            <div class="col-md-12">
                <div class="well">
                    <div class="form-group">
                        <div class="col-md-10">
                            <h5>Custom Checkout Purchase Settings</h5>
                            Define custom checkout purchase settings here.  If you had the following checkout properties defined "COLOR", "SIZE", "WIDTH", you would be able to define custom stock levels, purchase price, and shipping for each possible combination of "COLOR", "SIZE", and "WIDTH".
                        </div>
                        <div class="col-md-2">
                            <button type="button" class="btn btn-success btn-sm pull-right" data-bind="click: function (data, event) { addNewCustomCheckoutPropertySetting() }">
                                <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;New
                            </button>
                        </div>
                    </div>
                    <!-- ko foreach: { data: viewModel.product().Product.CheckoutPropertySettingsList, as: 'checkoutPropertySetting' } -->
                    <div class="panel panel-default">
                        <div class="panel-heading"><b>Checkout Setting # <span data-bind="text: $index() + 1"></span></b><span class="glyphicon glyphicon-trash pull-right" data-bind="    click: function (data, event) { removeCustomCheckoutPropertySetting($index()) }" title="Remove"></span></div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-md-4" data-bind="foreach: { data: checkoutPropertySetting.CheckoutPropertySettingKeys, as: 'settingKey' }">
                                    <div class="form-group">
                                        <label class="control-label col-md-4" data-bind="text: settingKey.Key"></label>
                                        <div class="col-md-8">
                                            <select class="form-control" data-bind="optionsCaption: 'Choose...', options: getCustomCheckoutPropDropdown(settingKey.Key()), value: settingKey.Value, optionsValue: 'Value'"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label col-md-7">Custom Price?</label>
                                        <div class="col-md-5">
                                            <button type="button" class="btn" data-bind="css: { 'btn-success': checkoutPropertySetting.DefineCustomPrice() }, click: function (data, event) { checkoutPropertySetting.DefineCustomPrice(true) }">Yes</button>
                                            <button type="button" class="btn" data-bind="css: { 'btn-danger': !checkoutPropertySetting.DefineCustomPrice() }, click: function (data, event) { checkoutPropertySetting.DefineCustomPrice(false) }">No</button>
                                        </div>
                                    </div>
                                    <div class="form-group" data-bind="visible: checkoutPropertySetting.DefineCustomPrice() == true">
                                        <label class="control-label col-md-4">Price</label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">$</span>
                                                <input type="text" class="form-control" data-bind="value: checkoutPropertySetting.PurchaseSettings.PurchasePrice">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-4">In Stock</label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">#</span>
                                                <input type="number" class="form-control" data-bind="value: checkoutPropertySetting.PurchaseSettings.StockLevel">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label col-md-7">Custom Shipping?</label>
                                        <div class="col-md-5">
                                            <button type="button" class="btn" data-bind="css: { 'btn-success': checkoutPropertySetting.DefineCustomShipping() }, click: function (data, event) { checkoutPropertySetting.DefineCustomShipping(true) }">Yes</button>
                                            <button type="button" class="btn" data-bind="css: { 'btn-danger': !checkoutPropertySetting.DefineCustomShipping() }, click: function (data, event) { checkoutPropertySetting.DefineCustomShipping(false) }">No</button>
                                        </div>
                                    </div>
                                    <!-- ko foreach: { data: checkoutPropertySetting.PurchaseSettings.ShippingMethodList, as: 'shippingMethod' } -->
                                    <div class="form-group" data-bind="visible: checkoutPropertySetting.DefineCustomShipping() == true">
                                        <label class="control-label col-md-2" data-bind="text: shippingMethod.Name"></label>
                                        <div class="col-md-10">
                                            <div class="input-group">
                                                <span class="input-group-addon">$</span>
                                                <input type="text" class="form-control" data-bind="value: shippingMethod.Price">
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /ko -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /ko -->
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                    <button class="btn btn-primary btn-lg pull-right" type="button" id="viewModelSubmitButton">Save Product</button>
            </div>
        </div>
    </div>
    <form id="viewModelSubmitForm" class="form-horizontal" method="post" action="<%=Url.Content("~/Admin/Product/Edit_Post") %>">
        <input id="viewModelHiddenInput" name="productData" type="hidden" />
    </form>
    <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/EditProductPropertyDialog.ascx")); %>
    <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/EditProductImageDialog.ascx")); %>
    <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/EditProductPurchaseSettings.ascx")); %>
    <script type="text/javascript">

        var productData = '{"Product":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(Product))%>}';
        var imageData = '{"ImageList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(ImageList))%>}';

        var BASE_AJAX_PATH = '<%=ConfigurationManager.AppSettings["BaseWebsiteURL"]%>';
        var defaultImageSource = '<%=ConfigurationManager.AppSettings["[CHIMERA_VALUE_DEFAULT_IMAGE]"]%>';

        var SEARCH_PROPERTY_KEY = '<%=CEP.StaticProperty.SEARCH_PROPERTY_KEY%>';
        var CHECKOUT_PROPERTY_KEY = '<%=CEP.StaticProperty.CHECKOUT_PROPERTY_KEY%>';
        var SHIPPING_METHOD_PROPERTY_KEY = '<%=CEP.StaticProperty.SHIPPING_METHOD_PROPERTY_KEY%>';

        viewModel = {
            product: ko.observable(ko.mapping.fromJSON(productData)),
            searchPropertyArray: ko.observableArray(<%=JsonConvert.SerializeObject(AllStaticProperties.Where(e => e.KeyName.Equals(CEP.StaticProperty.SEARCH_PROPERTY_KEY)).FirstOrDefault().PropertyNameValues)%>),
            checkoutPropertyArray: ko.observableArray(<%=JsonConvert.SerializeObject(AllStaticProperties.Where(e => e.KeyName.Equals(CEP.StaticProperty.CHECKOUT_PROPERTY_KEY)).FirstOrDefault().PropertyNameValues)%>),
            shippingMethodPropertyArray: ko.observableArray(<%=JsonConvert.SerializeObject(AllStaticProperties.Where(e => e.KeyName.Equals(CEP.StaticProperty.SHIPPING_METHOD_PROPERTY_KEY)).FirstOrDefault().PropertyNameValues)%>),
            imageList: ko.observable(ko.mapping.fromJSON(imageData))
        };

        LoadChimeraAdminEditProduct();
    </script>
</asp:Content>
