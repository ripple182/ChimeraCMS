<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Simple Title Only
    [#Display_Description#]= #
    [#Parent_Categories#]= Headers & Descriptions
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="rposts">
    <%= CMM.ShowChildAndGetValue("h4","Title", EFT.SmallText) %>
</div>