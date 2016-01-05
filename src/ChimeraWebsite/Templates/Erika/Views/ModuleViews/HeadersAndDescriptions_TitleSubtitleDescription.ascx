<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Title Subtitle Description
    [#Display_Description#]= #
    [#Parent_Categories#]= Headers & Descriptions
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_MEDIUM]
    [#Default_Value_Subtitle#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<%= CMM.ShowChildAndGetValue("h2","Title", EFT.SmallText) %>
<%= CMM.ShowChildAndGetValue("p", "Subtitle", EFT.SmallText, "", "main-meta")%>
<%= CMM.ShowChildAndGetValue("p", "Description", EFT.LargeWYSIWYG)%>
