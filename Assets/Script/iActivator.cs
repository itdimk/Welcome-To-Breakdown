using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace Itdimk
{
    public enum Hooks
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


    public class iActivator : MonoBehaviour
    {
        public List<GameObject> TargetObjects = new List<GameObject>();
        public List<MonoBehaviour> TargetScripts = new List<MonoBehaviour>();
        public List<Component> TargetComponents = new List<Component>();

        public Hooks ActivateBy;
        public Hooks DeactivateBy;


        public float TargetTime;
        public string TargetButton;
        public string[] TargetTriggerTags = {"Player"};

        private int triggerEnterCount;
        private int triggerExitCount;

        private float startTick;
        private bool _ignoreTime = false;
        private readonly Dictionary<Type, PropertyInfo> _cachedType
            = new Dictionary<Type, PropertyInfo>();
        
        public UnityEvent OnActivate;
        public UnityEvent OnDeactivate;

        private void Start()
        {
            startTick = Time.time;
        }
        
        private void OnEnable()
        {
            startTick = Time.time;
            _ignoreTime = false;
        }

        private void Update()
        {
            if (!string.IsNullOrWhiteSpace(TargetButton))
            {
                bool buttonPressed = Input.GetButton(TargetButton);

                if (buttonPressed)
                {
                    if (ActivateBy == Hooks.ButtonDown)
                        SetAll(true);
                    if (DeactivateBy == Hooks.ButtonDown)
                        SetAll(false);
                }
                else
                {
                    if (ActivateBy == Hooks.ButtonUp)
                        SetAll(true);
                    if (DeactivateBy == Hooks.ButtonUp)
                        SetAll(false);
                }
            }
        }

        private void FixedUpdate()
        {
            bool timeIsUp = Time.time >= startTick + TargetTime;

            if (!_ignoreTime && ActivateBy == Hooks.Time && timeIsUp)
            {
                SetAll(true);
                _ignoreTime = true;
            }
            else if (!_ignoreTime && DeactivateBy == Hooks.Time && timeIsUp)
            {
                SetAll(false);
                _ignoreTime = true;
            }

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            SetAllByTriggerIfRequired(other.tag, Hooks.TriggerEnter);

            if (TargetTriggerTags.Contains(other.gameObject.tag))
                triggerEnterCount++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            SetAllByTriggerIfRequired(other.tag, Hooks.TriggerExitExact);

            if (TargetTriggerTags.Contains(other.gameObject.tag))
                triggerExitCount++;

            if (triggerExitCount == triggerEnterCount)
                SetAllByTriggerIfRequired(other.tag, Hooks.TriggerExit);
        }

        private void OnTriggerStay2D(Collider2D other)
            => SetAllByTriggerIfRequired(other.tag, Hooks.TriggerStay);

        private void SetAllByTriggerIfRequired(string tag, Hooks hook)
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
            TargetComponents.Concat(TargetScripts).ToList().ForEach(c =>
            {
                Type type = c.GetType();
                if (!_cachedType.ContainsKey(type))
                    _cachedType.Add(type, type.GetProperty(nameof(enabled)));

                _cachedType[type].SetValue(c, value);
            });

            if (value && DeactivateBy == Hooks.Time || !value && ActivateBy == Hooks.Time)
                startTick = Time.time;
        }
    }
}