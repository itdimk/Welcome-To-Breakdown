using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        int levelsCompleted = PlayerPrefs.GetInt("levels-completed", 0) + 1;
        SceneManager.LoadScene(levelsCompleted);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
