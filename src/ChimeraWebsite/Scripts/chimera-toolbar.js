/*
*
*   Chimera Toolbar javascript functions, written by Chris Ripple.  www.chrisripple.com.
*
*/

var columnModuleBorderClass = 'chimeraEditableColumnModuleBorderColor';
var rowModuleBorderClass = 'chimeraEditableColumnParentBorderColor';

var LG_MD_ColumnSizeAndClassNameArray = new Array();
var SM_XS_ColumnSizeAndClassNameArray = new Array();

var CanEditAnyModule = true;

//
//  Function called to create the knockout view model for a chimera editable page.
//
function LoadChimeraEditablePage()
{
    initializeColumnSizeAndClassNames();

    LoadChimeraToolbarModuleSettingsDialog();

    LoadChimeraToolbarEditImageDialog();

    LoadChimeraToolbarEditIconDialog();

    LoadChimeraWYSIWYGHyperlinkDialog();

    LoadChimeraWYSIWYGIconDialog();

    LoadChimeraWYSIWYGButtonDialog();

    LoadChimeraToolbarEditButtonDialog();

    LoadChimeraToolbarProductSearchDialog();

    viewModel.getRowIconToolbarRightStyle = function (row)
    {
        return Default_LG_MD_Column_Width * 12 + "px";
    };

    viewModel.CurrentWYSIWYGCaretPosition = ko.observable();

    viewModel.specialCategory = ko.observable();

    viewModel.specialCategory.subscribe(function (oldValue)
    {
        if (viewModel.specialCategory() != null)
        {
            viewModel.typeToShow(null);
            viewModel.moduleCurrentlyViewing(null);
        }
    });

    viewModel.addNewEmptyRow = function ()
    {
        var firstColumnModule = viewModel.columnModuleTypes().ColumnModuleTypes()[0].ColumnModuleModel.ColumnModule;

        var newRowModule = new RowModule(null);

        //add the new row & column to the beginning of the document
        viewModel.page().PageModel.Page.RowModuleList.unshift(newRowModule);

    };

    viewModel.getPreviewModuleDeviceVisibility = function (bootstrapDevice)
    {
        if (viewModel.moduleCurrentlyViewing() != null)
        {
            var moduleClassNames = viewModel.moduleCurrentlyViewing().ColumnModuleModel.ColumnModule.AdditionalClassNames();

            if (moduleClassNames.indexOf('visible-' + bootstrapDevice) !== -1)
            {
                return "glyphicon-ok";
            }
            else
            {
                return "glyphicon-remove";
            }
        }

        return "";
    };

    viewModel.getPreviewModuleDeviceSize = function (bootstrapDevice)
    {
        if (viewModel.moduleCurrentlyViewing() != null)
        {
            var moduleClassNames = viewModel.moduleCurrentlyViewing().ColumnModuleModel.ColumnModule.AdditionalClassNames();

            return getClassSizeInt(getFullBootClassName(moduleClassNames, "col-" + bootstrapDevice + "-"));
        }

        return "";
    };

    viewModel.getAdditionalRowModuleCSS = function (item)
    {
        var fullClassAttribute = item.AdditionalClassNames();

        if(item.ColumnModuleList().length == 0)
        {
            fullClassAttribute += " chimera-empty-row"; 
        }

        if(viewModel.currentlyHoveringOverRow(item))
        {
            fullClassAttribute += " chimeraEditableColumnParentBorderColor";
        }

        return fullClassAttribute;
    };

    viewModel.typeToShow = ko.observable();

    viewModel.typeToShow.subscribe(function (oldValue)
    {
        if (viewModel.typeToShow() != null)
        {
            viewModel.specialCategory(null);
        }
    });

    viewModel.getModuleTypes = ko.computed(function ()
    {
        var ColumnModuleTypesArray = new Array();

        if (viewModel.typeToShow() != null)
        {
            ko.utils.arrayForEach(this.columnModuleTypes().ColumnModuleTypes(), function (item)
            {
                for (var i = 0; i < item.Categories().length; i++)
                {
                    if (item.Categories()[i] == viewModel.typeToShow())
                    {
                        ColumnModuleTypesArray.push(item);
                        break;
                    }
                }
            });
        }

        return ColumnModuleTypesArray;

    }, viewModel);

    //code used to set the preview section of a module
    viewModel.moduleCurrentlyViewing = ko.observable();

    //
    //  Method called whenever we are attempting to display a module via knockout template and we need the template id.
    //
    viewModel.getColumnModuleTypeTemplateName = function (item)
    {
        if (item !== null)
        {
            if (item.ColumnModuleModel === undefined)
            {
                return item.ModuleTypeName() + 'ChimeraTemplate';
            }
            else
            {
                return item.ColumnModuleModel.ColumnModule.ModuleTypeName() + 'ChimeraTemplate';
            }
        }

        return 'DummyChimeraTemplate';
    };

    //
    //  Method called whenever we are attempting to display a module via knockout template and we need the model object to pass to the template.
    //
    viewModel.getColumnModuleTypeTemplateData = function (item)
    {
        if (item !== null)
        {
            if (item.ColumnModuleModel === undefined)
            {
                return item.ChildrenValueDictionary;
            }
            else
            {
                return item.ColumnModuleModel.ColumnModule.ChildrenValueDictionary;
            }
        }

        return '';
    };

    viewModel.currentlyEditingModule = ko.observable();

    viewModel.currentlyEditingModuleSet = function (item, event)
    {
        if (CanEditAnyModule)
        {
            $(".chimera-editable-largehtml").each(function ()
            {
                var textArea = $(this);

                if (!textArea.next().hasClass("CodeMirror"))
                {

                    var editor = CodeMirror.fromTextArea(textArea.get(0), {
                        mode: "application/xml",
                        theme: "blackboard",
                        lineNumbers: true,
                        lineWrapping: true
                    });

                    editor.on("change", function (cm, change)
                    {
                        item.ChildrenValueDictionary[textArea.attr('child-value-key')].Value(editor.getValue());
                    });
                }
            });

            $('.CodeMirror').each(function (i, el)
            {
                el.CodeMirror.refresh();
            });

            $(event.target).closest('.chimeraEditableColumnModule').addClass('prevent');

            var summerNoteChildren = $(event.target).closest('.chimeraEditableColumnModule').find(".summernote-wysiwyg");

            summerNoteChildren.each(function ()
            {
                var child = $(this);

                if (child.code().length == 0)
                {
                    child.summernote({
                        onblur: function (e)
                        {
                            item.ChildrenValueDictionary[child.attr('child-value-key')].Value(child.code());
                        },
                        height: 300,
                        focus: false,
                        toolbar: [
                          //['style', ['style']], // no style button
                          ['style', ['bold', 'italic', 'underline']],
                          ['fontsize', ['fontsize']],
                          ['color', ['color']],
                          ['para', ['ul', 'ol', 'paragraph']],
                          //['height', ['height']],
                          ['insert', ['link', 'icon', 'button']],
                          //['insert', ['picture', 'link']], // no insert buttons
                          //['table', ['table']], // no table button
                          //['help', ['help']] //no help button,
                          //['view', ['codeview']]
                        ]
                    });

                    child.code(item.ChildrenValueDictionary[child.attr('child-value-key')].Value());
                }

                $(".chimera-wysiwyg-link-button").unbind('click');
                $(".chimera-wysiwyg-link-button").click(function ()
                {
                    viewModel.setHyperLinkDialog();
                });

                $(".chimera-wysiwyg-icon-button").unbind('click');
                $(".chimera-wysiwyg-icon-button").click(function ()
                {
                    viewModel.setIconWYSIWYGDialog();
                });

                $(".chimera-wysiwyg-btn-button").unbind('click');
                $(".chimera-wysiwyg-btn-button").click(function ()
                {
                    viewModel.setButtonWYSIWYGDialog();
                });
            });

            viewModel.currentlyEditingModule(item);

            var summernoteWYSIWYG = GetEditingCurrentSummernote();

            if(summernoteWYSIWYG != null)
            {
                summernoteWYSIWYG = summernoteWYSIWYG.next().find(".note-editable");

                summernoteWYSIWYG.unbind('click');

                summernoteWYSIWYG.click(function ()
                {
                    var _object = summernoteWYSIWYG.get(0);

                    viewModel.CurrentWYSIWYGCaretPosition(getCaretCharacterOffsetWithin(_object));
                });
            }
        }
    };

    //
    //  Check if we are currently editing this module or not.
    //
    viewModel.currentlyEditingThisModule = function (item)
    {
        return item === viewModel.currentlyEditingModule();
    };

    viewModel.currentlyHoveringModule = ko.observable();

    viewModel.currentlyHoveringOverModule = function (item)
    {
        return item === viewModel.currentlyHoveringModule();
    };

    viewModel.currentlyHoveringRow = ko.observable();

    viewModel.currentlyHoveringOverRow = function (item)
    {
        return item === viewModel.currentlyHoveringRow();
    };

    //
    //  Called when the user's mouse leaves the module to remove editing html.
    //
    viewModel.clearEditingModule = function (item, event)
    {
        viewModel.currentlyHoveringModule(null);

        viewModel.currentlyEditingModule(null);

        $('.chimeraEditableColumnModule').removeClass('prevent');

        if (event != null)
        {
            event.stopPropagation();
        }
    };

    //
    //  Method called whenever the user clicks the add button when previewing a module.
    //
    viewModel.addNewModuleToPage = function (item)
    {
        if (item !== null)
        {
            var newRowModule = new RowModule(ko.mapping.fromJS(ko.mapping.toJS(item.ColumnModuleModel.ColumnModule)));

            //add the new row & column to the beginning of the document
            viewModel.page().PageModel.Page.RowModuleList.unshift(newRowModule);

            //hide preview menu
            viewModel.typeToShow(null);
            viewModel.moduleCurrentlyViewing(null);

            Chimera_OnAddOrOnLoad();
        }
    };

    viewModel.removeRowModule = function (index)
    {
        viewModel.page().PageModel.Page.RowModuleList.splice(index,1);
    };

    viewModel.moveDirectionRowModule = function (index, direction)
    {
        var rowModule = ko.mapping.fromJS(ko.mapping.toJS(viewModel.page().PageModel.Page.RowModuleList()[index]));
        
        var newIndex;

        if (direction === 'up')
        {
            newIndex = index - 1;

            viewModel.page().PageModel.Page.RowModuleList.splice(index, 1);
            viewModel.page().PageModel.Page.RowModuleList.splice(newIndex, 0, rowModule);
        }
        else
        {
            newIndex = index + 1;

            viewModel.page().PageModel.Page.RowModuleList.splice(index, 1);
            viewModel.page().PageModel.Page.RowModuleList.splice(newIndex, 0, rowModule);
        }

        Chimera_OnAddOrOnLoad();
    };

    //
    //  Called when the user clicks on the remove button
    //
    viewModel.removeColumnModule = function (rowIndex, columnIndex)
    {
        viewModel.page().PageModel.Page.RowModuleList()[rowIndex].ColumnModuleList.splice(columnIndex, 1);
    };

    ko.bindingHandlers.resizable = {
        init: function (element, valueAccessor)
        {
            var options = valueAccessor();
            $(element).resizable(options);
        }
    };

    ko.bindingHandlers.sortable.afterMove = function(args)
    {
        Chimera_OnAddOrOnLoad();
    };

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

    viewModel.currentlyEditingModule(null);

    $(window).resize(function ()
    {
        updateEditingModeText();
    });

    updateEditingModeText();

    $("#chimera-editor-save-btn").click(function ()
    {
        $("#chimera-editor-save-input").val(ko.mapping.toJSON(viewModel.page().PageModel.Page));

        document.getElementById("chimera-editor-save-form").submit();

    });

    $("#chimera-editor-publish-btn").click(function ()
    {
        $("#chimera-editor-publish-input").val(ko.mapping.toJSON(viewModel.page().PageModel.Page));

        document.getElementById("chimera-editor-publish-form").submit();

    });
}

