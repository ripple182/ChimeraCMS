
//
//  Called whenever a product view page is loaded
//
function Chimera_ProductViewLoad()
{
    $(".preview-product-img").click(function ()
    {
        $(".main-product-img").attr("src", $(this).attr("src"));

    });

    $(".checkout-prop-select").change(function ()
    {
        Chimera_CalculateNewCustomPrice();
    });

    Chimera_CalculateNewCustomPrice();
}