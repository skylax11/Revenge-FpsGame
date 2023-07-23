using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
