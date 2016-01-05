<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%KnockoutEditableFieldModel MyModel = Model ?? new KnockoutEditableFieldModel(); %>
<input class="chimera-editable-extrasmalltext" data-bind="value: <%=MyModel.KeyForValue %>.Value" />    