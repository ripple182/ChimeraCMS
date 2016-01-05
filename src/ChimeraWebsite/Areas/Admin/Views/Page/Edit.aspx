<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%Chimera.Entities.Page.Page Page = ViewBag.Page; %>
    <div class="hero">
        <h3>
            <span>
                <%if (!Page.PageId.Equals(Guid.Empty))
                  {%>
                    Edit Page
                <%}
                  else
                  {%>
                    Add New Page Type
                <%}%>
            </span>
        </h3>
        <p>
            <%if (!Page.PageId.Equals(Guid.Empty))
              {%>
                    Edit some basic attributes of the page.
                <%}
              else
              {%>
                    Add a new page type.
            <%}%>
        </p>
    </div>
    <div class="form">
        <form class="form-horizontal" method="post" action="<%=Url.Content("~/Admin/Page/Edit_Post") %>">
            <input type="hidden" name="id" value="<%=!Page.PageId.Equals(Guid.Empty) ? Page.Id.ToString() : string.Empty %>" />
            <div class="form-group">
                <label class="control-label col-md-2" for="pageTitle">Page Title</label>
                <div class="col-md-6">
                    <input type="text" class="form-control" id="pageTitle" name="pageTitle" value="<%=Page.PageTitle %>">
                    <span class="help-block">The page title used to easily identify this page, and what the user will see in their browser tab.</span>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-2" for="pageFriendlyURL">Page Friendly URL</label>
                <div class="col-md-6">
                    <input type="text" class="form-control" id="pageFriendlyURL" name="pageFriendlyURL" value="<%=Page.PageFriendlyURL %>">
                    <span class="help-block">What the page will be identified as in the URL, this must be unique for all page types.</span>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-2">Publish Page?</label>
                <div class="col-md-10">
                    <select class="form-control" name="published">
                        <option value="false" <%if(!Page.Published){%> selected="selected" <%}%>>No</option>
                        <option value="true" <%if(Page.Published){%> selected="selected" <%}%>>Yes Publish</option>
                    </select>
                    <br />
                    <span class="help-block">Publishing this page will unpublish other pages of the same type so the user's will see this version.</span>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-8 col-md-offset-2">
                    <button type="submit" class="btn btn-success">Submit</button>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
