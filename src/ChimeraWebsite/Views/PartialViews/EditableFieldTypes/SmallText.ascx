<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%KnockoutEditableFieldModel MyModel = Model ?? new KnockoutEditableFieldModel(); %>
<input class="chimera-editable-smalltext" data-bind="value: <%=MyModel.KeyForValue %>.Value" />    