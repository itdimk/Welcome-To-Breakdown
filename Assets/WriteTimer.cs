using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WriteTimer : MonoBehaviour
{
    public Text Output;
    public UnityEvent TimeIsUp;
    
    private float _startTick;
    public float MaxTime ;
    
    // Start is called before the first frame update
    void Start()
    {
        _startTick = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (MaxTime > Time.fixedDeltaTime)
        {
            MaxTime -= Time.fixedDeltaTime;
            Output.text = Math.Round(MaxTime, 2).ToString();
        } 
        else
            TimeIsUp?.Invoke();
        
    }
}
