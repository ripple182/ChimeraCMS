<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Square Image Blurb
    [#Display_Description#]= #
    [#Parent_Categories#]= Blurbs
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_ThumbnailImage#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="rpost1">
    <div class="rimg">
        <%= CMM.ShowChildAndGetValue("img","ThumbnailImage", EFT.ImageSelect, "", "img-responsive") %>
    </div>
    <div class="rdetails">
        <%= CMM.ShowChildAndGetValue("h5","Title", EFT.SmallText) %>
        <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeText) %>
    </div>
</div>