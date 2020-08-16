using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointCrutch : MonoBehaviour
{
    private float initY;
    public float OffsetX;
    public float OffsetY = 0f;
    public Transform center;
    
    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector2(center.position.x + OffsetX, center.position.y + OffsetY);
    }
}