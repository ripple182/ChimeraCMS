<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Side Tab Content
    [#Display_Description#]= #
    [#Parent_Categories#]= Multi Content Holders
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Tab_1_Title#]= Tab 1 Title
    [#Default_Value_Tab_1_Content_Title#]= Tab 1 Title
    [#Default_Value_Tab_1_Description#]= Tab 1 Description
    [#Default_Value_Tab_2_Title#]= Tab 2 Title
    [#Default_Value_Tab_2_Content_Title#]= Tab 2 Title
    [#Default_Value_Tab_2_Description#]= Tab 2 Description
    [#Default_Value_Tab_3_Title#]= Tab 3 Title
    [#Default_Value_Tab_3_Content_Title#]= Tab 3 Title
    [#Default_Value_Tab_3_Description#]= Tab 3 Description
    [#Default_Value_Tab_4_Title#]= Tab 4 Title
    [#Default_Value_Tab_4_Content_Title#]= Tab 4 Title
    [#Default_Value_Tab_4_Description#]= Tab 4 Description
    [#Default_Value_Tab_5_Title#]= Tab 5 Title
    [#Default_Value_Tab_5_Content_Title#]= Tab 5 Title
    [#Default_Value_Tab_5_Description#]= Tab 5 Description
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="row tab-v3">
    <div class="col-sm-3">
        <ul class="nav nav-pills nav-stacked">
            <li class="active">
                <%= CMM.ShowChildAndGetValue("a","Tab_1_Title", EFT.SmallText, "data-toggle='tab'") %>
            </li>
            <li>
                <%= CMM.ShowChildAndGetValue("a","Tab_2_Title", EFT.SmallText, "data-toggle='tab'") %>
            </li>
            <li>
                <%= CMM.ShowChildAndGetValue("a","Tab_3_Title", EFT.SmallText, "data-toggle='tab'") %>
            </li>
            <li>
                <%= CMM.ShowChildAndGetValue("a","Tab_4_Title", EFT.SmallText, "data-toggle='tab'") %>
            </li>
            <li>
                <%= CMM.ShowChildAndGetValue("a","Tab_5_Title", EFT.SmallText, "data-toggle='tab'") %>
            </li>
        </ul>
    </div>
    <div class="col-sm-9">
        <div class="tab-content">
            <div class="tab-pane active">
                <%= CMM.ShowChildAndGetValue("h5","Tab_1_Content_Title", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Tab_1_Description", EFT.LargeWYSIWYG) %>
            </div>
            <div class="tab-pane">
                <%= CMM.ShowChildAndGetValue("h5","Tab_2_Content_Title", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Tab_2_Description", EFT.LargeWYSIWYG) %>
            </div>
            <div class="tab-pane">
                <%= CMM.ShowChildAndGetValue("h5","Tab_3_Content_Title", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Tab_3_Description", EFT.LargeWYSIWYG) %>
            </div>
            <div class="tab-pane">
                <%= CMM.ShowChildAndGetValue("h5","Tab_4_Content_Title", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Tab_4_Description", EFT.LargeWYSIWYG) %>
            </div>
            <div class="tab-pane">
                <%= CMM.ShowChildAndGetValue("h5","Tab_5_Content_Title", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Tab_5_Description", EFT.LargeWYSIWYG) %>
            </div>
        </div>
    </div>
</div>
