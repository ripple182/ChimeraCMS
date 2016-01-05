<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= NonAdjustable Default Image
    [#Display_Description#]= #
    [#Parent_Categories#]= Single Display Images
    [#AdditionalClassNameList#]= col-lg-3 col-md-3 col-sm-3 col-xs-6 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<%= CMM.ShowChildAndGetValue("img","Image", EFT.ImageSelect) %>