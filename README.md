# Retro Brick Game
![Build](https://github.com/beercan1989/retro-brick-game/workflows/Build/badge.svg)
![Publish](https://github.com/beercan1989/retro-brick-game/workflows/Publish/badge.svg)

Started out as a LOWREZJAM 2020 submission, a 64x64 low resolution game, but never got submitted.

So the game development now continues as an attempt to build the whole thing closer to its 
original dimensions.

A compiled and playable versions of this game for web, android, windows, linux and even osx; can be found here https://beercan.itch.io/retro-brick-game on my Itch page.

## Concept Art
Example "enemies", be it shooting targets or shapes to land.  
![Enemies](Art/enemies.png?raw=true "Enemies")

Idea's for how the shooter game section would look  
![Shooter 1](Art/shooter-1.png?raw=true "Shooter 1")
![Shooter 2](Art/shooter-2.png?raw=true "Shooter 2")
![Shooter 3](Art/shooter-3.png?raw=true "Shooter 3")
![Shooter 4](Art/shooter-4.png?raw=true "Shooter 4")

What happens on death, small version  
![Death 1](Art/death-1.png?raw=true "Death 1")
![Death 2](Art/death-2.png?raw=true "Death 2")
![Death 3](Art/death-3.png?raw=true "Death 3")

What happens on death, medium version  
![Death Step 1](Art/death-step-1.png?raw=true "Death Step 1")
![Death Step 2](Art/death-step-2.png?raw=true "Death Step 2")
![Death Step 3](Art/death-step-3.png?raw=true "Death Step 3")

## Game Details
* One pixel equates to 0.01 in distance for a positional value
* Blocks are 0.06 in size, so 6 pixels wide
* Block center position is in increments of 0.07 from 0
* There are 9x9 blocks in an area of 64x64 pixels
* Original game area is 10x20 blocks making it an area of 72x142 pixels
* Space calculations based on number of blocks
  ```
      (blocks) + (spaces)
  (6 * blocks) + (blocks + 2)
  ```
* But because I can't see how to do pixel perfect with even number of blocks
  we're going to have 11x21 blocks making it an area of 78x148 pixels 

## Scripts
* `./test.ps1` will create a cloned project and run `./CI/test.ps1`
* `./CI/test.ps1` attempts to create builds for each supported platforms
* `./CI/publish.ps1` publishes the current builds to Itch.io
* `./CI/build.ps1` creates fresh builds and publishes them.

## Sources of information
* https://github.com/beercan1989/playground-unity-2d
* https://seansleblanc.itch.io/better-minimal-webgl-template
