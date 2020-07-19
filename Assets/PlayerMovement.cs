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
    public float Health = 100f;
    private float horizontalMove = 0f;

    private bool jump = false;
    private bool Crouch = false;
    public Sprite HitSprite;
    public GameObject EndLevelScreen;
    public int EndLevelScreenTime = 3000;

    public TextMeshProUGUI HelthText;
    
    private Stopwatch delayedSceneLoadTimer = new Stopwatch();

    public float LadderMaxSpeed = 12;
    
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
        if(Health <= 0)
            Die();
        
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

    public void Cure(float cureAmount)
    {
        Health += cureAmount;
        HelthText.text = Health.ToString();
    }

    private void GetDamage(float damageAmount, Collider2D source)
    {
        Health -= damageAmount;
       
        var physics = GetComponent<Rigidbody2D>();
        var forceVector = new Vector2((source.transform.position.x - transform.position.x) * -3000, 
            (source.transform.position.y - transform.position.y) * -3000);
        
        physics.AddForce(forceVector);
        HelthText.text = Health.ToString();
        FindObjectOfType<AudioManager>().Play("PlayerHit");

        if (Health <= 20 && Health > 10)
        {
            FindObjectOfType<AudioManager>().Play("PlayerPizda");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;

            if (Input.GetKey(KeyCode.Space))
            {
                if(GetComponent<Rigidbody2D>().velocity.y < LadderMaxSpeed)
                    GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 650));
                
                if(GetComponent<Rigidbody2D>().velocity.y < 0)
                    GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 650));
            }
            else
            {
                if(GetComponent<Rigidbody2D>().velocity.y <= -LadderMaxSpeed)
                    GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 580));
            }
        }
        
        if (other.gameObject.CompareTag("EnemyBox"))
        {
            Debug.Log(other);
            GetDamage(8f, other);
            GetComponent<SpriteRenderer>().sprite = HitSprite;
        }
        if (other.gameObject.CompareTag("Spikes"))
        {
            GetDamage(10f, other);
            GetComponent<SpriteRenderer>().sprite = HitSprite;
        }
        if (other.gameObject.CompareTag("EnemyEye"))
        {
            GetDamage(4, other);
            GetComponent<SpriteRenderer>().sprite = HitSprite;
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