SET MainDirectory=C:\workspace\Chimera\trunk\src\ChimeraWebsite\
SET MinifierDirectory=C:\"Program Files (x86)"\Microsoft\"Microsoft Ajax Minifier"\ajaxmin.exe

SET File1="%MainDirectory%Styles\codemirror.css"
SET File2="%MainDirectory%Styles\codemirror\theme\blackboard.css"
SET File3="%MainDirectory%Styles\summernote.css"
SET File4="%MainDirectory%Styles\summernote-bs3.css"
SET File5="%MainDirectory%Styles\summernote-bs3-override.css"
SET File6="%MainDirectory%Styles\bootstrap-colorpicker.min.css"
SET File7="%MainDirectory%Styles\bootstrap-colorpicker-override.css"
SET File8="%MainDirectory%Styles\chimera.css"

start /wait %MinifierDirectory% %File1% %File2% %File3% %File4% %File5% %File6% %File7% %File8% -out E\CSS\all.css

SET File1="%MainDirectory%Styles\jquery.fileupload-ui-noscript.css"
SET File2="%MainDirectory%Styles\jquery.fileupload-ui.css"
SET File3="%MainDirectory%Styles\chimera-image-upload.css"

start /wait %MinifierDirectory% %File1% %File2% %File3% -out E\CSS\admin-bundle.css

SET File1="%MainDirectory%Scripts\codemirror.js"
SET File2="%MainDirectory%Scripts\codemirror\mode\xml\xml.js"
SET File3="%MainDirectory%Scripts\formatting.min.js"
SET File4="%MainDirectory%Scripts\summernote.min.js"
SET File5="%MainDirectory%Scripts\bootstrap-colorpicker.min.js"
SET File6="%MainDirectory%Scripts\chimera-common-utility.js"
SET File7="%MainDirectory%Scripts\chimera-toolbar.js"
SET File8="%MainDirectory%Scripts\EditScreenDialogs\chimera-toolbar-module-settings.js"
SET File9="%MainDirectory%Scripts\EditScreenDialogs\chimera-toolbar-edit-image.js"
SET File10="%MainDirectory%Scripts\EditScreenDialogs\chimera-toolbar-edit-icon.js"
SET File11="%MainDirectory%Scripts\EditScreenDialogs\chimera-toolbar-edit-button.js"
SET File12="%MainDirectory%Scripts\EditScreenDialogs\cimera-toolbar-edit-productSearch.js"
SET File13="%MainDirectory%Scripts\EditScreenDialogs\WYSIWYG\chimera-wysiwyg-utility-functions.js"
SET File14="%MainDirectory%Scripts\EditScreenDialogs\WYSIWYG\chimera-wysiwyg-hyperlink.js"
SET File15="%MainDirectory%Scripts\EditScreenDialogs\WYSIWYG\chimera-wysiwyg-icon.js"
SET File16="%MainDirectory%Scripts\EditScreenDialogs\WYSIWYG\chimera-wysiwyg-button.js"

start /wait %MinifierDirectory% %File1% %File2% %File3% %File4% %File5% %File6% %File7% %File8% %File9% %File10% %File11% %File12% %File13% %File14% %File15% %File16% -out E\JS\all.js

SET File1="%MainDirectory%Scripts\jquery.fileupload.js"
SET File2="%MainDirectory%Scripts\jquery.iframe-transport.js"
SET File3="%MainDirectory%Scripts\knockout-3.0.0.js"
SET File4="%MainDirectory%Scripts\knockout-mapping.js"
SET File5="%MainDirectory%Scripts\knockout-sortable.min.js"

start /wait %MinifierDirectory% %File1% %File2% %File3% %File4% %File5% -out E\JS\admin-bundle.js

mkdir E\IMAGES\bootstrap-colorpicker

xcopy "%MainDirectory%Images\bootstrap-colorpicker" "E\IMAGES\bootstrap-colorpicker" /D /E /C /R /I /K /Y
