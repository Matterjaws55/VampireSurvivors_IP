using UnityEngine;

namespace Player
{
    public class RotateInDirectionOfMovement : MonoBehaviour
    {
        private Vector3 _lastPosition;
        [SerializeField] private float _rotationSpeed = 10f;
        
        private void Start()
        {
            _lastPosition = transform.position;
        }

        private void Update()
        {
            Vector3 movementDirection = (transform.position - _lastPosition).normalized;
            
            if (movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            }
            
            _lastPosition = transform.position;
        }
    }
}
