using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] Transform headParent;
    [Header("Variables")]
    [SerializeField] float bobFreq;
    [SerializeField] float horizontalMagnitude;
    [SerializeField] float verticalMagnitude;
    [SerializeField] float lerpSpeed;

    private float walkingTime;
    private Vector3 TargetVector;


    private void Update()
    {
        setHeadBob();
    }
    void setHeadBob()
    {
        if (!CharacterMovement.instance.isWalking && !CharacterMovement.instance.isRunning)
        {
            walkingTime = 0f;
        }
        else
        {
            walkingTime += Time.deltaTime;
        }
        TargetVector = headParent.position + SetOffSet(walkingTime);
        head.position = Vector3.Lerp(head.position, TargetVector, lerpSpeed * Time.deltaTime);
        if ((head.position - TargetVector).magnitude <= 0.001f)
        {
            head.position = TargetVector;
        }
    }
    Vector3 SetOffSet(float time)
    {
        float horizontalOffSet = 0f;
        float verticalOffSet = 0f;
        Vector3 offset = Vector3.zero;
        if (time > 0f)
        {
            horizontalOffSet = Mathf.Cos(time * bobFreq*CharacterMovement.instance.TotalSpeed()) * horizontalMagnitude;
            verticalOffSet = Mathf.Sin(time * bobFreq * 2f * CharacterMovement.instance.TotalSpeed()) * verticalMagnitude;
            offset = headParent.right * horizontalOffSet + headParent.up * verticalOffSet;
        }
        return offset;
    }


}
