SET MainDirectory=C:\workspace\Chimera\trunk\src\ChimeraWebsite\
SET MinifierDirectory=C:\"Program Files (x86)"\Microsoft\"Microsoft Ajax Minifier"\ajaxmin.exe

SET File1="%MainDirectory%Styles\jqueryui\jquery-ui.min.css"
SET File2="%MainDirectory%Styles\chimera-public-face.css"
SET File3="%MainDirectory%Styles\social-media-FlatTheme.css"

start /wait %MinifierDirectory% %File1% %File2% %File3% -out G\CSS\all.css

SET File1="%MainDirectory%Scripts\jquery-1.10.2.min.js"
SET File2="%MainDirectory%Scripts\jquery-ui.min.js"
SET File3="%MainDirectory%Scripts\chimera-common-public-face.js"

start /wait %MinifierDirectory% %File1% %File2% %File3% -out G\JS\all.js

xcopy "%MainDirectory%Images" "G\IMAGES" /D /E /C /R /I /K /Y