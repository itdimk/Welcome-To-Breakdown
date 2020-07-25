using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public List<GameObject> TargetObjects = new List<GameObject>();
    public List<MonoBehaviour> TargetScripts = new List<MonoBehaviour>();
    public List<Component> TargetComponents = new List<Component>();


    public bool SetActiveOnTriggerEnter;
    public bool SetActiveOnTriggerStay;
    public bool SetActiveOnTriggerExit;
    public List<string> WhoIsTriggered = new List<string> {"Player"};

    public bool SetActiveInTime;
    public float Time;

    public bool SetActiveOnKeyDown;
    public bool SetActiveOnKeyUp;
    public KeyCode Key;

    public bool SetActiveTo;

    private float startTick;

    private readonly Dictionary<Type, PropertyInfo> _cachedType
        = new Dictionary<Type, PropertyInfo>();

    private void Start()
    {
        startTick = UnityEngine.Time.time;
    }

    private void FixedUpdate()
    {
        if (SetActiveInTime && UnityEngine.Time.time >= startTick + Time)
            SetAll();
        else if (SetActiveOnKeyDown && Input.GetKey(Key))
            SetAll();
        else if (SetActiveOnKeyUp && !Input.GetKey(Key))
            SetAll();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (WhoIsTriggered.Contains(other.gameObject.tag))
        {
            if (SetActiveOnTriggerEnter)
                SetAll();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (WhoIsTriggered.Contains(other.gameObject.tag))
        {
            if (SetActiveOnTriggerExit)
                SetAll();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (WhoIsTriggered.Contains(other.gameObject.tag))
        {
            if (SetActiveOnTriggerStay)
                SetAll();
        }
    }

    private void SetAll()
    {
        TargetObjects.ForEach(o => { o.SetActive(SetActiveTo); });
        TargetComponents.Concat(TargetScripts).ToList().ForEach(c =>
        {
            Type type = c.GetType();
            if (!_cachedType.ContainsKey(type))
                _cachedType.Add(type, type.GetProperty(nameof(enabled)));

            if ((bool) _cachedType[type].GetValue(c) != SetActiveTo)
                _cachedType[type].SetValue(c, SetActiveTo);
        });
    }
}