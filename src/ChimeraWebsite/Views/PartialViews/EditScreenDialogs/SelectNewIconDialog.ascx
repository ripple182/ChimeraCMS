<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: moduleSelectingNewIconFor, with: moduleSelectingNewIconFor">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: $root.closeSelectIconForModule">&times;</button>
                <h4 class="modal-title">Edit Icon</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="thumbnail chimera-current-icon-thumbnail">
                            <h5 class="text-center">Current Icon</h5>
                            <span class="text-center" data-bind="css: getModuleChildIconClass(''), style: { color: getModuleChildIconColor('') }"></span>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="thumbnail">
                            <h5 class="text-center">Current Color</h5>
                            <div id="chimera-icon-color-picker" class="demo demo-auto inl-bl colorpicker-element" data-container="true" data-color="rgb(50,216,62)" data-inline="true"></div>
                            <button id="chimera-icon-color-save" type="button" class="btn btn-primary btn-xs">Save current color</button>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="thumbnail chimera-icon-saved-colors">
                            <h5 class="text-center">Saved Colors</h5>
                            <!-- ko foreach: { data: $root.colorList().ColorList, as: 'color' } -->
                                <div data-bind="attr: { title: color.HexValue }, style: { 'background-color': color.HexValue  }, click: function(data, event){ $root.setEditingIconNewColorHex($data) }"></div>
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
                                <li data-bind="click: function (data, event) { $root.chooseNewIconForModuleChild(icon.ClassValue()) }">
                                    <span class="chimera-actual-icon" data-bind="css: icon.ClassValue"></span>
                                    <span class="chimera-icon-class-name" data-bind="text: icon.DisplayName"></span>
                                </li>
                            </ul>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-bind="click: $root.closeSelectIconForModule">Close</button>
            </div>
        </div>
    </div>
</div>
