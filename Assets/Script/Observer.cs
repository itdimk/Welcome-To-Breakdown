using System;
using UnityEngine;

namespace Itdimk
{
    public class Observer : MonoBehaviour
    {
        public Transform Direction;
        public Transform Origin;

        public TargetSetterBase Target;

        public float RotationSpeed = 1.0F;

        public float MaxAngle = 90F;
        public float MinAngle = 0F;

        public float Error = 0.01f;

        private float _thresholdAngle;
        private bool _mirror;

        public bool LimitAngle = true;
        public bool FlipSupport = false;
        public Transform FlipSupportProvider;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (FlipSupport)
                _mirror = FlipSupportProvider.rotation.y < 0;

            float currAngle = GetCurrentAngle();
            float targetAngle = GetTargetAngle();
            float rotationZ = GetRotationZ(currAngle, targetAngle);

            Rotate(currAngle, targetAngle, rotationZ);
        }

        private float GetCurrentAngle()
        {
            Vector2 currDirection = Direction.position - Origin.position;
            return Mathf.Atan2(currDirection.y, currDirection.x) * Mathf.Rad2Deg;
        }

        private float GetTargetAngle()
        {
            if (Target.GetTargetPos() == Vector3.zero)
                return GetCurrentAngle();
            
            Vector2 targetDirection = Target.GetTargetPos() - Origin.position;
            return LimitRotationIfRequired(Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg);
        }

        private void Rotate(float currAngle, float targetAngle, float rotationZ)
        {
            var axis = new Vector3(0, 0, 1);

            if (GetAngleDelta(_thresholdAngle, targetAngle) > Error)
            {
                transform.RotateAround(Origin.position, axis, rotationZ);

                if (Mathf.Abs(targetAngle - currAngle) <= Error)
                    _thresholdAngle = targetAngle;
            }
        }

        private float GetRotationZ(float currAngle, float targetAngle)
        {
            float rotationSpeed = Mathf.Min(GetAngleDelta(currAngle, targetAngle), RotationSpeed);

            if (currAngle - targetAngle > 180 || targetAngle - currAngle > 180)
                return Math.Sign(currAngle) * rotationSpeed;
            else
                return Mathf.Sign(targetAngle - currAngle) * rotationSpeed;
        }


        private float LimitRotationIfRequired(float rotation)
        {
            if (!LimitAngle)
                return rotation;

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
            if (angle1 < 0 || angle2 < 0)
            {
                angle1 = To360(angle1);
                angle2 = To360(angle2);
            }
            else
            {
                angle1 %= 360;
                angle2 %= 360;
            }

            float delta1 = Mathf.Abs(angle1 - angle2);
            float delta2 = 360 - delta1;

            return Mathf.Min(delta1, delta2);
        }
    }
}