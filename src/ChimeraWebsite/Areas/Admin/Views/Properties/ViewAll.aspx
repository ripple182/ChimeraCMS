<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<CEP.StaticProperty> StaticPropertyList = ViewBag.StaticPropertyList; %>
    <div class="hero">
        <h3><span>View All Properties</span></h3>
    </div>
    <%if (StaticPropertyList != null && StaticPropertyList.Count > 0)
      {%>
    <table class="table table-striped table-hover">
        <thead>
            <tr>
                <%if(AAH.SiteContext.CanAdminUserAccess(PropertyRoles.EDIT))
                  {%>
                <th></th>
                <%}%>
                <th>Name</th>
                <th>Values</th>
            </tr>
        </thead>
        <tbody>
            <%foreach (var Property in StaticPropertyList)
              {%>
                <tr>
                    <%if(AAH.SiteContext.CanAdminUserAccess(PropertyRoles.EDIT))
                    {%>
                    <td>
                        <a class="btn btn-primary btn-sm" href="<%=Url.Content("~/Admin/Properties/Edit?id=" + Property.Id.ToString()) %>">Edit</a>
                    </td>
                    <%}%>
                    <td>
                        <%=Property.KeyName %>
                    </td>
                    <td>
                        <%if(Property.PropertyNameValues != null && Property.PropertyNameValues.Count > 0)
                          {%>
                            <%foreach(var PropVal in Property.PropertyNameValues)
                              {%>
                                <button role="button" class="btn btn-info btn-xs"><%=PropVal %></button>
                            <%}%>
                        <%}%>
                    </td>
                </tr>
            <%}%>
        </tbody>
    </table>
    <%}
      else
      {%>
        <p>no properties exist.</p>
    <%}%>
</asp:Content>
