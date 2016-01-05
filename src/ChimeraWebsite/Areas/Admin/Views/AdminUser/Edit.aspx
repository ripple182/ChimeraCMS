<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%AdminUser AdminUser = ViewBag.AdminUser;%>
    <%List<AdminUserRole> AdminUserRoleList = ViewBag.AdminUserRoleList ?? new List<AdminUserRole>(); %>
    <div class="hero">
        <h3>
            <span>
                <%if (AdminUser != null)
                  {%>
                    Edit Admin User <span class="color"><%=AdminUser.Username %></span>
                <%}
                  else
                  {%>
                    Add New Admin User
                <%}%>
            </span>
        </h3>
        <p>
            <%if (AdminUser != null)
              {%>
                    Edit the admin user's roles, password, or whether they are "active" or not.  Admin users who are not active will not be able to login.
                <%}
              else
              {%>
                    Create the admin user's roles, password, or whether they are "active" or not.  Admin users who are not active will not be able to login.
            <%}%>
        </p>
    </div>
    <div class="form">
                        <form class="form-horizontal" method="post" action="<%=Url.Content("~/Admin/AdminUser/Edit_Post") %>">
                            <input type="hidden" name="id" value="<%=AdminUser != null ? AdminUser.Id.ToString() : string.Empty %>" />
                            <div class="form-group">
                                <label class="control-label col-md-2" for="username">Username</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control" id="username" name="username" <%if(AdminUser != null){%> readonly="readonly" disabled="disabled" value="<%=AdminUser.Username %>" <%}%>>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-2" for="email">Email</label>
                                <div class="col-md-6">
                                    <input type="text" class="form-control" id="email" name="email"  <%if(AdminUser != null){%>value="<%=AdminUser.Email %>"<%}%>>
                                    <span class="help-block">Email is used to notify you whenever a new order is placed.</span>
                                </div>
                            </div>
                            <%--dont allow people to change password in edit mode--%>
                            <%if (AdminUser == null)
                             {%>
                                <div class="form-group">
                                    <label class="control-label col-md-2" for="password">Password</label>
                                    <div class="col-md-6">
                                        <input type="password" class="form-control" id="password" name="password">
                                        <span class="help-block">Password is required to have at least 8 characters, 1 number, and at least 1 lowercase and 1 uppercase letter.</span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-2" for="passwordrepeat">Password Repeat</label>
                                    <div class="col-md-6">
                                        <input type="password" class="form-control" id="passwordrepeat" name="passwordrepeat">
                                    </div>
                                </div>
                            <%}%>
                            <div class="form-group">
                                <label class="control-label col-md-2">Active</label>
                                <div class="col-md-6">
                                    <div class="radio">
                                      <label>
                                        <input type="radio" name="active" value="yes" <%if(AdminUser == null || AdminUser.Active){%>checked<%}%>>
                                        Yes
                                      </label>
                                    </div>
                                    <div class="radio">
                                      <label>
                                        <input type="radio" name="active" value="no" <%if(AdminUser != null && !AdminUser.Active){%>checked<%}%>>
                                        No
                                      </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-2">Roles</label>
                                <div class="col-md-10">
                                    <%if(AdminUserRoleList != null && AdminUserRoleList.Count > 0)
                                    {%>
                                        <%foreach(var AdminUserRole in AdminUserRoleList)
                                        {%>
                                            <div class="checkbox">
                                              <label>
                                                <input type="checkbox" name="<%=AdminUserRole.Id.ToString()%>" <%if(AdminUser != null && AdminUser.RoleList.Contains(AdminUserRole.Name)){%> checked="checked" <%}%> <%if(AdminUserRole.Name.Equals("admin-all")){%> disabled="disabled" aria-readonly="true" <%}%> />
                                                &nbsp;&nbsp;<b><%=AdminUserRole.Name %></b>&nbsp;-&nbsp;<%=AdminUserRole.Description %>
                                              </label>
                                            </div>
                                        <%}%>
                                    <%}%>
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <div class="col-md-8 col-md-offset-2">
                                    <button type="submit" class="btn btn-success">Submit</button>
                                    <a href="<%=Url.Content("~/Admin/AdminUser/ViewAll") %>" class="btn btn-default">Cancel</a>
                                </div>
                            </div>
                        </form>
                    </div>
</asp:Content>
