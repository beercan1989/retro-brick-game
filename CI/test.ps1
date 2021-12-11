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
UnityBuild -BuildTarget Android -BuildName retro-brick-game.apk
UnityBuild -BuildTarget WebGL -BuildName retro-brick-game
