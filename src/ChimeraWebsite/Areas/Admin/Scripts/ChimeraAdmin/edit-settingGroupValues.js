//
// Called whenever the admin edit setting group values page is loaded
//
function LoadChimeraAdminEditSettingGroupValues()
{
    viewModel.settingValueImageDialog = ko.observable(null);
    viewModel.settingValueImageDialogSrc = ko.observable(null);

    ko.bindingHandlers.showModal = {
        init: function (element, valueAccessor)
        {
        },
        update: function (element, valueAccessor)
        {
            var value = valueAccessor();
            if (ko.utils.unwrapObservable(value))
            {
                $(element).modal('show');
                // this is to focus input field inside dialog
                $("input", element).focus();
            }
            else
            {
                $(element).modal('hide');
            }
        }
    };

    ko.applyBindings(viewModel);
}

function openSettingImageDialog(id)
{
    viewModel.settingValueImageDialog(id);
    viewModel.settingValueImageDialogSrc($("#" + viewModel.settingValueImageDialog()).val());

    if (viewModel.settingValueImageDialogSrc() == "")
    {
        viewModel.settingValueImageDialogSrc(defaultImageSource);
    }

    applyFileUploadEventListener();
}

//
//  Set the setting value to the selected gallery image
//
function setSettingImageValueFromGallery(imageObj)
{
    var newImageSource = getGalleryImageSource(imageObj).replace('thumb', '');

    viewModel.settingValueImageDialogSrc(newImageSource);

    $("#" + viewModel.settingValueImageDialog()).val(newImageSource);
    $("#" + viewModel.settingValueImageDialog()).next().attr("src", newImageSource);
}