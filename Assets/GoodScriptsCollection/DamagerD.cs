using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerD : MonoBehaviour
{
    public float Amount = 1.0F;
    public bool OnePunch = false;
    
    [Space]
    public bool DamageOnTrigger = false;
    public bool DamageOnCollision = true;

    [Space]
    public bool DamageOnStay;
    public bool DamageOnEnter = true;
    public bool DamageOnExit;

    [Space]
    public Transform PushOrigin;
    public float PushPower = 1000.0F;

    [Space]
    public List<string> Targets = new List<string> {"Player"};


    private void Start()
    {
        if (PushOrigin == null)
            PushOrigin = transform;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (DamageOnCollision && DamageOnEnter)
            DealDamageIfRequired(other.gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (DamageOnCollision && DamageOnExit)
            DealDamageIfRequired(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (DamageOnCollision && DamageOnStay)
            DealDamageIfRequired(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (DamageOnTrigger && DamageOnStay)
            DealDamageIfRequired(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DamageOnTrigger && DamageOnEnter)
            DealDamageIfRequired(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (DamageOnTrigger && DamageOnExit)
            DealDamageIfRequired(other.gameObject);
    }

    private void DealDamageIfRequired(GameObject target)
    {
        if (Targets.Contains(target.tag))
        {
            var healthCtr = target.GetComponent<HealthControllerD>();

            if (healthCtr != null)
            {
                if (OnePunch)
                    healthCtr.Die();
                else
                {
                    healthCtr.DealDamage(Amount);
                    Push(target);
                }
            }
        }
    }

    private void Push(GameObject other)
    {
        Vector3 originPos = PushOrigin.position;
        Vector3 target = other.transform.position;

        Vector3 forceVector = new Vector3(target.x - originPos.x, target.y - originPos.y).normalized;
        
        forceVector.Scale(new Vector3(PushPower, PushPower));

        var physics = other.GetComponent<Rigidbody2D>();

        if (physics != null)
            physics.AddForce(forceVector);
    }
}