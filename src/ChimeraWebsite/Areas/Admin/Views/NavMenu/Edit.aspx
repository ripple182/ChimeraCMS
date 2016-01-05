<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%NavigationMenu NavMenu = ViewBag.NavigationMenu;%>
    <%List<PageType> PageTypeList = ViewBag.PageTypeList; %>
    <div class="hero">
        <h3>
            <span>
                <%if (!string.IsNullOrWhiteSpace(NavMenu.Id))
                  {%>
                    Edit Navigation Menu <span class="color"><%=NavMenu.UserFriendlyName %></span>
                <%}
                  else
                  {%>
                    Add New Navigation Menu
                <%}%>
            </span>
        </h3>
    </div>
    <div class="form form-horizontal chimera-admin-edit-product-form">
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <h5>Details</h5>
                    <div class="form-group">
                        <label class="control-label col-md-2">Key Name</label>
                        <div class="col-md-10">
                            <input type="text" class="form-control" data-bind="enable: navMenu().NavMenu.Id() == '', value: navMenu().NavMenu.KeyName" placeholder="Enter the unique key name">
                            <span class="help-block" data-bind="visible: navMenu().NavMenu.Id() == ''">No spaces allowed</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">User Friendly Name</label>
                        <div class="col-md-10">
                            <input type="text" class="form-control" data-bind="value: navMenu().NavMenu.UserFriendlyName" placeholder="Enter a name that is friendly to the client">
                            <span class="help-block">Required</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <div class="form-group">
                        <div class="col-md-10">
                            <h5>Navigation Links</h5>
                            Click on the new button to add a new "Main" link, otherwise hover over the desired link to show the edit icons.
                        </div>
                        <div class="col-md-2">
                            <button type="button" class="btn btn-success btn-sm pull-right" data-bind="click: function (data, event) { addNewNavigationMenuLink(navMenu().NavMenu) }">
                                <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;New
                            </button>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <ul class="chimera-tree" data-bind="template: { name: 'NavigationMenuLinkTemplate', foreach: navMenu().NavMenu.ChildNavLinks }"></ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <button class="btn btn-primary btn-lg pull-right" type="button" id="viewModelSubmitButton">Save Navigation Menu</button>
            </div>
        </div>
    </div>
    <form id="viewModelSubmitForm" class="form-horizontal" method="post" action="<%=Url.Content("~/Admin/NavMenu/Edit_Post") %>">
        <input id="viewModelHiddenInput" name="navigationMenuData" type="hidden" />
    </form>
    <%Html.RenderPartial(Url.Content("~/Areas/Admin/Views/Shared/PartialViews/EditNavigationMenuLinkDialog.ascx")); %>
    <script type="text/html" id="NavigationMenuLinkTemplate">
        <li>
            <span data-bind="text: $data.Text, css: { 'chimera-new-menu-option': $data.Text() === NEW_MENU_OPTION_TEXT }"></span>
            &nbsp;
            <span class="glyphicon glyphicon-arrow-up" title="Move link up" data-bind="visible: $index() != 0, click: function (data, event) { moveNavLinkDirection($parent, $data, $index(), 'up'); }"></span>
            <span class="glyphicon glyphicon-arrow-up chimera-disabled-arrow" data-bind="visible: $index() == 0"></span>
            <span class="glyphicon glyphicon-arrow-down" title="Move link down" data-bind="visible: $index() != getNavLinkParentLength($parent) - 1, click: function (data, event) { moveNavLinkDirection($parent, $data, $index(), 'down'); }"></span>
            <span class="glyphicon glyphicon-arrow-down chimera-disabled-arrow" data-bind="visible: $index() == getNavLinkParentLength($parent) - 1"></span>
            <span class="glyphicon glyphicon-th-large" title="Edit link properties" data-bind="click: function (data, event) { $root.setNavMenuLinkEditDialog($data) }"></span>
            <span class="glyphicon glyphicon-plus" title="Add a child link under this link" data-bind="click: function (data, event) { addNewNavigationMenuLink($data) }"></span>
            <span class="glyphicon glyphicon-trash" title="Remove link and all children" data-bind="click: function (data, event) { removeNavLinkFromParent($parent, $index()); }"></span>
            <ul data-bind="template: { name: 'NavigationMenuLinkTemplate', foreach: $data.ChildNavLinks }"></ul>
        </li>
    </script>
    <script type="text/javascript">

        var navMenuData = '{"NavMenu":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(NavMenu))%>}';
        var pageTypeData = '{"PageTypeList":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(PageTypeList))%>}';    

        var NEW_MENU_OPTION_TEXT = "New Menu Option";

        viewModel = {
            navMenu: ko.observable(ko.mapping.fromJSON(navMenuData)),
            pageTypeList: ko.observable(ko.mapping.fromJSON(pageTypeData))
        };

        LoadChimeraAdminEditNavigationMenu();
    </script>
</asp:Content>
