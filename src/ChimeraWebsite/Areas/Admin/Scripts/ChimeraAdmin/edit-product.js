//
// Called whenever the admin edit product page is loaded
//
function LoadChimeraAdminEditProduct()
{
    viewModel.selectedProperty = ko.observable();
    viewModel.selectedPropertyOldValue = ko.observable();

    viewModel.isPropertySelected = function (property)
    {
        return viewModel.selectedProperty() === property;
    };

    viewModel.productImageDialog = ko.observable();

    viewModel.productEditNewPropertyName = ko.observable();

    viewModel.productEditNewPropertyDialog = ko.observable();

    viewModel.productEditNewPropertyDialogErrorMsg = ko.observable();

    viewModel.productEditNewPropertyDialog.subscribe(function (oldValue)
    {
        viewModel.productEditNewPropertyName(null);
    });

    viewModel.getExistingPropertyList = ko.computed(function ()
    {
        var key = viewModel.productEditNewPropertyDialog();

        var ReturnListOfProperties = new Array();

        if (key == SEARCH_PROPERTY_KEY)
        {
            ReturnListOfProperties = viewModel.searchPropertyArray();
        }
        else if (key == CHECKOUT_PROPERTY_KEY)
        {
            ReturnListOfProperties = viewModel.checkoutPropertyArray();
        }

        return ReturnListOfProperties;

    }, viewModel);

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

    $("#viewModelSubmitButton").click(function ()
    {
        $("#viewModelHiddenInput").val(ko.mapping.toJSON(viewModel.product().Product));

        document.getElementById("viewModelSubmitForm").submit();

    });
}

//
// Get the image source of the image we are trying to replace in the img uplaod dialog
//
function getProductImagePreviewSource()
{
    var index = viewModel.productImageDialog();

    var imageSource = "";

    if (index == -1)
    {
        imageSource = viewModel.product().Product.MainImage.ImagePath();
    }
    else
    {
        imageSource = viewModel.product().Product.AdditionalImages()[index - 1].ImagePath();
    }

    if (imageSource == null || imageSource == "")
    {
        imageSource = defaultImageSource;
    }

    return imageSource;
}

//
// Called whenever we want to open the upload image dialog
//
function openProductImageDialog(imageIndex)
{
    viewModel.productImageDialog(imageIndex);

    applyFileUploadEventListener();
}

//
// get html src attribute from image obj
//
function getGalleryImageSource(image)
{
    return image.Url() + image.FileName() + "thumb" + image.FileExtension() + "?v=" + image.ModifiedDateUTC();
};

//
//  Called when the user selects a new image to use for a product main/thumbnail
//
function setProductImageFromGallery(imageObj)
{
    var newImageSource = getGalleryImageSource(imageObj).replace('thumb', '');

    var index = viewModel.productImageDialog();

    if (index == -1)
    {
        viewModel.product().Product.MainImage.ImagePath(newImageSource);
    }
    else
    {
        viewModel.product().Product.AdditionalImages()[index - 1].ImagePath(newImageSource);
    }
}

//
//  Called whenever a user removes a checkout property, we must remove from all custom settings
//
function removeCheckoutPropertyNameFromAllCustomCheckout(propertyName)
{
    $.each(viewModel.product().Product.CheckoutPropertySettingsList(), function (i, mainElement)
    {
        var removeIndex = -1;

        $.each(mainElement.CheckoutPropertySettingKeys(), function (k, childElement)
        {
            if (childElement.Key() == propertyName)
            {
                removeIndex = k;
            }
        });

        if (removeIndex != -1)
        {
            mainElement.CheckoutPropertySettingKeys.splice(removeIndex, 1);
        }
    });
}

//
//  Called whenever a new checkout property is added to the product so we can add it to all the custom settings
//
function addNewCheckoutPropertyNameToAllCustomCheckout(propertyName)
{
    $.each(viewModel.product().Product.CheckoutPropertySettingsList(), function (i, mainElement)
    {
        mainElement.CheckoutPropertySettingKeys.push(new CheckoutPropertySettingKey(propertyName, ""));
    });
}

