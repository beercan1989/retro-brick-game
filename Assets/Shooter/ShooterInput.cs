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

        /// <summary>
        /// The player model.
        /// </summary>
        private Model _model;

        /// <summary>
        /// The prefab for the bullets we can fire.
        /// </summary>
        [SerializeField] private GameObject bullet;
        
        private void Awake()
        {
            _player = FindObjectOfType<PlayerBehaviour>();
            _model = _player.GetComponentInChildren<Model>();
            
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
            if (!enabled || bullet == null) return;

            var bounds = _model.SpawnBound(SpawnRotation.R0);
            var bulletY = bounds.Upper.y * PositionByBrick + PositionByBrick;
            var bulletPosition = _player.transform.position + new Vector3(0, bulletY, 0);

            Instantiate(bullet, bulletPosition, Quaternion.identity);
        }
    }
}
