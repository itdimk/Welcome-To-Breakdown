using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float Speed;
    public float Acceleration;

    private Rigidbody2D _physics;

    private float _inputX, _inputY;


    // Start is called before the first frame update
    void Start()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 force = new Vector2(_inputX * Acceleration, _inputY * Acceleration);

        _physics.AddForce(force);

        if (_physics.velocity.magnitude > Speed)
            _physics.velocity = Vector2.ClampMagnitude(_physics.velocity, Speed);

    }

}