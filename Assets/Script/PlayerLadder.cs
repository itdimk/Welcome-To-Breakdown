using System;
using UnityEngine;

namespace Itdimk
{
    public class PlayerLadder : MonoBehaviour
    {
        public string LadderTag = "Ladder";

        public float MaxLadderUpSpeed = 10.0F;
        public float MaxLadderDownSpeed = 10.0F;
        public float HorizontalMovementThreshold = 0.1f;

        private Rigidbody2D _physics;

        // Start is called before the first frame update
        void Start()
        {
            _physics = GetComponent<Rigidbody2D>() ??
                       throw new NullReferenceException(nameof(Rigidbody2D));
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(LadderTag) && CanMove())
            {
                if (Input.GetKey(KeyCode.Space))
                    MoveLadderUp();
                else
                    MoveLadderDown();
            }
        }

        private bool CanMove() => Math.Abs(Input.GetAxisRaw("Horizontal")) <= HorizontalMovementThreshold;

        private void MoveLadderUp()
            => _physics.velocity = new Vector2(_physics.velocity.x, MaxLadderUpSpeed);

        private void MoveLadderDown()
            => _physics.velocity = new Vector2(_physics.velocity.x, -MaxLadderDownSpeed);

    }
}