<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Unauthorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="logreg">
        <div class="row">
            <div class="col-md-12">
                <div class="logreg-page">
                    <h3>Sign In to <span class="color">Admin Panel</span></h3>
                    <hr />
                    <div class="form">
                        <form action="<%=Url.Content("~/Admin/Home/Attempt_Login") %>" method="post" class="form-horizontal">
                            <%--hidden input field to block bots--%>
                            <input type="text" name="email" class="email_field" tabindex="-1" style="display: none;" />
                            <div class="form-group">
                                <label class="control-label col-md-3" for="username">Username</label>
                                <div class="col-md-9">
                                    <input type="text" class="form-control" id="username" name="username">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3" for="email">Password</label>
                                <div class="col-md-9">
                                    <input type="password" class="form-control" id="password" name="password">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-9 col-md-offset-3">
                                    <button type="submit" class="btn btn-primary btn-md">Login</button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
