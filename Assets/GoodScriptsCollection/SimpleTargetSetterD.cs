using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTargetSetterD : TargetSetterD
{
    public Transform Target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Transform GetTarget()
    {
        return Target;
    }
}
