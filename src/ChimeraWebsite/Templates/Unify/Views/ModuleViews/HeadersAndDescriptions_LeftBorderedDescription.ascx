<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Left Bordered Description
    [#Display_Description#]= #
    [#Parent_Categories#]= Headers & Descriptions
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_MEDIUM]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="tag-box tag-box-v2">
    <%= CMM.ShowChildAndGetValue("h2","Title", EFT.SmallText) %>
    <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeWYSIWYG) %>
</div>
