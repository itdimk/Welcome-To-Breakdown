using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public string PlayerTag = "Player";
    public int Amount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            other.gameObject.GetComponent<PlayerMovement>().Cure(Amount);
            Destroy(gameObject);
        }
    }
}
