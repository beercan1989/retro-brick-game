
$UNITY_VERSION = "2019.4.12f1"

function UnityBuild($BuildTarget, $BuildName) {

  Write-Host "Starting build for: $BuildTarget" -ForegroundColor Green

  & "C:\Program Files\Unity\Hub\Editor\$UNITY_VERSION\Editor\Unity.exe" -batchmode -quit -nographics -buildName $BuildName -buildTarget $BuildTarget -executeMethod Editor.CI.Builder.Build | Write-Output

  if ($? -eq $false) {
    Write-Host "Failed $BuildName - $BuildTarget " -ForegroundColor Red 
    exit 1
  }

  Write-Host "Finished build for: $BuildTarget" -ForegroundColor Green
}

function ItchIoPublish($BuildTarget, $Project, $Channel) {

  Write-Host "Publishing ${BuildTarget} to ${Project}:${Channel}" -ForegroundColor Green
  
  butler push ./Build/${BuildTarget} ${Project}:${Channel}

  if ($? -eq $false) {
    Write-Host "Failed to publish ${BuildTarget} to ${Project}:${Channel} " -ForegroundColor Red
    exit 1
  }

  Write-Host "Published ${BuildTarget} to ${Project}:${Channel}" -ForegroundColor Green
}

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
