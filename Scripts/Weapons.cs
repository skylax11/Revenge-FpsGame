using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject item;
    [SerializeField] WeaponVariables changeToHand;
    public string weaponName;
    public GameObject ownKey;
    string IInteractable.name { get => weaponName; set => weaponName = value; }

    void IInteractable.interact()
    {
        if (WeaponManager.Instance.wepId == "Key")
        {
            if (weaponName == "Door")
            {

                item.GetComponent<Animator>().SetBool("kapiAc", true);

                if (WeaponManager.Instance.slot1.WeaponID == "Key")
                {

                    WeaponManager.Instance.changeWeapon(changeToHand);
                    SignScript.Instance.slot1.enabled = false;
                    WeaponManager.Instance.slot1 = changeToHand;
                }
                else if (WeaponManager.Instance.slot2.WeaponID == "Key")
                {

                    WeaponManager.Instance.changeWeapon(changeToHand);
                    SignScript.Instance.slot2.enabled = false;

                    WeaponManager.Instance.slot2 = changeToHand;
                }
                StartCoroutine("kapiKapa");

            }
            else
            {
                WeaponManager.Instance.swapWeapon(weaponName);
                SignScript.Instance.equipWep(weaponName);
                Destroy(this.gameObject);
            }
        }
        else if(weaponName !="Door")
        {
            
            WeaponManager.Instance.swapWeapon(weaponName);
            SignScript.Instance.equipWep(weaponName);
            Destroy(this.gameObject);
        }

    }
    IEnumerator kapiKapa()
    {
        yield return new WaitForSeconds(1f);
        item.GetComponent<Animator>().SetBool("kapiAc", false);

    }
}
