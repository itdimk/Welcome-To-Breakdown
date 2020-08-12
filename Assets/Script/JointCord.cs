using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itdimk
{
    public class JointCord : MonoBehaviour
    {
        public Transform ConnectedObject;
        private LineRenderer _line;

        public Material Material;
        public float Width = 1.0F;
        
        // Start is called before the first frame update
        void Start()
        {
            _line.material = Material;
            _line.startWidth = Width;
            _line.endWidth = Width;
        }

        private void Awake()
        {
            _line = gameObject.AddComponent<LineRenderer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_line != null)
            {
                _line.SetPositions(new[] {(Vector3)(Vector2)transform.position, (Vector3)(Vector2)ConnectedObject.transform.position});
            }
        }
    }
}