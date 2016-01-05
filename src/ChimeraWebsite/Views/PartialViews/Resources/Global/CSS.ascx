<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
    <link href="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "CSS/all.css"%>" rel="stylesheet">
<%}
  else
  {%>
    <link href="<%=Url.Content("~/Styles/jqueryui/jquery-ui.min.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Styles/chimera-public-face.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Styles/social-media-FlatTheme.css") %>" rel="stylesheet">
<%}%>
    