
//
//  Called whenever we are previewing a module, adding a new module, or loading a page to call necessary jquery functions for slideshows & etc.
//
function Chimera_OnAddOrOnLoad()
{
    CommonChimera_OnAddOrOnLoad();

    for (var i = 0; i < $(".carousel-v1").length; i++)
    {
        $(".carousel-v1:eq(" + i + ")").attr("id", "chimeraUnifyCarouselParentv1_" + i);

        $(".carousel-v1:eq(" + i + ") .carousel-arrow .left").first().attr("href", "#chimeraUnifyCarouselParentv1_" + i);
        $(".carousel-v1:eq(" + i + ") .carousel-arrow .right").first().attr("href", "#chimeraUnifyCarouselParentv1_" + i);
    }

    for (var i = 0; i < $(".testimonials-v1").length; i++)
    {
        $(".testimonials-v1:eq(" + i + ")").attr("id", "chimeraUnifyCarouselParentv2_" + i);

        $(".testimonials-v1:eq(" + i + ") .carousel-arrow .left").first().attr("href", "#chimeraUnifyCarouselParentv2_" + i);
        $(".testimonials-v1:eq(" + i + ") .carousel-arrow .right").first().attr("href", "#chimeraUnifyCarouselParentv2_" + i);
    }

    $(".carousel-control").click(function (e)
    {
        if ($(this).attr("data-slide") == "prev")
        {
            $(this).parent().parent().carousel('prev');
        }
        else
        {
            $(this).parent().parent().carousel('next');
        }

        e.stopPropagation();
        e.preventDefault();
    });

    //for bootstrap tabs
    for (var i = 0; i < $(".nav-tabs > li > a[data-toggle='tab']").length; i++)
    {
        $(".nav-tabs > li > a[data-toggle='tab']:eq(" + i + ")").attr("href", "#chimeraTab_" + i);
        $(".tab-content > .tab-pane:eq(" + i + ")").attr("id", "chimeraTab_" + i);
    }

    for (var i = 0; i < $(".nav-pills > li > a[data-toggle='tab']").length; i++)
    {
        $(".nav-pills > li > a[data-toggle='tab']:eq(" + i + ")").attr("href", "#chimeraTab_" + i);
        $(".tab-content > .tab-pane:eq(" + i + ")").attr("id", "chimeraTab_" + i);
    }

    $(".nav-tabs > li > a[data-toggle='tab']").click(function (e)
    {
        $(this).tab('show');
        e.stopPropagation();
        e.preventDefault();
    });

    $(".nav-pills > li > a[data-toggle='tab']").click(function (e)
    {
        $(this).tab('show');
        e.stopPropagation();
        e.preventDefault();
    });

    for (var i = 0; i < $(".acc-v1").length; i++)
    {
        $(".acc-v1:eq(" + i + ")").attr("id", "chimeraAccordionParent_" + i);
    }

    //for bootstrap accordions
    for (var i = 0; i < $(".acc-v1 a.accordion-toggle").length; i++)
    {
        var parentId = $(".acc-v1 a.accordion-toggle:eq(" + i + ")").parent().parent().parent().attr("id");

        $(".acc-v1 a.accordion-toggle:eq(" + i + ")").attr("href", "#chimeraAccordion_" + i);
        $(".acc-v1 div.panel-collapse:eq(" + i + ")").attr("id", "chimeraAccordion_" + i);
        $(".acc-v1 a.accordion-toggle:eq(" + i + ")").attr("data-parent", "#" + parentId);
    }
}