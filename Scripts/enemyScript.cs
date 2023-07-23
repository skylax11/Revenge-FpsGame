using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class enemyScript : MonoBehaviour
{
    bool once3 = true;
    public bool injured = false;
    public bool realized = false;
    bool once=false;
    public int can;
    [SerializeField] Animator anm;
    [Header("Weapon")]
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject dusman;
    [SerializeField] GameObject Enemycamera;
    [Header("Rig")]
    [SerializeField] Rig aimlay;
    [SerializeField] Rig aimBody;
    [SerializeField] Rig wepPose;
    [SerializeField] Rig aimHands;
    RaycastHit hit;
    RaycastHit hit2;

    [SerializeField] GameObject drawCircle;
    [SerializeField] GameObject aim;

    [SerializeField] GameObject gunTip;
    [SerializeField] AudioSource gunSound;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float Accuracy;
    float powerMagnitude;
    public bool once2 = false;
    [Header("Realize")]
    [SerializeField] float realizeDistance;
    [SerializeField] float realizeByShootDistance;



    void Start()
    {
        agent.height = 0.5f;
        agent.baseOffset = 0;
        can = 100;
    }
    public void dusmanCanDus(int damage,string deadType,string weapon,GameObject hit)
    {
        can -= damage;

        if (deadOlduMu(deadType, weapon, hit))
        {

        }
        else
        {
            realized = true;
            if ((((can > 0 && can <= 70) && deadType != "enemy_head") && injured == false))
            {
                anm.SetBool("injured", true);
                injured = true;
                StartCoroutine("runOnce");
            }
        }
    }
    IEnumerator runOnce()
    {
        yield return new WaitForSeconds(1f);
        anm.SetBool("injured", false);
    }
    float calculateDistance(GameObject x, GameObject y)
    {
        return Vector3.Distance(x.transform.position, y.transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(drawCircle.transform.position, 10f);
    }
    void RealizeByDistance()
    {
        if (calculateDistance(gameObject, dusman) <= realizeDistance)
        {
            realized = true;
        }
    }
    void RealizeByFire()
    {
        if (WeaponManager.Instance.haveEverShooted == true)
        {
            print("d");
            if (calculateDistance(gameObject, dusman) <= realizeByShootDistance && WeaponManager.Instance.haveEverShooted == true)
            {
                print("d");

                realized = true;
            }
        }
    }
   
    void Update()
    {

        if (CharacterMovement.instance.isSlowly == true)
        {
            if (once2 == false)
            {
                foreach (var x in Physics.OverlapSphere(drawCircle.transform.position, 10f))
                {
                    if (x.name == "newCharacter")
                    {
                        if (Physics.Linecast(aim.transform.position, dusman.transform.position, out hit2))
                        {
                            if (hit2.transform.name == "newCharacter")
                            {
                                once2 = true;
                                realized = true;
                            }
                        }

                    }
                }
                RealizeByFire();

            }

        }
        if(CharacterMovement.instance.isSlowly == false || realized == true)
        {
            RealizeByDistance();
            RealizeByFire();
            if ((Vector3.Distance(gameObject.transform.position, dusman.transform.position) <= realizeDistance || realized == true) && can > 0)
            {
                aimlay.weight += 0.1f;
                aimBody.weight += 0.1f;


                Vector3 playerPos = dusman.transform.position;
                Vector3 npcPos = gameObject.transform.position;
                Vector3 delta = new Vector3(playerPos.x - npcPos.x, 0.0f, playerPos.z - npcPos.z);
                Quaternion rotation = Quaternion.LookRotation(delta);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime);

                aimlay.weight += Time.deltaTime * 4f;
                aimBody.weight += Time.deltaTime * 5f;
                anm.SetBool("realized", true);
                if (once == false)
                {
                    Enemycamera.GetComponentInChildren<WeaponManager>().realizedCount++;

                    InvokeRepeating("InitiateFire", 0.1f, 0.4f);
                    InvokeRepeating("setAim", 0.1f, 1f * Accuracy);  // Accuracy rate increases when 2nd parameter increases
                    once = true;
                }


            }
            if (realized == true)
            {
                Chase();
            }
        }
    }
    public bool deadOlduMu(string deadType,string weaponName,GameObject hit)
    {
        if (weaponName == "Shotgun")
        {
            powerMagnitude = 10f;
        }
        else if (weaponName == "Baretta")
        {
            powerMagnitude = 1f;
        }
        else if (weaponName == "ak47")
        {
            powerMagnitude = 3f;
        }
        else if (weaponName == "awp")
        {
            powerMagnitude = 20f;
        }
        if (can <= 0)
        {
            anm.SetBool("dead", true);
            if (realized != false && once == true)
            {
                once = false;
                Enemycamera.GetComponentInChildren<WeaponManager>().realizedCount--;
            }
            Vector3 hitDirection = hit.transform.position - dusman.transform.position;
            agent.enabled = false;
            aimHands.weight = 0f;
            aimlay.weight = 0f;
            aimBody.weight = 0f;
            wepPose.weight = 0f;


            foreach (var r in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                r.isKinematic = false;
                r.useGravity = true;
            }
            hit.GetComponent<Rigidbody>().AddForce(hitDirection * powerMagnitude * 2, ForceMode.Force);
            StartCoroutine("dead", 0.1f);
            return true;
        }
        else
        {
            return false;
        }


    }
    public void InitiateFire()
    {
        if (Physics.Linecast(aim.transform.position, dusman.transform.position, out hit2))
        {
            if (hit2.transform.name == "newCharacter")
            {
                if (Physics.Raycast(gunTip.transform.position, gunTip.transform.forward, out hit, Mathf.Infinity) && can > 0)
                {
                    if (Enemycamera.GetComponentInChildren<WeaponManager>().realizedCount == 1)
                    {
                        StartCoroutine("ses", 0f);

                    }
                    else
                    {
                        StartCoroutine("ses", UnityEngine.Random.Range(0.1f, 0.9f));
                    }
                    if (hit.transform.name == "newCharacter")
                    {
                        healtRunScript.Instance.takeAwayHealth(UnityEngine.Random.Range(10, 20));
                    }
                }
            }
        }
        
    }
    IEnumerator dead(float waitforsec)
    {
        yield return new WaitForSeconds(waitforsec);
        anm.enabled = false;
    }
    IEnumerator ses(float waitforsec)
    {
        yield return new WaitForSeconds(waitforsec);
        gunSound.Play();
    }
    public void setAim()
    {
        gunTip.GetComponent<Animator>().SetBool("gunTipPlay", true);
    }
    void Chase()
    {
        if (Vector3.Distance(gameObject.transform.position, dusman.transform.position) >= 15)
        {
            anm.SetBool("realized2", true);
            if (agent.enabled == true)
            {
                print("asad");
                agent.isStopped = false;

                agent.SetDestination(Enemycamera.transform.position);

            }
        }
        else
        {
            anm.SetBool("realized", true);
            if (agent.enabled == true)
            {
                agent.isStopped = true;
            }
            anm.SetBool("realized2", false);


        }
    }
}
