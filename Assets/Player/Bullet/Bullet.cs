using System;
using System.Linq;
using UnityEngine;
using static PixelConstants;

namespace Player.Bullet
{
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// Number of pixels the bullet moves.
        /// </summary>
        private const int BulletSpeed = 500;
        
        /// <summary>
        /// Tracks the amount of movement the bullet has made.
        /// </summary>
        private float _movement;
        
        /// <summary>
        /// The starting position of the bullet.
        /// </summary>
        private Vector3 _startingPosition;
        
        /// <summary>
        /// Renderers of the bullet.
        /// </summary>
        private SpriteRenderer[] _renderers;

        /// <summary>
        /// Indicates if we have seen the bullet yet.
        /// </summary>
        private bool _seen;
        
        private void Awake()
        {
            _renderers = GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
        }

        private void Start()
        {
            _startingPosition = transform.position;
        }

        private void Update()
        {
            Move();
            HandleNotBeingVisible();
        }

        /// <summary>
        /// Move the bullet up the screen.
        /// </summary>
        private void Move()
        {
            // Increase the movement progress based on time and pixel conversion.
            _movement += Time.deltaTime * BulletSpeed;
            
            // We only want whole pixels.
            var movement = (int) Math.Round(_movement);

            // Lets move the enemy based on the progress we've made.
            transform.position = _startingPosition + new Vector3(0f, movement * PixelSize, 0f);
        }

        /// <summary>
        /// Check if the bullet is still visible on the screen.
        /// </summary>
        private void HandleNotBeingVisible()
        {
            // Make sure we've at least seen the bullet.
            if (_renderers.Any(_ => _.isVisible))
            {
                _seen = true;
            } else if (_seen) // Now we no longer see the bullet.
            {
                Destroy(gameObject);
            }
        }
    }
}
