using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;

    private int crutch = 1;

    public GameObject projectile;
    public Transform shotPoint;
    

    private float timeBtwShots;
    public float startTimeBtwShots;
    private void FixedUpdate()
    {
        
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ =  Mathf.Atan2(crutch *  difference.y, crutch *  difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                FindObjectOfType<AudioManager>().Play("Shooting");
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
        Crutch();
    }
    
    private void Crutch()
    {
        if (Input.mousePosition.x < transform.position.x && Input.GetKey("d"))
        {
            crutch = -1;
        }
        if (Input.mousePosition.x > transform.position.x &&  Input.GetKey("a"))
        {
            crutch = 1;
        }
    }
}
