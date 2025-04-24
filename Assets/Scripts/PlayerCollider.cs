using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public GameManager gameManager;

    private void Update()
    {
        if(gameManager.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameManager.TakeDamage(20);
        }
        if (other.gameObject.tag == "SlowEnemy")
        {
            gameManager.TakeDamage(30);
        }
        if (other.gameObject.tag == "FastEnemy")
        {
            gameManager.TakeDamage(10);
        }
    }
}