//
//  Called whenever the window is resized
//
function updateEditingModeText()
{
    if ($(window).width() > (Default_SM_XS_Column_Width * 12))
    {
        $("#chimeraEditingModeText").html("Desktop / Laptop");
    }
    else
    {
        $("#chimeraEditingModeText").html("Tablet / Phone");
    }
}

function openPreviewWindow(width)
{
    $("#previewPageForm").remove();

    var escapedPageData = ko.mapping.toJSON(viewModel.page());

    //TODO: temp fix for WYISWYG to correctly load in preview mode, scerws up double quotes if they typed them
    //escapedPageData = encodeURIComponent(escapedPageData.replace(/\\"/g, "\\'");

    var mapForm = document.createElement("form");
    mapForm.id = "previewPageForm";
    mapForm.target = "Map";
    mapForm.method = "POST"; // or "post" if appropriate
    mapForm.action = websiteDefaultUrl + viewModel.page().PageModel.Page.PageFriendlyURL();

    var mapInput = document.createElement("input");
    mapInput.type = "hidden";
    mapInput.name = "previewPageData";
    mapInput.value = escapedPageData;
    mapForm.appendChild(mapInput);

    document.body.appendChild(mapForm);

    if (width == -1)
    {
        width = $(window).width();
    }

    var map = window.open("", "Map", "toolbar=no, scrollbars=yes, resizable=no, width=" + width + ", height=" + $(window).height());

    mapForm.submit();
}

