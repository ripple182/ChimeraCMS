<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
    <link href="<%=AppSettings.PRODUCTION_EDITOR_CDN_URL + "CSS/admin-bundle.css"%>" rel="stylesheet">
<%}
  else
  {%>
    <link href="<%=Url.Content("~/Styles/jquery.fileupload-ui-noscript.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/jquery.fileupload-ui.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/chimera-image-upload.css") %>" rel="stylesheet">
<%}%>