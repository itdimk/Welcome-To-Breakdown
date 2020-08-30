using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _mousePosition;
    public bool HideCursor;
    
    // Start is called before the first frame update
    void OnEnable()
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
        transform.position = position;
   }
}
