##
# Build in unity
##
function UnityBuild($BuildTarget, $BuildName, $UNITY_VERSION = "2019.4.12f1") {

  Write-Host "Starting build for: $BuildTarget" -ForegroundColor Green

  & "C:\Program Files\Unity\Hub\Editor\$UNITY_VERSION\Editor\Unity.exe" -batchmode -quit -nographics -projectPath "$PWD" -buildName "$BuildName" -buildTarget "$BuildTarget" -executeMethod Editor.CI.Builder.Build | Write-Output

  if ($? -eq $false) {
    Write-Host "Failed $BuildName - $BuildTarget " -ForegroundColor Red
    exit 1
  }

  Write-Host "Finished build for: $BuildTarget" -ForegroundColor Green
}

##
# Publish to Itch.io
##
function ItchIoPublish($BuildTarget, $Project, $Channel) {

  Write-Host "Publishing ${BuildTarget} to ${Project}:${Channel}" -ForegroundColor Green

  butler push ./Build/${BuildTarget} ${Project}:${Channel}

  if ($? -eq $false) {
    Write-Host "Failed to publish ${BuildTarget} to ${Project}:${Channel} " -ForegroundColor Red
    exit 1
  }

  Write-Host "Published ${BuildTarget} to ${Project}:${Channel}" -ForegroundColor Green
}
