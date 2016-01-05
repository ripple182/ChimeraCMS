//
// Called whenever the admin edit nav menu is loaded
//
function LoadChimeraAdminEditNavigationMenu()
{
    viewModel.navMenuLinkEditDialog = ko.observable(null);

    viewModel.navMenuLinkEditDialog_UrlTypeRadio = ko.observable("true");

    viewModel.setNavMenuLinkEditDialog = function (data)
    {
        viewModel.navMenuLinkEditDialog(data);

        if (data.RealUrl() != '' && data.ChimeraPageUrl() == '')
        {
            viewModel.navMenuLinkEditDialog_UrlTypeRadio("false");
        }
        else if (data.RealUrl() == '' && data.ChimeraPageUrl() != '')
        {
            viewModel.navMenuLinkEditDialog_UrlTypeRadio("true");
        }
    };

    viewModel.navMenuLinkEditDialog_UrlTypeRadio.subscribe(function (oldValue)
    {
        if (viewModel.navMenuLinkEditDialog_UrlTypeRadio() == "true")
        {
            viewModel.navMenuLinkEditDialog().RealUrl("");
        }
        else if (viewModel.navMenuLinkEditDialog_UrlTypeRadio() == "false")
        {
            viewModel.navMenuLinkEditDialog().ChimeraPageUrl("");
        }
    });

    

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
        $("#viewModelHiddenInput").val(ko.mapping.toJSON(viewModel.navMenu().NavMenu));

        document.getElementById("viewModelSubmitForm").submit();

    });
}

//
//  Called whenver the client wants to move a navigation link up or down in the tree
//  
function moveNavLinkDirection(parentObject, objectToMove, index, direction)
{
    var ChildNavLinkArray = null;

    if (parentObject != undefined)
    {
        if (parentObject.ChildNavLinks == undefined || parentObject.ChildNavLinks == null)
        {
            ChildNavLinkArray = viewModel.navMenu().NavMenu.ChildNavLinks;
        }
        else
        {
            ChildNavLinkArray = parentObject.ChildNavLinks;
        }
    }

    var newIndex;

    if (direction === 'up')
    {
        newIndex = index - 1;

        ChildNavLinkArray.splice(index, 1);
        ChildNavLinkArray.splice(newIndex, 0, objectToMove);
    }
    else
    {
        newIndex = index + 1;

        ChildNavLinkArray.splice(index, 1);
        ChildNavLinkArray.splice(newIndex, 0, objectToMove);
    }
};

//
//  Called whenever the client wishes to remove the nav link
//
function removeNavLinkFromParent(parentObject, index)
{
    if (parentObject != undefined)
    {
        if (parentObject.ChildNavLinks == undefined || parentObject.ChildNavLinks == null)
        {
            viewModel.navMenu().NavMenu.ChildNavLinks.splice(index, 1);
        }
        else
        {
            return parentObject.ChildNavLinks.splice(index, 1);
        }
    }
}

//
//  Get the length of the parent array so we can toggle the "arrow-down" buttons.
//
function getNavLinkParentLength(parentObject)
{
    if (parentObject != undefined)
    {
        if (parentObject.ChildNavLinks == undefined || parentObject.ChildNavLinks == null)
        {
            return viewModel.navMenu().NavMenu.ChildNavLinks().length;
        }
        else
        {
            return parentObject.ChildNavLinks().length;
        }
    }
}

//
//  Add a new child link
//
function addNewNavigationMenuLink(parentObject)
{
    parentObject.ChildNavLinks.push(new NavigationMenuLink());
}

//
//  JS object to mimic c# object
//
function NavigationMenuLink()
{
    this.Text = ko.observable(NEW_MENU_OPTION_TEXT);
    this.ChimeraPageUrl = ko.observable("");
    this.RealUrl = ko.observable("");
    this.LinkAction = ko.observable("_self");
    this.ChildNavLinks = ko.observableArray();
    
}