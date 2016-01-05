<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
<script src="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "JS/all.js"%>"></script>
<%}
  else
  {%>
    <%--Required Scripts for all templates--%>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/Chimera/OnAddOrOnLoad.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/Chimera/ProductViewLoad.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/Chimera/ShoppingCartViewLoad.js")%>"></script>

    <script src="<%=Url.Content("~/Templates/Erika/Scripts/bootstrap.min.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/jquery.isotope.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/jquery.prettyPhoto.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/filter.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/jquery.flexslider-min.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/jquery.cslider.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/modernizr.custom.28468.js")%>"></script>
<%}%>
    
        