<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%if (AppSettings.InProductionMode)
  {%>

<script src="<%=AppSettings.PRODUCTION_TEMPLATE_CDN_URL + "JS/all.js"%>"></script>

<%}
  else
  {%>

<%}%>
    
        