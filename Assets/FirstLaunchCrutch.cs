using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLaunchCrutch : MonoBehaviour
{
    private const string firstLaunchFlag = "is-first-launch";

    public GameObject ActivateIfFirstLaunch;
    public GameObject ActivateIfNotFirstLaunch;

    public bool FullReset = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if(FullReset)
            PlayerPrefs.DeleteKey(firstLaunchFlag);
        else
        {
            int b = PlayerPrefs.GetInt(firstLaunchFlag);

            if (b != 0)
                ActivateIfNotFirstLaunch.SetActive(true);
            else
                ActivateIfFirstLaunch.SetActive(true);

            PlayerPrefs.SetInt(firstLaunchFlag, 3);
        }

    }

    private void Reset()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
