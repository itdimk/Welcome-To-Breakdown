using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Itdimk;
using UnityEngine;
using UnityEngine.Events;

public class TimeActivatorD : ActivatorBaseD
{
    public float ActivationTime = -1f;
    public float DeactivationTime = -1f;

    private float startTick;
    
    private bool isActivated ;
    private bool isDeactivated;

    private void Start()
    {
        startTick = Time.time;
    }
    
    private void FixedUpdate()
    {
        if (!isActivated && ActivationTime > 0 && Time.time >= startTick + ActivationTime)
        {
            isActivated = true;
            SetAll(true);
        }

        if (!isDeactivated && DeactivationTime > 0 && Time.time >= startTick + DeactivationTime)
        {
            isDeactivated = true;
            SetAll(false);
        }
    }
}    