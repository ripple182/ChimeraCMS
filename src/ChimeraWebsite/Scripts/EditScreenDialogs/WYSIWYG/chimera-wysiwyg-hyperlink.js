//
//  Called to initialize the hyperlink dialog for summernote wysiwyg.
//
function LoadChimeraWYSIWYGHyperlinkDialog()
{
    viewModel.hyperlinkDialog = ko.observable();

    viewModel.setHyperLinkDialog = function ()
    {
        var selectedText = getSelectionText();

        if (selectedText.length > 0)
        {
            var selectedTextInstanceNumber = GetInstanceNumberOfSelectedTextInsideFullString(selectedText);

            viewModel.hyperlinkDialog(new ChimeraHyperlink(selectedText, selectedTextInstanceNumber));
        }
    };

    viewModel.closeHyperlinkDialog = function (insertLinkBtnClicked)
    {
        if (insertLinkBtnClicked)
        {
            var newSelectedTextLink = null;

            var chimeraLinkObj = viewModel.hyperlinkDialog();

            if (chimeraLinkObj.PageRadioBtn() == "true" && chimeraLinkObj.PageFriendlyURL() !== undefined)
            {
                newSelectedTextLink = '<a target="' + chimeraLinkObj.LinkWindowRadioBtn() + '" href="' + websiteDefaultUrl + chimeraLinkObj.PageFriendlyURL() + '">' + chimeraLinkObj.TextToDisplay + '</a>';
            }
            else if (chimeraLinkObj.PageRadioBtn() == "false" && chimeraLinkObj.RealURL() != "")
            {
                if (chimeraLinkObj.RealURL().indexOf("http://") == -1)
                {
                    chimeraLinkObj.RealURL("http://" + chimeraLinkObj.RealURL());
                }

                newSelectedTextLink = '<a target="' + chimeraLinkObj.LinkWindowRadioBtn() + '" href="' + chimeraLinkObj.RealURL() + '">' + chimeraLinkObj.TextToDisplay + '</a>';
            }

            if (newSelectedTextLink != null)
            {
                SetSummernoteCodeWithNewStringAtPosition(chimeraLinkObj.TextToDisplay, newSelectedTextLink, chimeraLinkObj.SelectedTextInstanceNumber - 1);
            }
        }

        viewModel.hyperlinkDialog(null);
    };
}

//
//  JS object to represent the hyperlink object used in the dialog to easily edit.
//
function ChimeraHyperlink(textToDisplay, selectedTextInstanceNumber)
{
    this.TextToDisplay = textToDisplay;
    this.SelectedTextInstanceNumber = selectedTextInstanceNumber;
    this.PageRadioBtn = ko.observable("true");
    this.PageFriendlyURL = ko.observable("");
    this.RealURL = ko.observable("");
    this.LinkWindowRadioBtn = ko.observable("_self");
}