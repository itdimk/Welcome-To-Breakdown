using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBulletLegitWorkPls : MonoBehaviour
{
   public float speed;
   public float lifetime;

   private void Start()
   {
      Invoke("DestroyProjectile", lifetime);
   }

   private void FixedUpdate()
   {
      transform.Translate(transform.right * speed * Time.deltaTime);
   }

   void DestroyProjectile()
   {
      Destroy(gameObject);
   }
}