function sortableStartEvent(event, ui)
{
    CanEditAnyModule = false;
    
}

function sortableStopEvent(event, ui)
{
    CanEditAnyModule = true;
    viewModel.clearEditingModule();
}

//
//  Called on start and stop resize of a column event.
//
function columnResizeApplyCSS(columnDOM, removeClass)
{
    if (removeClass === undefined || removeClass === false)
    {
        columnDOM.addClass(columnModuleBorderClass);
        columnDOM.parent().addClass(rowModuleBorderClass);
    }
    else
    {
        columnDOM.removeClass(columnModuleBorderClass);
        columnDOM.parent().removeClass(rowModuleBorderClass);
    }
}

//
//  Called whenever we are resizing a column to grid snap it to 12 bootstrap columns
//
function columnResizeEvent(event, ui, item)
{
    var ColumnSizeArray = SM_XS_ColumnSizeAndClassNameArray;
    var UsingSmallArray = true;

    if ($(window).width() > (Default_SM_XS_Column_Width * 12))
    {
        UsingSmallArray = false;
        ColumnSizeArray = LG_MD_ColumnSizeAndClassNameArray;
    }

    viewModel.currentlyHoveringModule(null);
    var closest = null;
    var goal = ui.size.width;

    $.each(ColumnSizeArray, function ()
    {
        if (closest == null || Math.abs(this.pixelSize - goal) < Math.abs(closest.pixelSize - goal))
        {
            closest = this;
        }
    });

    removeOldBoostrapColumnClassNames(UsingSmallArray, item);

    if (UsingSmallArray)
    {
        item.AdditionalClassNames(item.AdditionalClassNames() + ' ' + ('col-sm-' + closest.className));
        item.AdditionalClassNames(item.AdditionalClassNames() + ' ' + ('col-xs-' + closest.className));
    }
    else
    {
        item.AdditionalClassNames(item.AdditionalClassNames() + ' ' + ('col-md-' + closest.className));
        item.AdditionalClassNames(item.AdditionalClassNames() + ' ' + ('col-lg-' + closest.className));
    }
    

    ui.element.removeAttr('style');
}

