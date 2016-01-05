<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%ShoppingCartModel ShopCartModel = ViewBag.ShoppingCartModel; %>
    <%Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/OrderViews/ShoppingCart/{1}.ascx", ChimeraTemplate.TemplateName, ShopCartModel.ViewType)), ShopCartModel);%>
</asp:Content>
