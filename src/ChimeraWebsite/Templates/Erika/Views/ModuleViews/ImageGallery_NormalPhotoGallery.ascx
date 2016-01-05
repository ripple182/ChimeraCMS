<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Default Photo Gallery
    [#Display_Description#]= #
    [#Parent_Categories#]= Image Gallery
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Image_1#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Image_2#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Image_3#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Image_4#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="ericka-gallery-1">
    <a class="prettyphoto" <%=CMM.GetHTMLAttributeValueFromKey("href","Image_1") %>>
        <%= CMM.ShowChildAndGetValue("img","Image_1", EFT.ImageSelect) %>
    </a>
    <a class="prettyphoto" <%=CMM.GetHTMLAttributeValueFromKey("href","Image_2") %>>
        <%= CMM.ShowChildAndGetValue("img","Image_2", EFT.ImageSelect) %>
    </a>
    <a class="prettyphoto" <%=CMM.GetHTMLAttributeValueFromKey("href","Image_3") %>>
        <%= CMM.ShowChildAndGetValue("img","Image_3", EFT.ImageSelect) %>
    </a>
    <a class="prettyphoto" <%=CMM.GetHTMLAttributeValueFromKey("href","Image_4") %>>
        <%= CMM.ShowChildAndGetValue("img","Image_4", EFT.ImageSelect) %>
    </a>
</div>
