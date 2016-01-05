<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%KnockoutEditableFieldModel MyModel = Model ?? new KnockoutEditableFieldModel(); %>
<div data-bind="visible: $root.currentlyEditingThisModule($parent) && <%=MyModel.KeyForValue %>.Active">
    <button type="button" class="btn btn-warning btn-lg" data-bind="click: function(data, event){ $root.setProductSearchFilterDialog($parent, '<%=MyModel.KeyForValue%>') } "><span class="glyphicon glyphicon-pencil"></span>&nbsp;Edit Search Filters</button>
</div>