<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<Notification> NotificationList = ViewBag.NotificationList; %>
    <%bool AllowReporting = ChimeraWebsite.Helpers.AppSettings.AllowPageReportRecording; %>
    <div class="hero">
        <h3><span>Dashboard</span></h3>
    </div>
    <div class="career">
        <div class="tabbable tabs-left">
            <ul class="nav nav-tabs">
                <%if (AllowReporting)
                { %>
                <li class="active"><a href="#tab1" data-toggle="tab">Analytics</a></li>
                <%}%>
                <li <%if (!AllowReporting){ %>class="active"<%}%>><a href="#tab2" data-toggle="tab">
                    <%if(NotificationList != null && NotificationList.Count > 0)
                      {%>
                    <span class="label label-success"><%=NotificationList.Count %></span>
                    <%}%>
                    Notifications

                    </a></li>
            </ul>
            <div class="tab-content">
                <%if (AllowReporting)
                { %>
                <div class="tab-pane active" id="tab1">
                    <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/Dashboard/Report.ascx"));%>
                </div>
                <%}%>
                <div class="tab-pane <%if (!AllowReporting){ %>active<%}%>" id="tab2">
                    <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/Dashboard/Notifications.ascx"));%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
