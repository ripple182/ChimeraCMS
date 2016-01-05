<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%string ViewType = ViewBag.ViewType; %>
<%Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/PageNotFoundViews/{1}.ascx", ChimeraTemplate.TemplateName, ViewType)));%>
</asp:Content>
