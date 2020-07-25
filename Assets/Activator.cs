using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public enum Hook
{
    None,
    TriggerEnter,
    TriggerStay,
    TriggerExit,
    ButtonDown,
    ButtonUp,
    Time
}


public class Activator : MonoBehaviour
{
    public List<GameObject> TargetObjects = new List<GameObject>();
    public List<MonoBehaviour> TargetScripts = new List<MonoBehaviour>();
    public List<Component> TargetComponents = new List<Component>();
    
    public Hook ActivateBy;
    public Hook DeactivateBy;

    public List<string> WhoIsTriggered = new List<string> {"Player"};

    public float TargetTime;
    public string TargetButton;

    private float startTick;

    private readonly Dictionary<Type, PropertyInfo> _cachedType
        = new Dictionary<Type, PropertyInfo>();

    private void Start()
    {
        startTick = Time.time;
    }

    private void FixedUpdate()
    {
        bool timeIsUp = Time.time >= startTick + TargetTime;

        if (ActivateBy == Hook.Time && timeIsUp)
            SetAll(true);
        else if (DeactivateBy == Hook.Time && timeIsUp)
            SetAll(false);

        if (!string.IsNullOrWhiteSpace(TargetButton))
        {
            bool buttonPressed = Input.GetButton(TargetButton);

            if (buttonPressed)
            {
                if (ActivateBy == Hook.ButtonDown)
                    SetAll(true);
                if (DeactivateBy == Hook.ButtonDown)
                    SetAll(false);
            }
            else
            {
                if (ActivateBy == Hook.ButtonUp)
                    SetAll(true);
                if (DeactivateBy == Hook.ButtonUp)
                    SetAll(false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
        => SetAllByTriggerIfRequired(other.tag, Hook.TriggerEnter);


    private void OnTriggerExit2D(Collider2D other)
        => SetAllByTriggerIfRequired(other.tag, Hook.TriggerExit);

    private void OnTriggerStay2D(Collider2D other)
        => SetAllByTriggerIfRequired(other.tag, Hook.TriggerStay);

    private void SetAllByTriggerIfRequired(string tag, Hook hook)
    {
        if (WhoIsTriggered.Contains(tag))
        {
            if (ActivateBy == hook)
                SetAll(true);
            else if (DeactivateBy == hook)
                SetAll(false);
        }
    }

    private void SetAll(bool value)
    {
        TargetObjects.ForEach(o => { o.SetActive(value); });
        TargetComponents.Concat(TargetScripts).ToList().ForEach(c =>
        {
            Type type = c.GetType();
            if (!_cachedType.ContainsKey(type))
                _cachedType.Add(type, type.GetProperty(nameof(enabled)));

            if ((bool) _cachedType[type].GetValue(c) != value)
                _cachedType[type].SetValue(c, value);
        });
    }
}