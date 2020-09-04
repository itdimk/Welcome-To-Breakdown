using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnQuit;
    public string PauseButton = "Cancel";
    public const string DifficultyPrefKey = "difficulty";
    public const string LanguagePrefKey = "lang";
    public Texture2D CustomCursor;
    [HideInInspector] public bool IsPaused;

    // Start is called before the first frame update
    void Start()
    {
        if(CustomCursor != null)
            Cursor.SetCursor(CustomCursor, new Vector2(21, 21), CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrWhiteSpace(PauseButton) && Input.GetButtonDown(PauseButton))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Restart()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(index);
        SceneManager.LoadSceneAsync(index);
        Resume();
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        OnPause.Invoke();
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        OnResume.Invoke();
    }

    public void Quit()
    {
      
        Resume();
        OnQuit.Invoke();
        Cursor.visible = true;
    }
    
    public void Exit()
    {
        Resume();
        Application.Quit();
        Debug.Log("Application quit is called");
    }
    
    public void SetLanguage(string value)
    {
        PlayerPrefs.SetString(LanguagePrefKey, value);
    }
    
    public void SetDifficulty(int value)
    {
        PlayerPrefs.SetInt(DifficultyPrefKey, value);
    }
    
    public void SwitchMainThemeMusic()
    {
        int current = PlayerPrefs.GetInt("mute-theme");
        
        if(current == 0)
            PlayerPrefs.SetInt("mute-theme", 1);
        else
            PlayerPrefs.SetInt("mute-theme", 0);
    }
    
    public string GetLanguage()
    {
        return PlayerPrefs.GetString(LanguagePrefKey);; 
    }
    
    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DifficultyPrefKey);; 
    }
}