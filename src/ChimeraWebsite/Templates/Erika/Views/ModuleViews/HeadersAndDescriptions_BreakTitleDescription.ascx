<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Break Title Description
    [#Display_Description#]= #
    [#Parent_Categories#]= Headers & Descriptions
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_MEDIUM]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
    <div class="hero">
        <h3>
            <%= CMM.ShowChildAndGetValue("span","Title", EFT.SmallText) %>
        </h3>
        <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeWYSIWYG) %>
    </div>