<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%List<SettingGroup> SettingGroupList = ViewBag.SettingGroupList; %>
    <%bool EditSchema = AAH.SiteContext.CanAdminUserAccess(SettingRoles.EDIT_SCHEMA); %>
    <%bool EditValues = AAH.SiteContext.CanAdminUserAccess(SettingRoles.EDIT_VALUES); %>
    <%List<SettingGroup> SocialMediaAndSEOList = SettingGroupList.Where(e => e.ParentCategory == ParentCategoryType.SOCIAL_MEDIA_AND_SEO).ToList();%>
    <%List<SettingGroup> EcommerceList = SettingGroupList.Where(e => e.ParentCategory == ParentCategoryType.ECOMMERCE).ToList();%>
    <%List<SettingGroup> WebsiteContentAndEmailList = SettingGroupList.Where(e => e.ParentCategory == ParentCategoryType.WEBSITE_CONTENT_AND_EMAIL).ToList();%>
    <%List<SettingGroup> OtherList = SettingGroupList.Where(e => e.ParentCategory == ParentCategoryType.OTHER).ToList();%>
    <%bool WebsiteContentAndEmailListExists = WebsiteContentAndEmailList != null && WebsiteContentAndEmailList.Count > 0 ? true : false; %>
    <%bool SocialMediaAndSEOListExists = SocialMediaAndSEOList != null && SocialMediaAndSEOList.Count > 0 ? true : false; %>
    <%bool EcommerceListExists = EcommerceList != null && EcommerceList.Count > 0 ? true : false; %>
    <%bool OtherListExists = OtherList != null && OtherList.Count > 0 ? true : false; %>
    <div class="hero">
        <h3><span>View Setting Groups</span></h3>
    </div>
    <div class="career">
        <div class="tabbable tabs-left">
            <ul class="nav nav-tabs">
                <%if (WebsiteContentAndEmailListExists)
                  {%>
                    <li class="active"><a href="#tab1" data-toggle="tab">Website Content & Email Settings</a></li>
                <%}%>
                <%if (SocialMediaAndSEOListExists)
                  {%>
                    <li <%if(!WebsiteContentAndEmailListExists){%> class="active" <%}%>><a href="#tab2" data-toggle="tab">Social Media & SEO Settings</a></li>
                <%}%>
                <%if (EcommerceListExists)
                  {%>
                    <li> <%if(!WebsiteContentAndEmailListExists && !SocialMediaAndSEOListExists){%> class="active" <%}%><a href="#tab3" data-toggle="tab">Ecommerce Screen Flow & PayPal Settings</a></li>
                <%}%>
                <%if (OtherListExists)
                  {%>
                    <li <%if(!WebsiteContentAndEmailListExists && !SocialMediaAndSEOListExists && !EcommerceListExists){%> class="active" <%}%>><a href="#tab4" data-toggle="tab">Other Settings</a></li>
                <%}%>
            </ul>
            <div class="tab-content">
                <%if (WebsiteContentAndEmailListExists)
                  {%>
                    <div class="tab-pane active" id="tab1">
                        <h5>Website Content & Email Settings</h5>
                        <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/ViewSettingsTable.ascx"), WebsiteContentAndEmailList); %>
                    </div>
                <%}%>
                <%if (SocialMediaAndSEOListExists)
                  {%>
                    <div class="tab-pane <%if(!WebsiteContentAndEmailListExists){%> active <%}%>" id="tab2">
                        <h5>Social Media & SEO Settings</h5>
                        <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/ViewSettingsTable.ascx"), SocialMediaAndSEOList); %>
                    </div>
                <%}%>
                <%if (EcommerceListExists)
                  {%>
                    <div class="tab-pane <%if(!WebsiteContentAndEmailListExists && !SocialMediaAndSEOListExists){%> active <%}%>" id="tab3">
                        <h5>Ecommerce Screen Flow & PayPal Settings</h5>
                        <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/ViewSettingsTable.ascx"), EcommerceList); %>
                    </div>
                <%}%>
                <%if(OtherListExists)
                  {%>
                    <div class="tab-pane <%if(!WebsiteContentAndEmailListExists && !SocialMediaAndSEOListExists && !EcommerceListExists){%> active <%}%>" id="tab4">
                        <h5>Other Settings</h5>
                        <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/ViewSettingsTable.ascx"), OtherList); %>
                    </div>
                <%}%>
            </div>
        </div>
    </div>
</asp:Content>
