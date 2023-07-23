using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectInteraction : MonoBehaviour
{
    public static objectInteraction instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] Transform Camera;
    [SerializeField] float interactionDistance;
    RaycastHit hit;

    private void Update()
    {
        if(Physics.Raycast(Camera.position,Camera.forward,out hit,interactionDistance))
        {
            if(hit.transform.GetComponent<IInteractable>() != null)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<IInteractable>().interact();
                }
            }
        }
    }

}
