using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePanel, pausePanel, winScreen, gameOverScreen, optionsScreen;

    [SerializeField]
    private Text trashText, livesText, ghostUses;

    [SerializeField]
    private Slider sfxSlider, musicSlider;

    [SerializeField]
    private AudioSource[] sfxSources;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private Image clockBar, trashBar, ghostCooldown;

    private bool canInput = true;

    public static bool paused;

    [SerializeField]
    private float roundTimer;

    private float startTimer;

    private int totalTrash, currentTrash;
    // Start is called before the first frame update
    void Start()
    {
        currentTrash = totalTrash;
        startTimer = roundTimer;
        Cursor.visible = false;      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && canInput)
        {
            PauseScreen();           
        }

        if (roundTimer > 0 && !paused)
        {

            roundTimer -= Time.deltaTime;
            clockBar.fillAmount = roundTimer / startTimer;
        }
        else if(roundTimer <= 0)
        {
            LoseScreen();
        }

        if (currentTrash <= 0)
        {
            WinScreen();
        }
    }

    public void SetLivesUI(int lives)
    {
        livesText.text = lives.ToString();
        if (lives <= 0)
        {
            LoseScreen();
        }
    }

    public void SetGhostUses(int uses)
    {
        ghostUses.text = uses.ToString();
    }

    public void SetTrashBar()
    {
        currentTrash--;
        trashBar.fillAmount = (float)currentTrash / (float)totalTrash;
    }

    public void SetTotalTrash(int trash, int puddles)
    {
        totalTrash = trash+puddles;
    }

    public void WinScreen()
    {
        canInput = false;
        paused = true;
        gamePanel.SetActive(false);
        winScreen.SetActive(true);
        Cursor.visible = true;
    }
    public void LoseScreen()
    {
        canInput = false;
        paused = true;
        gamePanel.SetActive(false);
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        paused = false;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Options(bool option)
    {
        if (option)
        {
            optionsScreen.SetActive(true);
            pausePanel.SetActive(false);
        }
        else
        {
            optionsScreen.SetActive(false);
            pausePanel.SetActive(true);
        }
        
    }

    public void PauseScreen()
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

    public void ChangeSFXVolume()
    {
        foreach (var item in sfxSources)
        {
            item.volume = sfxSlider.value;
        }
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }

    public void GhostCooldown(float startTime, float currentTime)
    {
        ghostCooldown.fillAmount = currentTime / startTime;
    }
}
