<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%List<SettingGroup> SettingGroupList = Model; %>
<%bool EditSchema = AAH.SiteContext.CanAdminUserAccess(SettingRoles.EDIT_SCHEMA); %>
<%bool EditValues = AAH.SiteContext.CanAdminUserAccess(SettingRoles.EDIT_VALUES); %>
<%if (SettingGroupList != null && SettingGroupList.Count > 0)
  {%>
<table class="table table-striped table-hover">
    <thead>
        <tr>
            <%if (EditSchema)
              { %><th>Group Key</th>
            <%}%>
            <th><%= EditSchema ? "User Friendly Name" : "Settings Group Name"%></th>
            <th class="chimera-settings-table-description">Description</th>
            <%if (EditSchema || EditValues)
              { %><th></th>
            <%}%>
        </tr>
    </thead>
    <tbody>
        <%foreach (var SetGroup in SettingGroupList)
          {%>
        <tr>
            <%if (EditSchema)
              { %><td><%=SetGroup.GroupKey %></td>
            <%}%>
            <td><%=SetGroup.UserFriendlyName %></td>
            <td><%=SetGroup.Description %></td>
            <%if (EditSchema || EditValues)
              {%>
            <td>
                <%if (EditSchema)
                  {%>
                <a class="btn btn-warning btn-sm" href="<%=Url.Content("~/Admin/Settings/EditSchema?id=" + SetGroup.Id) %>">Edit Schema</a>
                <%}%>
                <%if (EditValues)
                  {%>
                <a class="btn btn-primary btn-sm" href="<%=Url.Content("~/Admin/Settings/EditValues?id=" + SetGroup.Id) %>">Edit Values</a>
                <%}%>
            </td>
            <%}%>
        </tr>
        <%}%>
    </tbody>
</table>
<%}
  else
  {%>
<div class="row">
    <div class="col-md-12">
        <p>No setting groups exist.</p>
    </div>
</div>
<%}%>