using System;
using Level;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        
        /// <summary>
        /// The model we are using for the player.
        /// </summary>
        private GameObject _model;
        
        /// <summary>
        /// The model we are using the the player's death.
        /// </summary>
        private GameObject _death;

        /// <summary>
        /// The level object in the scene.
        /// </summary>
        private LevelProgression _level;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _model = GetComponentInChildren<Model>().gameObject;
            _death = GetComponentInChildren<Death>(true).gameObject;
            _level = FindObjectOfType<LevelProgression>();
        }

        private void Start()
        {
            // Make sure the rigid body is enabled.
            _rigidbody.simulated = true;
            
            // Make sure the model is showing.
            _model.SetActive(true);
            
            // Make sure the death is hidden.
            _death.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // If its not an enemy we don't care about it.
            if (!other.rigidbody.CompareTag("Enemy")) return;
            
            // Lets end the level where it is.
            _level.EndLevel();
            
            // Make sure the rigid body is disabled to prevent interactions with the enemy.
            _rigidbody.simulated = false;
            
            // Hide player model.
            _model.SetActive(false);
            
            // Show player death.
            _death.SetActive(true);
        }
    }
}
