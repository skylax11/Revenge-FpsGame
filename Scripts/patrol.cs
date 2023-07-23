using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrol : MonoBehaviour
{
    [SerializeField] public Animator anm;
    [SerializeField] Transform patrolPoint;
    [SerializeField] NavMeshAgent agent;
    void Start()
    {
    }
    public void StartPatrol()
    {
        agent.SetDestination(patrolPoint.transform.position);
    }

    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, patrolPoint.transform.position) <= 11)
        {
            anm.SetBool("initiatePatrol", false);
        }
    }
}
