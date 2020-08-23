using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsValueProviderD : ValueProviderBaseD
{
    public string PrefsKey;

    private Type _valueType;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(PrefsKey))
        {
            if (PlayerPrefs.GetInt(PrefsKey, int.MinValue) != int.MinValue)
                _valueType = typeof(int);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            else if (PlayerPrefs.GetFloat(PrefsKey, float.MinValue) != float.MinValue)
                _valueType = typeof(float);

            else if (PlayerPrefs.GetString(PrefsKey, null) != null)
                _valueType = typeof(string);
            
            else
                Debug.LogWarning($"Can't determine type of {nameof(PlayerPrefs)} item");
        }
        else
            Debug.LogWarning($"Key \"{PrefsKey}\" is not defined in {nameof(PlayerPrefs)}");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override object GetValue()
    {
        if (_valueType == typeof(float))
            return PlayerPrefs.GetFloat(PrefsKey);

        if (_valueType == typeof(int))
            return PlayerPrefs.GetInt(PrefsKey);

        if (_valueType == typeof(string))
            return PlayerPrefs.GetString(PrefsKey);

        return null;
    }

    public override void SetValue(object value)
    {
        if (_valueType == typeof(float))
            PlayerPrefs.SetFloat(PrefsKey, (float) Convert.ChangeType(value, _valueType));

        else if (_valueType == typeof(int))
            PlayerPrefs.SetInt(PrefsKey, (int) Convert.ChangeType(value, _valueType));

        else if (_valueType == typeof(string))
            PlayerPrefs.SetString(PrefsKey, (string) Convert.ChangeType(value, _valueType));
    }

    public override Type GetValueType()
    {
        return _valueType;
    }
}