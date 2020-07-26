using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public enum HookNew
{
    None,
    TriggerEnter,
    TriggerStay,
    TriggerExit,
    TriggerExitExact,
    ButtonDown,
    ButtonUp,
    Time
}


public class ActivatorNew : MonoBehaviour
{
    public List<GameObject> TargetObjects = new List<GameObject>();
    public List<MonoBehaviour> TargetScripts = new List<MonoBehaviour>();
    public List<Component> TargetComponents = new List<Component>();
    
    public HookNew ActivateBy;
    public HookNew DeactivateBy;

    public List<string> WhoIsTriggered = new List<string> {"Player"};

    public float TargetTime;
    public string TargetButton;

    private int triggerEnterCount;
    private int triggerExitCount;
    
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

        if (ActivateBy == HookNew.Time && timeIsUp)
            SetAll(true);
        else if (DeactivateBy == HookNew.Time && timeIsUp)
            SetAll(false);

        if (!string.IsNullOrWhiteSpace(TargetButton))
        {
            bool buttonPressed = Input.GetButton(TargetButton);

            if (buttonPressed)
            {
                if (ActivateBy == HookNew.ButtonDown)
                    SetAll(true);
                if (DeactivateBy == HookNew.ButtonDown)
                    SetAll(false);
            }
            else
            {
                if (ActivateBy == HookNew.ButtonUp)
                    SetAll(true);
                if (DeactivateBy == HookNew.ButtonUp)
                    SetAll(false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
         SetAllByTriggerIfRequired(other.tag, HookNew.TriggerEnter);
         triggerEnterCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         SetAllByTriggerIfRequired(other.tag, HookNew.TriggerExitExact);
         triggerExitCount++;

         if (triggerExitCount == triggerEnterCount)
             SetAllByTriggerIfRequired(other.tag, HookNew.TriggerExit);
    }

    private void OnTriggerStay2D(Collider2D other)
        => SetAllByTriggerIfRequired(other.tag, HookNew.TriggerStay);

    private void SetAllByTriggerIfRequired(string tag, HookNew hook)
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