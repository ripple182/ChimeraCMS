//
// Gets the instance number of a string inside the current editing summernote wysiwyg, "ab ab [cursor here]ab" would return 2
//
function GetInstanceNumberOfSelectedTextInsideFullString(selectedText)
{
    var _object = GetEditingCurrentSummernote().next().find(".note-editable").get(0);

    var positionsOfSelectedTextArray = getTextpositions(selectedText, _object.innerText);

    var selectedTextInstanceNumber = 0;

    var actualPositionOfSelectedTextStart = getCaretCharacterOffsetWithin(_object) - selectedText.length;

    for (var i = 0; 0 < positionsOfSelectedTextArray.length; i++)
    {
        if (positionsOfSelectedTextArray[i] <= actualPositionOfSelectedTextStart)
        {
            selectedTextInstanceNumber++;
        }
        else
        {
            break;
        }
    }

    return selectedTextInstanceNumber;
}

function SetSummernoteCodeWithNewStringAtPosition(OriginalText, NewHTMLToReplaceText, TextInstanceNumber, InsertingNewHTMLBetweenCharacters)
{
    var summernoteWYSIWYG = GetEditingCurrentSummernote();

    var positionsOfSelectedTextArray = getTextpositions(OriginalText, summernoteWYSIWYG.code());

    var ourTextIndex = positionsOfSelectedTextArray[TextInstanceNumber];

    if (ourTextIndex != undefined)
    {
        //split the string into two pieces excluding our selected text
        var finalCodeLeftSide = summernoteWYSIWYG.code().substring(0, ourTextIndex + 1);

        var finalCodeRightSide = summernoteWYSIWYG.code().substring(ourTextIndex + 1, summernoteWYSIWYG.code().length);

        if (InsertingNewHTMLBetweenCharacters != undefined && InsertingNewHTMLBetweenCharacters != null)
        {
            //split the string into two pieces excluding our selected text
            finalCodeLeftSide = summernoteWYSIWYG.code().substring(0, ourTextIndex + 1);

            finalCodeRightSide = summernoteWYSIWYG.code().substring(ourTextIndex + 1, summernoteWYSIWYG.code().length);
        }
        else
        {
            //split the string into two pieces excluding our selected text
            finalCodeLeftSide = summernoteWYSIWYG.code().substring(0, ourTextIndex);

            finalCodeRightSide = summernoteWYSIWYG.code().substring(ourTextIndex + OriginalText.length, summernoteWYSIWYG.code().length);
        }
        

        summernoteWYSIWYG.code(finalCodeLeftSide + NewHTMLToReplaceText + finalCodeRightSide);
    }
}

//
//  Get the text the user is currently selecting with their mouse
//
function getSelectionText()
{
    var text = "";
    if (window.getSelection)
    {
        text = window.getSelection().toString();
    } else if (document.selection && document.selection.type != "Control")
    {
        text = document.selection.createRange().text;
    }
    return text;
}

//
//  Get the jquery object of whichever summernote wysiwyg we are currently editing
//
function GetEditingCurrentSummernote()
{
    var summernoteWYSIWYG = null;

    $(".summernote-wysiwyg").each(function ()
    {
        if ($(this).parent().css("display") != "none")
        {
            summernoteWYSIWYG = $(this);
        }
    });

    return summernoteWYSIWYG;
}

//
// get the index of where the beginning of the text selection begins
//
function getCaretCharacterOffsetWithin(element)
{
    var caretOffset = 0;
    var doc = element.ownerDocument || element.document;
    var win = doc.defaultView || doc.parentWindow;
    var sel;
    if (typeof win.getSelection != "undefined")
    {
        var range = win.getSelection().getRangeAt(0);
        var preCaretRange = range.cloneRange();
        preCaretRange.selectNodeContents(element);
        preCaretRange.setEnd(range.endContainer, range.endOffset);
        caretOffset = preCaretRange.toString().length;
    } else if ((sel = doc.selection) && sel.type != "Control")
    {
        var textRange = sel.createRange();
        var preCaretTextRange = doc.body.createTextRange();
        preCaretTextRange.moveToElementText(element);
        preCaretTextRange.setEndPoint("EndToEnd", textRange);
        caretOffset = preCaretTextRange.text.length;
    }
    return caretOffset;
}

//
// Returns an array of all the possible indexs of the passed in string, "str" is string we are looking for inside "text".
//
function getTextpositions(str, text)
{
    var pos = [], regex = new RegExp("(.*?)" + str, "g"), prev = 0;
    text.replace(regex, function (_, s)
    {
        var p = s.length + prev;
        pos.push(p);
        prev = p + str.length;
    });
    return pos;
}