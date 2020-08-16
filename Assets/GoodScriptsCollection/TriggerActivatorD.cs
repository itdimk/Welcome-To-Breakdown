using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class TriggerActivatorD : MonoBehaviour
{
    public enum HooksD
    {
        None,
        TriggerEnter,
        TriggerExit,
        TriggerExitExact,
    }

    public List<GameObject> TargetObjects = new List<GameObject>();
    public List<MonoBehaviour> TargetScripts = new List<MonoBehaviour>();

    public string[] TargetTriggerTags = {"Player"};

    public HooksD ActivateBy;
    public HooksD DeactivateBy;

    private int insideTriggerCount;

    private readonly Dictionary<Type, PropertyInfo> _cachedType
        = new Dictionary<Type, PropertyInfo>();

    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SetAllByTriggerIfRequired(other.tag, HooksD.TriggerEnter);

        if (TargetTriggerTags.Contains(other.gameObject.tag))
            insideTriggerCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SetAllByTriggerIfRequired(other.tag, HooksD.TriggerExitExact);

        if (TargetTriggerTags.Contains(other.gameObject.tag))
            insideTriggerCount--;

        if (insideTriggerCount == 0)
            SetAllByTriggerIfRequired(other.tag, HooksD.TriggerExit);
    }


    private void SetAllByTriggerIfRequired(string tag, HooksD hook)
    {
        if (TargetTriggerTags.Contains(tag))
        {
            if (ActivateBy == hook)
                SetAll(true);
            else if (DeactivateBy == hook)
                SetAll(false);
        }
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