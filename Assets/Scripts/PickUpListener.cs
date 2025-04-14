using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupListener : MonoBehaviour
{
    public PickUp pickup;

    void Start()
    {
        if (pickup != null)
        {
            pickup.OnPickedUp += HandlePickup;
        }
    }

    void HandlePickup()
    {
        Debug.Log("Pickup collected!");
    }

    void OnDestroy()
    {
        if (pickup != null)
        {
            pickup.OnPickedUp -= HandlePickup;
        }
    }
}

