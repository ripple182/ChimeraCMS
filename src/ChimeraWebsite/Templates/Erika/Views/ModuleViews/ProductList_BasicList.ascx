<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Basic Product List
    [#Display_Description#]= #
    [#Parent_Categories#]= Product List
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_SearchUrl#]= [CHIMERA_DEFAULT_PRODUCT_LIST_URL]?viewType=ProductList_BasicList&numFakeProds=4
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<%= CMM.ShowChildAndGetValue(SpecialHTMLElement.ProductList,"SearchUrl", EFT.EditSearchProductFilters) %>