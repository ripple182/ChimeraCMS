<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= 4 Slided Slideshow
    [#Display_Description#]= #
    [#Parent_Categories#]= Large Description Slideshow
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Slide_1_Title#]= Title
    [#Default_Value_Slide_1_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_1_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
    [#Default_Value_Slide_1_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
    [#Default_Value_Slide_2_Title#]= Title
    [#Default_Value_Slide_2_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_2_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
    [#Default_Value_Slide_2_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
    [#Default_Value_Slide_3_Title#]= Title
    [#Default_Value_Slide_3_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_3_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
    [#Default_Value_Slide_3_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
    [#Default_Value_Slide_4_Title#]= Title
    [#Default_Value_Slide_4_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_4_Button#]= [CHIMERA_VALUE_DEFAULT_BUTTON]
    [#Default_Value_Slide_4_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="da-slider">
     <%for(int i = 1; i <= 4; i++)
          {%>
            <%Html.RenderPartial(Url.Content("~/Templates/Erika/Views/ModuleViews/PartialModuleViews/LargeDescriptionSlideshow_Slide.ascx"), new PartialColumnModuleModel { ColumnModuleModel = CMM, UniqueIdentifier = i.ToString() });%>
        <%}%>
    <nav class="da-arrows">
        <span class="da-arrows-prev"></span>
        <span class="da-arrows-next"></span>
    </nav>
</div>
