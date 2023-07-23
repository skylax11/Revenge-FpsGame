using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPartsofBody : MonoBehaviour
{
    void Start()
    {
        
    }
    public void dusmanCanDus(string tag,string weapon, GameObject hit)
    {

        if (hit.transform.root.CompareTag("harmless"))
        {
            if (tag == "enemy_head")
            {
                gameObject.GetComponentInParent<enemy_harmless>().dusmanCanDus(hit);
            }
            else if (tag == "enemy_otherparts")
            {
                gameObject.GetComponentInParent<enemy_harmless>().dusmanCanDus(hit);
            }
            else if (tag == "enemy_chest")
            {
                gameObject.GetComponentInParent<enemy_harmless>().dusmanCanDus(hit);
            }
        }
        else
        {
            if (tag == "enemy_head")
            {
                gameObject.GetComponentInParent<enemyScript>().dusmanCanDus(100, "head", weapon, hit);
            }
            else if (tag == "enemy_otherparts")
            {
                gameObject.GetComponentInParent<enemyScript>().dusmanCanDus(Random.Range(30, 45), "otherparts", weapon, hit);
            }
            else if (tag == "enemy_chest")
            {
                gameObject.GetComponentInParent<enemyScript>().dusmanCanDus(Random.Range(40, 65), "chest", weapon, hit);
            }
        }

    }
    void Update()
    {
        
    }
}
