using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {

        public Vector2 MoveInput { get; private set; }

        public void OnMovement(InputValue value)
        {
            MoveInput = value.Get<Vector2>();
        }
    }
}