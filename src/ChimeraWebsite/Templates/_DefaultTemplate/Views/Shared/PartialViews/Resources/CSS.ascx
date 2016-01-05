<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%SettingGroup TCS = Model; %>
<%if (AppSettings.InProductionMode)
  {%>

<link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/all.css"%>" rel="stylesheet">

<%}
  else
  {%>

<%}%>