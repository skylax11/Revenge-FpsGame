using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    RaycastHit hit;
    public bool Reloading;
    [Header("Fire Variables")]
    [SerializeField] public int CurrentAmmo;
    
    [SerializeField] float fireFreq= 0.15f;
    float fireCounter;
    [Header("Reload Variables")]
    [SerializeField] public int MaxAmmo;
    [SerializeField] public int TotalAmmo;
    [SerializeField] Ammo_Types Type;
    [Header("Muzzle Flash")]
    [SerializeField] Transform WeaponTip;
    [SerializeField] GameObject MuzzleFlash;
    [SerializeField] ParticleSystem BulletShell;

    [Header("Indicaters")]
    public TextMeshProUGUI CurAmmo;
    public TextMeshProUGUI TotAmmo;

    public enum Ammo_Types
    {
        _5_56,
        _7_62,
        _9mm,
        _45cal,
        _12ga
    }
    [Header("Weapon Slots")]
    [SerializeField] public WeaponVariables slot1;
    [SerializeField] public WeaponVariables slot2;

    [Header("Ammo Types")]

    [SerializeField] int _5_56;
    [SerializeField] int _7_62;
    [SerializeField] int _9mm;
    [SerializeField] int _45cal;
    [SerializeField] int _12ga;
    [Header("Bullet Holes & Particles")]
    [SerializeField] GameObject[] BulletHoles;
    [SerializeField] GameObject[] Particles;

    [Header("Aim")]
    public bool aim;
    [SerializeField] Vector3 orgPose;
    [SerializeField] Vector3 AimPose;

    [SerializeField] Quaternion orgRot;
    [SerializeField] Quaternion aimRot;

    [SerializeField] float aimSpeed;

    [SerializeField] float originalFOV;
    [SerializeField] float aimFOV;
    [Header("Weapons")]
    [SerializeField] GameObject pistol;

    [SerializeField] Transform WeaponTransform;
    [SerializeField] public Transform CurrentWeaponTransform;
    [SerializeField] public WeaponVariables CurrentWeaponTransformVariables;

    [SerializeField] GameObject CameraPos;
    [Header("Bullet Scatter")]

    [SerializeField] Quaternion MaxScatterr;
    [SerializeField] Quaternion MinScatter;
    [SerializeField] Quaternion CurrentScatter;
    [Header("Recoil")]
    [SerializeField] Vector2 MaxRecoil;
    [SerializeField] Vector2 MinRecoil;
    [SerializeField] recoil CamRecoil;
    [Header("Animation")]
    [SerializeField] public AnimationController anm;
    [Header("Change Weapon")]
    [SerializeField] WeaponVariables[] allWeapons;
    [Header("Sounds")]
    [SerializeField] public AudioSource ses;
    [SerializeField] AudioClip shot;
    [SerializeField] AudioClip reload;
    [SerializeField] public AudioClip ak47swap;
    [SerializeField] public AudioClip pistolswap;
    [SerializeField] public AudioClip sawnoffswap;
    [SerializeField] public AudioClip awpswap;
    [SerializeField] public AudioClip awpswap2;

    public string wepId;
    [SerializeField] public int BulletAtOnce;
    public bool ak47Birak = false;
    public string slot1Onceki;
    public string slot2OncekiOnceki;
    public string slot1OncekiOnceki;
    public string slot2Onceki;
    public bool haveEverShooted = false;
    public int realizedCount= 0;


    public void setSound(AudioClip clip, string type)
    {
        StartCoroutine("Set_haveEverShooted", 0.1f);
        if (type == "movement" && ses.isPlaying == false)
        {

            ses.clip = clip;
            ses.Play();
        }
        else if (type != "movement")
        {
            if (type == "shot")
            {
                haveEverShooted = true;
            }
            ses.clip = clip;
            ses.Play();

        }

    }
    IEnumerator Set_haveEverShooted(float time)
    {
        yield return new WaitForSeconds(time);
        haveEverShooted = false;
    }
    void SetRecoil()
    {
        float x = Random.Range(MaxRecoil.x, MinRecoil.x);
        float y = Random.Range(MaxRecoil.y, MinRecoil.y);
        MouseLook.Instance.recoil(x, y);

    }
    public void changeWeapon(WeaponVariables weapon)
    {
        if (weapon.WeaponParent != CurrentWeaponTransform)
        {
            CurrentWeaponTransform.gameObject.SetActive(false);
            weapon.WeaponParent.gameObject.SetActive(true);

            CurrentWeaponTransform.GetComponent<WeaponVariables>().CurrentAmmo = CurrentAmmo;
            BulletAtOnce = weapon.BulletAtOnce;
            CurrentWeaponTransform = weapon.WeaponParent;
            CurrentWeaponTransformVariables = weapon;
            CurrentAmmo = weapon.CurrentAmmo;
            anm = weapon.anm;
            wepId = weapon.WeaponID;
            fireFreq = weapon.fireFreq;
            TotalAmmo = weapon.totalAmmo;
            MaxAmmo = weapon.maxAmmo;
            Type = weapon.type;
            WeaponTip = weapon.WeaponTip;
            MuzzleFlash = weapon.MuzzleFlash;
            BulletShell = weapon.BulletShell;
            orgPose = weapon.orgPose;
            AimPose = weapon.AimPose;
            orgRot = weapon.orgRot;
            aimRot = weapon.aimRot;
            aimSpeed = weapon.aimSpeed;
            originalFOV = weapon.originalFOV;
            aimFOV = weapon.aimFOV;
            MaxScatterr = weapon.MaxScatterr;
            MinScatter = weapon.MinScatter;
            MaxRecoil = weapon.MaxRecoil;
            MinRecoil = weapon.MinRecoil;
            shot = weapon.shot;
            reload=weapon.reload;
        }
    }
    public void swapWeapon(string wepName)
    {
        WeaponVariables selectedWep = null;
        for (int i = 0; i < allWeapons.Length; i++)
        {
            if (allWeapons[i].WeaponID == wepName)
            {
                selectedWep = allWeapons[i];
            }
        }
        if (slot2 == null)
        {
            SignScript.Instance.anm.SetBool("toSlot2", true);
            slot2 = selectedWep;
            changeWeapon(slot2);
        }
        else
        {
            if (CurrentWeaponTransformVariables.WeaponID == slot1.WeaponID)
            {
                dropWeapon(slot1);
                slot1OncekiOnceki = slot1Onceki;
                slot1Onceki = slot1.WeaponID;
                slot1 = selectedWep;
                changeWeapon(slot1);
            }
            else if (CurrentWeaponTransformVariables.WeaponID == slot2.WeaponID)
            {
                dropWeapon(slot2);
                slot2OncekiOnceki = slot2Onceki;
                slot2Onceki = slot2.WeaponID;
                slot2 = selectedWep;
                changeWeapon(slot2);
            }
        }
    }
    void dropWeapon(WeaponVariables wep)
    {
        GameObject pickableWep = Instantiate(wep.pickableWep, CurrentWeaponTransform.position, CurrentWeaponTransform.rotation);
        pickableWep.GetComponent<Rigidbody>().AddForce(pickableWep.transform.position*Time.deltaTime);
    }
    Quaternion SetScatter()
    {
        if (CharacterMovement.instance.isWalking)
        {
            CurrentScatter = Quaternion.Euler(Random.Range(-MaxScatterr.eulerAngles.x, MaxScatterr.eulerAngles.x), Random.Range(-MaxScatterr.eulerAngles.y, MaxScatterr.eulerAngles.y), Random.Range(-MaxScatterr.eulerAngles.z, MaxScatterr.eulerAngles.z));

        }
        else if (aim)
        {
            if (wepId != "Shotgun")
            {
                CurrentScatter = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                CurrentScatter = Quaternion.Euler(Random.Range(-MinScatter.eulerAngles.x+1, MinScatter.eulerAngles.x-1), Random.Range(-MinScatter.eulerAngles.y+1, MinScatter.eulerAngles.y-1), Random.Range(-MinScatter.eulerAngles.z+1, MinScatter.eulerAngles.z-1));
            }
        }
        else
        {
            CurrentScatter = Quaternion.Euler(Random.Range(-MinScatter.eulerAngles.x, MinScatter.eulerAngles.x), Random.Range(-MinScatter.eulerAngles.y, MinScatter.eulerAngles.y), Random.Range(-MinScatter.eulerAngles.z, MinScatter.eulerAngles.z));
        }
        return CurrentScatter;
    }

    void CreateMuzzle()
    {
        GameObject muzzle = Instantiate(MuzzleFlash, WeaponTip.transform.position, WeaponTip.transform.rotation, WeaponTip);
        muzzle.GetComponent<ParticleSystem>().Play();
        Destroy(muzzle, 4f);
        BulletShell.Play();
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        changeWeapon(slot1);

    }
    private void Update()
    {
        Inputs();
        SetTotalAmmo();
        SetAim();
    }
    IEnumerator setFalse(float sec)
    {
        yield return new WaitForSeconds(sec);
        SetAnimation(anm, "shoot", false);
    }
    void Inputs()
    {
        WeaponTransform.localRotation = MouseLook.Instance.CameraParent.localRotation;
        CurAmmo.text = CurrentAmmo.ToString();
        TotAmmo.text = TotalAmmo.ToString();

        if ((Input.GetMouseButton(0) && Time.time > fireCounter) && CurrentAmmo > 0)
        {
            if (!Reloading)
            {
               SetAnimation(anm, "shoot", true);
               StartCoroutine("setFalse", 0.2f);
               Shoot();
               setSound(shot,"shot");
            }

        }
        if (Input.GetKeyDown(KeyCode.R) && TotalAmmo > 0)
        {
            if (CurrentWeaponTransform.gameObject.GetComponent<WeaponVariables>().WeaponID == "Shotgun")
            {
                setSound(reload,"reload");

                if (CurrentAmmo == 1)
                {
                    SetAnimation(anm, "reload", true);
                    Reload();
                }
                else
                {
                    SetAnimation(anm, "reload2", true);
                    Reload();
                }
            }
            else
            {
               SetAnimation(anm, "reload", true);
               setSound(reload,"reload");
               Reload();
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetAimBool();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && slot1!=null)
        {
            if (slot1.WeaponID == "Hand" && slot2.WeaponID == "ak47")
            {
                SetAnimation(anm, "hand", true);
                changeWeapon(slot1);

            }
            else if (slot1.WeaponID == "Hand" && slot2.WeaponID == "Baretta")
            {
                SetAnimation(anm, "hand", true);
                changeWeapon(slot1);
            }
            else
            {
                changeWeapon(slot1);
            }
                SignScript.Instance.anm.SetBool("toSlot1", true);
                SignScript.Instance.anm.SetBool("toSlot2", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && slot2 !=null)
        {
            changeWeapon(slot2);
            SignScript.Instance.anm.SetBool("toSlot2", true);
            SignScript.Instance.anm.SetBool("toSlot1", false);
        }
    }
    void Shoot()
    {
        fireCounter = fireFreq + Time.time;
        CreateMuzzle();
        CurrentAmmo--;

        for(int z =0 ; z < BulletAtOnce;  z++)
        {
            if (Physics.Raycast(CameraController.instance.Camera.position,SetScatter()*CameraController.instance.Camera.forward, out hit, Mathf.Infinity))
            {

            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                if (hit.transform.CompareTag("Metal"))
                {
                GameObject copybulletHole = Instantiate(BulletHoles[Random.Range(0, BulletHoles.Length)].gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                copybulletHole.transform.parent = hit.transform;
                Destroy(copybulletHole, 10f);
                }
                

                for (int i = 0; i < Particles.Length; i++)
                {
                    if (Particles[i].tag == hit.transform.tag)
                    {
                        GameObject particle = Instantiate(Particles[i].gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(particle, 3f);

                    }
                }
                    if (hit.transform.CompareTag("enemy_head") || hit.transform.CompareTag("enemy_otherparts") || hit.transform.CompareTag("enemy_chest"))
                    {
                        hit.transform.GetComponent<enemyPartsofBody>().dusmanCanDus(hit.transform.tag, wepId, hit.transform.gameObject);
                    }
                    else
                    {
                        print("girdi");
                        hit.transform.GetComponent<Rigidbody>().AddForce(200f * -hit.normal);
                    }

            }
                SetRecoil();
                CamRecoil.SetTarget();

            }
        }
        


    }
    public void Reload()
    {
        int Amount = SetReloadAmount(TotalAmmo);
        CurrentAmmo += Amount;
        if (Type == Ammo_Types._7_62)
        {
            _7_62 -= Amount;
        }
        if (Type == Ammo_Types._5_56)
        {
             _5_56 -= Amount;
        }
        if (Type == Ammo_Types._9mm)
        {
            _9mm -= Amount;
        }
        if (Type == Ammo_Types._45cal)
        {
             _45cal -= Amount;
        }
        if (Type == Ammo_Types._12ga)
        {
            _12ga -= Amount;
        }
    }
    void SetTotalAmmo()
    {
        if (Type == Ammo_Types._7_62)
        {
            TotalAmmo = _7_62;
        }
        if (Type == Ammo_Types._5_56)
        {
            TotalAmmo = _5_56;
        }
        if (Type == Ammo_Types._9mm)
        {
            TotalAmmo = _9mm;
        }
        if (Type == Ammo_Types._45cal)
        {
            TotalAmmo = _45cal;
        }
        if (Type == Ammo_Types._12ga)
        {
            TotalAmmo = _12ga;
        }

    }
    int SetReloadAmount(int Invamount)
    {
        int AmountNeeded = MaxAmmo - CurrentAmmo;
        if (AmountNeeded < Invamount)
        {
            return AmountNeeded;
        }
        else
        {
            return Invamount;
        }
    }
    void SetAimBool()
    {
        aim = !aim;
    }
    void SetAim()
    {
        if (aim)
        {
            CurrentWeaponTransform.localPosition = Vector3.Lerp(CurrentWeaponTransform.localPosition, AimPose, aimSpeed * Time.deltaTime);
            CurrentWeaponTransform.localRotation = Quaternion.Lerp(CurrentWeaponTransform.localRotation, aimRot, aimSpeed * Time.deltaTime);
            CameraPos.GetComponent<Camera>().fieldOfView = Mathf.Lerp(CameraPos.GetComponent<Camera>().fieldOfView, aimFOV, aimSpeed * Time.deltaTime);

            SetAnimation(CurrentWeaponTransform.gameObject.GetComponent<WeaponVariables>().anm, "aim", true);
        }
        else
        {
            CurrentWeaponTransform.localPosition = Vector3.Lerp(CurrentWeaponTransform.localPosition, orgPose, aimSpeed * Time.deltaTime);
            CurrentWeaponTransform.localRotation = Quaternion.Lerp(CurrentWeaponTransform.localRotation, orgRot, aimSpeed * Time.deltaTime);
            CameraPos.GetComponent<Camera>().fieldOfView = Mathf.Lerp(CameraPos.GetComponent<Camera>().fieldOfView, originalFOV, aimSpeed * Time.deltaTime);


            SetAnimation(CurrentWeaponTransformVariables.anm, "aim", false);

        }
    }
    public void SetAnimation(AnimationController from, string name, bool state)
    {
        from.anm.SetBool(name, state);
    }

}
