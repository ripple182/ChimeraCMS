
//
//  Called whenever a shopping cart view page is loaded
//
function Chimera_ShoppingCartViewLoad()
{
    //checkout-prod-shipping-price

    $(".preview-product-img").click(function ()
    {
        $(".main-product-img").attr("src", $(this).attr("src"));

    });

    $("#checkout-shipping-method-dropdown").change(function ()
    {
        Chimera_CalculateShippingMethodPrice($(this).val());
    });

    Chimera_CalculateShippingMethodPrice($("#checkout-shipping-method-dropdown").val());

    $("#checkout-submit-button").click(function ()
    {
        document.getElementById("checkout-init-form").submit();

    });
}