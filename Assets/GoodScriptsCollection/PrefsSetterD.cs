using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsSetterD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}