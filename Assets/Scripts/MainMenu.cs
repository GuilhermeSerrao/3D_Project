using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
}
