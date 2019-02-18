using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class UIController
{    
    // End game screen in scene for UI?
    public static void ShowEndGameScreen()
    {
        QuitGame();
    }

    // Time to quit the game 
    public static void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
