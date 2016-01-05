<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>
<script src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "JS/all.js"%>"></script>
<%}
  else
  {%>
<script src="<%=Url.Content("~/Scripts/jquery-1.10.2.min.js")%>"></script>
<script src="<%=Url.Content("~/Scripts/jquery-ui.min.js")%>"></script>
<script src="<%=Url.Content("~/Scripts/chimera-common-public-face.js")%>"></script>
<%}%>
    