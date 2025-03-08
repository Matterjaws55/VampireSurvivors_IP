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
        transform.position = Vector3.MoveTowards(this.transform.position, player.position, 2.5f * Time.deltaTime);
    }
}
