using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itdimk
{
    public class SelfDestroy : MonoBehaviour
    {
        public List<string> Who = new List<string> {"Player"};
        public float DestroyOnDelay = 1.0f;
        public bool DestroyOnCollision;
        public bool DestroyOnTrigger;
        private float startTick;


        // Start is called before the first frame update
        void Start()
        {
            startTick = Time.time;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (startTick + DestroyOnDelay < Time.time)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (Who.Count == 0 || Who.Contains(other.gameObject.tag))
            {
                if (DestroyOnCollision)
                    Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Who.Count == 0 || Who.Contains(other.gameObject.tag))
            {
                if (DestroyOnTrigger)
                    Destroy(gameObject);
            }
        }
    }
}