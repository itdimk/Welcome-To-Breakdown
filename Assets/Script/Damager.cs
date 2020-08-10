using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Itdimk
{
    public class Damager : MonoBehaviour
    {
        [FormerlySerializedAs("Push power")] public float pushPower = 1000.0F;
        [FormerlySerializedAs("One punch")] public bool onePunch = false;

        [FormerlySerializedAs("Amount")] public float amount = 1.0F;

        public bool DamageOnTrigger = false;
        public bool DamageOnCollision = false;

        public bool DamageOnStay;
        public bool DamageOnEnter;
        public bool DamageOnExit;


        [FormerlySerializedAs("Who")] public List<string> who = new List<string>();


        private void Start()
        {

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (DamageOnCollision && DamageOnEnter)
                DealDamageIfRequired(other.gameObject);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (DamageOnCollision && DamageOnExit)
                DealDamageIfRequired(other.gameObject);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (DamageOnCollision && DamageOnStay)
                DealDamageIfRequired(other.gameObject);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (DamageOnTrigger && DamageOnStay)
                DealDamageIfRequired(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (DamageOnTrigger && DamageOnEnter)
                DealDamageIfRequired(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (DamageOnTrigger && DamageOnExit)
                DealDamageIfRequired(other.gameObject);
        }

        private void DealDamageIfRequired(GameObject target)
        {
            if (who.Contains(target.tag))
            {
                var healthCtr = target.GetComponent<HealthController>();

                if (healthCtr != null)
                {

                    if (onePunch)
                        healthCtr.Die();
                    else
                    {
                        healthCtr.DealDamage(amount);
                        Push(target);
                    }
                }
            }
        }

        private void Push(GameObject other)
        {
            Vector3 myPos = transform.position;
            Vector3 target = other.transform.position;

            Vector3 forceVector = new Vector3(target.x - myPos.x, target.y - myPos.y);

            float multiplier = pushPower / forceVector.magnitude;
            forceVector.Scale(new Vector3(multiplier, multiplier));

            var physics = other.GetComponent<Rigidbody2D>();

            if (physics != null)
                physics.AddForce(forceVector);
        }
    }
}