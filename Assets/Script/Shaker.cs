using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itdimk
{
    public class Shaker : MonoBehaviour
    {
        public float Radius = 0.2f;
        public float Intensity = 50f;

        public Transform Center;

        private float _intensity;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_intensity-- <= 0)
            {
                transform.position = GetShakePoint();
                _intensity = (100 / Intensity);
            }
        }

        Vector3 GetShakePoint()
        {
            Vector2 currPos = Center.position;

            float randomRadius = Random.Range(-Radius, Radius);
            float randomAngle = Random.Range(0, Mathf.PI);

            Vector2 direction = new Vector2(
                randomRadius * Mathf.Cos(randomAngle),
                randomRadius * Mathf.Sin(randomAngle)
            );

            return currPos + direction;
        }
    }
}