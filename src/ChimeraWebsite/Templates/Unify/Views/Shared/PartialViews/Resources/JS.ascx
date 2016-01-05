<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>

    <script src="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "JS/all.js"%>" type="text/javascript"></script>

    <!--[if lt IE 9]><script src="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "JS/respond.js"%>"></script><![endif]-->
<%}
  else
  {%>

    <%--Required Scripts for all templates--%>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/Chimera/OnAddOrOnLoad.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/Chimera/ProductViewLoad.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/Chimera/ShoppingCartViewLoad.js")%>"></script>

    <script src="<%=Url.Content("~/Templates/Unify/Scripts/jquery-migrate-1.2.1.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/bootstrap.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/hover-dropdown.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/back-to-top.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/jquery.flexslider.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/app.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Templates/Unify/Scripts/index.js")%>" type="text/javascript"></script>

    <!--[if lt IE 9]><script src="<%=Url.Content("~/Templates/Unify/Scripts/respond.js")%>"></script><![endif]-->
<%}%>
    
        