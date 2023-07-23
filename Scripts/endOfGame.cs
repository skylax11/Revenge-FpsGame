using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endOfGame : MonoBehaviour
{
    [SerializeField] Animator anm;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("oyuncu"))
        {
            anm.SetBool("closeDoor", true);
            
            if (girdiMi == true)
            {
                VolumeManagement.instance.initiateIt();
                girdiMi = false;
            }
        }
    }
    bool girdiMi=true;
    public static endOfGame instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
