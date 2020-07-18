using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TriggerSpawner : MonoBehaviour
{
    [FormerlySerializedAs("Tags-activators")]
    public List<string> tagsActivators = new List<string>();

    [FormerlySerializedAs("OneSpawnOnly")] public bool oneSpawnOnly = false;

    [FormerlySerializedAs("Enemy")] public GameObject enemy;

    [FormerlySerializedAs("Count")] public int count;

    [FormerlySerializedAs("Radius")] public float radius;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tagsActivators.Contains(other.gameObject.tag))
        {
            for (int i = 0; i < count; ++i)
            {
                var obj = Instantiate(enemy, GetPositionToSpawn(), Quaternion.identity);
                obj.SetActive(true);
            }


            if (oneSpawnOnly)
                SelfDestroy();
        }
    }


    private Vector3 GetPositionToSpawn()
    {
        Vector3 pos = transform.position;
        pos.x += Random.Range(-radius, radius);
        pos.y += Random.Range(-radius, radius);
        return pos;
    }


    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}