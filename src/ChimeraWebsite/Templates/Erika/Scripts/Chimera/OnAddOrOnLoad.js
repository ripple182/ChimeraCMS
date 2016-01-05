
//
//  Called whenever we are previewing a module, adding a new module, or loading a page to call necessary jquery functions for slideshows & etc.
//
function Chimera_OnAddOrOnLoad()
{
    CommonChimera_OnAddOrOnLoad();

    $('.da-slider').cslider({
        autoplay: true,
        interval: 6000
    });

    $('.flexslider').flexslider({
        animation: "slide",
        controlNav: true,
        pauseOnHover: true,
        slideshowSpeed: 15000
    });

    $(".flexslider .flex-next").click(function (event)
    {
        event.stopPropagation();
    });

    //for bootstrap tabs
    for (var i = 0; i < $(".nav-tabs > li > a[data-toggle='tab']").length; i++)
    {
        $(".nav-tabs > li > a[data-toggle='tab']:eq(" + i + ")").attr("href", "#chimeraTab_" + i);
        $(".tab-content > .tab-pane:eq(" + i + ")").attr("id", "chimeraTab_" + i);
    }

    $(".nav-tabs > li > a[data-toggle='tab']").click(function (e)
    {
        
        $(this).tab('show');
        e.stopPropagation();
        e.preventDefault();
    });

    for (var i = 0; i < $(".accordion").length; i++)
    {
        $(".accordion:eq(" + i + ")").attr("id", "chimeraAccordionParent_" + i);
    }

    //for bootstrap accordions
    for (var i = 0; i < $(".accordion a.accordion-toggle").length; i++)
    {
        var parentId = $(".accordion a.accordion-toggle:eq(" + i + ")").parent().parent().parent().attr("id");

        $(".accordion a.accordion-toggle:eq(" + i + ")").attr("href", "#chimeraAccordion_" + i);
        $(".accordion div.accordion-body:eq(" + i + ")").attr("id", "chimeraAccordion_" + i);
        $(".accordion a.accordion-toggle:eq(" + i + ")").attr("data-parent", "#" + parentId);
    }

    //TODO: GET ACCORDION TO PLAY NICE WITHOUT EDITING
    /*$(".accordion a.accordion-toggle").click(function (e)
    {
        $(this).collapse('toggle');
        e.stopPropagation();
        e.preventDefault();
    });*/

    for (var i = 0; i < $("div.carousel").length; i++)
    {
        $("div.carousel:eq(" + i + ")").attr("id", "chimeraCarousel_" + i);

        $("div.carousel:eq(" + i + ") .carousel-control").attr("href", "#chimeraCarousel_" + i);

        $("div.carousel:eq(" + i + ") .carousel-control").first().attr("data-slide", "prev");
        $("div.carousel:eq(" + i + ") .carousel-control").first().next().attr("data-slide", "next");
    } 

    $("div.carousel .carousel-control").click(function (e)
    {
        if ($(this).attr("data-slide") == "prev")
        {
            $(this).parent().carousel('prev');
        }
        else
        {
            $(this).parent().carousel('next');
        }

        e.stopPropagation();
        e.preventDefault();
    });

    jQuery(".prettyphoto").prettyPhoto({
        overlay_gallery: false, social_tools: false
    });
}