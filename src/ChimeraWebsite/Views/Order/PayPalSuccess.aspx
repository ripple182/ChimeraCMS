<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%PayPalSuccessModel PayPalSuccess = ViewBag.PayPalSuccessModel; %>
    <%Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/OrderViews/SuccessfulOrder/{1}.ascx", ChimeraTemplate.TemplateName, PayPalSuccess.ViewType)), PayPalSuccess.PayPalPurchaseOrder);%>
</asp:Content>
