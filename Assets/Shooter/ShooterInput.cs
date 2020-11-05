using Level;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using static PixelConstants;

namespace Shooter
{
    public class ShooterInput : MonoBehaviour
    {
        
        /// <summary>
        /// The player object.
        /// </summary>
        private PlayerBehaviour _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerBehaviour>();
            
            LevelProgression.OnLevelEvent += HandleLevelEvents;
        }

        private void HandleLevelEvents(object sender, LevelEvent levelEvent)
        {
            if (levelEvent == LevelEvent.Finished)
            {
                enabled = false;
            }
        }

        public void OnMovement(InputAction.CallbackContext ctx)
        {
            if (!enabled) return;
                
            var movement = ctx.ReadValue<Vector2>();
            if (movement.x > 0)
            {
                _player.transform.position += Vector3.right * PixelSize;
            }
            else if (movement.x < 0)
            {
                _player.transform.position += Vector3.left * PixelSize;
            }
        }
        
        public void OnShoot(InputAction.CallbackContext ctx)
        {
            if (!enabled) return;
            
            // TODO - Implement
        }
    }
}