//
//  Called whenever the user wishes to remove a custom property setting
//
function removeCustomCheckoutPropertySetting(index)
{
    viewModel.product().Product.CheckoutPropertySettingsList.splice(index, 1);
}

//
//  Get the select options for the current checkout property name
//
function getCustomCheckoutPropDropdown(settingKeyName)
{
    var MainArray;

    $.each(viewModel.product().Product.CheckoutPropertyList(), function (i, mainElement)
    {
        if (mainElement.Name() == settingKeyName)
        {
            MainArray = mainElement.Values();

            //since using jquery loop we use return false instead of break.
            return false;
        }
    });

    return MainArray;
}

//
//  Called whenever the user wishes to add a custom rule for a specific combo of their checkout properties.
//
function addNewCustomCheckoutPropertySetting()
{
    var NewCustomCheckoutProp = new CheckoutPropertySetting();

    $.each(viewModel.product().Product.CheckoutPropertyList(), function (i, mainElement)
    {
        NewCustomCheckoutProp.CheckoutPropertySettingKeys.push(new CheckoutPropertySettingKey(mainElement.Name(), ""));
    });

    viewModel.product().Product.CheckoutPropertySettingsList.push(NewCustomCheckoutProp);
}

//
//  Function called whenever the user actually adds a new property
//
function addNewPropertyKeyToProduct(data)
{
    var key = viewModel.productEditNewPropertyDialog();

    if (key == SEARCH_PROPERTY_KEY)
    {
        viewModel.product().Product.SearchPropertyList.push(new Property(data));
    }
    else if (key == CHECKOUT_PROPERTY_KEY)
    {
        var NewProperty = new Property(data);

        viewModel.product().Product.CheckoutPropertyList.push(NewProperty);

        addNewCheckoutPropertyNameToAllCustomCheckout(data);
    }

    viewModel.productEditNewPropertyDialog(null);
}

//
//  Called whenever a user clicks on a property value to edit it
//
function selectPropertyValueToEdit(event, propValueObj)
{
    viewModel.selectedProperty(propValueObj);
    viewModel.selectedPropertyOldValue(ko.utils.unwrapObservable(propValueObj.Value()));
    $(event.target).next().children().first().focus();
    $(event.target).next().children().first().select();
}

//
//  get the property array we wish to alter
//
function getPropertyArrayToAlter(key)
{
    if (key == SEARCH_PROPERTY_KEY)
    {
        return viewModel.product().Product.SearchPropertyList();
    }
    else if (key == CHECKOUT_PROPERTY_KEY)
    {
        return viewModel.product().Product.CheckoutPropertyList();
    }

    return null;
}

//
// Remove a property (search or checkout) from the product.
//
function removePropertyAndValues(key, index)
{
    if (key == SEARCH_PROPERTY_KEY)
    {
        viewModel.product().Product.SearchPropertyList.splice(index, 1);
    }
    else if (key == CHECKOUT_PROPERTY_KEY)
    {
        var propertyKey = viewModel.product().Product.CheckoutPropertyList()[index].Name();

        removeCheckoutPropertyNameFromAllCustomCheckout(propertyKey);

        viewModel.product().Product.CheckoutPropertyList.splice(index, 1);
    }

}

//
//  Used in the add new property dialog
//
function doesProductAlreadyHaveProperty(propertyName)
{
    var doesIt = true;

    var MainArray = getPropertyArrayToAlter(viewModel.productEditNewPropertyDialog());

    $.each(MainArray, function (i, mainElement)
    {
        if (mainElement.Name() == propertyName)
        {
            doesIt = false;
        }
    });

    return doesIt;
}

//
//  Add a new random value to a property
//
function addNewPropertyValue(key, index)
{
    var MainArray = getPropertyArrayToAlter(key);

    if (key == SEARCH_PROPERTY_KEY || key == CHECKOUT_PROPERTY_KEY)
    {
        MainArray[index].Values.push(new PropertyValue());
    }
}

