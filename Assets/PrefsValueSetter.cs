using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsValueSetter : MonoBehaviour
{
    public string PrefsKey = "Language";
    public string PrefsValue;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString(PrefsKey, PrefsValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
