using System;
using Bricks;
using UnityEngine;
using UnityEngine.U2D;
using static PixelConstants;

namespace Level
{
    // TODO - Reconsider the naming when we introduce other types of level progression.
    public class LevelProgression : MonoBehaviour
    {
        public static event EventHandler<LevelEvent> OnLevelEvent;

        /// <summary>
        /// The number of pixels displayed so far.
        /// </summary>
        private float _levelProgress;

        /// <summary>
        /// The location of the level when we're at the end.
        /// </summary>
        private float _levelEnd;
        
        private void Awake()
        {
            // Lets find the left wall
            var leftWall = transform.Find("left");
            
            // How many bricks are in the wall?
            var numberOfBricks = leftWall.GetComponentsInChildren<Brick>().Length;

            // How long is the whole level in pixels?
            // (pixels for each brick) + (spaces between bricks + start and end spaces)
            var levelLength = PixelsPerBrick * numberOfBricks + numberOfBricks + 2;
            
            // Camera so we can work out the visible size to calculate the end position of the level.
            var pixelCamera = FindObjectOfType<PixelPerfectCamera>();
            
            // levelLength - (2 bricks + 2 spaces + camera view size / 2)
            _levelEnd = levelLength - (2f * PixelsPerBrick + 2f + pixelCamera.refResolutionY / 2f);

            OnLevelEvent += LogEvent;
        }

        private static void LogEvent(object sender, LevelEvent e)
        {
            Debug.LogFormat("LevelEvent: {0}", e);
        }

        private void Start()
        {
            // Report that the level has started.
            OnLevelEvent?.Invoke(this, LevelEvent.Started);
        }

        private void Update()
        {
            // Increase the level progress based on time and pixel conversion.
            _levelProgress += Time.deltaTime * PixelsPerSecond;
            
            // Have we completed level progression?
            if (_levelProgress >= _levelEnd)
            {
                // We've reached the end, time to just end it.
                EndLevel();
                
                // Lets just make sure we're not off by a single pixel.
                transform.position = new Vector3(0, -_levelEnd * PixelSize, 0);
            }
            else
            {
                // We only want whole pixels.
                var levelProgress = (int) Math.Round(_levelProgress);

                // Lets move the level based on the progress we've made.
                transform.position = new Vector3(0, -levelProgress * PixelSize, 0);
            }
        }

        /// <summary>
        /// Just end the level where it currently stands.
        /// </summary>
        public void EndLevel()
        {
            // Can only end the level if enabled.
            if (!enabled) return;
            
            // Report that the level has finished.
            OnLevelEvent?.Invoke(this, LevelEvent.Finished);
                
            // Level progress stops.
            enabled = false;
        }
    }
}