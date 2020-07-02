using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePanel, pausePanel;

    [SerializeField]
    private Text trashText, livesText;

    [SerializeField]
    private Image clockBar, trashBar;

    public static bool paused;

    [SerializeField]
    private float roundTimer;

    private float startTimer;
    // Start is called before the first frame update
    void Start()
    {

        startTimer = roundTimer;
        Cursor.visible = false;      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {                
                Time.timeScale = 0;
                paused = true;
                gamePanel.SetActive(false);
                pausePanel.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;
                paused = false;
                gamePanel.SetActive(true);
                pausePanel.SetActive(false);
                Cursor.visible = false;
            }
        }

        if (roundTimer > 0 && !paused)
        {

            roundTimer -= Time.deltaTime;
            clockBar.fillAmount = roundTimer / startTimer;
        }
        else if(roundTimer <= 0)
        {
            print("Time over");
        }
    }

    public void SetLivesUI(int lives)
    {
        livesText.text = lives.ToString();
    }



    
}
