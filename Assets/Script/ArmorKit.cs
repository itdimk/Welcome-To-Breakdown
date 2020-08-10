using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Itdimk
{
    public class ArmorKit : MonoBehaviour
    {
        [FormerlySerializedAs("Amount")] public float amount;

        [FormerlySerializedAs("One use")] public bool oneUse = false;

        [FormerlySerializedAs("Who can use")] public List<string> users = new List<string>();

        private void Start()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (users.Contains(other.gameObject.tag))
            {
                var health = other.gameObject.GetComponent<HealthController>();

                if (health != null)
                {
                    health.Armor(amount);

                    if (oneUse)
                        SelfDestruct();
                }
                else
                    Debug.LogError($"Can't get {nameof(HealthController)} of {other.gameObject}");
            }
        }

        private void SelfDestruct()
        {
            Destroy(gameObject);
        }
    }
}