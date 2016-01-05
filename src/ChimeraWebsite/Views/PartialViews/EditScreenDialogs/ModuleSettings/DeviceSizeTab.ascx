<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<table class="table table-striped chimera-visibility-table">
    <thead>
        <tr>
            <th colspan="3" class="success">
                The size of a module can range from <b>1</b> to <b>12</b>.  Below you can alter the size per device.
            </th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                <img src="<%=Url.Content("~/Images/phoneIcon.png") %>" title="Phones" alt="Phones" /></td>
            <td>Extra small devices
                    <small>Phones (< 768px)</small>
            </td>
            <td class="chimera-deviceSize-inputCol">
                <div class="input-group">
                    <input type="text" class="form-control" disabled data-bind="value: $root.getInputColumnSize('col-xs-')">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button" data-bind="click: function () { $root.changeColumnSizePerDevice('col-xs-', 1) }"><span class="glyphicon glyphicon-chevron-up"></span></button>
                        <button class="btn btn-default" type="button" data-bind="click: function (){ $root.changeColumnSizePerDevice('col-xs-', -1) } "><span class="glyphicon glyphicon-chevron-down"></span></button>
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <img src="<%=Url.Content("~/Images/tabletIcon.png") %>" title="Tablets" alt="Tablets" /></td>
            <td>Small devices
                    <small>Tablets (>= 768px)</small>
            </td>
            <td>
                <div class="input-group">
                    <input type="text" class="form-control" disabled data-bind="value: $root.getInputColumnSize('col-sm-')">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button" data-bind="click: function () { $root.changeColumnSizePerDevice('col-sm-', 1) }"><span class="glyphicon glyphicon-chevron-up"></span></button>
                        <button class="btn btn-default" type="button" data-bind="click: function () { $root.changeColumnSizePerDevice('col-sm-', -1) } "><span class="glyphicon glyphicon-chevron-down"></span></button>
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <img src="<%=Url.Content("~/Images/smallDesktopIcon.png") %>" title="Desktops" alt="Desktops" /></td>
            <td>Medium devices
                    <small>Laptops (>= 992px)</small>
            </td>
            <td>
                <div class="input-group">
                    <input type="text" class="form-control" disabled data-bind="value: $root.getInputColumnSize('col-md-')">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button" data-bind="click: function () { $root.changeColumnSizePerDevice('col-md-', 1) }"><span class="glyphicon glyphicon-chevron-up"></span></button>
                        <button class="btn btn-default" type="button" data-bind="click: function () { $root.changeColumnSizePerDevice('col-md-', -1) } "><span class="glyphicon glyphicon-chevron-down"></span></button>
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <img src="<%=Url.Content("~/Images/desktopIcon.png") %>" title="Desktops" alt="Desktops" /></td>
            <td>Large devices
                    <small>Desktops (>= 1200px)</small>
            </td>
            <td>
                <div class="input-group">
                    <input type="text" class="form-control" disabled data-bind="value: $root.getInputColumnSize('col-lg-')">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button" data-bind="click: function () { $root.changeColumnSizePerDevice('col-lg-', 1) }"><span class="glyphicon glyphicon-chevron-up"></span></button>
                        <button class="btn btn-default" type="button" data-bind="click: function () { $root.changeColumnSizePerDevice('col-lg-', -1) } "><span class="glyphicon glyphicon-chevron-down"></span></button>
                    </span>
                </div>
            </td>
        </tr>
    </tbody>
</table>
