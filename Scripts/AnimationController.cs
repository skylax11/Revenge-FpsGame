using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] enemy_harmless President;
    public Animator anm;
    void Start()
    {
        anm = GetComponent<Animator>();
    }

    void Update()
    {
     
    }
    #region Animation Functions
    public void SetReloadSituation()
    {
        WeaponManager.Instance.Reloading = false;
        print("merhabe");
    }
    public void First()
    {
        WeaponManager.Instance.Reloading = true;
        WeaponManager.Instance.SetAnimation(WeaponManager.Instance.anm, "reload", true);

    }
    public void First2()
    {
        WeaponManager.Instance.Reloading = true;
        WeaponManager.Instance.SetAnimation(WeaponManager.Instance.anm, "reload2", true);

    }
    public void End()
    {
        WeaponManager.Instance.Reloading = false;
        WeaponManager.Instance.SetAnimation(WeaponManager.Instance.anm, "reload", false);
    }
    public void End2()
    {
        WeaponManager.Instance.Reloading = false;
        WeaponManager.Instance.SetAnimation(WeaponManager.Instance.anm, "reload2", false);
    }
    public void EndShoot2()
    {
        anm.SetBool("shoot", false);
        anm.SetBool("shoot2", false);
    }
    public void shoot2()
    {
        if (WeaponManager.Instance.CurrentAmmo == 0 && WeaponManager.Instance.TotalAmmo > 0)
        {
            print("a");
            anm.SetBool("shoot2", true);
        }
        else if (WeaponManager.Instance.CurrentAmmo == 0 && WeaponManager.Instance.TotalAmmo == 0)
        {
            anm.SetBool("shoot2", true);
        }
    }
    public void ak47Cek()
    {
        WeaponManager.Instance.setSound(WeaponManager.Instance.ak47swap, "ak47");
        WeaponManager.Instance.ses.Play();
    }
    public void awpCek()
    {
        WeaponManager.Instance.setSound(WeaponManager.Instance.awpswap, "awp");
        WeaponManager.Instance.ses.Play();
    }
    public void awpCek2()
    {
        WeaponManager.Instance.setSound(WeaponManager.Instance.awpswap2, "awp");
        WeaponManager.Instance.ses.Play();
    }
    public void pistolCek()
    {
        WeaponManager.Instance.setSound(WeaponManager.Instance.pistolswap,"pistol");
        WeaponManager.Instance.ses.Play();
    }
    public void pompaliCek()
    {
        WeaponManager.Instance.setSound(WeaponManager.Instance.sawnoffswap,"shotgun");
        WeaponManager.Instance.ses.Play();
    }
    public void returnHand()
    {
        WeaponManager.Instance.changeWeapon(WeaponManager.Instance.slot1);
        print("RETURNHAND");
    }
    public void CheckPresident()
    {
        if (President.can > 0 && Input.GetMouseButton(0))
        {
            President.anm.SetBool("missed", true);
        }
    }
    #endregion
}
