using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_Manager : MonoBehaviour
{

    public GameObject Credit_Panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Alain");
    }

    public void LoadCredit()
    {
        Credit_Panel.SetActive(true);
    }

    public void ExitCredit()
    {
        Credit_Panel.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
