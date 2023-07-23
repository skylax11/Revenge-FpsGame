using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guntipscript : MonoBehaviour
{
    [SerializeField] Animator anm;
    void Start()
    {
        
    }
    public void setFalse()
    {
        anm.SetBool("gunTipPlay", false);
    }
    void Update()
    {
        
    }
}
