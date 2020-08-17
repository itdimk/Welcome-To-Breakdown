using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSearchSetterD : TargetSetterD
{
    public enum PriorityD
    {
        Nearest,
        Farthest,
        Any
    }

    public TargetSearchGlobalD Searcher;

    public string[] Tags;
    public float MaxDistance = 10.0f;
    public float MinDistance = 0.0f;
    public Transform CalcDistanceFrom;

    public PriorityD Priority;


    void Start()
    {
        if (CalcDistanceFrom == null)
            CalcDistanceFrom = transform;
    }


    private IEnumerable<GameObject> ProcessGameObjects(IEnumerable<GameObject> objects)
    {
        float GetDistance(GameObject o) => Vector2.Distance(o.transform.position, CalcDistanceFrom.position);
        
        bool Filter (GameObject o)
        {
            float distance = GetDistance(o);
            return distance <= MaxDistance && distance >= MinDistance;
        }
        
        switch (Priority)
        {
            case PriorityD.Any:
                return objects.Where(Filter);

            case PriorityD.Nearest:
                return objects.Where(Filter).OrderBy(GetDistance);

            case PriorityD.Farthest:
                return objects.Where(Filter).OrderByDescending(GetDistance);

            default:
                throw new ArgumentException($"Priority {Priority} is not supported");
        }
    }

    public override Transform GetTarget()
    {
        List<GameObject> candidates = new List<GameObject>();

        foreach (var targetTag in Tags)
        {
            var candidate = ProcessGameObjects(Searcher.GetObjectsByTagNotNull(targetTag)).FirstOrDefault();
            candidates.Add(candidate);
        }

        return ProcessGameObjects(candidates).FirstOrDefault()?.transform;
    }
}