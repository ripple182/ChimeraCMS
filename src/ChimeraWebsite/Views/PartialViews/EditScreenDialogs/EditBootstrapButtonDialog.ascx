<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: buttonComponentEditing, with: buttonComponentEditing">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: $root.closeButtonComponentEditing">&times;</button>
                <h4 class="modal-title">Edit Button</h4>
            </div>
            <div class="modal-body">
                <div class="row-fluid">
                    <div class="form-group">
                        <label>Button Preview</label>
                        <br />
                        <a class="btn" data-bind="css: getModuleChildBootstrapButtonClasses(''), text: getButtonComponentEditingValue('', 4)"></a>
                    </div>
                    <div class="form-group">
                        <label>Button text</label>
                        <input class="form-control" type="text" data-bind="value: $root.buttonComponentEditingButtonText">
                    </div>
                    <div class="form-group">
                        <label>Color</label>
                        <select class="form-control" data-bind="options: $root.availableButtonColorClasses, value: $root.buttonComponentEditingColor">
                        </select>
                    </div>
                    <div class="form-group">
                        <label>Size</label>
                        <select class="form-control" data-bind="options: $root.availableButtonSizeClasses, value: $root.buttonComponentEditingSize">
                        </select>
                    </div>
                    <div class="form-group">
                        <input type="radio" name="chimera-wysiwyg-link-radio" value="true" data-bind="checked: $root.buttonComponentEditingPageRadioBtn" />
                        <label>Link to Page</label>
                        <select class="form-control" data-bind="options: $root.pageTypeList().PageTypeList, optionsText: 'PageTitle', optionsValue: 'PageFriendlyURL', value: $root.buttonComponentEditingPage, optionsCaption: ''">
                        </select>
                    </div>
                    <div class="form-group">
                        <input type="radio" name="chimera-wysiwyg-link-radio" value="false" data-bind="checked: $root.buttonComponentEditingPageRadioBtn" />
                        <label>Link to URL</label>
                        <input class="form-control" type="text" data-bind="value: $root.buttonComponentEditingRealURL">
                    </div>
                    <div class="form-group">
                        <label>Link Action</label>
                        <br />
                        <input type="radio" name="chimera-wysiwyg-btn-window-radio" value="_self" data-bind="checked: $root.buttonComponentEditingLinkActionRadioBtn" /> Open in current window
                        <br />
                        <input type="radio" name="chimera-wysiwyg-btn-window-radio" value="_blank" data-bind="checked: $root.buttonComponentEditingLinkActionRadioBtn" /> Open in new window/tab
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-bind="click: $root.closeButtonComponentEditing">Close</button>
            </div>
        </div>
    </div>
</div>
