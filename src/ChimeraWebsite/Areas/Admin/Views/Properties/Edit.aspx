<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%CEP.StaticProperty StaticProperty = ViewBag.StaticProperty;%>
    <div class="hero">
        <h3>
            <span>Edit Property "<%=StaticProperty.KeyName %>"</span>
        </h3>
        <p>
            You can only add new values to a property that already exists.
        </p>
    </div>
    <div class="form form-horizontal chimera-admin-edit-product-form">
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <h5>Details</h5>
                    <br />
                    <div class="form-group">
                        <label class="control-label col-md-2">Property Name</label>
                        <div class="col-md-10">
                            <input disabled type="text" class="form-control" data-bind="value: staticProperty().StaticProperty.KeyName">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Add Value</label>
                        <div class="col-md-10">
                            <div class="input-group">
                                <input type="text" class="form-control" data-bind="value: currentlyAddNewPropertyValue" placeholder="Enter a new value and click the + New button">
                                <span class="input-group-btn">
                                    <button role="button" class="btn btn-success" data-bind="click: function (data, event) { addNewStaticPropertyValue() }">
                                        <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;New
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Current Values</label>
                        <div class="col-md-10" data-bind="visible: staticProperty().StaticProperty.PropertyNameValues().length > 0, foreach: staticProperty().StaticProperty.PropertyNameValues">
                            <button type="button" class="chimera-edit-property-btn-group btn btn-info btn-sm" data-bind="text: $data"></button>
                        </div>
                        <div class="col-md-10" data-bind="visible: staticProperty().StaticProperty.PropertyNameValues().length == 0">No values exist.</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <button class="btn btn-primary btn-lg pull-right" type="button" id="viewModelSubmitButton">Save Updates</button>
            </div>
        </div>
    </div>
    <form id="viewModelSubmitForm" class="form-horizontal" method="post" action="<%=Url.Content("~/Admin/Properties/Edit_Post") %>">
        <input id="viewModelHiddenInput" name="staticPropertyData" type="hidden" />
    </form>
    <script type="text/javascript">

        var staticPropertyData = '{"StaticProperty":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(StaticProperty))%>}';

        viewModel = {
            staticProperty: ko.observable(ko.mapping.fromJSON(staticPropertyData))
        };

        LoadChimeraAdminEditStaticProperties();
    </script>
</asp:Content>
