using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAi : MonoBehaviour
{
    public UnityEvent OnRocketsLaunchBegin;
    public UnityEvent OnRocketsLaunchEng;

    public float AttackInterval = 10;
    public float AttackDuration = 4;
    public float AnimationDelay = 3f;

    private float rocketLaunchingStart;
    private bool launching = false;
    public string AnimationParameter;
    
    // Start is called before the first frame update
    void Start()
    {
        rocketLaunchingStart = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LaunchRocketsIfRequired();
    }

    private void LaunchRocketsIfRequired()
    {
        if (!launching && Time.time % AttackInterval < 0.2f)
        {
            rocketLaunchingStart = Time.time;

            launching = true;
            GetComponent<Animator>().SetBool(AnimationParameter, launching);
            
            
         
        }

        if (launching && (Time.time - rocketLaunchingStart) > AttackDuration)
        {
            launching = false;
            GetComponent<Animator>().SetBool(AnimationParameter, false);
            OnRocketsLaunchEng?.Invoke();

        }

        if (launching && Time.time - rocketLaunchingStart > AnimationDelay)
        {
            OnRocketsLaunchBegin?.Invoke();
        }
    }
}


