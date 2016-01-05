<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<PageType> PageTypeList = ViewBag.PageTypeList; %>
    <div class="hero">
        <h3><span>Page Types</span></h3>
    </div>

    <%if (PageTypeList != null && PageTypeList.Count > 0)
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
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <%foreach (var PageType in PageTypeList.OrderByDescending(e => e.ModifiedDateUTC))
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
                            <td>
                                <a role="button" href="<%=Url.Content("~/Admin/Page/ViewPageHistory?pageId=" + PageType.PageId) %>" class="btn btn-primary btn-md">View Revision History</a>
                                <%if(!PageType.LatestVersionPublished)
                                  {%>
                                    <br />
                                    <span class="text-danger">Latest version has not been published!</span>
                                <%}%>
                            </td>
                        </tr>
                    <%}%>
                </tbody>
            </table>
        </div>
    </div>
    <%}%>
</asp:Content>
