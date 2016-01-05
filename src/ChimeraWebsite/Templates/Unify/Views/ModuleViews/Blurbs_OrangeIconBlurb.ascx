<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Orange Icon Blurb
    [#Display_Description#]= #
    [#Parent_Categories#]= Blurbs
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_ThumbnailIcon#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="servive-block">
    <div class="servive-block-in servive-block-colored servive-block-orange">
        <%= CMM.ShowChildAndGetValue("h4","Title", EFT.SmallText) %>
        <div>
            <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"ThumbnailIcon", EFT.IconSelect) %>
        </div>
        <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeText) %>
    </div>
</div>
