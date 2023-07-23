using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SignScript : MonoBehaviour
{
    [SerializeField] public Animator anm;
    [SerializeField] public Image slot1;
    [SerializeField] public Image slot2;
    [SerializeField] public Sprite ak47;
    [SerializeField] public Sprite pistol;
    [SerializeField] public Sprite sawOff;
    [SerializeField] public Sprite key;
    [SerializeField] public Sprite awp;

    public static SignScript Instance;
    public bool pistolBool = false;
    public bool ak47Bool = false;
    public bool shotgunBool = false;
    public bool keyBool= false;
    public bool awpBool = false;
    void Start()
    {

    }
    private void Awake()
    {
        Instance = this;
    }
    public void equipWep(string weaponID)
    {
        if (weaponID == "Shotgun" && ((pistolBool == false && ak47Bool == false) && (keyBool == false && awpBool == false)))
        {
            slot2.sprite = sawOff;
            slot2.GetComponent<Image>().enabled = true;
            shotgunBool = true;
        }
        else if (weaponID == "Shotgun" && ((pistolBool == true || ak47Bool == true) || (keyBool == true || awpBool == true)))
        {
            check(sawOff);
            shotgunBool = true;
        }
        if (weaponID == "Baretta" && ((shotgunBool == false && ak47Bool == false) && (keyBool == false && awpBool == false)))
        {

            slot2.sprite = pistol;
            slot2.GetComponent<Image>().enabled = true;
            pistolBool = true;
        }
        else if (weaponID == "Baretta" && ((shotgunBool == true || ak47Bool == true) || (keyBool == true || awpBool == true)))
        {
            check(pistol);
            pistolBool = true;
        }
        if (weaponID == "ak47" && ((pistolBool == false && shotgunBool == false) && (keyBool == false && awpBool == false)))
        {
            slot2.sprite = ak47;
            slot2.GetComponent<Image>().enabled = true;
            ak47Bool = true;
        }
        else if (weaponID == "ak47" && ((shotgunBool == true || pistolBool == true) || (keyBool == true || awpBool == true)))
        {
            check(ak47);
            ak47Bool = true;
        }
        if (weaponID == "awp" && ((pistolBool == false && shotgunBool == false) && (keyBool == false && ak47Bool == false)))
        {
            slot2.sprite = awp;
            slot2.GetComponent<Image>().enabled = true;
            awpBool = true;
        }
        else if (weaponID == "awp" && ((shotgunBool == true || pistolBool == true) || (keyBool == true || ak47Bool == true)))
        {
            check(awp);
            awpBool = true;
        }
        if (weaponID == "Key" && ((pistolBool == false && shotgunBool == false) && (ak47Bool == false && awpBool == false)))
        {
            slot2.sprite = key;
            slot2.GetComponent<Image>().enabled = true;
            keyBool = true;
        }
        else if (weaponID == "Key" && ((shotgunBool == true || pistolBool == true) || (ak47Bool == true || awpBool == true)))
        {
            check(key);
            keyBool = true;
        }
    }
    void check(Sprite sprite)
    {
        if (anm.GetBool("toSlot2") == true)
        {

            if (WeaponManager.Instance.slot2 != null && WeaponManager.Instance.slot2Onceki != "Hand")
            {
                print("f1");
                slot2.GetComponent<Image>().enabled = true;
                slot2.sprite = sprite;
            }
            else if (WeaponManager.Instance.slot2OncekiOnceki != "Hand")
            {
                print("f1");
                slot2.GetComponent<Image>().enabled = true;
                slot2.sprite = sprite;
            }
            else if (WeaponManager.Instance.slot2Onceki == "Hand")
            {
                print("f1");
                slot1.sprite = sprite;
                slot1.GetComponent<Image>().enabled = true;
            }
            else
            {
                print("f1");

                slot1.sprite = sprite;
                slot1.GetComponent<Image>().enabled = true;
            }
        }
        if (anm.GetBool("toSlot1") == true)
        {
            print(WeaponManager.Instance.slot1Onceki);

            if (WeaponManager.Instance.slot1 != null && WeaponManager.Instance.slot1Onceki != "Hand")
            {
                slot1.sprite = sprite;
                slot1.GetComponent<Image>().enabled = true;

            }
            else if (WeaponManager.Instance.slot1OncekiOnceki != "Hand")
            {
                slot1.sprite = sprite;
                slot1.GetComponent<Image>().enabled = true;
            }
            else if (WeaponManager.Instance.slot1Onceki == "Hand")
            {
                slot1.sprite = sprite;
                slot1.GetComponent<Image>().enabled = true;

            }
            else
            {
                slot2.sprite = sprite;
                print("f1");

                slot2.GetComponent<Image>().enabled = true;
            }
        }
    }
    void Update()
    {
        
    }
}
