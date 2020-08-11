using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject PauseCanvas;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && Input.GetButtonDown("Cancel"))
        {
            PauseCanvas?.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused && Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 1f;
            PauseCanvas?.SetActive(false);
            isPaused = false;
        }
    }
}