<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="modal fade" data-bind="showModal: navMenuLinkEditDialog, with: navMenuLinkEditDialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" aria-hidden="true" data-bind="click: function (data, event) { $root.navMenuLinkEditDialog(null); }">&times;</button>
                <h4 class="modal-title">Edit Navigation Menu Link</h4>
            </div>
            <div class="modal-body">
                <div class="row-fluid">
                    <div class="form-group">
                        <label>Menu text</label>
                        <input type="text" class="form-control" data-bind="value: $data.Text" />
                    </div>
                    <div class="form-group">
                        <input type="radio" name="chimera-wysiwyg-link-radio" value="true" data-bind="checked: $root.navMenuLinkEditDialog_UrlTypeRadio" />
                        <label>Page</label>
                        <select class="form-control" data-bind="options: $root.pageTypeList().PageTypeList, optionsText: 'PageTitle', optionsValue: 'PageFriendlyURL', value: $data.ChimeraPageUrl, optionsCaption: ''">
                        </select>
                    </div>
                    <div class="form-group">
                        <input type="radio" name="chimera-wysiwyg-link-radio" value="false" data-bind="checked: $root.navMenuLinkEditDialog_UrlTypeRadio" />
                        <label>URL</label>
                        <input class="form-control" type="text" data-bind="value: $data.RealUrl">
                    </div>
                    <div class="form-group">
                        <label>Link Action</label>
                        <br />
                        <input type="radio" name="chimera-wysiwyg-btn-window-radio" value="_self" data-bind="checked: $data.LinkAction" />
                        Open in current window
                        <br />
                        <input type="radio" name="chimera-wysiwyg-btn-window-radio" value="_blank" data-bind="checked: $data.LinkAction" />
                        Open in new window/tab
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-bind="click: function (data, event) { $root.navMenuLinkEditDialog(null); }">Close</button>
            </div>
        </div>
    </div>
</div>
