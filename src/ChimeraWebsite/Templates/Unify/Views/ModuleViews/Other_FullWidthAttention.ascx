<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Full Width Attention
    [#Display_Description#]= #
    [#Parent_Categories#]= Other
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_MEDIUM]
    [#Default_Value_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="purchase">
    <div class="container">
        <div class="row">
            <div class="col-md-9">
                <%= CMM.ShowChildAndGetValue("span","Title", EFT.SmallText) %>
                <%= CMM.ShowChildAndGetValue("p","Description", EFT.LargeText) %>
            </div>
            <div class="col-md-3">
                <%= CMM.ShowChildAndGetValue(SHE.BootstrapButton,"Button", EFT.BootstrapButton) %>
            </div>
        </div>
    </div>
</div>
