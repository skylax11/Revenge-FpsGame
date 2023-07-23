using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManagement : MonoBehaviour
{
    [SerializeField] Volume vignet1;
    VolumeProfile profile;
    Vignette vg;
    bool canHeal= false;
    public static VolumeManagement instance;
    ShadowsMidtonesHighlights shadow;
    [Range(0.0f, 0.6f)]
    public float value = 0.1f;
    public float value2 = 1f;

    private void Awake()
    {
        instance = this;
    }
    public void check()
    {
        if (healtRunScript.Instance.can <= 50)
        {
            
        }
        
    }
    void Start()
    {
        value = Mathf.Clamp(value, 0.0f, 0.6f);
        vignet1.profile.TryGet(out shadow);
        vignet1.profile.TryGet(out vg);
    }

    // Update is called once per frame
    void Update()
    {
        if (healtRunScript.Instance.can <= 60 && WeaponManager.Instance.realizedCount > 0)
        {
            while (value <= 0.6f)
            {
                value += Time.deltaTime * 0.2f;
                break;
            }
            if (value < 0.6f)
            {
                vg.intensity.Override(value);
            }
            canHeal = true;

        }
        if (WeaponManager.Instance.realizedCount == 0 && canHeal == true)
        {

            while (value > 0f)
            {
                value -= Time.deltaTime * 0.2f;
                break;
            }
            if (value > 0f)
            {
                vg.intensity.Override(value);
            }
        }

    }
    public void initiateIt()
    {
        vg.color.Override(Color.black);
        InvokeRepeating("end", 0.2f, 0.1f);
    }
    public void end()
    {
        print("a");
        shadow.active = true;

        while (value2 > 0f)
        {
            value2 -= Time.deltaTime *1.5f;
            break;
        }
        shadow.shadows.Override(new Vector4(value2, value2, value2, value2));
        shadow.midtones.Override(new Vector4(value2, value2, value2, value2));
        if (value2 <= 0f)
        {
            PassData.Instance.passTheData();
        }
    }
}
