<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Single Timeline Square
    [#Display_Description#]= #
    [#Parent_Categories#]= Other
    [#AdditionalClassNameList#]= col-lg-6 col-md-6 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Timeline#]= 1990
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_MEDIUM]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="time">
    <%= CMM.ShowChildAndGetValue("div","Timeline", EFT.SmallText, "", "tidate") %>
    <div class="timatter">
        <%= CMM.ShowChildAndGetValue("h5","Title", EFT.SmallText) %>
        <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeWYSIWYG) %>
    </div>
    <div class="clearfix"></div>
</div>
