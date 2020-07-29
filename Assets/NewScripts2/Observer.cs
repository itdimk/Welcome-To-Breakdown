using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode23
{
    LookAtMouse,
    LookAtTarget,
}

public class Observer : MonoBehaviour
{
    public Transform Target;
    public Transform Origin;
    public Transform Tip;

    public Mode23 Mode;

    public float RotationSpeed = 1.0F;

    public float MaxAngleUp = 90F;
    public float MaxAngleDown = 90F;

    public float Accuracy = 1.0f;

    private Camera _mainCamera;
    private float _thresholdAngle;
    private bool _mirror;

    public bool FlipSupport = false;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        
        if(Origin.parent != transform || Tip.parent != transform)
            Debug.LogWarning($"{nameof(Tip)} and {nameof(Origin)} should be children of this object");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FlipSupport)
            _mirror = transform.rotation.y < 0;

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
            case Mode23.LookAtMouse:
                targetDirection = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - Origin.position;
                break;
            case Mode23.LookAtTarget:
                targetDirection = Target.position - Origin.position;
                break;
        }

        return LimitRotation(Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg);
    }

    private void Rotate(float currAngle, float targetAngle, float rotationZ)
    {
        var axis = new Vector3(0, 0, 1);

        if (Mathf.Abs(_thresholdAngle - targetAngle) > Accuracy)
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
        if (!_mirror)
            return Mathf.Clamp(rotation, -MaxAngleDown, MaxAngleUp);

        if (rotation > 0)
            return Mathf.Clamp(rotation, 180 - MaxAngleUp, 180);

        return Mathf.Clamp(rotation, -180, -180 + MaxAngleDown);
    }

    private float GetAngleDelta(float angle1, float angle2)
    {
        return Mathf.Abs(angle1 - angle2);
    }
}