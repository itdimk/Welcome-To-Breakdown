using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itdimk
{
    public class SimpleTargetSetter : TargetSetterBase
    {
        public bool UseMouseAsTarget;
        public Transform Target;
        
        private Camera _mainCamera;
        

        public override Vector3 GetTargetPos()
        {
            if (UseMouseAsTarget)
                return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            else
                return Target.position;

        }

        // Start is called before the first frame update
        void Awake()
        {
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}