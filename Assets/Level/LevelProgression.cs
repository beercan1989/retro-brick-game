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
        /// The number of pixels that make up the level.
        /// </summary>
        private int _levelLength;

        /// <summary>
        /// The number of pixels displayed so far.
        /// </summary>
        private float _levelProgress;

        /// <summary>
        /// Current pixel perfect camera.
        /// </summary>
        private PixelPerfectCamera _camera;
        
        private void Awake()
        {
            // Lets find the left wall
            var leftWall = transform.Find("left");
            
            // How many bricks are in the wall?
            var numberOfBricks = leftWall.GetComponentsInChildren<Brick>().Length;

            // (pixels for each brick) + (spaces between bricks + start and end spaces)
            _levelLength = PixelsPerBrick * numberOfBricks + numberOfBricks + 2;

            // Camera so we can work out the visible size to calculate the end position of the level.
            _camera = FindObjectOfType<PixelPerfectCamera>();

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
            
            // _levelLength - (2 bricks + 2 spaces + camera view size / 2)
            var levelEnd = _levelLength - (2f * PixelsPerBrick + 2 + _camera.refResolutionY / 2f);
            
            // Have we completed level progression?
            if (_levelProgress >= levelEnd)
            {
                // Report that the level has finished.
                OnLevelEvent?.Invoke(this, LevelEvent.Finished);
                
                // Level progress stops.
                enabled = false;
                
                // Lets just make sure we're not off by a single pixel.
                transform.position = new Vector3(0, -levelEnd * PixelSize, 0);
            }
            else
            {
                // We only want whole pixels.
                var levelProgress = (int) Math.Round(_levelProgress);

                // Lets move the level based on the progress we've made.
                transform.position = new Vector3(0, -levelProgress * PixelSize, 0);
            }
        }
    }
}