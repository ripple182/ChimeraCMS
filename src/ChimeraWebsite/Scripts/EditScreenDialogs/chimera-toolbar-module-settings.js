//
//
//
function LoadChimeraToolbarModuleSettingsDialog()
{
    viewModel.rowModuleSettingsDialog = ko.observable();

    viewModel.closeRowModuleSettingsDialog = function (item)
    {
        viewModel.rowModuleSettingsDialog(null);
    };

    viewModel.moduleSettingsDialog = ko.observable();

    viewModel.closeModuleSettingsDialog = function (item)
    {
        if (viewModel.moduleSettingsDialog() != null && viewModel.areAllDevicesSetToHidden())
        {
            viewModel.removeColumnModule(viewModel.moduleSettingsDialog());
        }

        viewModel.moduleSettingsDialog(null);
    };

    viewModel.getVisibilityButtonClass = function (className)
    {
        var item = getRowOrColumnModuleForVisibilityEditing();

        if (item !== undefined && item !== null)
        {
            return item.indexOf('visible-' + className) !== -1 ? 'btn-success' : 'btn-default';
        }

        return 'btn-default';
    };

    viewModel.getHiddenButtonClass = function (className)
    {
        var item = getRowOrColumnModuleForVisibilityEditing();

        if (item !== undefined && item !== null)
        {
            return item.indexOf('hidden-' + className) !== -1 ? 'btn-danger' : 'btn-default';
        }

        return 'btn-default';
    };

    viewModel.addClass = function (className, visibleType)
    {
        var item = getRowOrColumnModuleForVisibilityEditing();

        if (item !== undefined && item !== null)
        {
            if (visibleType)
            {
                if (item.indexOf('hidden-' + className) !== -1)
                {
                    item = item.replace('hidden-' + className, 'visible-' + className);
                }
                else
                {
                    item = item + ' visible-' + className;
                }
            }
            else
            {
                if (item.indexOf('visible-' + className) !== -1)
                {
                    item = item.replace('visible-' + className, 'hidden-' + className);
                }
                else
                {
                    item = item + ' hidden-' + className;
                }
            }

            if (viewModel.moduleSettingsDialog() != null)
            {
                viewModel.moduleSettingsDialog().AdditionalClassNames(item);
            }
            else if (viewModel.rowModuleSettingsDialog() != null)
            {
                viewModel.rowModuleSettingsDialog().AdditionalClassNames(item);
            }
        }
    };

    viewModel.areAllDevicesSetToHidden = function ()
    {
        var moduleClassNames = viewModel.moduleSettingsDialog().AdditionalClassNames();

        if (moduleClassNames.indexOf('hidden-xs') !== -1 && moduleClassNames.indexOf('hidden-sm') !== -1 && moduleClassNames.indexOf('hidden-md') !== -1 && moduleClassNames.indexOf('hidden-lg') !== -1)
        {
            return true;
        }

        return false;
    };

    viewModel.changeColumnSizePerDevice = function (bootstrapDevicePrefix, positiveOrNegativeOne)
    {
        var number = getClassSizeInt(getFullBootClassName(viewModel.moduleSettingsDialog().AdditionalClassNames(), bootstrapDevicePrefix));

        var shouldContinue = true;

        if (number == 12 && positiveOrNegativeOne == 1)
        {
            shouldContinue = false;   
        }

        if (number == 1 && positiveOrNegativeOne == -1)
        {
            shouldContinue = false;
        }

        if (shouldContinue)
        {
            var newNumber = number + parseInt(positiveOrNegativeOne);

            viewModel.moduleSettingsDialog().AdditionalClassNames(viewModel.moduleSettingsDialog().AdditionalClassNames().replace(bootstrapDevicePrefix + number, bootstrapDevicePrefix + newNumber));
        }
    };

    viewModel.getInputColumnSize = function (bootstrapDevicePrefix)
    {
        return getClassSizeInt(getFullBootClassName(viewModel.moduleSettingsDialog().AdditionalClassNames(), bootstrapDevicePrefix));
    };
}

//
// When editing visibility of a module we reuse same code for row or columns
//
function getRowOrColumnModuleForVisibilityEditing()
{
    var item;

    if (viewModel.moduleSettingsDialog() != null)
    {
        item = viewModel.moduleSettingsDialog().AdditionalClassNames();
    }
    else if (viewModel.rowModuleSettingsDialog() != null)
    {
        item = viewModel.rowModuleSettingsDialog().AdditionalClassNames();
    }

    return item;
}

//
//
//
function getFullBootClassName(fullClassString, bootstrapDevicePrefix)
{
    fullClassString = fullClassString + " ";

    var index = fullClassString.indexOf(bootstrapDevicePrefix);

    return fullClassString.substring(index, index + 9);
}

//
//
//
function getClassSizeInt(fullBootClassName)
{
    var charOne = fullBootClassName.substr(-2, 1);
    var charTwo = fullBootClassName.substr(-1, 1);

    var number = parseInt(charOne);

    if (charTwo != ' ')
    {
        number = parseInt(charOne + charTwo);
    }

    return number;
}