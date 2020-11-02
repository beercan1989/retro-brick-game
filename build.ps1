
if(Test-Path "$PSScriptRoot/../retro-brick-game-clone") {

  Write-Output ""
  Write-Output "Using an existing clone..."
  Write-Output ""

} else {
  
  Write-Output ""
  Write-Output "Creating new clone..."
  Write-Output ""
  
  mkdir "$PSScriptRoot/../retro-brick-game-clone"

}

Push-Location "$PSScriptRoot/../retro-brick-game-clone"

Write-Output ""
Write-Output "Cloning the game state to: $PWD"
Write-Output ""

##
# https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/robocopy
##
robocopy "$PSScriptRoot" "$PSScriptRoot/../retro-brick-game-clone" /mir /copy:DATSO /nfl /ndl /np /xd "Temp" "Build" "Logs" "obj"

git status

. "$PSScriptRoot/../retro-brick-game-clone/CI/test.ps1"

Pop-Location
