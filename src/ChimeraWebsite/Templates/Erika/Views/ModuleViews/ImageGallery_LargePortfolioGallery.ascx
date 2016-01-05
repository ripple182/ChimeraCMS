<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Large Portfolio Gallery
    [#Display_Description#]= #
    [#Parent_Categories#]= Image Gallery
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Image_1#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Title_1#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description_1#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Image_2#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Title_2#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description_2#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Image_3#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Title_3#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Description_3#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="ericka-gallery-4">
    <div class="element" <%=CMM.ShowBasedOnActiveKey("Image_1") %>>
        <a class="prettyphoto" <%=CMM.GetHTMLAttributeValueFromKey("href","Image_1") %>>
            <%= CMM.ShowChildAndGetValue("img","Image_1", EFT.ImageSelect) %>
            <div class="pcap">
                <%= CMM.ShowChildAndGetValue("h5","Title_1", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Description_1", EFT.LargeText) %>
            </div>
        </a>
    </div>
    <div class="element" <%=CMM.ShowBasedOnActiveKey("Image_2") %>>
        <a class="prettyphoto" <%=CMM.GetHTMLAttributeValueFromKey("href","Image_2") %>>
            <%= CMM.ShowChildAndGetValue("img","Image_2", EFT.ImageSelect) %>
            <div class="pcap">
                <%= CMM.ShowChildAndGetValue("h5","Title_2", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Description_2", EFT.LargeText) %>
            </div>
        </a>
    </div>
    <div class="element" <%=CMM.ShowBasedOnActiveKey("Image_3") %>>
        <a class="prettyphoto" <%=CMM.GetHTMLAttributeValueFromKey("href","Image_3") %>>
            <%= CMM.ShowChildAndGetValue("img","Image_3", EFT.ImageSelect) %>
            <div class="pcap">
                <%= CMM.ShowChildAndGetValue("h5","Title_3", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Description_3", EFT.LargeText) %>
            </div>
        </a>
    </div>
</div>
