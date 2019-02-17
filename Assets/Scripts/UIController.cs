using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIController
{
    // Start game screen in scene for UI?
    public static void ShowStartGameScreen()
    {

    }
    
    // End game screen in scene for UI?
    public static void ShowEndGameScreen()
    {

    }

    // Time to quit the game 
    public static void QuitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
