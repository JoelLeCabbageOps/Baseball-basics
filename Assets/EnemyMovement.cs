using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent != null)
        {
             target = GameObject.FindGameObjectWithTag("Player").transform;
             agent.SetDestination(target.position);
        }
    }
}
