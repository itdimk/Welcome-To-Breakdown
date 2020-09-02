using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    public bool isOnLadder = false;

    public float runSpeed = 40f;
    public float Health = 100f;
    public float Armor = 0f;
    public float ArmorAbsorption = 0.9f;
    private float horizontalMove = 0f;

    private bool jump = false;
    private bool Crouch = false;
    public string HitAnimationName = "hit";
    public GameObject EndLevelScreen;
    public int EndLevelScreenTime = 3000;

    public TextMeshProUGUI HelthText;
    public TextMeshProUGUI ArmorText;
    public float HitPushPower = 3000;
    public UnityEvent OnDeath;
    private Stopwatch delayedSceneLoadTimer = new Stopwatch();

    private bool _pizdaPlayed = false;
    public float LadderMaxSpeed = 12;


    private void Start()
    {
        if(HelthText != null)
            HelthText.text = Mathf.Round(Health).ToString();
        
        if(ArmorText != null)
            ArmorText.text = Mathf.Round(Armor).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;


        if (!isOnLadder && Input.GetButtonDown("Jump"))
        {
            jump = true;
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


    void FixedUpdate()
    {
        if (Health <= 0)
            Die();

        if (!jump)
            FixClimb();

        jump = false;
        LoadNextSceneIfRequired();
    }

    void FixClimb()

    {
        GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 20));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GetDamage(4f, other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
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

            int activeIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.UnloadSceneAsync(activeIndex);
            SceneManager.LoadScene(activeIndex + 1);


            EndLevelScreen.SetActive(false);
        }
    }

    public void Cure(float cureAmount)
    {
        Health += cureAmount;

        if (HelthText != null)
            HelthText.text = Mathf.Round(Health).ToString();
    }

    public void ToArmor(float amount)
    {
        this.Armor += amount;

        if (ArmorText != null)
            ArmorText.text = Mathf.Round(Armor).ToString();
    }

    private void Push(GameObject other)
    {
        Vector3 myPos = other.transform.position;
        Vector3 target = transform.position;

        Vector3 forceVector = new Vector3(target.x - myPos.x, target.y - myPos.y);

        float multiplier = HitPushPower / forceVector.magnitude;
        forceVector.Scale(new Vector3(multiplier, multiplier));

        if (!double.IsNaN(forceVector.x) && double.IsNaN(forceVector.y))
        {
            var physics = GetComponent<Rigidbody2D>();

            if (physics != null)
                physics.AddForce(forceVector);
        }
    }

    private void GetDamage(float damageAmount, GameObject source)
    {
        damageAmount /= Time.deltaTime * 55;
       GetComponent<Animator>().Play(HitAnimationName);
        
        float absorbed = Math.Min(damageAmount * ArmorAbsorption, Armor * ArmorAbsorption);

      
        Health = Math.Max(0, Health - (damageAmount - absorbed));
        Armor = Math.Max(0, Armor - absorbed);

        Push(source.gameObject);

        if(HelthText != null)
            HelthText.text = Mathf.Round(Health).ToString();
        
        if(ArmorText != null)
            ArmorText.text = Mathf.Round(Armor).ToString();
        
        FindObjectOfType<AudioManager>().Play("PlayerHit");

        if (!_pizdaPlayed && Health <= 19)
        {
            FindObjectOfType<AudioManager>().Play("PlayerPizda");
            _pizdaPlayed = true;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        OnDeath.Invoke();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
     
        
        if (other.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;

            if (Input.GetKey(KeyCode.Space))
            {
                if (GetComponent<Rigidbody2D>().velocity.y < LadderMaxSpeed)
                    GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 650));

                if (GetComponent<Rigidbody2D>().velocity.y < 0)
                    GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 650));
            }
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.y <= -LadderMaxSpeed)
                    GetComponent<Rigidbody2D>()?.AddForce(new Vector2(0, 580));
            }
        }

        if (other.gameObject.CompareTag("EnemyBox"))
        {
            GetDamage(1.5f, other.gameObject);
        }

      

        if (other.gameObject.CompareTag("EnemyEye"))
        {
            GetDamage(1f, other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            GetDamage(2f, gameObject);
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