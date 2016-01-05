<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Normal Accordion Content
    [#Display_Description#]= #
    [#Parent_Categories#]= Multi Content Holders
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Accordion_1_Title#]= Accordion Title
    [#Default_Value_Accordion_1_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_2_Title#]= Accordion Title
    [#Default_Value_Accordion_2_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_3_Title#]= Accordion Title
    [#Default_Value_Accordion_3_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_4_Title#]= Accordion Title
    [#Default_Value_Accordion_4_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_5_Title#]= Accordion Title
    [#Default_Value_Accordion_5_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_6_Title#]= Accordion Title
    [#Default_Value_Accordion_6_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_7_Title#]= Accordion Title
    [#Default_Value_Accordion_7_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_8_Title#]= Accordion Title
    [#Default_Value_Accordion_8_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_9_Title#]= Accordion Title
    [#Default_Value_Accordion_9_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_10_Title#]= Accordion Title
    [#Default_Value_Accordion_10_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="panel-group acc-v1">
    <%for (int i = 1; i <= 10; i++)
      {%>
        <%Html.RenderPartial(Url.Content("~/Templates/Unify/Views/ModuleViews/PartialModuleViews/NormalAccordionContent_Accordion.ascx"), new PartialColumnModuleModel { ColumnModuleModel = CMM, UniqueIdentifier = i.ToString() });%>
    <%}%>
</div>
