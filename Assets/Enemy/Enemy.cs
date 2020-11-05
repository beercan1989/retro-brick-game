using System;
using System.Linq;
using Level;
using UnityEngine;
using Random = UnityEngine.Random;
using static PixelConstants;

namespace Enemy
{
    // TODO - Reconsider the naming/moving when we introduce other types of enemies.
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// All the child models this enemy can appear as.
        /// </summary>
        private GameObject[] _models;

        /// <summary>
        /// Current model in use.
        /// </summary>
        private GameObject _model;

        /// <summary>
        /// Renderers of the current model in use.
        /// </summary>
        private SpriteRenderer[] _renderers;

        /// <summary>
        /// Indicates if we have seen the model yet.
        /// </summary>
        private bool _modelSeen;

        /// <summary>
        /// The starting position of the enemy.
        /// </summary>
        private const float StartingPosition = 0.7f;
        
        /// <summary>
        /// Tracks the amount of movement the enemy has made.
        /// </summary>
        private float _movement;
        
        private void Awake()
        {
            _models = GetComponentsInChildren<Rigidbody2D>(includeInactive: true)
                .Select(child => child.gameObject)
                .ToArray();

            LevelProgression.OnLevelEvent += HandleLevelEvents;
        }

        private void Start()
        {
            Regenerate();
        }

        private void Update()
        {
            HandleRegeneration();
            Move();
        }

        private void Move()
        {
            // Increase the movement progress based on time and pixel conversion.
            _movement += Time.deltaTime * PixelsPerSecond;
            
            // We only want whole pixels.
            var movement = (int) Math.Round(_movement);

            // Lets move the enemy based on the progress we've made.
            transform.position = new Vector3(0f, StartingPosition - movement * PixelSize, 0f);
        }

        private void HandleLevelEvents(object sender, LevelEvent levelEvent)
        {
            if (levelEvent == LevelEvent.Finished)
            {
                enabled = false;
            }
        }

        /// <summary>
        /// Regenerate the enemy model
        /// </summary>
        private void Regenerate()
        {
            if (!PickModel()) return;
            RotateModel();
            PositionModel();
            EnableModel();
        }

        /// TODO - Look at better methods, breaks when visible in Scene view but Game view.
        /// <summary>
        /// Handle regeneration by checking to see if the model is still visible or not.
        /// </summary>
        private void HandleRegeneration()
        {
            // Make sure we've at least seen the model.
            if (_renderers.Any(_ => _.isVisible))
            {
                _modelSeen = true;
            } else if (_modelSeen) // Now we no longer see the model.
            {
                _model.SetActive(false);
                Regenerate();
            }
        }

        /// <summary>
        /// Pick a new model for the enemy.
        /// </summary>
        /// <returns>true if we have changed the model</returns>
        private bool PickModel()
        {
            if (_model != null && _model.activeInHierarchy) return false;
            _model = _models[Random.Range(0, _models.Length)];
            _renderers = _model.GetComponentsInChildren<SpriteRenderer>();
            Debug.LogFormat("Enemy model picked: {0}", _model);
            return true;
        }

        /// <summary>
        /// Randomly choose a rotation for the model, based on 90 degree increments. 
        /// </summary>
        private void RotateModel()
        {
            if (_model == null) return;
            transform.Rotate(0f, 0f, 90f * Random.Range(0, 4));
        }
        
        /// <summary>
        /// Randomly position the model along the top of the screen.
        /// </summary>
        private void PositionModel()
        {
            if (_model == null) return;
            // TODO - Work out how to position left to right.
            // TODO - Work out how we take into consideration the affect of rotation on suitable positions.
            
            // For now lets just set it in the centre at the top.
            transform.position = new Vector3(0f, StartingPosition, 0f);
            
            // Reset movement
            _movement = 0;
        }

        /// <summary>
        /// Enable the model so its glory can be seen by all.
        /// </summary>
        private void EnableModel()
        {
            if (_model == null) return;
            _model.SetActive(true);
        }
    }
}
