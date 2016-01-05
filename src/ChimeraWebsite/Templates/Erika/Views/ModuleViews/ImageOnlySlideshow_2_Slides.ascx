<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%--
    [#Display_Name#]= 2 Slided Slideshow
    [#Display_Description#]= #
    [#Parent_Categories#]= Image Only Slideshow
    [#AdditionalClassNameList#]= [CHIMERA_CSS_COLUMNS_FULLWIDTH_ALL_DEVICES] [CHIMERA_CSS_VISIBLE_ALL_DEVICES]
    [#Default_Value_Slide_1_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_SUPER_WIDE]
    [#Default_Value_Slide_2_Image#]= [CHIMERA_VALUE_DEFAULT_IMAGE_SUPER_WIDE]
--%>
<%ColumnModuleModel CMM = Model ?? new ColumnModuleModel(); %>
<div class="carousel slide">
    <div class="carousel-inner">
        <%for (int i = 1; i <= 2; i++)
          {%>
        <%Html.RenderPartial(Url.Content("~/Templates/Erika/Views/ModuleViews/PartialModuleViews/ImageOnlySlideshow_Slide.ascx"), new PartialColumnModuleModel { ColumnModuleModel = CMM, UniqueIdentifier = i.ToString() });%>
        <%}%>
    </div>
    <a class="left carousel-control">
        <span class="icon-prev"></span>
    </a>
    <a class="right carousel-control">
        <span class="icon-next"></span>
    </a>
</div>
