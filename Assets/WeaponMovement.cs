using System;
using UnityEngine;

namespace Scripts
{
    public class WeaponMovement : MonoBehaviour
{
    private Camera _mainCamera;

    public float maxAngleUp = 90.0F;
    public float maxAngleDown = 90.0F;
    public float offset = 0.0F;

    public KeyCode MoveRightKey;
    public KeyCode MoveLeftKey;

    private bool _mirror = false;
    
    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Input.GetKey(MoveRightKey))
            _mirror = false;

        if (Input.GetKey(MoveLeftKey))
            _mirror = transform;
        
        if(_mirror)
            ApplyRotationMirror(rotationZ);
        else
            ApplyRotation(rotationZ);
    }

    
    private void ApplyRotationMirror(float rotationZ)
    {
        Quaternion after = Quaternion.Euler(0f, 0f, LimitRotationMirror(rotationZ) + offset);
        
        transform.rotation = new Quaternion(after.x, after.y, after.z, after.w);
        transform.Rotate(180, 0, 0);
       
    }
    
    private void ApplyRotation(float rotationZ)
    {
        Quaternion after = Quaternion.Euler(0f, 0f, LimitRotation(rotationZ) + offset);

        transform.rotation = new Quaternion(after.x, after.y, after.z, after.w);
    }

    private float LimitRotation(float rotation)
    {
        if (rotation > maxAngleUp)
            return maxAngleUp;

        if (rotation < -maxAngleDown)
            return -maxAngleDown;

        return rotation;
    }

    private float LimitRotationMirror(float rotation)
    {
        float maxAngleUp_ = 180 - maxAngleUp;
        float maxAngleDown_ = -180 + maxAngleDown;


        if (rotation < maxAngleUp_ && rotation > maxAngleDown_)
        {
            float deltaUp = Mathf.Abs(maxAngleUp_ - rotation);
            float deltaDown = Mathf.Abs(maxAngleDown_ - rotation);

            rotation = deltaUp < deltaDown ? maxAngleUp_ : maxAngleDown_;
        }

        return rotation;
    }
}
}
