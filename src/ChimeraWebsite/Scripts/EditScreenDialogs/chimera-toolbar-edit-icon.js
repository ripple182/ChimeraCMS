//
//
//
function LoadChimeraToolbarEditIconDialog()
{
    

    viewModel.moduleSelectingNewIconFor = ko.observable(null);

    viewModel.moduleSelectingNewIconDictionaryKeyFor = ko.observable(null);

    viewModel.selectIconForModule = function (moduleObject, childDictionaryKeyToUpdate)
    {
        viewModel.moduleSelectingNewIconFor(moduleObject);

        viewModel.moduleSelectingNewIconDictionaryKeyFor(childDictionaryKeyToUpdate);

        $('#chimera-icon-color-picker').colorpicker({
            color: getModuleChildIconColor('')
        });

        $('#chimera-icon-color-picker').colorpicker().on('changeColor', function (ev)
        {
            setModuleIconComponentColorValue(ev.color.toHex());
        });

        $("#chimera-icon-color-save").click(function ()
        {
            var colorHex = getModuleChildIconColor('');

            $.post(BASE_AJAX_PATH + "Api/Upload/Color?colorHex=" + encodeURIComponent(colorHex), function (data)
            {
                if (data != '')
                {
                    viewModel.colorList().ColorList.unshift(ko.observable(ko.mapping.fromJSON(data)));
                }
            });
        });
    };

    viewModel.setEditingIconNewColorHex = function (colorHexObject)
    {
        $('#chimera-icon-color-picker').colorpicker('setValue', colorHexObject.HexValue());

        //setModuleIconComponentColorValue(colorHexObject.HexValue());
    };

    viewModel.closeSelectIconForModule = function ()
    {
        viewModel.moduleSelectingNewIconFor(null);
        viewModel.moduleSelectingNewIconDictionaryKeyFor(null);

        $('#chimera-icon-color-picker').colorpicker('destroy');
    };

    viewModel.chooseNewIconForModuleChild = function (newClassName)
    {
        var fullValue = newClassName + "|" + getModuleChildIconColor(viewModel.moduleSelectingNewIconFor().ChildrenValueDictionary[viewModel.moduleSelectingNewIconDictionaryKeyFor()].Value());

        viewModel.moduleSelectingNewIconFor().ChildrenValueDictionary[viewModel.moduleSelectingNewIconDictionaryKeyFor()].Value(fullValue);
    };
}

//
// The value of a module component when of type ICON is "class|color", this method returns the class.
//
function getModuleChildIconClass(childDictionaryValue)
{
    childDictionaryValue = getRealModuleChildIconValue(childDictionaryValue);

    return childDictionaryValue.split('|')[0];
};

//
// The value of a module component when of type ICON is "class|color", this method returns the color.
//
function getModuleChildIconColor(childDictionaryValue)
{
    childDictionaryValue = getRealModuleChildIconValue(childDictionaryValue);

    return childDictionaryValue.split('|')[1];
};

function getRealModuleChildIconValue(childDictionaryValue)
{
    if (childDictionaryValue == '' && viewModel.moduleSelectingNewIconFor() != null && viewModel.moduleSelectingNewIconDictionaryKeyFor() != null)
    {
        childDictionaryValue = viewModel.moduleSelectingNewIconFor().ChildrenValueDictionary[viewModel.moduleSelectingNewIconDictionaryKeyFor()].Value();
    }

    return childDictionaryValue;
}

function setModuleIconComponentColorValue(newColorValue)
{
    var fullValue = getModuleChildIconClass(viewModel.moduleSelectingNewIconFor().ChildrenValueDictionary[viewModel.moduleSelectingNewIconDictionaryKeyFor()].Value()) + "|" + newColorValue;

    viewModel.moduleSelectingNewIconFor().ChildrenValueDictionary[viewModel.moduleSelectingNewIconDictionaryKeyFor()].Value(fullValue);
}