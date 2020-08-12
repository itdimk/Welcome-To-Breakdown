using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public Transform StartPos;
    public string[] Tags;
    public Transform EndPos;
    
    private float _scaleMultipiler;
    private float _maxDistance;
    
    private RaycastHit2D[] _hitsBuffer = new RaycastHit2D[8];
    
    // Start is called before the first frame update
    void Start()
    {
        float currDistance = Vector3.Distance(StartPos.position, EndPos.position);
        float scaleX = transform.localScale.x;

        _scaleMultipiler = scaleX / currDistance;
        _maxDistance = currDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currDistance = Vector3.Distance(StartPos.position, EndPos.position);

        var direction = EndPos.position - StartPos.position;
        int hitsCount = Physics2D.RaycastNonAlloc(StartPos.position, direction, _hitsBuffer, _maxDistance);

        for (int i = 0; i < hitsCount; ++i)
        {
            var hitCollider = _hitsBuffer[i].collider;

            if (!hitCollider.isTrigger && Tags.Contains(hitCollider.gameObject.tag))
            {
                float distance = _hitsBuffer[i].distance;
                var currScale = transform.localScale;
                transform.localScale = new Vector3(distance * _scaleMultipiler,currScale.y, currScale.z);
                
                if(_hitsBuffer[i].collider.gameObject.name != "Ground")
                    Debug.Log(_hitsBuffer[i].collider.gameObject.name);
                return;
            }
        }
    }
}