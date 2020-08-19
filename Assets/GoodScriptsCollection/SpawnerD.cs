using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerD : MonoBehaviour
{
    public GameObject Target;
    
    public float SpawnRadius = 1.0f;
    public float[] SpawnIntervals = { 1f };
   
    [Space]
    public int OnlineCount = 2;
    public int TotalCount = 10;

    private List<GameObject> _spawned = new List<GameObject>();

    private int _currInterval;
    private float _startTick;

    public UnityEvent OnSpawn;
    
    
    // Start is called before the first frame update
    void Start()
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
        else if(TotalCount == 0)
            Destruct();
    }

    bool IsSpawnRequired()
    {
        if (_spawned.Count >= OnlineCount)
        {
            _startTick = Time.time;
            _spawned.RemoveAll(o => o.gameObject == null);
        }

        bool condition1 = _spawned.Count < OnlineCount;
        bool condition2 = _startTick + SpawnIntervals[_currInterval] <= Time.time;
        bool condition3 = TotalCount > 0;
        return condition1 && condition2 && condition3;
    }

    void MoveNext()
    {
        if (_currInterval + 1 < SpawnIntervals.Length)
            _currInterval++;
        else
            _currInterval = 0;
    }

    void Spawn()
    {
        OnSpawn?.Invoke();
        
        var obj = Instantiate(Target, transform);
        obj.transform.parent = null;
        obj.transform.position = GetSpawnPoint();
        obj.SetActive(true);

        _spawned.Add(obj);
        
        _startTick = Time.time;
        TotalCount--;
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