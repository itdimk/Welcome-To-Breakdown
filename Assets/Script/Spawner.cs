using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Itdimk
{
    public class Spawner : MonoBehaviour
    {
        public List<float> Intervals;
        public bool OneSoawn = false;
        public bool ActivateOnSpawn = true;
        public GameObject Target;
        public float Radius = 1.0f;

        public UnityEvent OnSpawn;
        
        private int _currentIndex;
        private float _startTick;

        
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
        }

        bool IsSpawnRequired() => _startTick + Intervals[_currentIndex] <= Time.time;

        void MoveNext()
        {
            if (_currentIndex + 1 < Intervals.Count)
                _currentIndex++;
            else
            {
                if (OneSoawn)
                    Destruct();
                else
                {
                    _currentIndex = 0;
                    _startTick = Time.time;
                }
            }
        }

        void Spawn()
        {
            var obj = Instantiate(Target, transform);
            obj.transform.parent = null;
            obj.transform.position = GetSpawnPoint();

            if (ActivateOnSpawn)
                obj.SetActive(true);
            
            OnSpawn?.Invoke();
        }

        Vector3 GetSpawnPoint()
        {
            Vector2 currPos = transform.position;

            float randomRadius = Random.Range(-Radius, Radius);
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
}