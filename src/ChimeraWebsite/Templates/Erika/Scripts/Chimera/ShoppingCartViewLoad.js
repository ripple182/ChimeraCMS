
//
//  Called whenever a shopping cart view page is loaded
//
function Chimera_ShoppingCartViewLoad()
{
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