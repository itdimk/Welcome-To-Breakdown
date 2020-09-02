using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CheatCodeValueProvider : ValueProviderBaseD
{
    StringBuilder _sequence = new StringBuilder();

    public float MaxInterval = 1;

    private float _startTick;
    
    // Start is called before the first frame update
    void Start()
    {
        _startTick = Time.unscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime - _startTick <= MaxInterval)
        {
            if (Input.inputString.Length > 0)
            {
                _sequence.Append(Input.inputString[0]);
                _startTick = Time.unscaledTime;
            }
        }
        else
        {
            _sequence.Clear();
            _startTick = Time.unscaledTime;
        }
    }

    public override object GetValue()
    {
        return _sequence.ToString();
    }

    public override void SetValue(object value)
    {
        throw new NotImplementedException();
    }

    public override Type GetValueType()
    {
        return typeof(string);
    }
}
