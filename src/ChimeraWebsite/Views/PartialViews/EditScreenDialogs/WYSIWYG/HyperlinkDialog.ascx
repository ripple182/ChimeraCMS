<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: hyperlinkDialog, with: hyperlinkDialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: function (data, event) { $root.closeHyperlinkDialog(false); }">&times;</button>
                <h4 class="modal-title">Insert Link</h4>
            </div>
            <div class="modal-body">
                <div class="row-fluid">
                    <div class="form-group">
                        <label>Text to display</label>
                        <textarea class="form-control" readonly data-bind="text: $data.TextToDisplay"></textarea>
                    </div>
                    <div class="form-group">
                        <input type="radio" name="chimera-wysiwyg-link-radio" value="true" data-bind="checked: $data.PageRadioBtn" />
                        <label>Page</label>
                        <select class="form-control" data-bind="options: $root.pageTypeList().PageTypeList, optionsText: 'PageTitle', optionsValue: 'PageFriendlyURL', value: $data.PageFriendlyURL, optionsCaption: ''">
                        </select>
                    </div>
                    <div class="form-group">
                        <input type="radio" name="chimera-wysiwyg-link-radio" value="false" data-bind="checked: $data.PageRadioBtn" />
                        <label>URL</label>
                        <input class="form-control" type="text" data-bind="value: $data.RealURL">
                    </div>
                    <div class="form-group">
                        <label>Link Action</label>
                        <br />
                        <input type="radio" name="chimera-wysiwyg-btn-window-radio" value="_self" data-bind="checked: $data.LinkWindowRadioBtn" /> Open in current window
                        <br />
                        <input type="radio" name="chimera-wysiwyg-btn-window-radio" value="_blank" data-bind="checked: $data.LinkWindowRadioBtn" /> Open in new window/tab
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-bind="click: function (data, event) { $root.closeHyperlinkDialog(true); }">Insert Link</button>
            </div>
        </div>
    </div>
</div>
