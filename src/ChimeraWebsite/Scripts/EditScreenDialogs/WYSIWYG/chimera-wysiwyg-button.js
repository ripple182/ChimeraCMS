//
//  Called to initialize the hyperlink dialog for summernote wysiwyg.
//
function LoadChimeraWYSIWYGButtonDialog()
{
    viewModel.buttonWYISWYGDialog = ko.observable();

    viewModel.getButtonWYISWYGDialogPreviewClasses = function ()
    {
        if (viewModel.buttonWYISWYGDialog() != null)
        {
            return viewModel.buttonWYISWYGDialog().ButtonColorClass() + " " + viewModel.buttonWYISWYGDialog().ButtonSizeClass();
        }

        return "";
    };

    viewModel.setButtonWYSIWYGDialog = function ()
    {
        var _object = GetEditingCurrentSummernote().next().find(".note-editable").get(0);

        var innerObjectText = _object.innerText;

        var textBeforeAndAfterIconIndex = "";

        var selectedTextInstanceNumber = -1;

        if (_object.innerText.length > 0)
        {
            textBeforeAndAfterIconIndex = innerObjectText.substring(viewModel.CurrentWYSIWYGCaretPosition() - 1, viewModel.CurrentWYSIWYGCaretPosition() + 1);

            selectedTextInstanceNumber = GetInstanceNumberOfSelectedTextInsideFullString(textBeforeAndAfterIconIndex);
        }

        viewModel.buttonWYISWYGDialog(new WYSIWYGButton(textBeforeAndAfterIconIndex, selectedTextInstanceNumber));

    };

    viewModel.closeButtonWYISWYGDialog = function (insertLinkBtnClicked)
    {
        if (insertLinkBtnClicked)
        {
            var summernoteWYSIWYG = GetEditingCurrentSummernote();

            var chimeraObj = viewModel.buttonWYISWYGDialog();

            var attributeHREF = 'href="';

            if (chimeraObj.PageRadioBtn() == "true" && chimeraObj.PageFriendlyURL() !== undefined)
            {
                attributeHREF += websiteDefaultUrl + chimeraObj.PageFriendlyURL();
            }
            else if (chimeraObj.PageRadioBtn() == "false" && chimeraObj.RealURL() != "")
            {
                if (chimeraObj.RealURL().indexOf("http://") == -1)
                {
                    chimeraObj.RealURL("http://" + chimeraObj.RealURL());
                }

                attributeHREF += chimeraObj.RealURL();
            }

            attributeHREF += '"';

            var buttonHTML = '<a class="btn ' + chimeraObj.ButtonColorClass() + ' ' + chimeraObj.ButtonSizeClass() + '" ' + attributeHREF + ' target="' + chimeraObj.LinkWindowRadioBtn() + '" >' + chimeraObj.ButtonText() + '</a>';

            if (chimeraObj.BetweenThisText == "" && chimeraObj.SelectedTextInstanceNumber == -1)
            {
                summernoteWYSIWYG.code(summernoteWYSIWYG.code() + buttonHTML);
            }
            else
            {
                SetSummernoteCodeWithNewStringAtPosition(chimeraObj.BetweenThisText, buttonHTML, chimeraObj.SelectedTextInstanceNumber, true);
            }
        }

        viewModel.buttonWYISWYGDialog(null);
    };
}

//
//  JS object used for editing the dialog
//
function WYSIWYGButton(betweenThisText, selectedTextInstanceNumber)
{
    this.BetweenThisText = betweenThisText;
    this.SelectedTextInstanceNumber = selectedTextInstanceNumber;
    this.ButtonText = ko.observable("Button Text");
    this.PageRadioBtn = ko.observable("true");
    this.LinkWindowRadioBtn = ko.observable("_self");
    this.PageFriendlyURL = ko.observable("");
    this.RealURL = ko.observable("");
    this.ButtonColorClass = ko.observable("btn-default");
    this.ButtonSizeClass = ko.observable("btn-md");
}