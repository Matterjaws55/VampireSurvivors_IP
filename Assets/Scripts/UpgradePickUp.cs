using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradePickUp : MonoBehaviour
{
    public static event Action OnUpgradePickedUp; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            OnUpgradePickedUp?.Invoke();

            Destroy(gameObject); 
        }
    }
}