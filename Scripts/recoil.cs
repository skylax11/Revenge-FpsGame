using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoil : MonoBehaviour
{
    [SerializeField] Vector3 MaxTargetPos;
    [SerializeField] Vector3 MinTargetPos;

    [SerializeField] Quaternion MinTargetRot;
    [SerializeField] Quaternion MaxTargetRot;

    Quaternion TargetRot;
    Quaternion OrgRot;

    [SerializeField] Transform objct;
    Vector3 TargetPos;
    Vector3 OrgPos;

    Vector3 SlideVector;
    Quaternion SlideRot;

    [SerializeField] float slideSpeed;
    [SerializeField] float lerpSpeed;
    [SerializeField] bool lerp;
    private void Start()
    {
        OrgPos = objct.localPosition;
        OrgRot = objct.localRotation;
    }
    private void Update()
    {
        if (lerp)
        {
            SlideVector = Vector3.MoveTowards(SlideVector, TargetPos, slideSpeed * Time.deltaTime);
            SlideRot = Quaternion.RotateTowards(SlideRot, TargetRot, slideSpeed * Time.deltaTime * 7f);
            if (SlideVector == TargetPos)
            {
                lerp = false;
            }
        }
        else
        {
            SlideVector = Vector3.MoveTowards(SlideVector, OrgPos, slideSpeed * Time.deltaTime);
            SlideRot = Quaternion.RotateTowards(SlideRot, OrgRot, slideSpeed * Time.deltaTime * 7f);

        }
        objct.localPosition = Vector3.Lerp(objct.localPosition, SlideVector, lerpSpeed * Time.deltaTime);
        objct.localRotation = Quaternion.Lerp(objct.localRotation, SlideRot, lerpSpeed * Time.deltaTime * 7f);
    }
    public void SetTarget()
    {
        if (!WeaponManager.Instance.aim)
        {
            TargetPos = OrgPos + new Vector3(Random.Range(MinTargetPos.x, MaxTargetPos.x), Random.Range(MinTargetPos.y, MaxTargetPos.y), Random.Range(MinTargetPos.z, MaxTargetPos.z));
            TargetRot = OrgRot * Quaternion.Euler(Random.Range(MinTargetRot.eulerAngles.x, MaxTargetRot.eulerAngles.x), Random.Range(MinTargetRot.eulerAngles.y, MaxTargetRot.eulerAngles.y), Random.Range(MinTargetRot.eulerAngles.z, MaxTargetRot.eulerAngles.z));
            lerp = true;
        }
        else
        {
            TargetPos = OrgPos + new Vector3(0, 0, Random.Range(MinTargetPos.z, MaxTargetPos.z));
            TargetRot = OrgRot * Quaternion.Euler(0, 0, Random.Range(MinTargetRot.eulerAngles.z, MaxTargetRot.eulerAngles.z));
            lerp = true;
        }

    }
}
