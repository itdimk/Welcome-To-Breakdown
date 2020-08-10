using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Itdimk
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        public bool AirControl;

        public float Speed = 6f;
        public float Smoothness = 0.2f;
        public float JumpForce = 1000f;

        public LayerMask WhatIsGround;
        public Transform IsGroundCheck;

        private Rigidbody2D _physics;
        private Collider2D[] _ground;

        private bool _isGrounded;
        private Vector2 _currVelocity;
        private float groundDetectionR = 0.1f;
        private bool _isFacingRight = true;

        private bool _jumpHasBeenPressed = false;

        private float _inputX;
        private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();

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
            _ground = Physics2D.OverlapCircleAll(IsGroundCheck.position, groundDetectionR, WhatIsGround)
                .Where(c => !c.isTrigger && c.gameObject != gameObject).ToArray();

            _isGrounded = _ground.Length > 0;
            _inputX = Input.GetAxisRaw("Horizontal");

            Move();
            SetAnimatorValues();
            _jumpHasBeenPressed = false;
        }

        private void Move()
        {
            if (_isGrounded || AirControl)
            {
                Vector2 groundMovement = GetGroundMovement();
                Vector2 targetVelocity = new Vector2(_inputX * Speed, _physics.velocity.y);

                if (_isGrounded)
                    targetVelocity += groundMovement;

                _physics.velocity = Vector2.SmoothDamp(_physics.velocity,
                    targetVelocity, ref _currVelocity, Smoothness);

                if ((_inputX > 0 && !_isFacingRight) || (_inputX < 0 && _isFacingRight))
                    Flip();
            }

            if (_isGrounded && _jumpHasBeenPressed)
            {
                var force = new Vector2(0, JumpForce);
                _physics.AddForce(force);
            }
        }

        private Vector2 GetGroundMovement()
        {
            foreach (var collider in _ground)
            {
                if (collider.TryGetComponent(out Rigidbody2D physics))
                    return physics.velocity;
            }

            return Vector2.zero;
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0, 180f, 0);
        }


        private void SetAnimatorValues()
        {

            _animator.SetFloat("SpeedX", Math.Abs(_inputX));

            _animator.SetBool("IsFalling", !_isGrounded && _physics.velocity.y < 0);

            _animator.SetBool("IsGrounded", _isGrounded);
        }
    }
}