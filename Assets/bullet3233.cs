using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet3233 : MonoBehaviour
{
    
    
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    
    void Start()
    {
        rb.velocity = transform.right * speed;

        /*var collider = GameObject.Find("PlaYeR").GetComponent<CapsuleCollider2D>();
        Debug.Log(GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider, true);*/

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        Enemy2 enemy2 = hitInfo.GetComponent<Enemy2>();
        if (enemy2 != null)
        {
            enemy2.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}