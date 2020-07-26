using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointCord : MonoBehaviour
{
    public Transform ConnectedObject;
    private LineRenderer _line;

    public Material Material;
    public float Width = 1.0F;

    public Joint2D Joint;

    // Start is called before the first frame update
    void Start()
    {
        _line.material = Material;
        _line.startWidth = Width;
        _line.endWidth = Width;
    }

    private void Awake()
    {
        _line = gameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_line != null)
        {
            _line.SetPositions(new[] {transform.position, ConnectedObject.transform.position});

            if (!TryGetComponent<Joint2D>(out _))
            {
                Destroy(_line);
                Destroy(this);
            }
        }
    }
}