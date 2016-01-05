<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= 4 Slided Slideshow
    [#Display_Description#]= #
    [#Parent_Categories#]= Large Image Slideshow
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Slide_1_CaptionBox#]= [CHIMERA_VALUE_EMPTY_TOGGLE]
    [#Default_Value_Slide_1_Title#]= Title
    [#Default_Value_Slide_1_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_1_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
    [#Default_Value_Slide_2_CaptionBox#]= [CHIMERA_VALUE_EMPTY_TOGGLE]
    [#Default_Value_Slide_2_Title#]= Title
    [#Default_Value_Slide_2_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_2_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
    [#Default_Value_Slide_3_CaptionBox#]= [CHIMERA_VALUE_EMPTY_TOGGLE]
    [#Default_Value_Slide_3_Title#]= Title
    [#Default_Value_Slide_3_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_3_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
    [#Default_Value_Slide_4_CaptionBox#]= [CHIMERA_VALUE_EMPTY_TOGGLE]
    [#Default_Value_Slide_4_Title#]= Title
    [#Default_Value_Slide_4_Description#]= [CHIMERA_VALUE_DESCRIPTION_MEDIUM]
    [#Default_Value_Slide_4_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_WIDE]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="flexslider">
    <ul class="slides">
        <%for(int i = 1; i <= 4; i++)
          {%>
            <%Html.RenderPartial(Url.Content("~/Templates/Erika/Views/ModuleViews/PartialModuleViews/LargeImageSlideshow_Slide.ascx"), new PartialColumnModuleModel{ ColumnModuleModel = CMM, UniqueIdentifier = i.ToString()});%>
        <%}%>
    </ul>
</div>