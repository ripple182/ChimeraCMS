<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
<script src="<%=AppSettings.PRODUCTION_EDITOR_CDN_URL + "JS/all.js"%>"></script>
<%}
  else
  {%>
    <script src="<%=Url.Content("~/Scripts/codemirror.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/codemirror/mode/xml/xml.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/formatting.min.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/summernote.min.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/bootstrap-colorpicker.min.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/chimera-common-utility.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/chimera-toolbar.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/chimera-toolbar-module-settings.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/chimera-toolbar-edit-image.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/chimera-toolbar-edit-icon.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/chimera-toolbar-edit-button.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/cimera-toolbar-edit-productSearch.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/WYSIWYG/chimera-wysiwyg-utility-functions.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/WYSIWYG/chimera-wysiwyg-hyperlink.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/WYSIWYG/chimera-wysiwyg-icon.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/EditScreenDialogs/WYSIWYG/chimera-wysiwyg-button.js")%>"></script>
<%}%>
<%Html.RenderPartial(Url.Content("~/Views/PartialViews/Resources/Editor/JS_ADMIN_BUNDLE.ascx"));%>
        