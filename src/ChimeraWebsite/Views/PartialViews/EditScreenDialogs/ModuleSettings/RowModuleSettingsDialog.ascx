<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: rowModuleSettingsDialog, with: rowModuleSettingsDialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: $root.closeRowModuleSettingsDialog">&times;</button>
                <h4 class="modal-title">Edit Row Settings</h4>
            </div>
            <div class="modal-body">
                <ul class="nav nav-tabs chimera-module-settings-tabs">
                    <li data-bind="css: { active: jQuery.isEmptyObject($data.ChildrenValueDictionary) }"><a href="#chimera-row-module-settings-visibility" data-toggle="tab"><span class="glyphicon glyphicon-eye-open"></span>Row Visibility</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="chimera-row-module-settings-visibility">
                        <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/ModuleSettings/DeviceVisibilityTab.ascx"));%>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-bind="click: $root.closeRowModuleSettingsDialog">Close</button>
            </div>
        </div>
    </div>
</div>
