using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public Transform StartPos;

    public Transform EndPos;

    public GameObject Marker;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currDistance = Vector3.Distance(StartPos.position, EndPos.position);

        var all = Physics2D.RaycastAll(StartPos.position, EndPos.position - StartPos.position, 100);
        all = all.Where(a => !a.collider.isTrigger && a.collider.gameObject.tag == "Obstacle").ToArray();

        if (all.Length > 0)
        {
            float distance = Vector2.Distance(StartPos.position, all.First().point);

            float multipiler = distance / currDistance;
            
            Marker.transform.position = all.First().point;


            transform.localScale = new Vector3(distance * 0.0299f, 0.5f, 1);
        }
    }
}