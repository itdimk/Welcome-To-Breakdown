using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpMovement : MonoBehaviour
{
    public List<Vector3> waypoints = new List<Vector3>();

    public bool Cycled = false;
    public float Speed;
    public float IsReachedDistance = 1.0F;

    private int _currWaypointIndex;
    private Rigidbody2D _physics;


    // Start is called before the first frame update
    void Start()
    {
        _physics = GetComponent<Rigidbody2D>() ??
                   throw new NullReferenceException($"Can't get {nameof(Rigidbody2D)}");
    }


    private void FixedUpdate()
    {
        MoveToCurrentWp();
        NextWpIfRequired();
    }

    private void MoveToCurrentWp()
    {
        var nextWpPos = waypoints[_currWaypointIndex];
        var pos = transform.position;
        Debug.Log(transform.position);
        nextWpPos.z = pos.z = 0;
        
        var direction = nextWpPos - pos;
        
        float scale = Speed / direction.magnitude;
        direction.Scale(new Vector2(scale, scale));
        
        _physics.velocity = direction;
    }

    private void NextWpIfRequired()
    {  
        var nextWpPos = waypoints[_currWaypointIndex];
        var pos = transform.position;

        nextWpPos.z = pos.z = 0;
        
        if ((nextWpPos - pos).magnitude <= IsReachedDistance)
        {
            if (_currWaypointIndex < waypoints.Count - 1)
                ++_currWaypointIndex;
            else if (Cycled)
                _currWaypointIndex = 0;
        }
    }
}
