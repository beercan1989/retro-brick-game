##
# Import functions
##
. "$PSScriptRoot/functions.ps1"

##
# Build
##
UnityBuild -BuildTarget StandaloneWindows64 -BuildName retro-brick-game.exe
UnityBuild -BuildTarget StandaloneLinux64 -BuildName retro-brick-game
UnityBuild -BuildTarget StandaloneOSX -BuildName retro-brick-game
UnityBuild -BuildTarget WebGL -BuildName retro-brick-game

##
# Publish
##
ItchIoPublish -BuildTarget StandaloneWindows64 -Project beercan/retro-brick-game -Channel windows
ItchIoPublish -BuildTarget StandaloneLinux64 -Project beercan/retro-brick-game -Channel linux
ItchIoPublish -BuildTarget StandaloneOSX -Project beercan/retro-brick-game -Channel osx
ItchIoPublish -BuildTarget WebGL -Project beercan/retro-brick-game -Channel webgl
