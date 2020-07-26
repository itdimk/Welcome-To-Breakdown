using System;
using UnityEngine;

public class AltWeaponMovement : MonoBehaviour
{
    private Camera _mainCamera;

    public float maxAngleUp = 90.0F;
    public float maxAngleDown = 90.0F;

    private bool _mirror = false;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (transform.parent.rotation.y > 0)
            _mirror = false;

        if (transform.parent.rotation.y < 0)
            _mirror = true;

        if (_mirror)
        {
            if (rotationZ >= 0 && rotationZ <= 180)
                rotationZ -= 180;
            else
                rotationZ += 180;
        }
        
        ApplyRotation(rotationZ);
        
        if (_mirror)
            transform.Rotate(0, -180, 0);
    }


    private void ApplyRotation(float rotationZ)
    {
        Quaternion after = Quaternion.Euler(0f, 0f, LimitRotation(rotationZ));
        transform.rotation = new Quaternion(after.x, after.y, after.z, after.w);
    }
    

    private float LimitRotation(float rotation)
    {
        float maxAngleUp = _mirror ? this.maxAngleDown : this.maxAngleUp;
        float maxAngleDown = _mirror ? this.maxAngleUp : this.maxAngleDown;
        
        
        if (rotation > maxAngleUp)
            return maxAngleUp;

        if (rotation < -maxAngleDown)
            return -maxAngleDown;

        return rotation;
    }
}