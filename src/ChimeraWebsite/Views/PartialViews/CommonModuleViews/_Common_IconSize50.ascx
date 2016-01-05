<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Icon Size 50
    [#Display_Description#]= #
    [#Parent_Categories#]= Single Icons
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-3 col-xs-6 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Icon#]= [CHIMERA_VALUE_DEFAULT_ICON]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="c-icon c-icon-50">
    <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"Icon", EFT.IconSelect) %>
</div>