<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Custom HTML Content
    [#Display_Description#]= #
    [#Parent_Categories#]= Other
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_CustomHTML#]= [CHIMERA_VALUE_DESCRIPTION_LONG]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<%= CMM.ShowChildAndGetValue("div","CustomHTML", EFT.LargeHTML) %>