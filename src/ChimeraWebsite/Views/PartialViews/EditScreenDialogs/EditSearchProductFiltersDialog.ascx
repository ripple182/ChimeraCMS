<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: productSearchFilterComponentEditing, with: productSearchFilterComponentEditing">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: $root.closeProductSearchFilterComponentEditing">&times;</button>
                <h4 class="modal-title">Edit Product List Search Filters</h4>
            </div>
            <div class="modal-body">
                <div class="row-fluid">
                    <div class="form-group">
                        <label>Filter Type:&nbsp;&nbsp;</label>
                        <button type="button" class="btn" data-bind="css: { 'btn-success': $root.productSearchFilterType() == 'Active' }, click: function (data, event) { $root.productSearchFilterType('Active'); $root.productSearchClearFilterType(); }">All Active</button>
                        <button type="button" class="btn" data-bind="css: { 'btn-success': $root.productSearchFilterType() == 'Specific' }, click: function (data, event) { $root.productSearchFilterType('Specific'); $root.productSearchClearFilterType(); }">Specific Products</button>
                        <button type="button" class="btn" data-bind="css: { 'btn-success': $root.productSearchFilterType() == 'Filtered' }, click: function (data, event) { $root.productSearchFilterType('Filtered'); $root.productSearchClearFilterType(); }">Search Properties</button>
                    </div>
                    <div data-bind="visible: $root.productSearchFilterType() == 'Specific'">
                        Select the checkboxes of the products you wish to include.
                        <div class="form-group" data-bind="foreach: { data: $root.previewProductList().PreviewProductList, as: 'product' }">
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" data-bind="attr: { 'value': product.Id }, checked: $root.productSearchSpecificProductArray">
                                    <span data-bind="text: product.Name"></span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div data-bind="visible: $root.productSearchFilterType() == 'Filtered'">
                        Select the checkboxes of the values in accordance with their search property of the products you wish to include.
                        <!-- ko foreach: { data: $root.productSearchPropertyList().ProductSearchPropertyList, as: 'searchProperty' } -->
                        <div class="form-group">
                            <label data-bind="text: searchProperty.Name"></label>
                            <!-- ko foreach: { data: searchProperty.Values, as: 'propertyValue' } -->
                            <div class="checkbox">
                                <label>
                                    <input type="checkbox" data-bind="attr: { 'value': searchProperty.Name() + ':' + propertyValue.Value() }, checked: $root.productSearchFilteredPropertyArray">
                                    <span data-bind="text: propertyValue.Value"></span>
                                </label>
                            </div>
                            <!-- /ko -->
                        </div>
                        <!-- /ko -->
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-bind="click: $root.saveProductSearchFilterComponentEditing">Save Changes</button>
                <button type="button" class="btn btn-default" data-bind="click: $root.closeProductSearchFilterComponentEditing">Close</button>
            </div>
        </div>
    </div>
</div>
