using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastMarker : MonoBehaviour
{
    public Transform From;
    public Transform To;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var all = Physics2D.RaycastAll(From.position, (To.position - From.position).normalized, 50);
        all = all.Where(a =>  a.collider.gameObject.tag == "Obstacle").ToArray();
        transform.position = all.First().point;

    }
}
