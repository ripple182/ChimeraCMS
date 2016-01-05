<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%System.Web.Mvc.ControllerContext ControllerContext = ViewContext.Controller.ControllerContext; %>
<%System.Web.HttpContextBase HttpContext = Request.RequestContext.HttpContext; %>
<%PageModel PageModel = ViewBag.PageModel ?? new PageModel(); %>
<%List<ColumnModuleType> ColumnModuleTypeList = ColumnModuleType.GetList(ControllerContext, HttpContext);%>
<%List<Chimera.Entities.Uploads.Image> ImageList = ImageDAO.LoadAll(); %>
<%List<ColorHex> ColorHexList = ColorHexDAO.LoadAll(); %>
<%List<Icon> AvailableIconss = AvailableIcons.GetList(ControllerContext, HttpContext); %>
<%List<PageType> PageTypeList = Chimera.DataAccess.PageDAO.LoadPageTypes(); %>
<%List<PreviewProduct> PreviewProductList = ProductDAO.LoadPreviewProducts(); %>
<%List<CEP.Property> SearchPropertyList = ProductDAO.LoadProductSearchProperties(); %>
<%Dictionary<string, string> DefaultTokenDictionary = DefaultTokenValues.GetDictionary(ControllerContext, HttpContext); %>
<%string DefaultIcon = DefaultTokenDictionary["[CHIMERA_VALUE_DEFAULT_ICON]"];%>
<script type="text/javascript">

    <%if (ViewBag.PreviewPageData == null)
      {%>

        var columnModuleTypesData = '{"ColumnModuleTypes":<%=HttpUtility.JavaScriptStringEncode((new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ColumnModuleTypeList)))%>}';
        var pageData = '{"PageModel":<%=HttpUtility.JavaScriptStringEncode(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(PageModel))%>}';
        var imageData = '{"ImageList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(ImageList))%>}';
        var colorData = '{"ColorList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(ColorHexList))%>}';
        var iconData = '{"IconList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(AvailableIconss))%>}';
        var pageTypeData = '{"PageTypeList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(PageTypeList))%>}';    
        var previewProductData = '{"PreviewProductList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(PreviewProductList))%>}';
        var productSearchPropertyData = '{"ProductSearchPropertyList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(SearchPropertyList))%>}';
        var websiteDefaultUrl = '<%=Url.Content("~/")%>';
        var Default_LG_MD_Column_Width = <%=ConfigurationManager.AppSettings["Default_LG_MD_Column_Width"]%>;
        var Default_SM_XS_Column_Width = <%=ConfigurationManager.AppSettings["Default_SM_XS_Column_Width"]%>;
        var EditorRowToolbarRight = '<%=ConfigurationManager.AppSettings["EditorRowToolbarRight"]%>';
        var Default_New_Row_Module_Classes = '<%=DefaultTokenDictionary["[CHIMERA_CSS_VISIBLE_ALL_DEVICES]"]%>';
        var Default_Icon_Classes = '<%=DefaultIcon.Split('|')[0]%>';    
        var Default_Icon_Color = '<%=DefaultIcon.Split('|')[1]%>';
        var AvailableButtonColorClasses = '<%=DefaultTokenDictionary["[CHIMERA_AVAILABLE_BUTTON_COLOR_CLASSES]"]%>'.split('|');
        var AvailableButtonSizeClasses = '<%=DefaultTokenDictionary["[CHIMERA_AVAILABLE_BUTTON_SIZE_CLASSES]"]%>'.split('|');
        var defaultImageSource = '<%=DefaultTokenDictionary["[CHIMERA_VALUE_DEFAULT_IMAGE]"]%>';
    <%}
      else
      {%>
        var columnModuleTypesData = "";
        var pageData = '<%=HttpUtility.JavaScriptStringEncode(ViewBag.PreviewPageData)%>';
        var imageData = "";
        var colorData = "";
        var iconData = "";
        var pageTypeData = "";
        var previewProductData = "";
        var productSearchPropertyData = "";
        var websiteDefaultUrl = "";
        var Default_LG_MD_Column_Width = 1;
        var Default_SM_XS_Column_Width = 1;
        var EditorRowToolbarRight = "";
        var Default_New_Row_Module_Classes = "";
        var Default_Icon_Classes = '';    
        var Default_Icon_Color = '';
        var AvailableButtonColorClasses = new Array();
        var AvailableButtonSizeClasses = new Array();
        var defaultImageSource  = '';

    <%}%>

    viewModel = {
        columnModuleTypes: ko.observable(ko.mapping.fromJSON(columnModuleTypesData)),
        page: ko.observable(ko.mapping.fromJSON(pageData)),
        imageList: ko.observable(ko.mapping.fromJSON(imageData)),
        colorList: ko.observable(ko.mapping.fromJSON(colorData)),
        iconList: ko.observable(ko.mapping.fromJSON(iconData)),
        pageTypeList: ko.observable(ko.mapping.fromJSON(pageTypeData)),
        previewProductList: ko.observable(ko.mapping.fromJSON(previewProductData)),
        productSearchPropertyList: ko.observable(ko.mapping.fromJSON(productSearchPropertyData)),
        availableButtonColorClasses: ko.observableArray(AvailableButtonColorClasses),
        availableButtonSizeClasses: ko.observableArray(AvailableButtonSizeClasses)
    };

    LoadChimeraEditablePage();
</script>
