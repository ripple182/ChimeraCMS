<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%ProductModel ProdModel = ViewBag.ProductModel; %>
    <%Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/ProductViews/Views/{1}.ascx", ChimeraTemplate.TemplateName, ProdModel.ViewType)), ProdModel.Product);%>
</asp:Content>
