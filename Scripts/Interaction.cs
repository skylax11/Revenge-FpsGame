using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] float ForceMagnitude;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb != null)
        {
            Vector3 forceDirection = hit.transform.position - this.transform.position;
            forceDirection.y = 0f;
            forceDirection.Normalize();
            rb.AddForceAtPosition(forceDirection * ForceMagnitude, this.transform.position, ForceMode.Impulse); 
        }
    }
}
