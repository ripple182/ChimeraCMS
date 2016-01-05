<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Square Sharp Testimonial
    [#Display_Description#]= #
    [#Parent_Categories#]= Other
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_TestimonialText#]= [CHIMERA_VALUE_TITLE_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<%= CMM.ShowChildAndGetValue("div","Description", EFT.LargeWYSIWYG, "", "test") %>
<div class="test-arrow"></div>
<div class="tauth">
    <i class="icon-user"></i>&nbsp;<%= CMM.ShowChildAndGetValue("span","TestimonialText", EFT.SmallText) %>
</div>
