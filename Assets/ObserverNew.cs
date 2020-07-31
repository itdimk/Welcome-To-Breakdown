using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public enum ModeNew
{
    LookAtMouse,
    LookAtTarget,
}

public class ObserverNew : MonoBehaviour
{
    public Transform Target;
    public Transform Tip;
    public Transform Origin;
    
    public ModeNew Mode;

    public float RotationSpeed = 1.0F;

    public bool LimitAngle = true;
    public float MaxAngle = 90F;
    public float MinAngle = 90F;

    public float Accuracy = 1.0f;

    private Camera _mainCamera;
    private float _thresholdAngle;
    private bool _mirror;

    public bool FlipSupport = false;
    public Transform FlipSupportProvider;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FlipSupport)
            _mirror =FlipSupportProvider.rotation.y < 0;

        float currAngle = GetCurrentAngle();
        float targetAngle = GetTargetAngle();
        float rotationZ = GetRotationZ(currAngle, targetAngle);

        Rotate(currAngle, targetAngle, rotationZ);
    }

    private float GetCurrentAngle()
    {
        Vector2 currDirection = Tip.position - Origin.position;
        return Mathf.Atan2(currDirection.y, currDirection.x) * Mathf.Rad2Deg;
    }

    private float GetTargetAngle()
    {
        Vector2 targetDirection = Vector2.zero;

        switch (Mode)
        {
            case ModeNew.LookAtMouse:
                targetDirection = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - Origin.position;
                break;
            case ModeNew.LookAtTarget:
                targetDirection = Target.position - Origin.position;
                break;
        }

        if(LimitAngle)
            return LimitRotation(Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg);
        else
            return  Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    }

    private void Rotate(float currAngle, float targetAngle, float rotationZ)
    {
        var axis = new Vector3(0, 0, 1);

        if (GetAngleDelta(_thresholdAngle, targetAngle) > Accuracy)
        {
            transform.RotateAround(Origin.position, axis, rotationZ);

            if (Mathf.Abs(targetAngle - currAngle) <= Accuracy)
                _thresholdAngle = targetAngle;
        }
    }

    private float GetRotationZ(float currAngle, float targetAngle)
    {
        float rotationSpeed = Mathf.Min(Math.Abs(currAngle - targetAngle), RotationSpeed);

        if (currAngle - targetAngle > 180 || targetAngle - currAngle > 180)
            return Math.Sign(currAngle) * rotationSpeed;
        else
           return Mathf.Sign(targetAngle - currAngle) * rotationSpeed;
    }

    
    private float LimitRotation(float rotation)
    {
        float maxAngle = _mirror ? (MaxAngle + 180) % 360 : MaxAngle;
        float minAngle = _mirror ? (MinAngle + 180) % 360 : MinAngle;

        rotation = To360(rotation);

        if (!IsBetween(rotation, minAngle, maxAngle))
        {
            rotation = GetNearestAngle(rotation, minAngle, maxAngle);
        }

        return To180(rotation);
    }

    private float To360(float angle) => angle < 0 ? angle + 360 : angle;
    private float To180(float angle) => angle > 180 ? angle - 360 : angle;

    private bool IsBetween(float targetAngle, float min, float max)
    {
        if (min > max)
        {
            float tmp = min;
            min = max;
            max = tmp;
        }

        if (max - min < 180)
            return targetAngle >= min && targetAngle <= max;
        else
            return !(targetAngle >= min && targetAngle <= max);
    }
    
    private float GetNearestAngle(float target, float candidate1, float candidate2)
    {
        float delta1 = GetAngleDelta(target, candidate1);
        float delta2 = GetAngleDelta(target, candidate2);

        return delta1 < delta2 ? candidate1 : candidate2;
    }
    
    private float GetAngleDelta(float angle1, float angle2)
    {
        angle1 %= 360;
        angle2 %= 360;

        float delta1 = Mathf.Abs(angle1 - angle2);
        float delta2 = 360 - delta1;

        return Mathf.Min(delta1, delta2);
    }

}


/*
private float LimitRotation(float rotation)
{
    if (!_mirror)
        return Mathf.Clamp(rotation, -MinAngle, MaxAngle);

    if (rotation > 0)
        return Mathf.Clamp(rotation, 180 - MaxAngle, 180);

    return Mathf.Clamp(rotation, -180, -180 + MinAngle);
}

private float LimitRotation2(float rotation)
{
    float maxAngle = _mirror ? (MaxAngle + 180) % 360 : MaxAngle;
    float minAngle = _mirror ? (MinAngle + 180) % 360 : MinAngle;
    
    if (rotation < 0)
        rotation += 360;

    if (Math.Abs(maxAngle - minAngle) < 180)
    {
        float avg = ((maxAngle + minAngle) / 2f + 180) % 360f;

        if (rotation >= avg && rotation < minAngle)
            rotation = minAngle;

        if (rotation <= avg && rotation >= 0)
        {
            rotation = maxAngle;
        }
    }
    else
    {
        float avg = (maxAngle + minAngle) / 2f;

        if (rotation <= avg && rotation > maxAngle)
            rotation = maxAngle;

        if (rotation > avg && rotation < minAngle)
            rotation = minAngle;
    }
    
    
    if (rotation > 180)
        rotation -= 360;

    return rotation;
}
*/