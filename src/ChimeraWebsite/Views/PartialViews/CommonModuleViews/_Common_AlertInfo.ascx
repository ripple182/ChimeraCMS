<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Default Info Alert
    [#Display_Description#]= #
    [#Parent_Categories#]= Alerts
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Text#]= [CHIMERA_VALUE_TITLE_LONG]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<%= CMM.ShowChildAndGetValue("div","Text", EFT.LargeWYSIWYG, "", "alert alert-info") %>