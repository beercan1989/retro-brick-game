using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter
{
    public class ShooterInput : MonoBehaviour
    {
        /// <summary>
        /// Distance taken up by one pixel within the game.
        /// </summary>
        private const float PixelSize = 0.01f;
        
        /// <summary>
        /// The player object.
        /// </summary>
        private Transform _player;

        private void Awake()
        {
            _player = GameObject.Find("player").transform;
        }

        public void OnInputMovement(InputAction.CallbackContext ctx)
        {
            var movement = ctx.ReadValue<Vector2>();
            if (movement.x > 0)
            {
                _player.position += Vector3.right * PixelSize;
            }
            else if (movement.x < 0)
            {
                _player.position += Vector3.left * PixelSize;
            }
        }
    }
}
