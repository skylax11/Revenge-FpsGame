using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] RectTransform Crosshairx;

    [SerializeField] float MaxSize;
    [SerializeField] float MinSize;
    [SerializeField] float CurrentSize;

    [SerializeField] float speed;

    
    private void Update()
    {
        Inputs();
        setSize();
    }
    void setSize()
    {
        Crosshairx.sizeDelta = new Vector2(CurrentSize, CurrentSize);
    }
    void Inputs()
    {
     
        if (!CharacterMovement.instance.isWalking && !CharacterMovement.instance.isRunning)
        {
            setMinimum();
        }
        else if (CharacterMovement.instance.isWalking)
        {
            setMax();
        }
        if (CharacterMovement.instance.isRunning || WeaponManager.Instance.aim == true)
        {
            setDeActive();
        }
        else
        {
            if (WeaponManager.Instance.CurrentWeaponTransform.gameObject.GetComponent<WeaponVariables>().WeaponID == "Hand")
            {
                setDeActive();
            }
            else
            {
                setActive();
            }
        }
    }
    void setMinimum()
    {

        CurrentSize = Mathf.Lerp(CurrentSize, MinSize,speed*Time.deltaTime);
    }

    void setMax()
    {
        CurrentSize = Mathf.Lerp(CurrentSize, MaxSize, speed * Time.deltaTime);

    }
    void setActive()
    {
        Crosshairx.gameObject.GetComponentInParent<Canvas>().enabled = true; 
    }
    void setDeActive()
    {
        Crosshairx.gameObject.GetComponentInParent<Canvas>().enabled = false;
    }
}
