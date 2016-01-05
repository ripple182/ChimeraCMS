<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%SettingGroup TCS = Model; %>
<%if (AppSettings.InProductionMode)
  {%>

    <link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/all.css"%>" rel="stylesheet">

    <link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/" + TCS.GetSettingVal("ThemeColor") + ".css"%>" rel="stylesheet">
    <link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/" + TCS.GetSettingVal("ThemeColor") + ".css"%>" rel="stylesheet">
    <link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/" + TCS.GetSettingVal("ThemeColor") + ".css"%>" rel="stylesheet">

    <!--[if IE 7]><link rel="stylesheet" href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/font-awesome-ie7.css"%>"><![endif]-->
<%}
  else
  {%>
    <link href="<%=Url.Content("~/Templates/Unify/Styles/bootstrap.css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/style.css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/responsive.css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/font-awesome.css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/flexslider.css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/page_search.css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/Chimera-Override/style.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/Chimera-Custom/common.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/Chimera-Custom/shoppingCart.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/Chimera-Custom/products.css") %>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/Chimera-Custom/social-media.css") %>" rel="stylesheet">

    <link href="<%=Url.Content("~/Templates/Unify/Styles/" + TCS.GetSettingVal("ThemeColor") + ".css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/" + TCS.GetSettingVal("ThemeHeaderStyle") + ".css")%>" rel="stylesheet">
    <link href="<%=Url.Content("~/Templates/Unify/Styles/" + TCS.GetSettingVal("ThemeHeaderColor") + ".css")%>" rel="stylesheet">

    <!--[if IE 7]><link rel="stylesheet" href="<%=Url.Content("~/Templates/Unify/Styles/font-awesome-ie7.css")%>"><![endif]-->
<%}%>