//
//  Removes old bootstrap column class names.
//
function removeOldBoostrapColumnClassNames(usingSmallArray, columnModule)
{
    for (var i = 12; i > 0; i--)
    {
        if (usingSmallArray)
        {
            columnModule.AdditionalClassNames(columnModule.AdditionalClassNames().replace('col-sm-' + i, ''));
            columnModule.AdditionalClassNames(columnModule.AdditionalClassNames().replace('col-xs-' + i, ''));
        }
        else
        {
            columnModule.AdditionalClassNames(columnModule.AdditionalClassNames().replace('col-md-' + i, ''));
            columnModule.AdditionalClassNames(columnModule.AdditionalClassNames().replace('col-lg-' + i, ''));
        }
    }
}

//
//  Called when page loaded to initialize our column size with class names for boostrap.
//
function initializeColumnSizeAndClassNames()
{
    for (var i = 1; i <= 12; i++)
    {
        SM_XS_ColumnSizeAndClassNameArray.push(new ColumnSizeAndClassName(Default_SM_XS_Column_Width * i, i));
        LG_MD_ColumnSizeAndClassNameArray.push(new ColumnSizeAndClassName(Default_LG_MD_Column_Width * i, i));
    }
}

//
//  Simple object to hold column pixel width with matching bootstrap column name.
//
function ColumnSizeAndClassName(pixelSize, className)
{
    this.pixelSize = pixelSize;
    this.className = className;
}

function MetaTag()
{
    this.AttributeList = ko.observableArray();
    this.AttributeList.push(new MetaTagAttribute("",""));
}

function MetaTagAttribute(AttributeName, AttributeValue)
{
    this.AttributeName = ko.observable(AttributeName);
    this.AttributeValue = ko.observable(AttributeValue);
}

//
//  Javascript object to represent a row module so that we can add new ones.
//
function RowModule(ColumnModule)
{
    this.AdditionalClassNames = ko.observable(Default_New_Row_Module_Classes);
    this.ColumnModuleList = ko.observableArray();

    if (ColumnModule != null)
    {
        this.ColumnModuleList.push(ColumnModule);
    }
}