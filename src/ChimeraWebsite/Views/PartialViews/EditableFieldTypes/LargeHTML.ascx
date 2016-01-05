<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%KnockoutEditableFieldModel MyModel = Model ?? new KnockoutEditableFieldModel(); %>
<textarea class="chimera-editable-largehtml" data-bind="attr: { 'child-value-key': '<%=MyModel.KeyForValue %>'}, value: <%=MyModel.KeyForValue %>.Value"></textarea>