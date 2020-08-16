using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerD : MonoBehaviour
{
    public GameObject Target;
    public float[] Intervals = { 5f };
    public bool ActivateOnSpawn = true;
   
    public float SpawnRadius = 1.0f;

    [Space]
    public int OnlineCount = 2;
    public int TotalCount = 10;
    
    private List<GameObject> Spawned = new List<GameObject>();

    private int _currInterval;
    private float _startTick;

    // Start is called before the first frame update
    void OnEnable()
    {
        _startTick = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsSpawnRequired())
        {
            Spawn();
            MoveNext();
        }
    }

    bool IsSpawnRequired()
    {
        if (Spawned.Count >= MaxCount)
            _startTick = Time.time;

        Spawned.RemoveAll(o => o.gameObject == null);
        return Spawned.Count < MaxCount && _startTick + Intervals[_currInterval] <= Time.time;
    }

    void MoveNext()
    {
        if (_currInterval + 1 < Intervals.Count)
            _currInterval++;
        else
        {
            if (OneSoawn)
                Destruct();
            else
            {
                _currInterval = 0;
                _startTick = Time.time;
            }
        }
    }

    void Spawn()
    {
        var obj = Instantiate(Target, transform);
        obj.transform.parent = null;
        obj.transform.position = GetSpawnPoint();
        Spawned.Add(obj);

        if (ActivateOnSpawn)
            obj.SetActive(true);
    }

    Vector3 GetSpawnPoint()
    {
        Vector2 currPos = transform.position;

        float randomRadius = Random.Range(-SpawnRadius, SpawnRadius);
        float randomAngle = Random.Range(0, Mathf.PI);

        Vector2 direction = new Vector2(
            randomRadius * Mathf.Cos(randomAngle),
            randomRadius * Mathf.Sin(randomAngle)
        );

        return currPos + direction;
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}