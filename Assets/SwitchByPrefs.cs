using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class SwitchByPrefs : MonoBehaviour
{
    [Serializable]
    public class Case
    {
        public string PrefsValue;
        public string TargetPropertyValue;
    }

    public Component Target;
    public string TargetPropertyName;
    public bool IgnoreCase = true;

    [Space] public string PrefsKey;
    [Space] public List<Case> Cases;

    [Space]
    public float UpdateInterval = 1E10F;

    [Space]
    public UnityEvent OnValueSet;


    private float _startTick = float.MinValue;
    
    // Start is called before the first frame update
    void Start()
    {
        
      
    }

    private void FixedUpdate()
    {
        if (Time.time - _startTick >= UpdateInterval)
        {
            _startTick = Time.time;
            Process();
        }
    }

    private void Process()
    {
        IComparable prefsValue = GetPrefsValue();

        if (prefsValue != null)
        {
            Case match = GetCaseByPrefsValue(prefsValue);

            if (match != null)
            {
                FieldInfo targetField = GetTargetField();
                PropertyInfo targetProperty = GetTargetProperty();
                
                if (targetField != null)
                    SetTargetField(targetField, match.TargetPropertyValue);
                else if(targetProperty != null)
                    SetTargetProperty(targetProperty, match.TargetPropertyValue);
                else
                    Debug.LogWarning($"Can't find field / property \"{TargetPropertyName}\" in {Target}");
            }
            else
                Debug.LogWarning($"Case isn't defined for {nameof(Case.PrefsValue)} == \"{prefsValue}\"");
        }
        else
            Debug.LogWarning($"Key \"{PrefsKey}\" doesn't exist in {nameof(PlayerPrefs)}");
    }


    FieldInfo GetTargetField()
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;

        if (IgnoreCase)
            flags |= BindingFlags.IgnoreCase;
        
        return Target.GetType().GetField(TargetPropertyName, flags);
    }
    
    PropertyInfo GetTargetProperty()
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;
        
        if (IgnoreCase)
            flags |= BindingFlags.IgnoreCase;
        
        return Target.GetType().GetProperty(TargetPropertyName, flags);
    }

    void SetTargetField(FieldInfo targetField, object value)
    {
        targetField.SetValue(Target, Convert.ChangeType(value, targetField.FieldType));
        OnValueSet?.Invoke();
    }
    
    void SetTargetProperty(PropertyInfo targetProperty, object value)
    {
        targetProperty.SetValue(Target, Convert.ChangeType(value, targetProperty.PropertyType));
        OnValueSet?.Invoke();
    }

    Case GetCaseByPrefsValue(IComparable prefsValue)
    {
        return Cases.FirstOrDefault(c => prefsValue
            .CompareTo(Convert.ChangeType(c.PrefsValue,
                prefsValue.GetType())) == 0);
    }

    IComparable GetPrefsValue()
    {
        int value1 = PlayerPrefs.GetInt(PrefsKey, int.MinValue);

        if (value1 != int.MinValue)
            return value1;

        float value2 = PlayerPrefs.GetFloat(PrefsKey, float.MinValue);

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (value2 != float.MinValue)
            return value2;

        return PlayerPrefs.GetString(PrefsKey, null);
    }
}