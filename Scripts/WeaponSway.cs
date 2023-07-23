using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float slerpSpeed;
    [SerializeField] float intensity;
    [SerializeField] float Aimintensity;

    private void Update()
    {
        sway();
    }
    void sway()
    {
        float x = Input.GetAxis("Mouse X") * totalIntensity();
        float y = Input.GetAxis("Mouse Y") * totalIntensity();
        Quaternion xrot = Quaternion.AngleAxis(-y, Vector3.right);
        Quaternion yrot = Quaternion.AngleAxis(x, Vector3.up);
        Quaternion rot = xrot * yrot;
        weapon.localRotation = Quaternion.Lerp(weapon.localRotation, rot, slerpSpeed * Time.deltaTime);

    }
    float totalIntensity()
    {
        if (WeaponManager.Instance.aim)
        {
            return Aimintensity;
        }
        else
        {
            return intensity;
        }
    }

}
