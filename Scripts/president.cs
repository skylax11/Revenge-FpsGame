using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class president : MonoBehaviour
{
    [SerializeField] enemy_harmless script;

    void Start()
    {
           
    }
    public void setDeadSituation()
    {
        PassData.Instance.hasKilled = true;
    }
    void Update()
    {
        
    }
}
