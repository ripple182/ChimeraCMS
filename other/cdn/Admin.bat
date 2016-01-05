SET MainDirectory=C:\workspace\Chimera\trunk\src\ChimeraWebsite\
SET MinifierDirectory=C:\"Program Files (x86)"\Microsoft\"Microsoft Ajax Minifier"\ajaxmin.exe

SET File1="%MainDirectory%Styles\jqueryui\jquery-ui.min.css"
SET File2="%MainDirectory%Templates\Erika\Styles\bootstrap.css"
SET File3="%MainDirectory%Templates\Erika\Styles\font-awesome.css"
SET File4="%MainDirectory%Templates\Erika\Styles\style.css"
SET File5="%MainDirectory%Templates\Erika\Styles\blue.css"
SET File6="%MainDirectory%Areas\Admin\Styles\ChimeraAdmin\customstyle.css"

start /wait %MinifierDirectory% %File1% %File2% %File3% %File4% %File5% %File6% -out A\CSS\all.css

SET File1="%MainDirectory%Scripts\jquery-1.10.2.min.js"
SET File2="%MainDirectory%Scripts\jquery-ui.min.js"
SET File3="%MainDirectory%Scripts\chimera-common-utility.js"
SET File4="%MainDirectory%Templates\Erika\Scripts\bootstrap.min.js"
SET File5="%MainDirectory%Templates\Erika\Scripts\jquery.isotope.js"
SET File6="%MainDirectory%Templates\Erika\Scripts\filter.js"
SET File7="%MainDirectory%Templates\Erika\Scripts\modernizr.custom.28468.js"
SET File8="%MainDirectory%Areas\Admin\Scripts\timeago.js"
SET File9="%MainDirectory%Areas\Admin\Scripts\ChimeraAdmin\edit-product.js"
SET File10="%MainDirectory%Areas\Admin\Scripts\ChimeraAdmin\edit-settingGroupSchema.js"
SET File11="%MainDirectory%Areas\Admin\Scripts\ChimeraAdmin\edit-settingGroupValues.js"
SET File12="%MainDirectory%Areas\Admin\Scripts\ChimeraAdmin\edit-staticProperties.js"
SET File13="%MainDirectory%Areas\Admin\Scripts\ChimeraAdmin\edit-navigationMenu.js"

start /wait %MinifierDirectory% %File1% %File2% %File3% %File4% %File5% %File6% %File7% %File8% %File9% %File10% %File11% %File12% %File13% -out A\JS\all.js

xcopy "%MainDirectory%Areas\Admin\Images" "A\IMAGES" /D /E /C /R /I /K /Y

xcopy "%MainDirectory%Templates\Erika\Images\dot.png" "A\IMAGES" /D /E /C /R /I /K /Y
xcopy "%MainDirectory%Templates\Erika\Images\header-back.png" "A\IMAGES" /D /E /C /R /I /K /Y
xcopy "%MainDirectory%Templates\Erika\Images\body-back.png" "A\IMAGES" /D /E /C /R /I /K /Y

xcopy "%MainDirectory%Templates\Erika\Fonts" "A\FONTS" /D /E /C /R /I /K /Y