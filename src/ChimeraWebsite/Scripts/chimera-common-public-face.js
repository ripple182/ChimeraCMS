
var CHIMERA_PAGE_UNLOADED = false;

//
//  Called whenever we are previewing a module, adding a new module, or loading a page to call necessary jquery functions for slideshows & etc.
//
function CommonChimera_OnAddOrOnLoad()
{
    $(window).on('beforeunload', function ()
    {
        Chimera_ExitPage();
    });

    $(window).on("unload", function ()
    {
        Chimera_ExitPage();
    });

    Chimera_CheckProductListsAJAX();
}

function Chimera_ExitPage()
{
    if (!CHIMERA_PAGE_UNLOADED)
    {
        CHIMERA_PAGE_UNLOADED = true;

        $.ajax({
            type: 'POST',
            async: true,
            url: BASE_AJAX_PATH + "PageExit/Exit"
        });
    }
}

//
//  Check the page to see if any product lists need to be loaded
//
function Chimera_CheckProductListsAJAX(forceUpdate)
{
    //load product divs
    $(".chimera-product-list").each(function ()
    {
        var divElement = $(this);

        var showEmptyDiv = false;

        if ((forceUpdate != undefined && forceUpdate != null && forceUpdate == true) || divElement.html() == "" || divElement.children().first().hasClass("chimera-product-list-empty"))
        {
            if (divElement.attr("product-list-url") != "#")
            {
                divElement.html("<div class='chimera-product-list-empty'><img src='" + PRODUCTION_GLOBAL_CDN_URL + "IMAGES/ajax-loader.gif' /></div>");

                $.ajax({
                    url: divElement.attr("product-list-url"),
                    type: "get",
                    success: function (response)
                    {
                        if (response != "")
                        {
                            divElement.html(response);
                        }
                        else
                        {
                            showEmptyDiv = true;
                        }
                    },
                    error: function ()
                    {
                        showEmptyDiv = true;
                    }
                });
            }
            else
            {
                showEmptyDiv = true;
            }
        }

        if (showEmptyDiv)
        {
            divElement.html("<div class='chimera-product-list-empty'>" + No_Products_Found_Default_Message + "</div>");
        }

    });
}

//
//  Called on the shopping cart page to calculate the new grand total for a specific selected shipping method
//
function Chimera_CalculateShippingMethodPrice(shippingMethodName)
{
    var shippingMethodPrice = 0;
    var itemSubTotal = 0;
    var taxTotal = 0;

    if ($("#checkout-item-total").length > 0)
    {
        itemSubTotal = parseFloat($("#checkout-item-total").html());
    }

    if ($("#checkout-tax-total").length > 0)
    {
        taxTotal = parseFloat($("#checkout-tax-total").html());
    }

    if ($(".checkout-prod-shipping-price[shipping-property-name='" + shippingMethodName + "']").length > 0)
    {
        $(".checkout-prod-shipping-price[shipping-property-name='" + shippingMethodName + "']").each(function ()
        {
            shippingMethodPrice += parseFloat($(this).val());
        });

        $("#checkout-shipping-method-total").html(shippingMethodPrice.toFixed(2));
    }

    $("#checkout-grand-total").html((shippingMethodPrice + itemSubTotal + taxTotal).toFixed(2));
}

//
//  Called whenever the customer is on a product view page and we need to generate a new price
//
function Chimera_CalculateNewCustomPrice()
{
    if ($(".checkout-prod-setting-price").length > 0)
    {
        var ArrayOfValues = new Array();

        $(".checkout-prop-select").each(function ()
        {
            ArrayOfValues.push($(this).attr("checkout-property-name") + ":" + $(this).val());

        });

        if (ArrayOfValues.length > 0)
        {
            var newPrice = null;

            $(".checkout-prod-setting-price").each(function ()
            {
                var checkoutProdSettingId = $(this).attr("id");
                var checkoutProdSettingPrice = $(this).val();

                var valuesMatched = 0;

                $.each(ArrayOfValues, function (i, mainElement)
                {
                    if (checkoutProdSettingId.indexOf(mainElement) !== -1)
                    {
                        valuesMatched++;
                    }
                });

                if (valuesMatched == ArrayOfValues.length)
                {
                    newPrice = checkoutProdSettingPrice;

                    return false;
                }
            });

            if (newPrice != null)
            {
                $("#checkout-custom-price").html(newPrice);
            }
            else
            {
                $("#checkout-custom-price").html($("#checkout-custom-price").attr("checkout-default-price"));
            }
        }
    }
    else
    {
        $("#checkout-custom-price").html($("#checkout-custom-price").attr("checkout-default-price"));
    }
}