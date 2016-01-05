<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<table class="table table-striped chimera-visibility-table">
    <tbody>
        <tr>
            <td>
                <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES/phoneIcon.png"%>" title="Phones" alt="Phones" /></td>
            <td>Extra small devices
                    <small>Phones (< 768px)</small>
            </td>
            <td>
                <button type="button" class="btn" data-bind="css: $root.getVisibilityButtonClass('xs'), click: function () { $root.addClass('xs', true) }">visible</button>
                <button type="button" class="btn" data-bind="css: $root.getHiddenButtonClass('xs'), click: function () { $root.addClass('xs', false) }">hidden</button>
            </td>
        </tr>
        <tr>
            <td>
                <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES/tabletIcon.png" %>" title="Tablets" alt="Tablets" /></td>
            <td>Small devices
                    <small>Tablets (>= 768px)</small>
            </td>
            <td>
                <button type="button" class="btn" data-bind="css: $root.getVisibilityButtonClass('sm'), click: function () { $root.addClass('sm', true) }">visible</button>
                <button type="button" class="btn" data-bind="css: $root.getHiddenButtonClass('sm'), click: function () { $root.addClass('sm', false) }">hidden</button>
            </td>
        </tr>
        <tr>
            <td>
                <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES/smallDesktopIcon.png" %>" title="Desktops" alt="Desktops" /></td>
            <td>Medium devices
                    <small>Laptops (>= 992px)</small>
            </td>
            <td>
                <button type="button" class="btn" data-bind="css: $root.getVisibilityButtonClass('md'), click: function () { $root.addClass('md', true) }">visible</button>
                <button type="button" class="btn" data-bind="css: $root.getHiddenButtonClass('md'), click: function () { $root.addClass('md', false) }">hidden</button>
            </td>
        </tr>
        <tr>
            <td>
                <img src="<%=AppSettings.PRODUCTION_GLOBAL_CDN_URL + "IMAGES/desktopIcon.png" %>" title="Desktops" alt="Desktops" /></td>
            <td>Large devices
                    <small>Desktops (>= 1200px)</small>
            </td>
            <td>
                <button type="button" class="btn" data-bind="css: $root.getVisibilityButtonClass('lg'), click: function () { $root.addClass('lg', true) }">visible</button>
                <button type="button" class="btn" data-bind="css: $root.getHiddenButtonClass('lg'), click: function () { $root.addClass('lg', false) }">hidden</button>
            </td>
        </tr>
    </tbody>
</table>
