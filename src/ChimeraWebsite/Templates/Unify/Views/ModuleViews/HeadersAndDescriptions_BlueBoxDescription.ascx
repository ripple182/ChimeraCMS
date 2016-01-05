<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Blue Box Description
    [#Display_Description#]= #
    [#Parent_Categories#]= Headers & Descriptions
    [#AdditionalClassNameList#]= col-lg-6 col-md-6 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_MEDIUM]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
    [#Default_Value_Icon_1#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Icon_1_Text#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Icon_2#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Icon_2_Text#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Icon_3#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Icon_3_Text#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Icon_4#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Icon_4_Text#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Icon_5#]= [CHIMERA_VALUE_DEFAULT_ICON]
    [#Default_Value_Icon_5_Text#]= [CHIMERA_VALUE_TITLE_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="search-blocks search-blocks-colored search-blocks-blue">
    <div class="row">
        <div class="col-md-4 search-img">
            <%= CMM.ShowChildAndGetValue("img","Image", EFT.ImageSelect, "", "img-responsive") %>
            <ul class="list-unstyled">
                <li>
                    <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"Icon_1", EFT.IconSelect) %>
                    <%= CMM.ShowChildAndGetValue("span","Icon_1_Text", EFT.ExtraSmallText) %>
                </li>
                <li>
                    <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"Icon_2", EFT.IconSelect) %>
                    <%= CMM.ShowChildAndGetValue("span","Icon_2_Text", EFT.ExtraSmallText) %>
                </li>
                <li>
                    <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"Icon_3", EFT.IconSelect) %>
                    <%= CMM.ShowChildAndGetValue("span","Icon_3_Text", EFT.ExtraSmallText) %>
                </li>
                <li>
                    <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"Icon_4", EFT.IconSelect) %>
                   <%= CMM.ShowChildAndGetValue("span","Icon_4_Text", EFT.ExtraSmallText) %>
                </li>
                <li>
                    <%= CMM.ShowChildAndGetValue(SpecialHTMLElement.Icon,"Icon_5", EFT.IconSelect) %>
                    <%= CMM.ShowChildAndGetValue("span","Icon_5_Text", EFT.ExtraSmallText) %>
                </li>
            </ul>
        </div>
        <div class="col-md-8">
            <h2><%= CMM.ShowChildAndGetValue("a","Title", EFT.SmallText, "href='#'") %></h2>
            <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeWYSIWYG) %>
            <%= CMM.ShowChildAndGetValue(SHE.BootstrapButton,"Button", EFT.BootstrapButton) %>
        </div>
    </div>
</div>
