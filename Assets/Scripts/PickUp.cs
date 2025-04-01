using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public delegate void PickedUpAction();
    public event PickedUpAction OnPickedUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            
            OnPickedUp?.Invoke();
            Destroy(gameObject); 
        }
    }
}