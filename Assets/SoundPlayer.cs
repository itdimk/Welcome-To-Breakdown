using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public string SoundName;

    public AudioManager Manager;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        Manager.Play(SoundName);
    }

    // Update is called once per frame
    void Update()
    {
    }
}