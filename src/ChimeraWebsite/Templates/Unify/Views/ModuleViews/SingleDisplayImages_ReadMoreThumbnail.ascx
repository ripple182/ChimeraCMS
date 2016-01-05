<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Read More Thumbnail
    [#Display_Description#]= #
    [#Parent_Categories#]= Single Display Images
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-3 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="thumbnails thumbnail-style thumbnail-kenburn">
    <div class="thumbnail-img">
        <div class="overflow-hidden">
            <%= CMM.ShowChildAndGetValue("img","Image", EFT.ImageSelect, "", "img-responsive") %>
        </div>
        <%= CMM.ShowChildAndGetValue(SHE.BootstrapButton,"Button", EFT.BootstrapButton, "", "btn-more hover-effect") %>
    </div>
    <div class="caption">
        <h3><%= CMM.ShowChildAndGetValue("a","Title", EFT.SmallText, "href='#'", "hover-effect") %></h3>
        <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeText) %>
    </div>
</div>
