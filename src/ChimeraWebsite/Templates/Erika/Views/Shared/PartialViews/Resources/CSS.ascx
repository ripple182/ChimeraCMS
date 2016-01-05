<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%SettingGroup TCS = Model; %>
<link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700,400italic' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Oswald:400,700' rel='stylesheet' type='text/css'>

<%if(AppSettings.InProductionMode)
      {%>
<link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/all.css"%>" rel="stylesheet">
<link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/" + TCS.GetSettingVal("ThemeColor") + ".css" %>" rel="stylesheet">

<!--[if IE 7]>
        <link rel="stylesheet" href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/font-awesome-ie7.css"%>">
        <![endif]-->

<!-- HTML5 Support for IE -->
<!--[if lt IE 9]>
         <script src="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "JS/html5shim.js"%>"></script>
        <![endif]-->
<%}
      else
      {%>
<link href="<%=Url.Content("~/Templates/Erika/Styles/bootstrap.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/font-awesome.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/slider.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/Chimera-Override/slider.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/prettyPhoto.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/flexslider.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/Chimera-Override/flexslider.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/style.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/Chimera-Override/style.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/Chimera-Custom/common.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/Chimera-Custom/shoppingCart.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/Chimera-Custom/products.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/Chimera-Custom/social-media.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/" + TCS.GetSettingVal("ThemeColor") + ".css") %>" rel="stylesheet">

<!--[if IE 7]>
        <link rel="stylesheet" href="<%=Url.Content("~/Templates/Erika/Styles/font-awesome-ie7.css")%>">
        <![endif]-->

<!-- HTML5 Support for IE -->
<!--[if lt IE 9]>
         <script src="<%=Url.Content("~/Templates/Erika/Scripts/html5shim.js")%>"></script>
        <![endif]-->
<%}%>