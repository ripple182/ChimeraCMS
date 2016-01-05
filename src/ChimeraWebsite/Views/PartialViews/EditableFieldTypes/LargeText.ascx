<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%KnockoutEditableFieldModel MyModel = Model ?? new KnockoutEditableFieldModel(); %>
<textarea class="chimera-editable-largetext" data-bind="value: <%=MyModel.KeyForValue %>.Value"></textarea>