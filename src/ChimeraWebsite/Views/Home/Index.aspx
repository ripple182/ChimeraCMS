<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%ChimeraWebsite.Models.PageModel PageModel = ViewBag.PageModel ?? new ChimeraWebsite.Models.PageModel(); %>
<%if(!PageModel.InEditMode)
{%>
    <%foreach (var Row in PageModel.Page.RowModuleList)
      {%>
          <div class="row">
              <%foreach (var Column in Row.ColumnModuleList)
              {
                  if (Column.ModuleTypeName.ToUpper().Contains("_COMMON_"))
                  {
                      Html.RenderPartial(Url.Content(String.Format("~/Views/PartialViews/CommonModuleViews/{1}.ascx", ChimeraWebsite.Models.ChimeraTemplate.TemplateName, Column.ModuleTypeName)), new ChimeraWebsite.Models.ColumnModuleModel(ViewContext.Controller.ControllerContext, PageModel.InEditMode, Column));
                  }
                  else
                  {

                      Html.RenderPartial(Url.Content(String.Format("~/Templates/{0}/Views/ModuleViews/{1}.ascx", ChimeraWebsite.Models.ChimeraTemplate.TemplateName, Column.ModuleTypeName)), new ChimeraWebsite.Models.ColumnModuleModel(ViewContext.Controller.ControllerContext, PageModel.InEditMode, Column));
                  }
              }%>
          </div>
      <%}
}
else if(ViewBag.PreviewPageData != null)
{%>
<!-- ko foreach: { data: page().PageModel.Page.RowModuleList, as: 'row' } -->
    <div class="row" data-bind="foreach: row.ColumnModuleList">
        <div
            data-bind="
    template: {
        name: $root.getColumnModuleTypeTemplateName($data),
        data: $root.getColumnModuleTypeTemplateData($data)
    },
    css: $data.AdditionalClassNames()">
        </div>
    </div>
<!-- /ko -->
<%}else{%>
    <!-- ko foreach: { data: page().PageModel.Page.RowModuleList, as: 'row' } -->
    <div class="chimeraRowIconBar chimeraIconColor" data-bind="
    visible: $root.currentlyHoveringOverRow(row),
    css: { 'chimeraRowIconBar-specialLeftBorder': row.ColumnModuleList().length == 0 },
    style: { right: EditorRowToolbarRight },
    event: {
        mouseleave: function () { $root.currentlyHoveringRow(null) },
        mouseover: function () { $root.currentlyHoveringRow(row) }
    }">
            <div title="Edit Settings" data-bind="click: function (data, event) { $root.rowModuleSettingsDialog(row);}">
                <span class="glyphicon glyphicon-th-large"></span>
            </div>
            <div title="Move Up" data-bind="visible: $index() !== 0, click: function (data, event) { $root.moveDirectionRowModule($index(), 'up') } ">
                <span class="glyphicon glyphicon-arrow-up"></span>
            </div>
            <div class="chimeraDisabledIconButton" data-bind="visible: $index() === 0">
                <span class="glyphicon glyphicon-arrow-up"></span>
            </div>
            <br />
            <div title="Delete" data-bind="click: function (data, event) { $root.removeRowModule($index()) } ">
                <span class="glyphicon glyphicon-trash"></span>
            </div>
            <div title="Move Down" data-bind="visible: $index() != $parent.page().PageModel.Page.RowModuleList().length - 1, click: function (data, event) { $root.moveDirectionRowModule($index(), 'down') } ">
                <span class="glyphicon glyphicon-arrow-down"></span>
            </div>
            <div class="chimeraDisabledIconButton" data-bind="visible: $index() == $parent.page().PageModel.Page.RowModuleList().length - 1">
                <span class="glyphicon glyphicon-arrow-down"></span>
            </div>
        </div>
    <div class="chimeraEditableRowModule row" data-bind="
    css: $root.getAdditionalRowModuleCSS(row),
    event: {
        mouseleave: function (data, event) { $root.currentlyHoveringRow(null); },
        mouseover: function (data, event) { $root.currentlyHoveringRow(row); }
    },
    sortable: {
        data: row.ColumnModuleList,
        options: {
            cancel: '.prevent',
            start: function (event, ui) { sortableStartEvent(event, ui) },
            stop: function (event, ui) { sortableStopEvent(event, ui); }
        }
    }">
        <div class="chimeraEditableColumnModule" 
            data-bind="
    event: {
        mouseleave: function () { $root.currentlyHoveringModule(null); },
        mouseover: function () { $root.currentlyHoveringModule($data); }
    },
    click: function (data, event) { $root.currentlyEditingModuleSet($data, event) },
    template: {
        name: $root.getColumnModuleTypeTemplateName($data),
        data: $root.getColumnModuleTypeTemplateData($data)
    },
    css: $data.AdditionalClassNames(),
    resizable: {
        handles: 'e',
        start: function (event, ui) { columnResizeApplyCSS($(this)); },
        stop: function (event, ui) { columnResizeApplyCSS($(this), true); },
        resize: function (event, ui) { columnResizeEvent(event, ui, $data); }
    }">
        </div>
    </div>
<!-- /ko -->
<%}%>
    
</asp:Content>
