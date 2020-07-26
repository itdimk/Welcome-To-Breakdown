using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;


    public GameObject projectile;
    public Transform shotPoint;

    public AudioManager Audio;
    
    private float timeBtwShots;
    public float startTimeBtwShots;
    private void FixedUpdate()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                Audio.Play("Shooting");
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
    }

}
