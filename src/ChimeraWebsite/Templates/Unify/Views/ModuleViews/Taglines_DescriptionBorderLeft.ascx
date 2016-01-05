<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Description Border Left
    [#Display_Description#]= #
    [#Parent_Categories#]= Taglines
    [#AdditionalClassNameList#]= col-lg-6 col-md-6 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="tag-box tag-box-v2">
    <%= CMM.ShowChildAndGetValue("h2","Title", EFT.SmallText) %>
    <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeWYSIWYG) %>
</div>
