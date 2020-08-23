using UnityEngine;

public class PreemptionTargetProviderD : TargetProviderBaseD
{
    public Rigidbody2D Target;
    public float Scale = 0.04f;
    
    private GameObject PreemptionMarker;
    
    // Start is called before the first frame update
    void Start()
    {
        PreemptionMarker = new GameObject("PreemptionMarker");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Transform GetTarget()
    {
        Vector2 targetPos = Target.gameObject.transform.position;
        Vector2 myPos = transform.position;
        
        float distance = Vector2.Distance(targetPos, myPos);
        
        PreemptionMarker.transform.position =  targetPos + Target.velocity * (distance * Scale);
        return PreemptionMarker.transform;
    }
}
