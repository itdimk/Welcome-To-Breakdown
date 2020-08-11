using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameManager Manager;
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
        if (!Manager.IsPaused && Input.GetButtonDown("Cancel"))
        {
            PauseCanvas?.SetActive(true);
            Manager.Pause();
        }
        else if (Manager.IsPaused && Input.GetButtonDown("Cancel"))
        {
            PauseCanvas?.SetActive(false);
            Manager.Resume();
        }
    }
}