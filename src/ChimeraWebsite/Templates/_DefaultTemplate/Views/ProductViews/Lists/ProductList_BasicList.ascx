<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%ProductListModel ProductModel = Model; %>
<%if (ProductModel != null && ProductModel.ProductList != null && ProductModel.ProductList.Count > 0)
{%>
    <%for (int index = 0; index < ProductModel.ProductList.Count; index++)
    {
        var Prod = ProductModel.ProductList[index];

         if (index % 4 == 0 && index != 0)
         {%>
            </div>
        <%}
          if(index % 4 == 0)
          {%>
            <div class="row">
        <%}%>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-6 prodList_basicList">
            <div class="well">
                <h5><%=Prod.Name %></h5>
                <img class="img-thumbnail" src="<%=!Prod.MainImage.ImagePath.Equals(string.Empty) ? Prod.MainImage.ImagePath : System.Configuration.ConfigurationManager.AppSettings["[CHIMERA_VALUE_DEFAULT_IMAGE]"] %>" title="<%=Prod.Name %>" alt="<%=Prod.Name %>" />
                <div class="text-success"><%=Prod.PurchaseSettings.PurchasePrice.ToString("C") %></div>
                <a target="_blank" href="<%=Url.Content("~/ViewProduct/Details?id=" + Prod.Id) %>" role="button" class="btn btn-primary btn-md">View Details</a>
            </div>
        </div>
    <%}%>
<%}%>