//
// Called whenever the admin is edting static properties
//
function LoadChimeraAdminEditStaticProperties()
{
    viewModel.currentlyAddNewPropertyValue = ko.observable(null);


    ko.applyBindings(viewModel);

    $("#viewModelSubmitButton").click(function ()
    {
        $("#viewModelHiddenInput").val(ko.mapping.toJSON(viewModel.staticProperty().StaticProperty));

        document.getElementById("viewModelSubmitForm").submit();

    });
}

//
//  Add a new static property value
//
function addNewStaticPropertyValue()
{
    if (viewModel.currentlyAddNewPropertyValue() != null)
    {
        viewModel.staticProperty().StaticProperty.PropertyNameValues.push(viewModel.currentlyAddNewPropertyValue());

        viewModel.currentlyAddNewPropertyValue(null);
    }
}

//
//  Remove a static property value
//
function removeStaticPropertyValue(index)
{
    viewModel.staticProperty().StaticProperty.PropertyNameValues.splice(index, 1);
}