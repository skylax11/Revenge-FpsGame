using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initiatePatrol : MonoBehaviour
{
    [SerializeField] patrol[] patrol;
    public bool doItOnce = true;

    void Start()
    {
        print("benim ad :" + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("oyuncu") && doItOnce == true)
        {

            foreach (var thePatrol in patrol)
            {
                doItOnce = false;
                thePatrol.anm.SetBool("initiatePatrol", true);
                thePatrol.StartPatrol();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("oyuncu") && doItOnce == true)
        {

            foreach (var thePatrol in patrol)
            {
                doItOnce = false;
                thePatrol.anm.SetBool("initiatePatrol", true);
                thePatrol.StartPatrol();
            }
        }
    }
    void Update()
    {
        
    }
}
