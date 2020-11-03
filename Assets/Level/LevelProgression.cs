using System;
using Bricks;
using UnityEngine;
using UnityEngine.U2D;

namespace Level
{
    // TODO - Reconsider the naming when we introduce other types of level progression.
    public class LevelProgression : MonoBehaviour
    {
        public static event EventHandler<LevelEvent> OnLevelEvent;

        /// <summary>
        /// Distance taken up by one pixel within the game;
        /// </summary>
        private const float PixelSize = 0.01f;

        /// <summary>
        /// Number of pixels taken up by each block
        /// </summary>
        private const int PixelsPerBrick = 6;

        /// <summary>
        /// Number of pixels to progress the game by per second.
        /// </summary>
        private const int PixelsPerSecond = 50; // TODO - Work into level difficulty

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
            var leftWall = transform.Find("left");
            var numberOfBricks = leftWall.GetComponentsInChildren<Brick>().Length;

            // (pixels for each brick) + (spaces between bricks + start and end spaces)
            _levelLength = PixelsPerBrick * numberOfBricks + numberOfBricks + 2;

            _camera = FindObjectOfType<PixelPerfectCamera>();

            OnLevelEvent += LogEvent;
        }

        // TODO - Remove this debug stuff
        private static void LogEvent(object sender, LevelEvent e)
        {
            Debug.LogFormat("LevelEvent: {0}", e);
        }

        private void Start()
        {
            OnLevelEvent?.Invoke(this, LevelEvent.Started);
        }

        private void Update()
        {
            // Increase the level progress based on time and pixel conversion.
            _levelProgress += Time.deltaTime * PixelsPerSecond;
            
            // _levelLength - (2 bricks) - (camera view size / 2)
            var levelEnd = _levelLength - ((2f * PixelsPerBrick) + (_camera.refResolutionY / 2f));
            
            // Have we completed level progression?
            if (_levelProgress >= levelEnd)
            {
                OnLevelEvent?.Invoke(this, LevelEvent.Finished);
                
                // Level progress stops.
                enabled = false;
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