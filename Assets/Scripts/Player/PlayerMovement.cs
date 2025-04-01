using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        private PlayerControls _playerControls;
        private CharacterController _characterController;
        [SerializeField]
        private float _moveSpeed = 5;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerControls = GetComponent<PlayerControls>();
        }
        void Start()
        {

        }

        void Update()
        {
            Vector3 moveInput = new Vector3(_playerControls.MoveInput.x, 0, _playerControls.MoveInput.y);
            moveInput *= _moveSpeed;
            moveInput *= Time.deltaTime;

            _characterController.Move(moveInput);
        }
    }
}
