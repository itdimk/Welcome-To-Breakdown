using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public bool isOnLadder = false;

    public float runSpeed = 40f;
    public int Health = 100;
    private float horizontalMove = 0f;

    private bool jump = false;
    private bool Crouch = false;
    public Sprite HitSprite;
    

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (!isOnLadder && Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }


        if (Input.GetButtonDown("Crouch"))
        {
            Crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        if (!jump)
            FixClimb();

        controller.Move(horizontalMove * Time.fixedDeltaTime, Crouch, jump);
        jump = false;
    }

    void FixClimb()

    {
        GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 20));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Scary"))
        {
            GameObject.FindWithTag("Scary2").gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }

        if (other.gameObject.CompareTag("Secrets1"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Secrets2"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("EnemyBox"))
        {
            GetDamage(10f, other);
            Health -= 10;
            GetComponent<SpriteRenderer>().sprite = HitSprite;
        }
        if (other.gameObject.CompareTag("Spikes"))
        {
            Health -= 10;
            GetComponent<SpriteRenderer>().sprite = HitSprite;
        }
        if (other.gameObject.CompareTag("Chest"))
        {
            
        }
        
        if(Health <= 0)
            Die();
    }
    

    private void GetDamage(float damageAmount, Collider2D source)
    {
        Health -= 10;
       
        var physics = GetComponent<Rigidbody2D>();
        var forceVector = new Vector2((source.transform.position.x - transform.position.x) * -3000, 
            (source.transform.position.y - transform.position.y) * -3000);
        
        physics.AddForce(forceVector);
        Debug.Log(forceVector);
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;

            if (Input.GetKey(KeyCode.Space))
                GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 600));
        }
        
        if (other.gameObject.CompareTag("EnemyBox"))
        {
            GetDamage(10f, other);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Scary"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = false;
        }
    }
}