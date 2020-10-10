
$UNITY_VERSION = "2019.4.12f1"

function UnityBuild($BuildTarget, $BuildName) {

  Write-Output "Starting build for: $BuildTarget"

  & "C:\Program Files\Unity\Hub\Editor\$UNITY_VERSION\Editor\Unity.exe" -batchmode -quit -nographics -buildName $BuildName -buildTarget $BuildTarget -executeMethod Editor.CI.Builder.Build | Write-Output

  # TODO - Check for failures

  Write-Output "Finished build for: $BuildTarget"
}

UnityBuild -BuildTarget StandaloneWindows64 -BuildName retro-brick-game.exe
UnityBuild -BuildTarget StandaloneLinux64 -BuildName retro-brick-game
UnityBuild -BuildTarget StandaloneOSX -BuildName retro-brick-game
UnityBuild -BuildTarget WebGL -BuildName retro-brick-game