//
//  Called on the "ON BLUR" whenever the user clicks off of the property editing textbox
//
function finishedEditingPropertyValue(key, index)
{
    if (viewModel.selectedProperty() !== undefined && viewModel.selectedProperty() !== null)
    {
        var MainArray = getPropertyArrayToAlter(key);

        if (key == SEARCH_PROPERTY_KEY || key == CHECKOUT_PROPERTY_KEY)
        {
            $.each(MainArray, function (i, mainElement)
            {
                var duplicantCheckCount = 0;

                $.each(mainElement.Values(), function (k, childElement)
                {
                    if (childElement.Value() == null || childElement.Value() == "")
                    {
                        MainArray[i].Values.splice(k, 1);
                    }
                    else if(mainElement.Values()[index] != undefined && childElement.Value() == mainElement.Values()[index].Value())
                    {
                        duplicantCheckCount++;
                    }
                });

                if (duplicantCheckCount > 1)
                {
                    mainElement.Values()[index].Value(viewModel.selectedPropertyOldValue());
                }
                else if(mainElement.Values()[index] != undefined)
                {
                    mainElement.Values()[index].Value(mainElement.Values()[index].Value().replace(/\W/g, ''));
                }

            });
        }
    }

    viewModel.selectedProperty(null);
    viewModel.selectedPropertyOldValue(null);
}

//
//  Upload the new property to the server so it can persist into database
//
function submitNewPropertyToServer()
{
    var name = viewModel.productEditNewPropertyName();
    var key = viewModel.productEditNewPropertyDialog();

    if (name != null && name != "")
    {
        $.post(BASE_AJAX_PATH + "Api/Upload/StaticProperty?key=" + encodeURIComponent(key) + "&name=" + encodeURIComponent(name), function (data)
        {
            //returns the name to add to our observable array
            if (data != 'Property Already Exists')
            {
                if (data != '')
                {

                    if (key == SEARCH_PROPERTY_KEY)
                    {
                        viewModel.searchPropertyArray().push(data);
                    }
                    else if (key == CHECKOUT_PROPERTY_KEY)
                    {
                        viewModel.checkoutPropertyArray().push(data);
                    }

                    addNewPropertyKeyToProduct(data);
                }
            }
            else
            {
                //show error message that this property already exists
                viewModel.productEditNewPropertyName();
            }
        });
    }
}

//
//  Return the image source for the product main image or addtional images.
//
function getProductImageSource(productImage)
{
    var returnImageSource = defaultImageSource;

    if (productImage != undefined && productImage != null && productImage.ImagePath() != undefined && productImage.ImagePath() != null && productImage.ImagePath() != "")
    {
        returnImageSource = productImage.ImagePath();
    }

    return returnImageSource;
}

//
//  Javascript object to represent a property so we are able to push new ones
//
function Property(name)
{
    this.Name = ko.observable(name);
    this.Values = ko.observableArray();
    this.Values.push(new PropertyValue());
}

function PropertyValue(value)
{
    if (value == undefined)
    {
        //var rand = Math.floor((Math.random() * 10000) + 1);

        //this.Value = ko.observable("Value # " + rand);

        this.Value = ko.observable("CLICK TO CHANGE");
    }
    else
    {
        this.Value = ko.observable(value);
    }
}

//
//  Javascript object to represent a shipping method property so we are able to push new ones
//
function ShippingMethodProperty(name, price)
{
    this.Name = ko.observable(name);
    this.Price = ko.observable(price);
}

//
// JS object to represent our checkout property setting c# object
//
function CheckoutPropertySetting()
{
    this.CheckoutPropertySettingKeys = ko.observableArray();
    this.DefineCustomPrice = ko.observable(false);
    this.DefineCustomShipping = ko.observable(false);
    this.PurchaseSettings = new PurchaseSettings();
}

//
// JS object to represent our checkout property setting key c# object
//
function CheckoutPropertySettingKey(propertyKey, propertyValue)
{
    this.Key = ko.observable(propertyKey);
    this.Value = ko.observable(propertyValue);
}

//
// JS object to represent our purchase settings c# object
//
function PurchaseSettings()
{
    var MyObject = this;

    MyObject.PurchasePrice = ko.observable(0);
    MyObject.StockLevel = ko.observable(0);
    MyObject.ShippingMethodList = ko.observableArray();

    $.each(viewModel.shippingMethodPropertyArray(), function (i, mainElement)
    {
        MyObject.ShippingMethodList.push(new ShippingMethodProperty(mainElement, 0));
    });
}