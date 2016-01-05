<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%SettingGroup TCS = Model; %>
<link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700,400italic' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Oswald:400,700' rel='stylesheet' type='text/css'>

<%if (AppSettings.InProductionMode)
  {%>
<link href="<%=AppSettings.PRODUCTION_ADMIN_CDN_URL + "CSS/all.css"%>" rel="stylesheet">

<!--[if IE 7]>
        <link rel="stylesheet" href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/font-awesome-ie7.css"%>">
        <![endif]-->

<!-- HTML5 Support for IE -->
<!--[if lt IE 9]>
         <script src="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "JS/html5shim.js"%>"></script>
        <![endif]-->
<link href="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "CSS/blue.css" %>" rel="stylesheet">
<%}
  else
  {%>
<%--Styles used for chimera editor--%>
<link href="<%=Url.Content("~/Styles/jqueryui/jquery-ui.min.css") %>" rel="stylesheet">

<%--Admin console uses Erika template--%>
<link href="<%=Url.Content("~/Templates/Erika/Styles/bootstrap.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/font-awesome.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/style.css") %>" rel="stylesheet">
<link href="<%=Url.Content("~/Templates/Erika/Styles/blue.css") %>" rel="stylesheet">
<!--[if IE 7]>
            <link rel="stylesheet" href="<%=Url.Content("~/Templates/Erika/Styles/font-awesome-ie7.css")%>">
            <![endif]-->
<!--[if lt IE 9]>
             <script src="<%=Url.Content("~/Templates/Erika/Scripts/html5shim.js")%>"></script>
            <![endif]-->

<%--Our custom css for the admin console--%>
<link href="<%=Url.Content("~/Areas/Admin/Styles/ChimeraAdmin/customstyle.css") %>" rel="stylesheet">
<%}%>

<%--Shared resources between the admin console and editor--%>
<%Html.RenderPartial(Url.Content("~/Views/PartialViews/Resources/Editor/CSS_ADMIN_BUNDLE.ascx"));%>