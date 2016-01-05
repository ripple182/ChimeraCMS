<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
    <%List<Notification> NotificationList = ViewBag.NotificationList; %>
<br /><br />
 <%if (NotificationList != null && NotificationList.Count > 0)
      {%>
    <div class="row">
        <div class="col-md-12">
            <div class="well">
                <h5>Dashboard Warnings</h5>
                <table class="table chimera-dashboard-table">
                    <%foreach (var Notif in NotificationList)
                      {%>
                    <tr>
                        <td>
                            <%if (Notif.ActionList != null && Notif.ActionList.Count > 0)
                              {%>
                                <div class="btn-group">
                                    <button type="button" class="btn btn-warning btn-md dropdown-toggle" data-toggle="dropdown">
                                        <span class="glyphicon glyphicon-cog"></span>&nbsp;Action&nbsp;<span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu" role="menu">
                                        <%foreach(var NotifAction in Notif.ActionList)
                                          {%>
                                            <%if (AAH.SiteContext.CanAdminUserAccess(ListExtensions.ToStringList<string>(NotifAction.ViewAdminUserRolesRequired, ";")))
                                              {%>
                                                <li>
                                                    <a href="<%=Url.Content(NotifAction.MvcUrl.GenerateFullUrl(Notif.EntityId)) %>">
                                                        <%if(!string.IsNullOrWhiteSpace(NotifAction.IconClass)){%><span class="<%=NotifAction.IconClass %>"></span>&nbsp;<%}%>
                                                        <%=NotifAction.Name %>
                                                    </a>
                                                </li>
                                            <%}%>
                                        <%}%>
                                    </ul>
                                </div>
                            <%}%>
                        </td>
                        <td>
                            <div class="<%=Notif.GetWarningLevelClass() %>">
                                <span class="<%=Notif.GetTypeIconClass() %>"></span>&nbsp;&nbsp;<a href="<%=Url.Content(Notif.MvcUrl.GenerateFullUrl(Notif.EntityId)) %>"><%=Notif.Description %></a>
                            </div>
                        </td>
                    </tr>
                    <%}%>
                </table>
            </div>
        </div>
    </div>
    <%}else{%>

 <div class="error">
            <div class="error-page">
                <p class="error-med">No notifications to monitor.</p>
            </div>
        </div>
<%}%>