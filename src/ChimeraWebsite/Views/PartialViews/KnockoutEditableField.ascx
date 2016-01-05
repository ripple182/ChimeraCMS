<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%ChimeraWebsite.Models.Editor.KnockoutEditableFieldModel MyModel = Model ?? new ChimeraWebsite.Models.Editor.KnockoutEditableFieldModel(); %>
<%string EndTag = string.Empty; %>
<%---------------%>
<%--IMAGE FIELD--%>
<%---------------%>
<%if (MyModel.HtmlElement.ToUpper().Equals("IMG"))
{%>
    <img class="<%=MyModel.ClassAttribute %>" <%=MyModel.NonClassAttributes %> data-bind="visible: !$root.currentlyEditingThisModule($parent) && <%=MyModel.KeyForValue %>.Active, attr: { src: <%=MyModel.KeyForValue %>.Value }" />
<%}%>
<%--------------%>
<%--ICON FIELD--%>
<%--------------%>
<%else if (MyModel.HtmlElement.ToUpper().Equals("ICON"))
{%>
    <span class="<%=MyModel.ClassAttribute %>" <%=MyModel.NonClassAttributes %> data-bind="visible: !$root.currentlyEditingThisModule($parent) && <%=MyModel.KeyForValue %>.Active, css: getModuleChildIconClass(<%=MyModel.KeyForValue %>.Value()), style: { color: getModuleChildIconColor(<%=MyModel.KeyForValue %>.Value()) }"></span>
<%}%>
<%--------------%>
<%--BOOTSTRAPBUTTON FIELD--%>
<%--------------%>
<%else if (MyModel.HtmlElement.ToUpper().Equals(SpecialHTMLElement.BootstrapButton.ToUpper()))
{%>
    <a class="btn <%=MyModel.ClassAttribute %>" <%=MyModel.NonClassAttributes %> role="button" data-bind="visible: !$root.currentlyEditingThisModule($parent) && <%=MyModel.KeyForValue %>.Active, css: getModuleChildBootstrapButtonClasses(<%=MyModel.KeyForValue %>.Value()), attr: { target: getButtonComponentEditingValue(<%=MyModel.KeyForValue %>.Value(), 2), href: getButtonComponentEditingValue(<%=MyModel.KeyForValue %>.Value(), 3) }, text: getButtonComponentEditingValue(<%=MyModel.KeyForValue %>.Value(), 4)"></a>
<%}%>
<%--------------%>
<%--PRODUCTLIST DIV--%>
<%--------------%>
<%else if (MyModel.HtmlElement.ToUpper().Equals(SpecialHTMLElement.ProductList.ToUpper()))
{%>
    <div class="chimera-product-list <%=MyModel.ClassAttribute %>" <%=MyModel.NonClassAttributes %> data-bind="visible: !$root.currentlyEditingThisModule($parent) && <%=MyModel.KeyForValue %>.Active, attr: { 'product-list-url': <%=MyModel.KeyForValue %>.Value() }"></div>
<%}%>
<%------------------------%>
<%--OTHER REGULAR FIELDS--%>
<%------------------------%>
  <%else
  {%>
    <<%=MyModel.HtmlElement %> class="<%=MyModel.ClassAttribute %>" <%=MyModel.NonClassAttributes %> data-bind="visible: !$root.currentlyEditingThisModule($parent) && <%=MyModel.KeyForValue %>.Active, html: <%=MyModel.KeyForValue %>.Value"></<%=MyModel.HtmlElement%>>
    <<%=MyModel.HtmlElement %> data-bind="visible: $root.currentlyEditingThisModule($parent)  && <%=MyModel.KeyForValue %>.Active">
    <%EndTag = String.Format("</{0}>", MyModel.HtmlElement);%>
<%}%>
<%Html.RenderPartial(Url.Content(String.Format("~/Views/PartialViews/EditableFieldTypes/{0}.ascx", MyModel.EditType)), MyModel); %>
<%=EndTag %>