<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="hero">
        <h3><span>View All Admin Users</span></h3>
        <p>Click on the "Edit" button in order to change details about a particular user.</p>
    </div>
    <%List<AdminUser> AdminUserList = ViewBag.AdminUserList ?? new List<AdminUser>(); %>
    <%if (AdminUserList != null && AdminUserList.Count > 0)
      {%>
    <table class="table table-striped table-hover">
        <thead>
            <tr>
                <th></th>
                <th>Username</th>
                <th>Active</th>
                <th>Last Login</th>
                <th>Roles</th>
            </tr>
        </thead>
        <tbody>
            <%foreach (var AdminUser in AdminUserList)
              {%>
            <tr>
                <td><a href="<%=Url.Content("~/Admin/AdminUser/Edit?id=" + AdminUser.Id) %>" class="btn btn-default">Edit</a></td>
                <td><%=AdminUser.Username %></td>
                <td><%=AdminUser.Active %></td>
                <%if (!AdminUser.LastLoginDateUTC.Equals(DateTime.MinValue))
                  {%>
                <td class="timeago" title="<%=AdminUser.LastLoginDateUTC.ToString("o")%>"></td>
                <%}
                  else
                  {%>
                <td class="timeago" title="">has never logged in</td>
                <%}%>
                <td>
                    <%if (AdminUser.RoleList != null && AdminUser.RoleList.Count > 0)
                      {%>
                    <ul>
                        <%foreach (var Role in AdminUser.RoleList)
                          {%>
                        <li><%=Role %></li>
                        <%}%>
                    </ul>
                    <%}%>
                </td>
            </tr>
            <%}%>
        </tbody>
    </table>
    <%}%>
</asp:Content>
