using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool PausedGame = false;

    public GameObject pauseMenuUI;
    public GameObject DeathMenuUI;


    private void Start()
    {
        PausedGame = false;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.end)
        {
            if (PausedGame)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PausedGame = false;
    }

    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PausedGame = true;
    }

    public void Retry()
    {
        DeathMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
