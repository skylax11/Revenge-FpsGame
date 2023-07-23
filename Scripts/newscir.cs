using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class newscir : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject goal;

    void Start()
    {
        agent.SetDestination(goal.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
