using UnityEngine;
using UnityEngine.Events;

public class SelfDestroyD : MonoBehaviour
{
    public float DestroyOnDelay = 1.0f;
    
    public GameObject DestroyEffect;
    public UnityEvent OnDestroy;

    private float startTick;
    
    // Start is called before the first frame update
    void Start()
    {
        startTick = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startTick + DestroyOnDelay < Time.time)
        {
            Destroy(gameObject);
            OnDestroy?.Invoke();
            
            if (DestroyEffect != null)
                Instantiate(DestroyEffect, transform.position, Quaternion.identity).SetActive(true);
        }
    }
}
