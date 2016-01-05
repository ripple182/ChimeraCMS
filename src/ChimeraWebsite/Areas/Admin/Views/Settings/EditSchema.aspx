<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Authorized.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%SettingGroup SettingGroup = ViewBag.SettingGroup;%>
    <%List<string> StaticPropertyKeyList = ViewBag.StaticPropertyKeyList; %>
    <div class="hero">
        <h3>
            <span>
                <%if (SettingGroup != null)
                  {%>
                    Edit Setting Group Schema
                <%}
                  else
                  {%>
                    Add New Setting Group Schema
                <%}%>
            </span>
        </h3>
    </div>
    <div class="form form-horizontal chimera-admin-edit-product-form">
        <div class="row">
            <div class="col-md-12">
                <div class="well">
                    <h5>Schema Details</h5>
                    <div class="form-group">
                        <label class="control-label col-md-2">Group Key</label>
                        <div class="col-md-10">
                            <input type="text" class="form-control" data-bind="value: settingGroup().SettingGroup.GroupKey" placeholder="Enter the unique key used to identify this setting group">
                            <span class="help-block">No spaces allowed</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Friendly Name</label>
                        <div class="col-md-10">
                            <input type="text" class="form-control" data-bind="value: settingGroup().SettingGroup.UserFriendlyName" placeholder="Enter user friendly identifier">
                            <span class="help-block">The name for the setting group that the client will see in order to edit values, i.e. "WEB PAGE SETTINGS", "PAYPAL SETTINGS", etc.</span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Description</label>
                        <div class="col-md-10">
                            <textarea class="form-control" data-bind="value: settingGroup().SettingGroup.Description"></textarea>
                            <span class="help-block">Description of the collection of settings that define this setting group, for example if these settings alter the header/footer info for website.</span>
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
                            <h5>Editable Settings</h5>
                            The list of editable settings the client will eventually be allowed to edit, i.e. PaypalAuthToken, PageTitle, Phone, etc.
                        </div>
                        <div class="col-md-2">
                            <button type="button" class="btn btn-success btn-sm pull-right" data-bind="click: function (data, event) { addNewSettingSchema() }">
                                <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;New
                            </button>
                        </div>
                    </div>
                    <!-- ko foreach: { data: viewModel.settingGroup().SettingGroup.SettingsList, as: 'setting' } -->
                    <div class="panel panel-default">
                        <div class="panel-heading"><b>Setting # <span data-bind="text: $index() + 1"></span></b><span class="glyphicon glyphicon-trash pull-right" data-bind="    click: function (data, event) { removeSettingFromSettingGroup($index()) }" title="Remove"></span></div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Key</label>
                                        <div class="col-md-9">
                                            <input type="text" class="form-control" data-bind="value: setting.Key" placeholder="Enter the unique key to identify this setting">
                                            <span class="help-block">No spaces allowed.</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Friendly Name</label>
                                        <div class="col-md-9">
                                            <input type="text" class="form-control" data-bind="value: setting.UserFriendlyName" placeholder="Enter the user friendly name">
                                            <span class="help-block">Friendly name for the client.</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Field Type</label>
                                        <div class="col-md-9">
                                            <select class="form-control" data-bind="value: setting.EntryType">
                                                <%foreach (var DatEntType in Enum.GetValues(typeof(DataEntryType)))
                                                  {%>
                                                <option value="<%=(Int32) DatEntType %>"><%=DatEntType%></option>
                                                <%}%>
                                            </select>
                                            <span class="help-block">Form field client will use to update value.</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" data-bind="visible: doesDataEntryTypeRequireProperties(setting.EntryType())">
                                        <label class="control-label col-md-3">Field Values</label>
                                        <div class="col-md-9">
                                            <select class="form-control" data-bind="optionsCaption: 'Choose...', options: $root.staticPropKeyList, value: setting.DataEntryStaticPropertyKey"></select>
                                            <span class="help-block">Static property values serve as field options.</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label col-md-2">Description</label>
                                        <div class="col-md-10">
                                            <textarea class="form-control" data-bind="value: setting.Description"></textarea>
                                            <span class="help-block">Description of what this setting is and what changing it does, for the client to gain a better understanding.</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="well">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <h5>Setting Attributes</h5>
                                            </div>
                                            <div class="col-md-9">
                                                <button role="button" class="btn btn-sm btn-success pull-right" data-bind="click: function (data, event) { addNewSettingAttribute(setting); }">
                                                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;New
                                                </button>
                                            </div>
                                        </div>
                                        <div class="row" data-bind="visible: setting.SettingAttributeList().length > 0">
                                            <div class="col-md-12">
                                                <table class="table chimera-admin-edit-setting-attribute-table">
                                                    <thead>
                                                        <tr>
                                                            <th>Key</th>
                                                            <th>Value</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody data-bind="foreach: { data: setting.SettingAttributeList, as: 'setAttr' }">
                                                        <tr>
                                                            <td>
                                                                <input class="form-control" type="text" data-bind="value: setAttr.Key" placeholder="Enter a Key" />
                                                            </td>
                                                            <td>
                                                                <input class="form-control" type="text" data-bind="value: setAttr.Value" placeholder="Enter a Value" />
                                                            </td>
                                                            <td>
                                                                <span class="glyphicon glyphicon-trash" title="Remove Setting Attribute" data-bind="click: function (data, event) { removeSettingAttribute(setting, $index()) }"></span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- /ko -->
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <button class="btn btn-primary btn-lg pull-right" type="button" id="viewModelSubmitButton">Save Setting Group</button>
            </div>
        </div>
    </div>
    <form id="viewModelSubmitForm" class="form-horizontal" method="post" action="<%=Url.Content("~/Admin/Settings/EditSchema_Post") %>">
        <input id="viewModelHiddenInput" name="settingGroupData" type="hidden" />
    </form>
    <script type="text/javascript">

        var settingGroupData = '{"SettingGroup":<%=HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(SettingGroup))%>}';

        viewModel = {
            settingGroup: ko.observable(ko.mapping.fromJSON(settingGroupData)),
            staticPropKeyList: ko.observableArray(<%=JsonConvert.SerializeObject(StaticPropertyKeyList)%>),
            dataTypesRequireProperties: ko.observableArray(<%=JsonConvert.SerializeObject(DataEntryTypeProperty.DataTypesRequireProperties)%>)
        };

        LoadChimeraAdminEditSettingGroupSchema();
    </script>
</asp:Content>
