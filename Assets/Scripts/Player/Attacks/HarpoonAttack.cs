using UnityEngine;

namespace Player.Attacks
{
    using UnityEngine;

    namespace Player.Attacks
    {
        public class HarpoonAttack : MonoBehaviour
        {
            private Vector3 _attackDirection;

            [SerializeField] private float _speed = 20;
            private void Start()
            {
                Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
                Vector3 mousePosition = Input.mousePosition;
                _attackDirection = (mousePosition - screenCenter).normalized;

                _attackDirection.z = _attackDirection.y;
                _attackDirection.y = 0;
                _attackDirection *= _speed;
            }
            
            private void Update()
            {
                Vector3 pos = transform.localPosition;
                pos += _attackDirection * Time.deltaTime;
                transform.localPosition = pos;
            }
        }
    }
}