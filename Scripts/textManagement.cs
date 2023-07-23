using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textManagement : MonoBehaviour
{
    GameObject PassData;
    void Start()
    {
        PassData = GameObject.Find("PassData");
        gameObject.GetComponent<TextMeshProUGUI>().text = PassData.GetComponent<PassData>().message;
    }

    void Update()
    {
        
    }
}
