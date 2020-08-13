using System.Linq;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public Transform StartPos;
    public string[] Tags;
    public Transform EndPos;
    
    private float _scaleMultiplier;
    private float _maxDistance;
    private readonly RaycastHit2D[] _hitsBuffer = new RaycastHit2D[10];
    
    // Start is called before the first frame update
    void Start()
    {
        float currDistance = Vector3.Distance(StartPos.position, EndPos.position);
        float scaleX = transform.localScale.x;

        _scaleMultiplier = scaleX / currDistance;
        _maxDistance = currDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var direction = (EndPos.position - StartPos.position).normalized;
        int hitsCount = Physics2D.RaycastNonAlloc(StartPos.position, direction, _hitsBuffer, _maxDistance);

        for (int i = 0; i < hitsCount; ++i)
        {
            var hitCollider = _hitsBuffer[i].collider;

            if (!hitCollider.isTrigger && Tags.Contains(hitCollider.gameObject.tag))
            {
                float distance = _hitsBuffer[i].distance;
                var currScale = transform.localScale;
                
                transform.localScale = new Vector3(distance * _scaleMultiplier, currScale.y, currScale.z);
                return;
            }
        }
    }
}