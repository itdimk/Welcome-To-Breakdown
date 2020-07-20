using System;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    private Camera _mainCamera;

    public float maxAngle = 90.0F;
    public float minAngle = -90.0F;
    public float offset = 0.0F;

    public float smoothness = 1.0F;

    private bool left = false;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        ApplyRotation(rotationZ);
    }

    private void ApplyRotation(float rotationZ)
    {
        Quaternion before = transform.rotation;
        Quaternion after = Quaternion.Euler(0f, 0f, LimitRotation(rotationZ) + offset);
        
        transform.rotation = new Quaternion(after.x, after.y, after.z, after.w);
    }

    private float LimitRotation(float rotation)
    {
        if (transform.parent.transform.rotation.y < 0 && !left)
        {
            maxAngle = 270;
            minAngle = 90;
            
            left = true;
        }
        
        if (transform.parent.transform.rotation.y >= 0)
        {
            maxAngle = 90;
            minAngle = -90;
            
            left = false;
        }

        if (left)
        {
            float rotation360 = rotation > 0 ? rotation : rotation + 360;
      
            Debug.Log(rotation360);
            
            if (rotation360 > maxAngle)
                return -90;

            if (rotation360 < minAngle)
                return 90;
        }
        else
        {
            if (rotation > maxAngle)
                return maxAngle;

            if (rotation < minAngle)
                return minAngle;
        }
        
    
        

        
        return rotation;
    }
}