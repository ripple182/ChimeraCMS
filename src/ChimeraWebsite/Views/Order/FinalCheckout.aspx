<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%string ViewType = ViewBag.ViewType; %>
    <%string ConfirmationNumber = ViewBag.ConfirmationNumber; %>
    <%Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/OrderViews/FinalCheckout/{1}.ascx", ChimeraTemplate.TemplateName, ViewType)), ConfirmationNumber);%>
</asp:Content>
