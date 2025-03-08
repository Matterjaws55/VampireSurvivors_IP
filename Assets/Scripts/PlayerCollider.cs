using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public GameManager gameManager;

    private void Update()
    {
        if(gameManager.healthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameManager.TakeDamage(10);
        }
    }
}
