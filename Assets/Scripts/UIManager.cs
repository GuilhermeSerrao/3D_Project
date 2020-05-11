using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text trashText, livesText, ghostModeText;

    [SerializeField]
    private int lives, maxLives;

    public int totalTrash, pickedTrash = 0;

    public bool ghostMode;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        lives = maxLives;
        livesText.text = lives + " lives remaining";
        trashText.text = pickedTrash + " out of " + totalTrash + " trash picked ".ToString();
        ghostModeText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        livesText.text = lives + " lives remaining";
        trashText.text = pickedTrash + " out of " + totalTrash + " trash picked ".ToString();
        ghostModeText.enabled = ghostMode;
        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
        }
        else if (pickedTrash == totalTrash)
        {
            SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
        }
    }

    public void ReduceLives()
    {
        lives--;
        UpdateUI();
    }
}
