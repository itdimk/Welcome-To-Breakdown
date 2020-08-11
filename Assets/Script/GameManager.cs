﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public string PauseButton = "Cancel";


    [HideInInspector] public bool IsPaused;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(PauseButton))
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
        Time.timeScale = 0.01f;
        OnPause.Invoke();
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        OnResume.Invoke();
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Application quit is called");
    }
}