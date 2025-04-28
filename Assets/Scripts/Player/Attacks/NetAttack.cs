using System;
using UnityEngine;
using Utility;

namespace Player.Attacks
{
    public class NetAttack : PlayerAttack
    {
        [SerializeField] private float _speed = 2;

        [SerializeField] private AnimationCurve _scaleCurve;
        [SerializeField] private AnimationCurve _yCurve;

        [SerializeField] private Transform _netTransform;

        private Lifetime _lifetime;
        private void Start()
        {
            _lifetime = GetComponent<Lifetime>();
            Vector3 localeulers = GameObject.FindWithTag("PlayerArt").transform.localEulerAngles;
            
            transform.rotation = Quaternion.Euler(0, localeulers.y, 0);

            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;
        }
        
        private void Update()
        {
            Vector3 pos = transform.localPosition;
            pos += transform.forward * Time.deltaTime * _speed;
            transform.localPosition = pos;

            float scaleOneAxis = _scaleCurve.Evaluate(_lifetime.GetLifetimeRatio());
            Vector3 scale = new Vector3(scaleOneAxis, scaleOneAxis, scaleOneAxis);
            _netTransform.localScale = scale;
            
            float newY = _yCurve.Evaluate(_lifetime.GetLifetimeRatio());
            
            Vector3 localPosition = _netTransform.localPosition;
            localPosition.y = newY;
            _netTransform.localPosition = localPosition;
        }
    }
}