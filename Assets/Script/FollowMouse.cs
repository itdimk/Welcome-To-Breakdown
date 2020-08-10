using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itdimk
{
    public class FollowMouse : MonoBehaviour
    {
        private Camera _mainCamera;
        private Vector3 _mousePosition;
        public bool HideCursor;
        
        public float Smoothness = 0.05f;
        
        private Vector3 _velocity;
    
        
        void Start()
        {
            _mainCamera = Camera.main;
            Cursor.visible = !HideCursor;
        }

        private void Update()
        {
            _mousePosition = Input.mousePosition;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 position = _mainCamera.ScreenToWorldPoint(_mousePosition);
            transform.position = Vector3.SmoothDamp(transform.position, position, ref _velocity, 
                Smoothness) ;
        }
    }
}