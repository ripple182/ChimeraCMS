<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Square Rounded Testimonial
    [#Display_Description#]= #
    [#Parent_Categories#]= Other
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_TestimonialText#]= [CHIMERA_VALUE_TITLE_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="testi">
    <%= CMM.ShowChildAndGetValue("div","Description", EFT.LargeWYSIWYG, "", "tquote") %>
    <div class="tauthor pull-right">
        <i class="icon-user"></i>&nbsp;<%= CMM.ShowChildAndGetValue("span","TestimonialText", EFT.SmallText) %>
    </div>
    <div class="clearfix"></div>
</div>
