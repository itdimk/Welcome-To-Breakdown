using System;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    private Camera _mainCamera;

<<<<<<< HEAD
    public float maxAngleUp = 90.0F;
    public float maxAngleDown = 90.0F;
    public float offset = 0.0F;
    
    public KeyCode MoveRightKey;
    public KeyCode MoveLeftKey;

    private bool _mirror = false;
    
=======
    public float maxAngle = 90.0F;
    public float minAngle = -90.0F;
    public float offset = 0.0F;

    public float smoothness = 1.0F;

    private bool left = false;

>>>>>>> 94ac9efec86d93cfb8b0f0c7b7e6e92ff5f98b71
    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

<<<<<<< HEAD
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

=======
        ApplyRotation(rotationZ);
    }

    private void ApplyRotation(float rotationZ)
    {
        Quaternion before = transform.rotation;
        Quaternion after = Quaternion.Euler(0f, 0f, LimitRotation(rotationZ) + offset);
        
>>>>>>> 94ac9efec86d93cfb8b0f0c7b7e6e92ff5f98b71
        transform.rotation = new Quaternion(after.x, after.y, after.z, after.w);
    }

    private float LimitRotation(float rotation)
    {
<<<<<<< HEAD
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

=======
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
        
    
        

        
>>>>>>> 94ac9efec86d93cfb8b0f0c7b7e6e92ff5f98b71
        return rotation;
    }
}