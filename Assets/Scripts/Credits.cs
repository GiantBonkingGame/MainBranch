﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public static bool PausedGame = false;

    public GameObject CreditsUI;

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
