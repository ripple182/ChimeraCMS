<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Title Button Cutout
    [#Display_Description#]= #
    [#Parent_Categories#]= Taglines
    [#AdditionalClassNameList#]= col-lg-6 col-md-6 col-sm-6 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Title#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="row tag-box tag-box-v5">
    <div class="col-md-8">
        <%= CMM.ShowChildAndGetValue("span","Title", EFT.SmallText) %>
    </div>
    <div class="col-md-4">
        <p><%= CMM.ShowChildAndGetValue(SHE.BootstrapButton,"Button", EFT.BootstrapButton) %></p>
    </div>
</div>
