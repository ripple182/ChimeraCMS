<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%EditProductPropertyTable PropertyModel = Model; %>
<div class="well">
    <div class="form-group">
        <div class="col-md-10">
            <h5><%=PropertyModel.HeaderText %></h5>
            <%=PropertyModel.DescriptionText %>
        </div>
        <div class="col-md-2">
            <button type="button" class="btn btn-success btn-sm pull-right" data-bind="click: function (data, event) { $root.productEditNewPropertyDialog(<%=PropertyModel.JSGlobalVariableStaticPropertyKey%>) }">
                <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;New
            </button>
        </div>
    </div>
    <table class="table table-striped chimera-edit-product-property-table" data-bind="visible: <%=PropertyModel.KnockoutListName%>().length > 0">
        <thead>
            <tr>
                <th></th>
                <th>Name</th>
                <th>Values</th>
            </tr>
        </thead>
        <tbody data-bind="foreach: { data: <%=PropertyModel.KnockoutListName%>, as: 'mainProperty' }">
            <tr>
                <td>
                    <span class="glyphicon glyphicon-trash" data-bind="click: function (data, event) { removePropertyAndValues(<%=PropertyModel.JSGlobalVariableStaticPropertyKey%>, $index()) }" title="Remove Property & Values"></span>
                </td>
                <td data-bind="text: mainProperty.Name"></td>
                <td>
                    <!-- ko foreach: { data: mainProperty.Values, as: 'propValue' } -->
                    <button type="button" class="btn btn-info btn-xs" data-bind="visible: !$root.isPropertySelected(propValue), text: propValue.Value, click: function(data, event) { selectPropertyValueToEdit(event, propValue) }">
                    </button>
                    <button type="button" class="btn btn-info btn-xs" data-bind="visible: $root.isPropertySelected(propValue)">
                        <input type="text" data-bind="value: propValue.Value, event: { blur: function(data, event) { finishedEditingPropertyValue(<%=PropertyModel.JSGlobalVariableStaticPropertyKey%>, $index()) } }" />
                    </button>
                    <!-- /ko -->
                    <button type="button" class="btn btn-success btn-xs" data-bind="click: function (data, event) { addNewPropertyValue(<%=PropertyModel.JSGlobalVariableStaticPropertyKey%>, $index()) }">
                        <span class="glyphicon glyphicon-plus"></span>
                    </button>
                </td>

            </tr>
        </tbody>
    </table>
</div>
