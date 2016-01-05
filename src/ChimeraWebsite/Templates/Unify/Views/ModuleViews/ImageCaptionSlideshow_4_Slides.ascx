<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= 4 Slided Slideshow
    [#Display_Description#]= #
    [#Parent_Categories#]= Image Caption Slideshow
    [#AdditionalClassNameList#]= col-lg-5 col-md-5 col-sm-12 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Slide_1_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_SUPER_WIDE]
    [#Default_Value_Slide_1_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Slide_2_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_SUPER_WIDE]
    [#Default_Value_Slide_2_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Slide_3_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_SUPER_WIDE]
    [#Default_Value_Slide_3_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Slide_4_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_SUPER_WIDE]
    [#Default_Value_Slide_4_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="carousel slide carousel-v1">
    <div class="carousel-inner">
        <%for (int i = 1; i <= 4; i++)
          {%>
        <%Html.RenderPartial(Url.Content("~/Templates/Unify/Views/ModuleViews/PartialModuleViews/ImageCaptionSlideshow_Slide.ascx"), new PartialColumnModuleModel { ColumnModuleModel = CMM, UniqueIdentifier = i.ToString() });%>
        <%}%>
    </div>
    <div class="carousel-arrow">
        <a class="left carousel-control" href="#" data-slide="prev">
            <i class="icon-angle-left"></i>
        </a>
        <a class="right carousel-control" href="#" data-slide="next">
            <i class="icon-angle-right"></i>
        </a>
    </div>
</div>
