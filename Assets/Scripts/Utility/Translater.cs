using System;
using UnityEngine;

namespace Utility
{
    public class Translater : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _velocity;

        private void Update()
        {
            Vector3 pos = transform.localPosition;
            pos += _velocity * Time.deltaTime;
            transform.localPosition = pos;
        }
    }
}