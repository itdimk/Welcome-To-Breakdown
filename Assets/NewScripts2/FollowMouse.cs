using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _mousePosition;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        Vector2 position = _mainCamera.ScreenToWorldPoint(_mousePosition);
        transform.position = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
     
   }
}
