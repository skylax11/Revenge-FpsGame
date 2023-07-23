using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassData : MonoBehaviour
{
    public static PassData Instance;
    public bool hasKilled = false;
    public string message;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
    public void passTheData()
    {
        print("load scene?? , please ??");
        if (hasKilled == false)
        {
            message = "YOU SHOULD HAVE KILLED HIM. BUT INSTEAD. YOU RAN AWAY. YOU BETRAYED US.   NOT COOL.";
            SceneManager.LoadScene(2);
        }
        else if (hasKilled == true)
        {
            message = "YOU'VE DONE IT !  YOU HAVE TAKEN OUR REVENGE\r\n\r\n   ALL OF YOUR COMRADES PROUD WITH YOU\r\n\r\n\r\n ";
            SceneManager.LoadScene(2);
        }
    }
    public void dead()
    {
        message = "WELCOME ! YOU HAVE DIED JUST LIKE US. THANKS FOR YOUR EFFORT!";
        SceneManager.LoadScene(2);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
