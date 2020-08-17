using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itdimk
{
    public class PreemptionMarker : MonoBehaviour
    {
        public Rigidbody2D Target;
        public float Scale = 0.5f;
        public bool IgnoreY = false;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 targetPos = Target.gameObject.transform.position;

            Vector2 newPos = targetPos + (Target.velocity * Scale);

            if (IgnoreY)
                newPos.y = transform.position.y;
            
            transform.position = newPos;
        }
    }
}