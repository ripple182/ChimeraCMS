<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/html" id="product-edit-purchase-settings-template">
    <div class="form-horizontal">
        <div class="form-group">
            <label class="control-label col-md-2">Purchase Price</label>
            <div class="col-md-10">
                <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <input type="text" class="form-control" data-bind="value: $data.PurchasePrice" placeholder="enter a purchase price, decimals accepted">
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">In Stock</label>
            <div class="col-md-10">
                <div class="input-group">
                    <span class="input-group-addon">#</span>
                    <input type="number" class="form-control" data-bind="value: $data.StockLevel">
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">Shipping Method Cost</label>
            <div class="col-md-10">
                <div class="panel panel-default">
                    <div class="panel-body" data-bind="foreach: { data: $data.ShippingMethodList, as: 'shippingMethod' }">
                        <div class="form-group">
                            <label class="control-label col-md-1" data-bind="text: shippingMethod.Name"></label>
                            <div class="col-md-11">
                                <div class="input-group">
                                    <span class="input-group-addon">$</span>
                                    <input type="text" class="form-control" data-bind="value: shippingMethod.Price">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</script>
