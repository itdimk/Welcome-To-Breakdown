using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementD : MonoBehaviour
{
    public bool AirControl;

    public float Speed = 6f;
    public float Smoothness = 0.2f;
    public float JumpForce = 1000f;

    public LayerMask WhatIsGround;
    public Transform GroundCheck;

    private Collider2D[] _ground;

    private Vector2 _currVelocity;
    private float groundDetectionR = 0.1f;
    private bool _isFacingRight = true;

    private bool _jumpHasBeenPressed = false;
    private Rigidbody2D _physics;
    private float _inputX;

    
    public bool IsFalling { get; private set; }
    public bool IsGrounded { get; private set; }
    public float HorizontalSpeed { get; set; }

    void Start()
    {
        _physics = GetComponent<Rigidbody2D>()
                  ?? throw new NullReferenceException($"Can't get {nameof(Rigidbody2D)}");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            _jumpHasBeenPressed = true;
    }

    void FixedUpdate()
    {
        _ground = Physics2D.OverlapCircleAll(GroundCheck.position, groundDetectionR, WhatIsGround)
            .Where(c => !c.isTrigger && c.gameObject != gameObject).ToArray();

        IsGrounded = _ground.Length > 0;
        _inputX = Input.GetAxisRaw("Horizontal");

        Move();
        _jumpHasBeenPressed = false;
    }

    private void Move()
    {
        if (IsGrounded || AirControl)
        {
            Vector2 groundMovement = GetGroundMovement();
            Vector2 targetVelocity = new Vector2(_inputX * Speed, _physics.velocity.y);

            if (IsGrounded)
                targetVelocity += groundMovement;

            _physics.velocity = Vector2.SmoothDamp(_physics.velocity,
                targetVelocity, ref _currVelocity, Smoothness);

            if ((_inputX > 0 && !_isFacingRight) || (_inputX < 0 && _isFacingRight))
                Flip();
        }

        if (IsGrounded && _jumpHasBeenPressed)
        {
            var force = new Vector2(0, JumpForce);
            _physics.AddForce(force);
        }

        IsFalling = _physics.velocity.y < 0;
        HorizontalSpeed = Mathf.Abs(_inputX);
    }

    private Vector2 GetGroundMovement()
    {
        foreach (var groundCollider in _ground)
        {
            if (groundCollider.TryGetComponent(out Rigidbody2D physics))
                return physics.velocity;
        }

        return Vector2.zero;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180f, 0);
    }
}