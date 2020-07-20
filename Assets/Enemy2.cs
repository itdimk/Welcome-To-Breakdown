using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy2 : MonoBehaviour
{
    public int health = 100;
    [FormerlySerializedAs("DESTROY PARENT")] public bool destroyParent = false;
    
    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
         Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy( gameObject);
        
        if(destroyParent)
            Destroy(transform.parent.gameObject);
    }
}
