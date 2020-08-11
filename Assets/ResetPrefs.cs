using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Fucked successfully");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
