<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= 2 Slided Slideshow
    [#Display_Description#]= #
    [#Parent_Categories#]= Client Testimonial Slideshow
    [#AdditionalClassNameList#]= col-lg-5 col-md-5 col-sm-12 col-xs-12 [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Slide_1_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Slide_1_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Slide_1_ClientName#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Slide_1_ClientSubtitle#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Slide_2_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Slide_2_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Slide_2_ClientName#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Slide_2_ClientSubtitle#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Slide_3_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE]
    [#Default_Value_Slide_3_Description#]= [CHIMERA_VALUE_DESCRIPTION_SMALL]
    [#Default_Value_Slide_3_ClientName#]= [CHIMERA_VALUE_TITLE_SMALL]
    [#Default_Value_Slide_3_ClientSubtitle#]= [CHIMERA_VALUE_TITLE_SMALL]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="carousel slide testimonials testimonials-v1">
    <div class="carousel-inner">
        <%for (int i = 1; i <= 3; i++)
          {%>
        <%Html.RenderPartial(Url.Content("~/Templates/Unify/Views/ModuleViews/PartialModuleViews/ClientTestimonialSlideshow_Slide.ascx"), new PartialColumnModuleModel { ColumnModuleModel = CMM, UniqueIdentifier = i.ToString() });%>
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
