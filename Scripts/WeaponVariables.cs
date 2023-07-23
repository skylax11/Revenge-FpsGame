using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVariables : MonoBehaviour
{
    public string WeaponID;
    public Transform WeaponParent;
    [Header("Animations")]
    public AnimationController anm;
    [Header("Fire Variables")]
    public float fireFreq;
    public float fireRange;
    [Header("Reload Variables")]
    public int CurrentAmmo;
    public int maxAmmo;
    public int totalAmmo;
    public int BulletAtOnce;

    public WeaponManager.Ammo_Types type;
    [Header("Muzzle Flash")]
    public Transform WeaponTip;
    public GameObject MuzzleFlash;
    public ParticleSystem BulletShell;
    [Header("Aim")]
    public bool aim;
    public Vector3 orgPose;
    public Vector3 AimPose;

    public Quaternion orgRot;
    public Quaternion aimRot;

    public float aimSpeed;

    public float originalFOV;
    public float aimFOV;
    [Header("Bullet Scatter")]

    public Quaternion MaxScatterr;
    public Quaternion MinScatter;
    public Quaternion CurrentScatter;
    [Header("Recoil")]
    public Vector2 MaxRecoil;
    public Vector2 MinRecoil;
    [Header("Throw Weapon")]
    public GameObject pickableWep;
    [Header("Sound")]
    public AudioClip shot;
    public AudioClip reload;

}
