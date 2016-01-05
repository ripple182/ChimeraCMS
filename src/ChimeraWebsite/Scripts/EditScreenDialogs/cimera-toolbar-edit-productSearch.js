//
//
//
function LoadChimeraToolbarProductSearchDialog()
{
    viewModel.productSearchFilterComponentEditing = ko.observable(null);

    viewModel.productSearchFilterComponentEditingDictionaryKeyFor = ko.observable(null);

    viewModel.productSearchFilterType = ko.observable(null);

    viewModel.productSearchClearFilterType = function ()
    {
        viewModel.productSearchSpecificProductArray([]);

        viewModel.productSearchFilteredPropertyArray([]);
    };

    viewModel.productSearchSpecificProductArray = ko.observableArray();

    viewModel.productSearchFilteredPropertyArray = ko.observableArray();

    viewModel.setProductSearchFilterDialog = function (moduleObject, childDictionaryKeyToUpdate)
    {
        if (moduleObject != null)
        {
            var url = moduleObject.ChildrenValueDictionary[childDictionaryKeyToUpdate].Value();

            var activeUrl = BASE_AJAX_PATH + "SearchProducts/Active";

            var specificUrl = BASE_AJAX_PATH + "SearchProducts/Specific";

            var filteredUrl = BASE_AJAX_PATH + "SearchProducts/Filtered";

            //get action result and set to filter type
            if (url.indexOf(activeUrl) != -1)
            {
                viewModel.productSearchFilterType("Active");
                url = url.substring(activeUrl.length);
            }
            else if (url.indexOf(specificUrl) != -1)
            {
                viewModel.productSearchFilterType("Specific");
                url = url.substring(specificUrl.length);
            }
            else if (url.indexOf(filteredUrl) != -1)
            {
                viewModel.productSearchFilterType("Filtered");
                url = url.substring(filteredUrl.length);
            }

            var firstAmpersandIndex = url.indexOf("&");

            if (firstAmpersandIndex != -1)
            {
                url = url.substring(firstAmpersandIndex + 1);

                url = url.replace(/%2C/g, ",");

                //specific query
                if (url.indexOf("ids=") != -1)
                {
                    //remove "ids="
                    url = url.substring(4);

                    viewModel.productSearchSpecificProductArray(url.split(","));
                }
                //else extract the search properties
                else
                {
                    var searchPropertyArray = url.split("&");

                    if (searchPropertyArray.length > 0)
                    {
                        $.each(viewModel.productSearchPropertyList().ProductSearchPropertyList(), function (i, mainElement)
                        {
                            $.each(searchPropertyArray, function (k, childElement)
                            {
                                if (childElement.indexOf(mainElement.Name() + "=") != -1)
                                {
                                    var propertyValues = childElement.replace(mainElement.Name() + "=", "");

                                    var propertyValuesArray = propertyValues.split(",");

                                    if (propertyValuesArray.length > 0)
                                    {
                                        $.each(propertyValuesArray, function (j, propVal)
                                        {
                                            viewModel.productSearchFilteredPropertyArray.push(mainElement.Name() + ":" + propVal);
                                        });
                                    }
                                }
                            });
                        });
                    }
                }
            }
        }

        viewModel.productSearchFilterComponentEditing(moduleObject);

        viewModel.productSearchFilterComponentEditingDictionaryKeyFor(childDictionaryKeyToUpdate);
    };

    viewModel.closeProductSearchFilterComponentEditing = function ()
    {
        viewModel.productSearchFilterComponentEditing(null);

        viewModel.productSearchFilterComponentEditingDictionaryKeyFor(null);
    };

    viewModel.saveProductSearchFilterComponentEditing = function ()
    {
        //construct the url and save to module

        var finalURL = BASE_AJAX_PATH + "SearchProducts/" + viewModel.productSearchFilterType() + "?viewType=" + viewModel.productSearchFilterComponentEditing().ModuleTypeName();

        if (viewModel.productSearchFilterType() == 'Specific')
        {
            finalURL += "&ids=" + encodeURIComponent(viewModel.productSearchSpecificProductArray().join(","));
        }
        else if (viewModel.productSearchFilterType() == 'Filtered')
        {
            var arrayOfProperties = new Array();

            $.each(viewModel.productSearchFilteredPropertyArray(), function (i, mainElement)
            {
                var keyAndValueArray = mainElement.split(':');

                var addNewProperty = true;

                $.each(arrayOfProperties, function (k, childElement)
                {
                    if (childElement.Name == keyAndValueArray[0])
                    {
                        addNewProperty = false;

                        childElement.Values.push(keyAndValueArray[1]);
                    }
                });

                if (addNewProperty)
                {
                    arrayOfProperties.push(new Property(keyAndValueArray[0], keyAndValueArray[1]));
                }
            });

            $.each(arrayOfProperties, function (k, childElement)
            {
                finalURL += "&" + childElement.Name + "=" + encodeURIComponent(childElement.Values.join(','));
            });
        }

        viewModel.productSearchFilterComponentEditing().ChildrenValueDictionary[viewModel.productSearchFilterComponentEditingDictionaryKeyFor()].Value(finalURL);

        viewModel.productSearchFilterComponentEditing(null);

        viewModel.productSearchFilterComponentEditingDictionaryKeyFor(null);

        viewModel.productSearchFilterType(null);

        viewModel.productSearchSpecificProductArray([]);

        viewModel.productSearchFilteredPropertyArray([]);

        Chimera_CheckProductListsAJAX(true);
    };
}

function Property(name, value)
{
    this.Name = name;
    this.Values = new Array();
    this.Values.push(value);
}