<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
<script src="<%=AppSettings.PRODUCTION_ADMIN_CDN_URL + "JS/all.js"%>"></script>
<%}
  else
  {%>
    <%--Common JS Files--%>
    <script src="<%=Url.Content("~/Scripts/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/jquery-ui.min.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/chimera-common-utility.js")%>"></script>

    <%--Erika Template JS Files--%>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/bootstrap.min.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/jquery.isotope.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/filter.js")%>"></script>
    <script src="<%=Url.Content("~/Templates/Erika/Scripts/modernizr.custom.28468.js")%>"></script>

    <%--Admin specific JS Files--%>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/timeago.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/highcharts.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/exporting.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/ChimeraAdmin/dashboard.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/ChimeraAdmin/edit-product.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/ChimeraAdmin/edit-settingGroupSchema.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/ChimeraAdmin/edit-settingGroupValues.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/ChimeraAdmin/edit-staticProperties.js")%>"></script>
    <script src="<%=Url.Content("~/Areas/Admin/Scripts/ChimeraAdmin/edit-navigationMenu.js")%>"></script>
<%}%>
<%Html.RenderPartial(Url.Content("~/Views/PartialViews/Resources/Editor/JS_ADMIN_BUNDLE.ascx"));%>
        