using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowD : MonoBehaviour
{
    public TargetProviderBaseD Target;
    public float Smoothness = 0.1f;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Target.GetTarget().position,
            ref _velocity, Smoothness);
    }
}