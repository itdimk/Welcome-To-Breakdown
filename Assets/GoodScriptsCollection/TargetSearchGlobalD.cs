using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Util;
using UnityEngine;

public class TargetSearchGlobalD : MonoBehaviour
{
    public int ScanInterval = 4;

    private readonly Dictionary<string, List<GameObject>> _objects = new Dictionary<string, List<GameObject>>();
    private float _startTick;
    private string[] _objectsKeys;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= _startTick + ScanInterval)
        {
            _startTick = Time.time;
            RefreshGameObjects();
        }
    }

    void RefreshGameObjects()
    {
        _objectsKeys = _objects.Keys.ToArray();

        foreach (var key in _objectsKeys)
        {
            _objects[key].ClearFast();
            _objects[key].AddRange(GameObject.FindGameObjectsWithTag(key));
        }
    }

    public List<GameObject> GetObjectsByTag(string targetTag)
    {
        if (!_objects.ContainsKey(targetTag))
        {
            _objects.Add(targetTag, new List<GameObject>());
            RefreshGameObjects();
        }

        return _objects[targetTag];
    }

    public List<GameObject> GetObjectsByTagNotNull(string targetTag)
    {
        if (!_objects.ContainsKey(targetTag))
        {
            _objects.Add(targetTag, new List<GameObject>());
            RefreshGameObjects();
        }
        else
            _objects[targetTag].RemoveAll(o => o.gameObject == null);
        
        return _objects[targetTag];
    }
}