using System;
using UnityEngine;

namespace Utility
{
    public class Lifetime : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 1f;
        
        float _timer = 0f;
        private void Start()
        {
            Destroy(gameObject, _lifetime);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        public float GetLifetimeRatio()
        {
            return _timer/_lifetime;
        }
    }
}