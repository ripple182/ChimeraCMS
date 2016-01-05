//
//
//
function LoadChimeraToolbarEditImageDialog()
{
    viewModel.moduleUploadingNewImageFor = ko.observable(null);

    viewModel.moduleUploadingNewImageDictionaryKeyFor = ko.observable(null);

    viewModel.uploadImageForModule = function (moduleObject, childDictionaryKeyToUpdate)
    {
        viewModel.moduleUploadingNewImageFor(moduleObject);

        viewModel.moduleUploadingNewImageDictionaryKeyFor(childDictionaryKeyToUpdate);

        applyFileUploadEventListener();
    };

    viewModel.closeUploadModuleImage = function ()
    {
        viewModel.moduleUploadingNewImageFor(null);
        viewModel.moduleUploadingNewImageDictionaryKeyFor(null);
    };

    viewModel.getModuleUploadingImageSource = function ()
    {
        if (viewModel.moduleUploadingNewImageFor() != null && viewModel.moduleUploadingNewImageFor() != undefined && viewModel.moduleUploadingNewImageDictionaryKeyFor() != null && viewModel.moduleUploadingNewImageDictionaryKeyFor() != undefined)
        {
            var imageSource = viewModel.moduleUploadingNewImageFor().ChildrenValueDictionary[viewModel.moduleUploadingNewImageDictionaryKeyFor()].Value();

            if (imageSource !== undefined && '' != imageSource)
            {

                return imageSource;
            }
        }

        return defaultImageSource;
    };

    viewModel.getGalleryImageSource = function (image)
    {
        return image.Url() + image.FileName() + "thumb" + image.FileExtension() + "?v=" + image.ModifiedDateUTC();
    };

    viewModel.setModuleUploadImageSourceFromGalleryImage = function (image)
    {
        var newImageSource = viewModel.getGalleryImageSource(image).replace('thumb','');

        viewModel.moduleUploadingNewImageFor().ChildrenValueDictionary[viewModel.moduleUploadingNewImageDictionaryKeyFor()].Value(newImageSource);
    };
}