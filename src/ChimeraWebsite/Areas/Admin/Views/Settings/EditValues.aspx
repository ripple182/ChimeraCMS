<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%SettingGroup SettingGroup = ViewBag.SettingGroup; %>
    <%List<CEP.StaticProperty> StaticPropertyList = ViewBag.StaticPropertyList; %>
    <%List<Chimera.Entities.Uploads.Image> ImageList = ViewBag.ImageList; %>
    <div class="hero">
        <h3>
            <span>Edit Setting "<%=SettingGroup.UserFriendlyName %>" Values
            </span>
        </h3>
    </div>
    <form class="form form-horizontal" method="post" action="<%=Url.Content("~/Admin/Settings/EditValues_Post") %>">
        <input type="hidden" value="<%=SettingGroup.Id %>" name="id" />
        <div class="chimera-admin-edit-product-form">
            <div class="row">
                <div class="col-md-12">
                    <%foreach(var Sett in SettingGroup.SettingsList)
                      {%>
                        <div class="form-group">
                            <label class="control-label col-md-2"><%=Sett.UserFriendlyName %></label>
                            <div class="col-md-10">
                                <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/DataEntryFormFields.ascx"), new DataEntryFormField(Sett, StaticPropertyList.Where(e => e.KeyName.Equals(Sett.DataEntryStaticPropertyKey)).FirstOrDefault())); %>
                                <span class="help-block"><%=Sett.Description %></span>
                            </div>
                        </div>
                    <%}%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <button class="btn btn-primary btn-lg pull-right" type="submit">Save Settings</button>
                </div>
            </div>
        </div>
    </form>
    <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/EditSettingGroupValueImageDialog.ascx")); %>
    <script type="text/javascript">

        var imageData = '{"ImageList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(ImageList))%>}';

        var BASE_AJAX_PATH = '<%=ConfigurationManager.AppSettings["BaseWebsiteURL"]%>';
        var defaultImageSource = '<%=ConfigurationManager.AppSettings["[CHIMERA_VALUE_DEFAULT_IMAGE]"]%>';

        viewModel = {
            imageList: ko.observable(ko.mapping.fromJSON(imageData))
        };

        LoadChimeraAdminEditSettingGroupValues();
    </script>
</asp:Content>
