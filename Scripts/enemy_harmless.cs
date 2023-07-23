using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_harmless : MonoBehaviour
{
    [SerializeField] public Animator anm;
    [SerializeField] GameObject dusman;
    [SerializeField] enemy_harmless yanýmdaki;

    public int can;
    void Start()
    {
        can = 1;
    }

    public void dusmanCanDus(GameObject hit)
    {

        Vector3 hitDirection = hit.transform.position - dusman.transform.position;

        foreach (var r in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            anm.enabled = false;
            r.isKinematic = false;
            r.useGravity = true;
            r.AddForce(hitDirection * 3f * 0.1f, ForceMode.Force);
        }
        hit.GetComponent<Rigidbody>().AddForce(hitDirection * 3f * 10f, ForceMode.Force);
        if (gameObject.GetComponent<president>() != null)
        {
            gameObject.GetComponent<president>().setDeadSituation();
        }
        if(yanýmdaki != null)
        {
             Notice();
        }
        
    }
    public void enableAnimation()
    {
        anm.SetBool("run", true);
    }
    public void Notice()
    {
        yanýmdaki.enableAnimation();
    }
    void Update()
    {
        
    }
}
