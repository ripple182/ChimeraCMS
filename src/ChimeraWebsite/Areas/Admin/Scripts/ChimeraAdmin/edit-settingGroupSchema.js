//
// Called whenever the admin edit setting group schema page is loaded
//
function LoadChimeraAdminEditSettingGroupSchema()
{
    ko.applyBindings(viewModel);

    $("#viewModelSubmitButton").click(function ()
    {
        $("#viewModelHiddenInput").val(ko.mapping.toJSON(viewModel.settingGroup().SettingGroup));

        document.getElementById("viewModelSubmitForm").submit();

    });
}

//
//  Remove a setting attribute from a setting
//
function removeSettingAttribute(setting, index)
{
    setting.SettingAttributeList.splice(index, 1);
}

//
//  Add a new setting attribute to a setting
//
function addNewSettingAttribute(setting)
{
    setting.SettingAttributeList.push(new SettingAttribute());
}

//
// Find out if the setting requires a property type dropdown
//
function doesDataEntryTypeRequireProperties(entryType)
{
    return (viewModel.dataTypesRequireProperties.indexOf(parseInt(entryType)) != -1);
}


//
//  Called when the user wishes to add a new setting to the setting group
//
function addNewSettingSchema()
{
    viewModel.settingGroup().SettingGroup.SettingsList.push(new Setting());
}

//
//  remove a setting from the setting group
//
function removeSettingFromSettingGroup(index)
{
    viewModel.settingGroup().SettingGroup.SettingsList.splice(index, 1);
}

//
//  Javascript object to represent a setting c# object
//
function Setting()
{
    this.Key = ko.observable("");
    this.UserFriendlyName = ko.observable("");
    this.Description = ko.observable("");
    this.Value = ko.observable("");
    this.EntryType = ko.observable(1);
    this.DataEntryStaticPropertyKey = ko.observable("");
    this.SettingAttributeList = ko.observableArray();
}

//
//  Javascript object to represent a setting c# object
//
function SettingAttribute()
{
    this.Key = ko.observable("");
    this.Value = ko.observable("");
}