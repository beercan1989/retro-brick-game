# Retro Brick Game
![Build](https://github.com/beercan1989/retro-brick-game/workflows/Build/badge.svg)
![Publish](https://github.com/beercan1989/retro-brick-game/workflows/Publish/badge.svg)

Started out as a LOWREZJAM 2020 submission, a 64x64 low resolution game, but never got submitted.

So the game development now continues as an attempt to build the whole thing closer to its 
original dimensions.

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
