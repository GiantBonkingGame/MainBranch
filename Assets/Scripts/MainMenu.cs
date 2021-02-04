using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject MainMenuClick;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //we dont have credits
    }

    public void Options()
    {
        OptionsMenu.SetActive(true);
        MainMenuClick.SetActive(false);
    }
    public void OptionsOff()
    {
        OptionsMenu.SetActive(false);
        MainMenuClick.SetActive(true);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
