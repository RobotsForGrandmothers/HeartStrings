using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button play;
    public Button quit;

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(OnClickPlay);
        quit.onClick.AddListener(OnClickQuit);
    }

    void OnClickPlay()
    {
        SceneManager.LoadScene("Main");
    }

    void OnClickQuit()
    {
        UIController.QuitGame();
    }
}
