<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: productEditNewPropertyDialog, with: productEditNewPropertyDialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: function (data, event) { $root.productEditNewPropertyDialog(null); }">&times;</button>
                <h4 class="modal-title">Add New Property</h4>
            </div>
            <div class="modal-body">

                <div class="row">
                    <div class="col-md-12">
                        <h5>Choose From Existing Properties</h5>
                        <br />
                        <!-- ko foreach: $root.getExistingPropertyList -->
                        <button type="button" class="btn btn-info" data-bind="enable: doesProductAlreadyHaveProperty($data), text: $data, click: function (data, event) { addNewPropertyKeyToProduct($data) }"></button>
                        <!-- /ko -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 form-inline">
                        <br />
                        <br />
                        <h5>Enter a New Property</h5>
                        <br />
                        <div class="form-group">
                            <label>New Property&nbsp;&nbsp;</label>
                        </div>
                        <div class="form-group">
                            <input type="text" class="form-control" placeholder="enter property name" data-bind="value: $root.productEditNewPropertyName">
                        </div>
                        &nbsp;
                        <button type="button" class="btn btn-primary" data-bind="click: function (data, event) { submitNewPropertyToServer() }">Add New Property</button>
                        <br />
                        <div class="alert alert-danger" data-bind="visible: $root.productEditNewPropertyDialogErrorMsg() != null">
                            <button type="button" class="close" aria-hidden="true" data-bind="click: function (data, event) { $root.productEditNewPropertyDialogErrorMsg(null) }">&times;</button>
                            <strong data-bind="text: $root.productEditNewPropertyDialogErrorMsg"></strong>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-bind="click: function (data, event) { $root.productEditNewPropertyDialog(null); }">Close</button>
            </div>
        </div>
    </div>
</div>
