using System.Linq;
using UnityEngine;

namespace Player
{
    // TODO - Add sound
    public class Death : MonoBehaviour
    {
        /// <summary>
        /// Number of seconds each death scene is displayed for.
        /// </summary>
        private const float DeathSceneLength = 0.05f;

        /// <summary>
        /// The death models available.
        /// </summary>
        private GameObject[] _models;

        /// <summary>
        /// The current death model.
        /// </summary>
        private int _model;

        /// <summary>
        /// Amount of time expended being dead.
        /// </summary>
        private float _time;

        /// <summary>
        /// Number of death scenes we've displayed
        /// </summary>
        private int _loop = 1;

        private void Awake()
        {
            _models = transform.Cast<Transform>().Select(_ => _.gameObject).ToArray();
        }

        private void Start()
        {
            _models[_model].SetActive(true);
        }

        private void Update()
        {
            _time += Time.deltaTime;

            // Nothing to do until the scene time is up.
            if (_time < DeathSceneLength * _loop) return;

            // Deactivate the old model.
            _models[_model].SetActive(false);
            
            // Increase the model or reset.
            if (_model < _models.Length - 1)
            {
                _model += 1;
            }
            else
            {
                _model = 0;
            }
            
            // Activate the new model
            _models[_model].SetActive(true);

            _loop += 1;
        }
    }
}