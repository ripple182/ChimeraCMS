<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: iconWYISWYGDialog, with: iconWYISWYGDialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header chimera-wysiwyg-icon-dialog-modal-header">
                <button type="button" class="btn btn-danger pull-right" data-bind="click: function (data, event) { $root.closeIconWYISWYGDialog(false) }">Cancel</button>
                <button type="button" class="btn btn-primary pull-right" data-bind="click: function (data, event) { $root.closeIconWYISWYGDialog(true) }">Insert Icon</button>
                <h4 class="modal-title">Insert Icon</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="thumbnail chimera-current-icon-thumbnail">
                            <h5 class="text-center">Current Icon</h5>
                            <span class="text-center" data-bind="css: $data.Classes, style: { color: $data.ColorHex }"></span>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="thumbnail">
                            <h5 class="text-center">Current Color</h5>
                            <div id="chimera-icon-color-picker-wyiwyg" class="demo demo-auto inl-bl colorpicker-element" data-container="true" data-color="rgb(50,216,62)" data-inline="true"></div>
                            <button id="chimera-icon-color-save-wyiwyg" type="button" class="btn btn-primary btn-xs">Save current color</button>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="thumbnail chimera-icon-saved-colors">
                            <h5 class="text-center">Saved Colors</h5>
                            <!-- ko foreach: { data: $root.colorList().ColorList, as: 'color' } -->
                                <div data-bind="attr: { title: color.HexValue }, style: { 'background-color': color.HexValue  }, click: function(data, event){ $parent.ColorHex($data) }"></div>
                            <!-- /ko -->
                            <h6 class="text-center" data-bind="visible: $root.colorList().ColorList().length == 0">No previously saved colors found.</h6>
                        </div>
                        
                    </div>
                </div>
                <div class="span12">
                    &nbsp;
                </div>
                <div class="row">
                    <div class="col-lg-12">
                            <h5>Icon Gallery</h5>
                            <h6>click on an icon below to replace your current selected icon with.</h6>
                        <ul class="chimera-available-icons" data-bind="foreach: { data: $root.iconList().IconList, as: 'icon' }">
                                <li data-bind="click: function (data, event) { $parent.Classes(icon.ClassValue()) }">
                                    <span data-bind="css: icon.ClassValue"></span>
                                    <span class="chimera-icon-class-name" data-bind="text: icon.DisplayName"></span>
                                </li>
                            </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
