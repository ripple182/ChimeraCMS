<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= Default Accordion Content
    [#Display_Description#]= #
    [#Parent_Categories#]= Multi Content Holders
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Accordion_1_Title#]= Accordion 1 Title
    [#Default_Value_Accordion_1_Description#]= Accordion 1 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_2_Title#]= Accordion 2 Title
    [#Default_Value_Accordion_2_Description#]= Accordion 2 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_3_Title#]= Accordion 3 Title
    [#Default_Value_Accordion_3_Description#]= Accordion 3 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_4_Title#]= Accordion 4 Title
    [#Default_Value_Accordion_4_Description#]= Accordion 4 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_5_Title#]= Accordion 5 Title
    [#Default_Value_Accordion_5_Description#]= Accordion 5 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_6_Title#]= Accordion 6 Title
    [#Default_Value_Accordion_6_Description#]= Accordion 6 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_7_Title#]= Accordion 7 Title
    [#Default_Value_Accordion_7_Description#]= Accordion 7 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_8_Title#]= Accordion 8 Title
    [#Default_Value_Accordion_8_Description#]= Accordion 8 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_9_Title#]= Accordion 9 Title
    [#Default_Value_Accordion_9_Description#]= Accordion 9 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Accordion_10_Title#]= Accordion 10 Title
    [#Default_Value_Accordion_10_Description#]= Accordion 10 Description <br/><br/> [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="faq">
    <div class="accordion">
        <%for(int i = 1; i <= 10; i++)
          {%>
            <%Html.RenderPartial(Url.Content("~/Templates/Erika/Views/ModuleViews/PartialModuleViews/MultiAccordionContent_Accordion.ascx"), new PartialColumnModuleModel { ColumnModuleModel = CMM, UniqueIdentifier = i.ToString() });%>
        <%}%>
    </div>
</div>
