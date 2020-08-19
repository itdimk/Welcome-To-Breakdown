using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelfDestroy : MonoBehaviour
{
    public List<string> Who = new List<string> {"Player"};
    public float DestroyOnDelay = 1.0f;
    public bool DestroyOnCollision;
    public bool DestroyOnTrigger;
    private float startTick;

    public UnityEvent OnDestroy;

    // Start is called before the first frame update
    void Start()
    {
        startTick = Time.time;
    }

    private void OnEnable()
    {
        startTick = Time.time;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startTick + DestroyOnDelay < Time.time)
        {
            Destroy(gameObject);
            OnDestroy?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (enabled && (Who.Count == 0 || Who.Contains(other.gameObject.tag)))
        {
            if (DestroyOnCollision)
            {
                Destroy(gameObject);
                OnDestroy?.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && (Who.Count == 0 || Who.Contains(other.gameObject.tag)))
        {
            if (DestroyOnTrigger)
            {
                Destroy(gameObject);
                OnDestroy?.Invoke();
            }
        }
    }
}