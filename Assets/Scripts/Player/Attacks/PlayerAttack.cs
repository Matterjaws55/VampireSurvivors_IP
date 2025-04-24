using System;
using UnityEngine;

namespace Player.Attacks
{
    public class PlayerAttack : MonoBehaviour
    {
        private void Start()
        {
            Vector3 pos = transform.position;
            pos.y = 0;
            
            transform.position = pos;
        }
    }
}