<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
<script src="<%=AppSettings.PRODUCTION_EDITOR_CDN_URL + "JS/admin-bundle.js"%>"></script>
<%}
  else
  {%>
    <script src="<%=Url.Content("~/Scripts/jquery.fileupload.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/jquery.iframe-transport.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/knockout-3.0.0.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/knockout-mapping.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/knockout-sortable.min.js")%>"></script>
<%}%>

        