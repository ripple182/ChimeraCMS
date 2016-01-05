//
// [CHIMERA_VALUE_DEFAULT_BUTTON]= btn-default|btn-md|_self|#|Button Text
// Button Color - index 0
// Button Size - index 1
// <a target=""> - index 2
// <a href=""> - index 3
// Button Text - index 4
//
function LoadChimeraToolbarEditButtonDialog()
{
    viewModel.buttonComponentEditing = ko.observable(null);

    viewModel.buttonComponentEditingDictionaryKeyFor = ko.observable(null);

    viewModel.buttonComponentEditingButtonText = ko.observable(null);

    viewModel.buttonComponentEditingColor = ko.observable(null);

    viewModel.buttonComponentEditingSize = ko.observable(null);

    viewModel.buttonComponentEditingPage = ko.observable(null);

    viewModel.buttonComponentEditingPageRadioBtn = ko.observable(null);

    viewModel.buttonComponentEditingRealURL = ko.observable(null);

    viewModel.buttonComponentEditingLinkActionRadioBtn = ko.observable(null);

    viewModel.setButtonComponentEditing = function (moduleObject, childDictionaryKeyToUpdate)
    {
        viewModel.buttonComponentEditing(moduleObject);

        viewModel.buttonComponentEditingDictionaryKeyFor(childDictionaryKeyToUpdate);

        viewModel.buttonComponentEditingColor(getButtonComponentEditingValue('', 0));

        viewModel.buttonComponentEditingSize(getButtonComponentEditingValue('', 1));

        viewModel.buttonComponentEditingLinkActionRadioBtn(getButtonComponentEditingValue('', 2));

        viewModel.buttonComponentEditingButtonText(getButtonComponentEditingValue('', 4));

        var hyperlink = getButtonComponentEditingValue('', 3);

        var indexOf = hyperlink.indexOf(websiteDefaultUrl);

        //if not -1 then it is a chimera page link
        if (indexOf !== -1)
        {
            var indexOf = indexOf + websiteDefaultUrl.length;

            pageFriendlyURL = hyperlink.substring(indexOf, hyperlink.length);

            viewModel.buttonComponentEditingPage(href.substring(indexOf));
            viewModel.buttonComponentEditingPageRadioBtn("true");
        }
        //else this is a real link
        else
        {
            viewModel.buttonComponentEditingPageRadioBtn("false");
            viewModel.buttonComponentEditingRealURL(hyperlink);
        }
    };

    viewModel.closeButtonComponentEditing = function ()
    {
        viewModel.buttonComponentEditing(null);

        viewModel.buttonComponentEditingDictionaryKeyFor(null);

        viewModel.buttonComponentEditingButtonText(null);

        viewModel.buttonComponentEditingColor(null);

        viewModel.buttonComponentEditingSize(null);

        viewModel.buttonComponentEditingPage(null);

        viewModel.buttonComponentEditingPageRadioBtn(null);

        viewModel.buttonComponentEditingRealURL(null);

        viewModel.buttonComponentEditingLinkActionRadioBtn(null);
    };

    viewModel.buttonComponentEditingColor.subscribe(function (newValue)
    {
        setButtonComponentEditingValue(newValue, 0);
    });

    viewModel.buttonComponentEditingSize.subscribe(function (newValue)
    {
        setButtonComponentEditingValue(newValue, 1);
    });

    viewModel.buttonComponentEditingLinkActionRadioBtn.subscribe(function (newValue)
    {
        setButtonComponentEditingValue(newValue, 2);
    });

    viewModel.buttonComponentEditingButtonText.subscribe(function (newValue)
    {
        setButtonComponentEditingValue(newValue, 4);
    });

    viewModel.buttonComponentEditingPage.subscribe(function (newValue)
    {
        if (viewModel.buttonComponentEditingPageRadioBtn() == "true")
        {
            setButtonComponentEditingValue(websiteDefaultUrl + newValue, 3);
        }
    });

    viewModel.buttonComponentEditingPageRadioBtn.subscribe(function (newValue)
    {
        if (newValue == "true")
        {
            setButtonComponentEditingValue(websiteDefaultUrl + viewModel.buttonComponentEditingPage(), 3);
        }
        else
        {
            setButtonComponentEditingValue(viewModel.buttonComponentEditingRealURL(), 3);
        }
    });

    viewModel.buttonComponentEditingRealURL.subscribe(function (newValue)
    {
        if (viewModel.buttonComponentEditingPageRadioBtn() == "false")
        {
            setButtonComponentEditingValue(newValue, 3);
        }
    });
}

function getButtonComponentEditingValue(childDictionaryValue, index)
{
    if (childDictionaryValue == '' && viewModel.buttonComponentEditing() != null && viewModel.buttonComponentEditingDictionaryKeyFor() != null)
    {
        childDictionaryValue = viewModel.buttonComponentEditing().ChildrenValueDictionary[viewModel.buttonComponentEditingDictionaryKeyFor()].Value();
    }

    return childDictionaryValue.split('|')[index];
}

function setButtonComponentEditingValue(newValue, index)
{
    if (viewModel.buttonComponentEditing() != null && viewModel.buttonComponentEditingDictionaryKeyFor() != null)
    {
        var childDictionaryValue = viewModel.buttonComponentEditing().ChildrenValueDictionary[viewModel.buttonComponentEditingDictionaryKeyFor()].Value();

        var splitValues = childDictionaryValue.split('|');

        splitValues[index] = newValue;
        
        viewModel.buttonComponentEditing().ChildrenValueDictionary[viewModel.buttonComponentEditingDictionaryKeyFor()].Value(splitValues.join('|'));
    }
}

//
//  Called when displaying the regular button html for any button module
//
function getModuleChildBootstrapButtonClasses(childDictionaryValue)
{
    if (childDictionaryValue == '' && viewModel.buttonComponentEditing() != null && viewModel.buttonComponentEditingDictionaryKeyFor() != null)
    {
        childDictionaryValue = viewModel.buttonComponentEditing().ChildrenValueDictionary[viewModel.buttonComponentEditingDictionaryKeyFor()].Value();
    }

    return getButtonComponentEditingValue(childDictionaryValue, 0) + " " + getButtonComponentEditingValue(childDictionaryValue, 1);
}