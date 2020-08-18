using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class RayEmitterD : MonoBehaviour
{
    public string[] Obstacles;

    public float MaxDistance = 15;
    public int MaxHits = 15;
    private float _scaleMultiplier = -1;
    private RaycastHit2D[] _hitsBuffer;

    // Start is called before the first frame update
    void Start()
    {
        _hitsBuffer = new RaycastHit2D[MaxHits];
        _scaleMultiplier = CalcScaleMultiplier();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var tr = transform;
        int hitsCount = Physics2D.RaycastNonAlloc(tr.position, tr.right, _hitsBuffer, MaxDistance);
        var hit = _hitsBuffer.Take(hitsCount).FirstOrDefault(h
            => !h.collider.isTrigger && Obstacles.Contains(h.collider.tag));

        float distance = hit != default ? hit.distance : MaxDistance;
        SetScale(distance, _scaleMultiplier);
    }

    void SetScale(float distance, float multiplier)
    {
        var tr = transform;
        var trScale = tr.localScale;
        tr.localScale = new Vector3(distance * multiplier, trScale.y, trScale.z);
    }

    private float CalcScaleMultiplier()
    {
        var tr = transform;
        int hitsCount = Physics2D.RaycastNonAlloc(tr.position, tr.right, _hitsBuffer, MaxDistance);
        var hit = _hitsBuffer.Take(hitsCount).FirstOrDefault(h
            => !h.collider.isTrigger && Obstacles.Contains(h.collider.tag));

        if (hit != default)
            return tr.localScale.x / hit.distance;
        else
        {
            Debug.LogWarning("Ray must touch an obstacle to determine length of it");
            return 1f;
        }
    }
}