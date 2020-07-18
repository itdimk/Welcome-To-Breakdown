using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Sprite UnpressedSprite;

    public Sprite PressedSprite;

    public string PlayerTag;

    public GameObject Door;

    public float MoveDoorDistanceY;
    public float MoveDoorDistanceX;

    private bool flag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = UnpressedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!flag && other.gameObject.CompareTag(PlayerTag))
        {
            GetComponent<SpriteRenderer>().sprite = PressedSprite;
            MoveDoor();
            flag = true;
        }
    }

    private void MoveDoor()
    {
        var pos = Door.transform.position;
        
        Door.transform.position = new Vector3(
            pos.x + MoveDoorDistanceX,
            pos.y + MoveDoorDistanceY);
    }
}
