using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class NumberOutputD : MonoBehaviour
{
    public Component WriteTo;
    public string PropertyName;
    public float Multiplier = 1.0f;
    public bool Round;

    private PropertyInfo _property;

    // Start is called before the first frame update
    void Start()
    {
        _property = WriteTo.GetType().GetProperty(PropertyName);

        if (_property == null)
            throw new ArgumentException($"Property {PropertyName} is not found");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetNumber(float value)
    {
        if (Round)
            value = Mathf.Round(value);

        SetPropertyValue(value);
    }

    private void SetPropertyValue(float value)
    {
        if (_property.PropertyType == typeof(string))
            _property.SetValue(WriteTo, value.ToString());

        else if (_property.PropertyType == typeof(float))
            _property.SetValue(WriteTo, value);

        else if (_property.PropertyType == typeof(int))
            _property.SetValue(WriteTo, (int) value);
        
        else
            throw new Exception($"Property type {_property.PropertyType} is not supported");
    }
}