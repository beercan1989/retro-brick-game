using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private GameObject _model;
        private GameObject _death;
        
        private void Awake()
        {
            _model = GetComponentInChildren<Rigidbody2D>().gameObject;
            _death = GetComponentInChildren<OnDeathAnimation>(true).gameObject;
        }

        private void Start()
        {
            _model.SetActive(true);
            _death.SetActive(false);
        }

        private void Update()
        {
        
        }
    }
}
