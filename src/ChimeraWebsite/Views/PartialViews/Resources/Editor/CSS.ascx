<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
    <link href="<%=AppSettings.PRODUCTION_EDITOR_CDN_URL + "CSS/all.css"%>" rel="stylesheet">
<%}
  else
  {%>
    <link href="<%=Url.Content("~/Styles/codemirror.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/codemirror/theme/blackboard.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/summernote.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/summernote-bs3.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/summernote-bs3-override.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/bootstrap-colorpicker.min.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/bootstrap-colorpicker-override.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/chimera.css") %>" rel="stylesheet">
<%}%>
<%Html.RenderPartial(Url.Content("~/Views/PartialViews/Resources/Editor/CSS_ADMIN_BUNDLE.ascx"));%>