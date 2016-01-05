//
//  Called to initialize the hyperlink dialog for summernote wysiwyg.
//
function LoadChimeraWYSIWYGIconDialog()
{
    viewModel.iconWYISWYGDialog = ko.observable();

    viewModel.setIconWYSIWYGDialog = function ()
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

        viewModel.iconWYISWYGDialog(new WYSIWYGIcon(textBeforeAndAfterIconIndex, selectedTextInstanceNumber, Default_Icon_Color, Default_Icon_Classes));

        $('#chimera-icon-color-picker-wyiwyg').colorpicker({
            color: Default_Icon_Color
        });

        $('#chimera-icon-color-picker-wyiwyg').colorpicker().on('changeColor', function (ev)
        {
            viewModel.iconWYISWYGDialog().ColorHex(ev.color.toHex());
        });

        $("#chimera-icon-color-save-wyiwyg").click(function ()
        {
            var colorHex = viewModel.iconWYISWYGDialog().ColorHex();

            $.post("Api/Upload/Color?colorHex=" + encodeURIComponent(colorHex), function (data)
            {
                if (data != '')
                {
                    viewModel.colorList().ColorList.unshift(ko.observable(ko.mapping.fromJSON(data)));
                }
            });
        });
    };

    viewModel.closeIconWYISWYGDialog = function (insertLinkBtnClicked)
    {
        if (insertLinkBtnClicked)
        {
            var summernoteWYSIWYG = GetEditingCurrentSummernote();

            var chimeraIconObj = viewModel.iconWYISWYGDialog();

            var iconHTML = '<span class="' + chimeraIconObj.Classes() + '" style="color: ' + chimeraIconObj.ColorHex() + ';"></span>';

            if (chimeraIconObj.IconBetweenThisText == "" && chimeraIconObj.SelectedTextInstanceNumber == -1)
            {
                summernoteWYSIWYG.code(summernoteWYSIWYG.code() + iconHTML);
            }
            else
            {
                SetSummernoteCodeWithNewStringAtPosition(chimeraIconObj.IconBetweenThisText, iconHTML, chimeraIconObj.SelectedTextInstanceNumber, true);
            }
        }

        viewModel.iconWYISWYGDialog(null);
    };
}

//
//  JS object used for editing the dialog
//
function WYSIWYGIcon(iconBetweenThisText, selectedTextInstanceNumber, colorHex, classes)
{
    this.IconBetweenThisText = iconBetweenThisText;
    this.SelectedTextInstanceNumber = selectedTextInstanceNumber;
    this.ColorHex = ko.observable(colorHex);
    this.Classes = ko.observable(classes);
}