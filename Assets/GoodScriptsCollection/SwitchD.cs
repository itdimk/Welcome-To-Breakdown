using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SwitchD : MonoBehaviour
{
    [Serializable]
    public class Case
    {
        public string Value;
        public UnityEvent OnMatch;
    }
    
    public ValueProviderBaseD valueProvider;
    public float UpdateInterval = 1E10F;

    [Space]
    public List<Case> Cases;
    [Space]
    public UnityEvent DefaultCase;
    
    
    private float _startTick = float.MinValue;
    private Type _propertyType;

    // Start is called before the first frame update
    void Start()
    {
        _propertyType = valueProvider.GetValueType();
    }

    private void Update()
    {
        if (Time.unscaledTime - _startTick >= UpdateInterval)
        {
            _startTick = Time.unscaledTime;
            Refresh();
        }
    }

    public void Refresh()
    {
        var c = GetCase();

        if (c != null)
            c.OnMatch?.Invoke();
        else
            DefaultCase?.Invoke();
    }

    private Case GetCase()
    {
        if (valueProvider.GetValue() is IComparable value)
        {
            return Cases.FirstOrDefault(c
                => value.CompareTo(Convert.ChangeType(c.Value, _propertyType)) == 0);
        }

        return null;
    }
}