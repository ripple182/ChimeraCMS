<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Custom Orange Panel
    [#Display_Description#]= #
    [#Parent_Categories#]= Panels
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_ShowTitle#]= [CHIMERA_VALUE_EMPTY_TOGGLE]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_LONG]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="panel panel-orange">
  <div class="panel-heading" <%=CMM.ShowBasedOnActiveKey("ShowTitle") %>>
    <%= CMM.ShowChildAndGetValue("h3", "Title", EFT.SmallText, "", "panel-title") %>
  </div>
  <%= CMM.ShowChildAndGetValue("div", "Description", EFT.LargeWYSIWYG, "", "panel-body") %>
</div>