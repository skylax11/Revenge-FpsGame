using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject panel2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenSettings()
    {
        print("a");
        panel.SetActive(true);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);

    }
    public void OpenControls()
    {
        panel2.SetActive(true);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
    public void BackToMenu()
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
    }
}
