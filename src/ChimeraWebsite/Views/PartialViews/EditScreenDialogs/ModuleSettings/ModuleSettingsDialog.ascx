<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: moduleSettingsDialog, with: moduleSettingsDialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: $root.closeModuleSettingsDialog">&times;</button>
                <h4 class="modal-title">Edit Module Settings</h4>
            </div>
            <div class="modal-body">
                <ul class="nav nav-tabs chimera-module-settings-tabs">
                    <li data-bind="visible: !jQuery.isEmptyObject($data.ChildrenValueDictionary), css: { active: !jQuery.isEmptyObject($data.ChildrenValueDictionary) }"><a href="#chimera-module-settings-values" data-toggle="tab"><span class="glyphicon glyphicon-pencil"></span>Children Visibility</a></li>
                    <li data-bind="css: { active: jQuery.isEmptyObject($data.ChildrenValueDictionary) }"><a href="#chimera-module-settings-visibility" data-toggle="tab"><span class="glyphicon glyphicon-eye-open"></span>Component Visibility</a></li>
                    <li><a href="#chimera-module-settings-sizes" data-toggle="tab"><span class="glyphicon  glyphicon-resize-horizontal"></span>Device Sizes</a></li>
                </ul>
                <div class="tab-content">
                    <div data-bind="visible: !jQuery.isEmptyObject($data.ChildrenValueDictionary), css: { active: !jQuery.isEmptyObject($data.ChildrenValueDictionary) }" class="tab-pane" id="chimera-module-settings-values">
                        <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/ModuleSettings/ChildComponentActiveTab.ascx"));%>
                    </div>
                    <div data-bind="css: { active: jQuery.isEmptyObject($data.ChildrenValueDictionary) }" class="tab-pane" id="chimera-module-settings-visibility">
                        <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/ModuleSettings/DeviceVisibilityTab.ascx"));%>
                    </div>
                    <div class="tab-pane" id="chimera-module-settings-sizes">
                        <%Html.RenderPartial(Url.Content("~/Views/PartialViews/EditScreenDialogs/ModuleSettings/DeviceSizeTab.ascx"));%>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <table class="table table-striped">
                    <tbody>
                        <tr data-bind="visible: $root.areAllDevicesSetToHidden()">
                            <td colspan="3" class="danger">WARNING - The module is no longer visible on any device, if you click the close button the module will be removed.
                            </td>
                        </tr>
                    </tbody>
                </table>
                <button type="button" class="btn btn-default" data-bind="click: $root.closeModuleSettingsDialog">Close</button>
            </div>
        </div>
    </div>
</div>
