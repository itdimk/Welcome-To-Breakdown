using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itdimk
{
    public class Checkpoint : MonoBehaviour
    {
        public CheckpointManager Manager;

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
            if (other.gameObject == Manager.Player)
                Manager.SaveData(this);
        }
    }
}