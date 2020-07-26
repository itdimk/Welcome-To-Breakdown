using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boom : MonoBehaviour
{
    [FormerlySerializedAs("Delay")] public float DelayToBoom = 5f;
    public float DelayToDestroy = 6f;
    public Collider2D BoomWave;
    
    private float startTick = 0;

    private bool isBooming = false;


    private AudioManager _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        startTick = Time.time;
        _audio = FindObjectOfType<AudioManager>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isBooming && Time.time >= startTick + DelayToBoom)
        {
            BoomAction();
            isBooming = true;
        }

        if (isBooming && Time.time >= startTick + DelayToDestroy)
        {
            Destroy(gameObject);
        }
    }

    void BoomAction()
    {
        _audio.Play("BoomBomb");
        BoomWave.enabled = true;
    }
}
