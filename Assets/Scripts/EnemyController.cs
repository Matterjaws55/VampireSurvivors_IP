using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        if (this.gameObject.tag == "Enemy")
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.position, 2.5f * Time.deltaTime);
        }
        else if (this.gameObject.tag == "SlowEnemy")
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.position, 1f * Time.deltaTime);
        }
        else if (this.gameObject.tag == "FastEnemy")
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.position, 4f * Time.deltaTime);
        }
    }
}
