<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<PageRevision> PageRevisionList = ViewBag.PageRevisionList; %>
    <div class="hero">
        <h3><span>Page Types</span></h3>
    </div>

    <%if (PageRevisionList != null && PageRevisionList.Count > 0)
      {%>
    <div class="row">
        <div class="col-md-12">
            <table class="table">
                <thead>
                    <tr>
                        <th>Page Title</th>
                        <th>Friendly URL</th>
                        <th class="center">Published</th>
                        <th class="center">Last Modified</th>
                        <%if(AAH.SiteContext.CanAdminUserAccess(PageRoles.EDIT))
                          {%>
                        <th></th>
                        <%}%>
                    </tr>
                </thead>
                <tbody>
                    <%foreach (var PageType in PageRevisionList.OrderByDescending(e => e.ModifiedDateUTC))
                      {%>
                        <tr>
                            <td><%=PageType.PageTitle %></td>
                            <td><%=PageType.PageFriendlyURL %></td>
                            <td class="center">
                                <%if(PageType.Published)
                                  {%>
                                    <span class="glyphicon glyphicon-star chimera-golden-star"></span>
                                <%}else{%>
                                    <span class="glyphicon glyphicon-star chimera-empty-star"></span>
                                <%}%>
                            </td>
                            <td class="center timeago" title="<%=PageType.ModifiedDateUTC.ToString("o") %>"></td>
                            <%if(AAH.SiteContext.CanAdminUserAccess(PageRoles.EDIT))
                          {%>
                            <td>
                                <a role="button" href="<%=Url.Content("~/Admin/Page/Edit?id=" + PageType.Id) %>" class="btn btn-primary btn-md">Edit Details</a>
                                <a target="_blank" role="button" href="<%=Url.Content("~/?id=" + PageType.Id) %>" class="btn btn-warning btn-md">Launch Editor</a>
                            </td>
                            <%}%>
                        </tr>
                    <%}%>
                </tbody>
            </table>
        </div>
    </div>
    <%}%>
</asp:Content>
