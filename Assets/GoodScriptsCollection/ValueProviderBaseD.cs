using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class ValueProviderBaseD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract object GetValue();
    public abstract void SetValue(object value);
    public abstract Type GetValueType();

}
