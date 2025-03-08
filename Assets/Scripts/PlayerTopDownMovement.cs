using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTopDownMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    ParticleSystem clickEffect;
    [SerializeField]
    LayerMask clickableLayers;

    public float rotationSpeed = 8f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        HandleMouseInput();
        FaceTarget();
    }

    void HandleMouseInput()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) 
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
            {
                agent.destination = hit.point;
                Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}