<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Small Circle Blurb
    [#Display_Description#]= #
    [#Parent_Categories#]= Blurbs
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_ThumbnailImage#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_ThumbnailIcon#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Inactive_ThumbnailImage#]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="afeature">
    <div class="afmatter">
        <%= CMM.ShowChildAndGetValue("img","ThumbnailImage", EFT.ImageSelect) %>
        <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"ThumbnailIcon", EFT.IconSelect) %>
        <%= CMM.ShowChildAndGetValue("h4","Title", EFT.SmallText) %>
        <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeText) %>
    </div>
</div>