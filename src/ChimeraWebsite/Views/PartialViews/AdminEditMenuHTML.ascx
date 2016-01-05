<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%PageModel PageModel = ViewBag.PageModel ?? new PageModel(); %>
<%System.Web.Mvc.ControllerContext ControllerContext = ViewContext.Controller.ControllerContext; %>
<%System.Web.HttpContextBase HttpContext = Request.RequestContext.HttpContext; %>
<%List<ColumnModuleType> ColumnModuleTypeList = ColumnModuleType.GetList(ControllerContext, HttpContext);%>
<%if(ViewBag.PreviewPageData == null)
  {%>
<form id="chimera-editor-save-form" method="post" action="<%=Url.Content("~/Admin/Page/Editor_Save") %>">
    <input id="chimera-editor-save-input" type="hidden" name="pageData" />
</form>
<form id="chimera-editor-publish-form" method="post" action="<%=Url.Content("~/Admin/Page/Editor_Publish") %>">
    <input id="chimera-editor-publish-input" type="hidden" name="pageData" />
</form>
<ul class="chimera-navbar-fixed-top">
    <li>
        <div class="hidden-sm hidden-xs chimera-toolbar-buttons navbar-right">
            <div class="btn-group">
                <button id="chimera-editor-save-btn" type="button" class="btn btn-primary btn-sm">Save</button>
                <button id="chimera-editor-publish-btn" type="button" class="btn btn-success btn-sm">Publish</button>
            </div>
            <br />
            <div class="btn-group special-margin">
                <button id="chimera-editor-previewPage-btn" type="button" class="btn btn-warning btn-sm dropdown-toggle" data-toggle="dropdown">
                    Preview Page <span class="caret"></span>
                </button>
                <ul class="dropdown-menu" role="menu">
                    <li><a href="#" onclick="openPreviewWindow(567)">Phone</a></li>
                    <li><a href="#" onclick="openPreviewWindow(768)">Tablet</a></li>
                    <li><a href="#" onclick="openPreviewWindow(992)">Desktop</a></li>
                    <li><a href="#" onclick="openPreviewWindow(-1)">Large Desktop</a></li>
                </ul>
            </div>
        </div>
        <div class="hidden-sm hidden-xs chimera-toolbar-buttons navbar-right">
            <div class="chimera-toolbar-editingModeDiv">
                <span id="chimeraEditingModeText"></span>
                <br />
                Editing Mode
            </div>
        </div>
        <div class="pull-left categoryDiv" data-bind="css: { categoryDivBottomBorder: typeToShow() == 'none'}">
            <div class="categoryNavDiv">
                <div data-bind="event: { click: function(){specialCategory('rows')}}">
                    Rows
                    <div class="regularCaretDiv" data-bind="style: { visibility: specialCategory() == 'rows' ? 'hidden' : 'visible'}">
                        <b class="caret"></b>
                    </div>
                    <div class="selectedCaret" data-bind="style: { display: specialCategory() == 'rows' ? 'block' : 'none'}"></div>
                </div>
                <%foreach (var CategoryName in ChimeraWebsite.Models.Editor.ColumnModuleTypeCategory.GetList(ControllerContext, HttpContext).OrderBy(name => name))
                  {%>
                <div data-bind="event: { click: function(){typeToShow('<%=CategoryName%>')}}">
                    <%=CategoryName.Replace(" ","<br/>") %>
                    <div class="regularCaretDiv" data-bind="style: { visibility: typeToShow() == '<%=CategoryName%>' ? 'hidden' : 'visible'}">
                        <b class="caret"></b>
                    </div>
                    <div class="selectedCaret" data-bind="style: { display: typeToShow() == '<%=CategoryName%>' ? 'block' : 'none'}"></div>
                </div>
                <%}%>
            </div>
        </div>
        <div class="specialCategoryDiv" data-bind="visible: specialCategory() != null, event: { mouseleave: function(){specialCategory(null)}}, css: { categoryDivBottomBorder: specialCategory() != null}">
            <button class="btn btn-sm" data-bind="click: addNewEmptyRow">Add New<br />Empty Row</button>
        </div>
        <div class="moduleDiv" data-bind="css: { categoryDivBottomBorder: typeToShow() != null && moduleCurrentlyViewing() == null}">
            <div class="moduleNavDiv" data-bind="foreach: getModuleTypes">
                <div data-bind="event: { click: function(){$root.moduleCurrentlyViewing($data); Chimera_OnAddOrOnLoad();}}">
                    <span data-bind="html: DisplayName().replace(/ /g, '<br/>')"></span>
                    <div class="regularCaretDiv" data-bind="style: { visibility: $root.moduleCurrentlyViewing() != null && $root.moduleCurrentlyViewing().DisplayName == $data.DisplayName ? 'hidden' : 'visible'}">
                        <b class="caret"></b>
                    </div>
                    <div class="selectedCaret" data-bind="style: { display: $root.moduleCurrentlyViewing() != null && $root.moduleCurrentlyViewing().DisplayName == $data.DisplayName ? 'block' : 'none'}"></div>
                </div>
            </div>
            <!-- ko if: moduleCurrentlyViewing() != null -->
            <div class="moduleDescriptionDiv" data-bind=" css: { moduleDescriptionDivBottomBorder: moduleCurrentlyViewing() != null}, style: { display: moduleCurrentlyViewing() != null ? 'block' : 'none'}">
                <div class="container">
                    <div class="row">
                        <div class="moduleDescriptionDivBackground col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <button class="btn btn-danger btn-sm pull-right" data-bind="click: function(){ typeToShow(null); moduleCurrentlyViewing(null); }">
                                <span><span class="glyphicon glyphicon-remove"></span>&nbsp;&nbsp;Close</span>
                            </button>
                            <h3 data-bind="text: moduleCurrentlyViewing() != null ? moduleCurrentlyViewing().DisplayName : ''"></h3>
                            <table class="table table-striped chimera-preview-module-table">
	                            <thead>
		                            <tr>
			                            <th></th>
			                            <th></th>
			                            <th>
                                            <span class="glyphicon glyphicon-eye-open"></span>&nbsp;&nbsp;Visibility
			                            </th>
			                            <th>
                                            <span class="glyphicon  glyphicon-resize-horizontal"></span>&nbsp;&nbsp;Width (1 - 12)
			                            </th>
		                            </tr>
	                            </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES/phoneIcon.png" %>" title="Phones" alt="Phones"></td>
                                        <td>Extra small devices
                                                <small>Phones (&lt; 768px)</small>
                                        </td>
                                        <td>
                                            <span class="glyphicon" data-bind="css: $root.getPreviewModuleDeviceVisibility('xs')"></span>
                                        </td>
			                            <td data-bind="text: $root.getPreviewModuleDeviceSize('xs')"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES/tabletIcon.png" %>" title="Tablets" alt="Tablets"></td>
                                        <td>Small devices
                                                <small>Tablets (&gt;= 768px)</small>
                                        </td>
                                        <td>
                                            <span class="glyphicon" data-bind="css: $root.getPreviewModuleDeviceVisibility('sm')"></span>
                                        </td>
			                            <td data-bind="text: $root.getPreviewModuleDeviceSize('sm')"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES/smallDesktopIcon.png" %>" title="Desktops" alt="Desktops"></td>
                                        <td>Medium devices
                                                <small>Laptops (&gt;= 992px)</small>
                                        </td>
                                        <td>
                                            <span class="glyphicon" data-bind="css: $root.getPreviewModuleDeviceVisibility('md')"></span>
                                        </td>
			                            <td data-bind="text: $root.getPreviewModuleDeviceSize('md')"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES//desktopIcon.png" %>" title="Desktops" alt="Desktops"></td>
                                        <td>Large devices
                                                <small>Desktops (&gt;= 1200px)</small>
                                        </td>
                                        <td>
                                            <span class="glyphicon" data-bind="css: $root.getPreviewModuleDeviceVisibility('lg')"></span>
                                        </td>
			                            <td data-bind="text: $root.getPreviewModuleDeviceSize('lg')"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <button type="button" class="btn btn-primary btn-lg" data-bind="click: function(){addNewModuleToPage(moduleCurrentlyViewing())}">Click To Add</button>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-6 col-xs-12">
                            <div class="row chimera-preview-row">
                                <div data-bind="css: moduleCurrentlyViewing().ColumnModuleModel.ColumnModule.AdditionalClassNames(),  template: { name: getColumnModuleTypeTemplateName(moduleCurrentlyViewing()), data: getColumnModuleTypeTemplateData(moduleCurrentlyViewing()) }"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /ko -->
        </div>
    </li>
</ul>
<% }%>
<%--The following foreach loop will generate knockout templates for each of our modules--%>
<%foreach (var ColumnModType in ColumnModuleTypeList)
  {%>
<script type="text/html" id="<%=ColumnModType.ColumnModuleModel.ColumnModule.ModuleTypeName%>ChimeraTemplate">

    <%if(ColumnModType.ColumnModuleModel.ColumnModule.ModuleTypeName.ToUpper().Contains("_COMMON_"))
      {%>
        <%Html.RenderPartial(Url.Content(String.Format("~/Views/PartialViews/CommonModuleViews/{0}.ascx", ColumnModType.ColumnModuleModel.ColumnModule.ModuleTypeName)), new ColumnModuleModel(ControllerContext, PageModel.InEditMode, ColumnModType.ColumnModuleModel.ColumnModule));%>
    <%}
      else
      {%>
        <%Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/ModuleViews/{1}.ascx", ChimeraTemplate.TemplateName, ColumnModType.ColumnModuleModel.ColumnModule.ModuleTypeName)), new ColumnModuleModel(ControllerContext, PageModel.InEditMode, ColumnModType.ColumnModuleModel.ColumnModule));%>
    <%}%>

    
    <%if(ViewBag.PreviewPageData == null)
    {%>
        <div class="chimeraIconBar chimeraIconColor" data-bind="visible: $root.currentlyHoveringOverModule($parent)">
            <div data-bind="click: function(data, event) { $root.clearEditingModule(null, event) }, style: { visibility: $root.currentlyEditingModule() == $parent ? 'visible' : 'hidden'}" title="Done Editings">
                <span class="glyphicon glyphicon-check"></span>
            </div>
            <div data-bind="click: function(data, event) { $root.moduleSettingsDialog($parent); event.stopPropagation(); }" title="Edit Settings">
                <span class="glyphicon glyphicon-th-large"></span>
            </div>
            <div data-bind="click: function(data, event){ $root.removeColumnModule($parentContext.$parentContext.$index(), $index()) } " title="Delete">
                <span class="glyphicon glyphicon-trash"></span>
            </div>
        </div>
    <%}%>
</script>
<%}%>
<%if(ViewBag.PreviewPageData == null)
{%>
    <%--Create a dummy template to use whenever not viewing a specific module type.--%>
    <script type="text/html" id="DummyChimeraTemplate">
    </script>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/ModuleSettings/RowModuleSettingsDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/ModuleSettings/ModuleSettingsDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/UploadNewImageDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/SelectNewIconDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/EditBootstrapButtonDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/EditSearchProductFiltersDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/WYSIWYG/HyperlinkDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/WYSIWYG/IconDialog.ascx"));%>
    <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/WYSIWYG/ButtonDialog.ascx"));%>
<%}%>