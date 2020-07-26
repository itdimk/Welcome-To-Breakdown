using System;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;


public class WaypointMovementWoPhys : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    public bool Cycled = false;
    public float Speed;
    public float IsReachedDistance = 0.1F;
    public bool FreezeX = false;
    public bool FreezeY = false;
    public float Smoothness = 0.1f;

    private int _currWaypointIndex;
    private bool isReached = false;
    private Vector3 _currVelocity;

    void Start()
    {
    }


    private void FixedUpdate()
    {
        NextWpIfRequired();
        MoveToCurrentWp();
    }

    private void MoveToCurrentWp()
    {
        var nextWpPos = waypoints[_currWaypointIndex].position;
        var pos = transform.position;
        nextWpPos.z = pos.z = 0;

        if (FreezeX) nextWpPos.x = pos.x;
        if (FreezeY) nextWpPos.y = pos.y;

        transform.position = Vector3.SmoothDamp(pos, nextWpPos,
            ref _currVelocity, Smoothness, Speed);

        NextWpIfRequired();
    }

    private void NextWpIfRequired()
    {
        var nextWpPos = waypoints[_currWaypointIndex].position;
        var pos = transform.position;

        if (IsReached(pos, nextWpPos))
        {
            if (_currWaypointIndex < waypoints.Count - 1)
                ++_currWaypointIndex;
            else if (Cycled)
                _currWaypointIndex = 0;
        }
    }

    private bool IsReached(Vector2 pos, Vector2 waypoint)
    {
        if (FreezeX) return Mathf.Abs(waypoint.y - pos.y) <= IsReachedDistance;
        if (FreezeY) return Mathf.Abs(waypoint.x - pos.x) <= IsReachedDistance;
        return Vector2.Distance(pos, waypoint) <= IsReachedDistance;
    }
}