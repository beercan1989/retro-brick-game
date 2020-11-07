using System;
using System.Linq;
using Level;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;
using static PixelConstants;

namespace Enemy
{
    // TODO - Reconsider the naming/moving when we introduce other types of enemies.
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The base position of the enemy in the Y.
        /// </summary>
        private const float BasePositionY = 0.7f;

        /// <summary>
        /// The grid position for a brick.
        /// </summary>
        private const float PositionByBrick = PixelsPerBrick * PixelSize + PixelSize;
        
        /// <summary>
        /// All the child models this enemy can appear as.
        /// </summary>
        private Model[] _models;

        /// <summary>
        /// Current model in use.
        /// </summary>
        private Model _model;

        /// <summary>
        /// Renderers of the current model in use.
        /// </summary>
        private SpriteRenderer[] _renderers;

        /// <summary>
        /// Indicates if we have seen the model yet.
        /// </summary>
        private bool _modelSeen;

        /// <summary>
        /// Current rotation of the enemy.
        /// </summary>
        private SpawnRotation _spawnRotation;

        /// <summary>
        /// Tracks the amount of movement the enemy has made.
        /// </summary>
        private float _movement;

        /// <summary>
        /// The starting position of the enemy.
        /// </summary>
        private Vector3 _startingPosition;
        
        private void Awake()
        {
            _models = GetComponentsInChildren<Model>(includeInactive: true);

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
            transform.position = _startingPosition - new Vector3(0f, movement * PixelSize, 0f);
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
            EnableModel();
            RotateModel();
            PositionModel();
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
                _model.gameObject.SetActive(false);
                Regenerate();
            }
        }

        /// <summary>
        /// Pick a new model for the enemy.
        /// </summary>
        /// <returns>true if we have changed the model</returns>
        private bool PickModel()
        {
            if (_model != null && _model.gameObject.activeInHierarchy) return false;
            _model = _models[Random.Range(0, _models.Length)];
            _renderers = _model.GetComponentsInChildren<SpriteRenderer>();
            _modelSeen = false;
            Debug.LogFormat("Enemy model picked: {0}", _model);
            return true;
        }

        /// <summary>
        /// Randomly choose a rotation for the model, based on 90 degree increments. 
        /// </summary>
        private void RotateModel()
        {
            if (_model == null) return;

            var spawnRotations = Enum.GetValues(typeof(SpawnRotation)).OfType<SpawnRotation>().ToArray();
            _spawnRotation = spawnRotations[Random.Range(0, spawnRotations.Length)];

            transform.Rotate(0f, 0f, (int) _spawnRotation);
        }
        
        /// <summary>
        /// Randomly position the model along the top of the screen.
        /// </summary>
        private void PositionModel()
        {
            if (_model == null) return;

            // Find the spawn bounds for the current rotation.
            var spawnBound = _model.SpawnBound(_spawnRotation);
            
            // Calculate the random x and y positions
            var randomX = Random.Range(spawnBound.Lower.x, spawnBound.Upper.x) * PositionByBrick;
            var randomY = Random.Range(spawnBound.Lower.y, spawnBound.Upper.y) * PositionByBrick;

            // Line
            // 0  lower (-3, 0) upper (3, 0)
            // 90 lower (-3, 0) upper (0, 0)
            // 180 lower (-3, -3) upper (3, 0)
            // 270 lower (0, 0) upper (3, 0)

            // Take the starting point and the move it into the random bounds.
            _startingPosition = transform.position = new Vector3(randomX, BasePositionY + randomY, 0);
            
            // Reset movement
            _movement = 0;
        }

        /// <summary>
        /// Enable the model so its glory can be seen by all.
        /// </summary>
        private void EnableModel()
        {
            if (_model == null) return;
            _model.gameObject.SetActive(true);
        }
    }
}
