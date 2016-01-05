<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Bordered Icon Blurb
    [#Display_Description#]= #
    [#Parent_Categories#]= Blurbs
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_ThumbnailIcon#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="bg-light">
    <h4>
        <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"ThumbnailIcon", EFT.IconSelect) %>
        <%= CMM.ShowChildAndGetValue("span","Title", EFT.SmallText) %>
    </h4>
    <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeText) %>
</div>
