##
# Import functions
##
. "$PSScriptRoot/functions.ps1"

##
# Publish
##
ItchIoPublish -BuildTarget StandaloneWindows64 -Project beercan/retro-brick-game -Channel windows
ItchIoPublish -BuildTarget StandaloneLinux64 -Project beercan/retro-brick-game -Channel linux
ItchIoPublish -BuildTarget StandaloneOSX -Project beercan/retro-brick-game -Channel osx
ItchIoPublish -BuildTarget Android -Project beercan/retro-brick-game -Channel android
ItchIoPublish -BuildTarget WebGL -Project beercan/retro-brick-game -Channel webgl
