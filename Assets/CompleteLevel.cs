using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class CompleteLevel : MonoBehaviour
{
    public void Start()
    {
        int levels = PlayerPrefs.GetInt("levels-completed");
        int currLevel = SceneManager.GetActiveScene().buildIndex - 1;
        
        if(currLevel > levels)
            PlayerPrefs.SetInt("levels-completed", currLevel);
       
    }
}
