using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu, optionsScreen;
    void Update()
    {
        if (Input.anyKeyDown && SceneManager.GetActiveScene().name != "MainMenu")
        {
            Application.Quit();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OptionsScreen(bool options)
    {
        if (options)
        {
            mainMenu.SetActive(false);
            optionsScreen.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            optionsScreen.SetActive(false);
        }
    }


}
