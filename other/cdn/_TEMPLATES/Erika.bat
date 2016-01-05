mkdir Erika
mkdir Erika\CSS
mkdir Erika\JS
mkdir Erika\IMAGES
mkdir Erika\FONTS

SET MainDirectory=C:\workspace\Chimera\trunk\src\ChimeraWebsite\Templates\Erika\
SET MinifierDirectory=C:\"Program Files (x86)"\Microsoft\"Microsoft Ajax Minifier"\ajaxmin.exe

SET File1="%MainDirectory%Styles\bootstrap.css"
SET File2="%MainDirectory%Styles\font-awesome.css"
SET File3="%MainDirectory%Styles\slider.css"
SET File4="%MainDirectory%Styles\Chimera-Override\slider.css"
SET File5="%MainDirectory%Styles\prettyPhoto.css"
SET File6="%MainDirectory%Styles\flexslider.css"
SET File7="%MainDirectory%Styles\Chimera-Override\flexslider.css"
SET File8="%MainDirectory%Styles\style.css"
SET File9="%MainDirectory%Styles\Chimera-Override\style.css"
SET File10="%MainDirectory%Styles\Chimera-Custom\common.css"
SET File11="%MainDirectory%Styles\Chimera-Custom\shoppingCart.css"
SET File12="%MainDirectory%Styles\Chimera-Custom\products.css"
SET File13="%MainDirectory%Styles\Chimera-Custom\social-media.css"

start /wait %MinifierDirectory% %File1% %File2% %File3% %File4% %File5% %File6% %File7% %File8% %File9% %File10% %File11% %File12% %File13% -out Erika\CSS\all.css

SET File1="%MainDirectory%Scripts\Chimera\OnAddOrOnLoad.js"
SET File2="%MainDirectory%Scripts\Chimera\ProductViewLoad.js"
SET File3="%MainDirectory%Scripts\Chimera\ShoppingCartViewLoad.js"
SET File4="%MainDirectory%Scripts\bootstrap.min.js"
SET File5="%MainDirectory%Scripts\jquery.isotope.js"
SET File6="%MainDirectory%Scripts\jquery.prettyPhoto.js"
SET File7="%MainDirectory%Scripts\filter.js"
SET File8="%MainDirectory%Scripts\jquery.flexslider-min.js"
SET File9="%MainDirectory%Scripts\jquery.cslider.js"
SET File10="%MainDirectory%Scripts\modernizr.custom.28468.js"

start /wait %MinifierDirectory% %File1% %File2% %File3% %File4% %File5% %File6% %File7% %File8% %File9% %File10% -out Erika\JS\all.js

start /wait %MinifierDirectory% "%MainDirectory%Styles\font-awesome-ie7.css" -out Erika\CSS\font-awesome-ie7.css
start /wait %MinifierDirectory% "%MainDirectory%Styles\blue.css" -out Erika\CSS\blue.css
start /wait %MinifierDirectory% "%MainDirectory%Styles\brown.css" -out Erika\CSS\brown.css
start /wait %MinifierDirectory% "%MainDirectory%Styles\green.css" -out Erika\CSS\green.css
start /wait %MinifierDirectory% "%MainDirectory%Styles\orange.css" -out Erika\CSS\orange.css
start /wait %MinifierDirectory% "%MainDirectory%Styles\pink.css" -out Erika\CSS\pink.css
start /wait %MinifierDirectory% "%MainDirectory%Styles\red.css" -out Erika\CSS\red.css

start /wait %MinifierDirectory% "%MainDirectory%Scripts\html5shim.js" -out Erika\JS\html5shim.js

xcopy "%MainDirectory%Images" "Erika\IMAGES" /D /E /C /R /I /K /Y
xcopy "%MainDirectory%Fonts" "Erika\FONTS" /D /E /C /R /I /K /Y
