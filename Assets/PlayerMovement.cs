using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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
    public GameObject EndLevelScreen;
    public int EndLevelScreenTime = 3000;

    public TextMeshProUGUI HelthText;
    
    private Stopwatch delayedSceneLoadTimer = new Stopwatch();

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
        LoadNextSceneIfRequired();
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
           // GameObject.FindWithTag("Scary2").gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
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
        if (other.gameObject.CompareTag("EndLevel"))
        {
            delayedSceneLoadTimer.Restart();
        }

        if (other.CompareTag("EndLevel"))
        {
            EndLevelScreen.SetActive(true);
        }
        if (other.gameObject.CompareTag("EnemyEye"))
        {
            GetDamage(-5, other);
            GetComponent<SpriteRenderer>().sprite = HitSprite;
        }

        if(Health <= 0)
            Die();
    }

    private void LoadNextSceneIfRequired()
    {
        if (delayedSceneLoadTimer.IsRunning && delayedSceneLoadTimer.ElapsedMilliseconds >= EndLevelScreenTime)
        {
            delayedSceneLoadTimer.Stop();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            EndLevelScreen.SetActive(false);
        }
    }
    

    private void GetDamage(float damageAmount, Collider2D source)
    {
        Health -= 10;
       
        var physics = GetComponent<Rigidbody2D>();
        var forceVector = new Vector2((source.transform.position.x - transform.position.x) * -3000, 
            (source.transform.position.y - transform.position.y) * -3000);
        
        physics.AddForce(forceVector);
        HelthText.text = Health.ToString();
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