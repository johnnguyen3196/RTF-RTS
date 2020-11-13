using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject GameOverMenuUI;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI RetryText;

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        //FindObjectOfType<AudioManager>().Play("Pause");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameOverMenuUI.SetActive(false);
        SceneManager.LoadScene("Level1");
    }

    public void SetGameOverText(string text)
    {
        GameOverText.text = text;
    }

    public void DisplayGameOverMenu()
    {
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;


    }
}
