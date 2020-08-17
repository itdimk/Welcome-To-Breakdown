using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Itdimk;
using UnityEngine;
using UnityEngine.Events;

public class TimeActivatorD : MonoBehaviour
{
    public List<GameObject> TargetObjects = new List<GameObject>();
    public List<MonoBehaviour> TargetScripts = new List<MonoBehaviour>();

    public float ActivationTime = -1f;
    public float DeactivationTime = -1f;

    private float startTick;

    private readonly Dictionary<Type, PropertyInfo> _cachedType
        = new Dictionary<Type, PropertyInfo>();

    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    private bool isActivated = false;
    private bool isDeactivated = false;

    private void Start()
    {
        startTick = Time.time;
    }


    private void FixedUpdate()
    {
        if (!isActivated && ActivationTime > 0 && Time.time >= startTick + ActivationTime)
            SetAll(true);

        if (!isDeactivated && DeactivationTime > 0 && Time.time >= startTick + DeactivationTime)
            SetAll(false);
    }


    private void SetAll(bool value)
    {
        if (value)
            OnActivate?.Invoke();
        else
            OnDeactivate?.Invoke();

        TargetObjects.ForEach(o => { o.SetActive(value); });
        TargetScripts.ForEach(c =>
        {
            Type type = c.GetType();
            if (!_cachedType.ContainsKey(type))
                _cachedType.Add(type, type.GetProperty(nameof(enabled)));

            _cachedType[type].SetValue(c, value);
        });
    }
}