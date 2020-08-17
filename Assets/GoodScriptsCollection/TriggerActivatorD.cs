using System.Linq;
using UnityEngine;

public class TriggerActivatorD : ActivatorBaseD
{
    public enum HooksD
    {
        None,
        TriggerEnter,
        TriggerExit,
        TriggerExitExact,
    }

    public string[] TargetTriggerTags = {"Player"};

    public HooksD ActivateBy;
    public HooksD DeactivateBy;

    private int insideTriggerCount;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TargetTriggerTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount++;
            SetAllByHookIfRequired(HooksD.TriggerEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (TargetTriggerTags.Contains(other.gameObject.tag))
        {
            insideTriggerCount--;
            SetAllByHookIfRequired(HooksD.TriggerExitExact);
        }

        if (insideTriggerCount == 0)
            SetAllByHookIfRequired(HooksD.TriggerExit);
    }


    private void SetAllByHookIfRequired(HooksD hook)
    {
        if (ActivateBy == hook)
            SetAll(true);
        else if (DeactivateBy == hook)
            SetAll(false);
    }
}