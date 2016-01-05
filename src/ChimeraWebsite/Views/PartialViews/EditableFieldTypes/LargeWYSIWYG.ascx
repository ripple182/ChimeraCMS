<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%KnockoutEditableFieldModel MyModel = Model ?? new KnockoutEditableFieldModel(); %>
<span class="summernote-wysiwyg" data-bind="attr: { 'child-value-key': '<%=MyModel.KeyForValue %>'}"></span>