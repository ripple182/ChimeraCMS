<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Icon Size 350
    [#Display_Description#]= #
    [#Parent_Categories#]= Single Icons
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Icon#]= [CHIMERA_VALUE_DEFAULT_ICON]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="c-icon c-icon-350">
    <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"Icon", EFT.IconSelect) %>
</div>