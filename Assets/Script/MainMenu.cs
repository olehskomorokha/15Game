using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public static PauseMenu instanse;
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        instanse.Resume();
    }

}
