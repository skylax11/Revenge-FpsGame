using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class healtRunScript : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public int can = 100;

    [SerializeField] public Image stamina;
    [SerializeField] TextMeshProUGUI health;
    public static healtRunScript Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void decreaseStamina()
    {
        stamina.fillAmount -= Time.deltaTime* 0.12f;
    }
    public void increaseStamina()
    {
        stamina.fillAmount += Time.deltaTime * 0.08f;
    }
    public void takeAwayHealth(int damage)
    {
        VolumeManagement.instance.check();
        can -= damage;
        if (can <= 0)
        {
            PassData.Instance.dead();
        }
        health.text = can.ToString();
    }
    void Start()
    {
        health.text = can.ToString();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            decreaseStamina();
        }
        else
        {
            increaseStamina();
        }
    }
}
