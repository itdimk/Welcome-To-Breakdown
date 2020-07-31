using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;

    public GameObject ShootingEffect;
    public GameObject projectile;
    public Transform shotPoint;
    public Transform playEffectAt;

    public AudioManager Audio;
    
    private float timeBtwShots;
    public float startTimeBtwShots;
    private void FixedUpdate()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(ShootingEffect, playEffectAt.position, Quaternion.identity);
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
