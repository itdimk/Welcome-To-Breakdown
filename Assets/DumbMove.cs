using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbMove : MonoBehaviour
{
    public float MoveDownDistance = 1.0F;
    public float MoveRightDistance = 1.0F;
    public float speed = 0.1F;
    private Vector2 startPosition;

    private bool goingToLeft = false;
    private bool goingToUp = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (MoveRightDistance >= 0.01F && !goingToLeft && pos.x < startPosition.x + MoveRightDistance)
            transform.position = new Vector3(pos.x + speed, pos.y, transform.position.z);


        if (MoveDownDistance >= 0.01F && !goingToUp && pos.y < startPosition.y + MoveDownDistance)
            transform.position = new Vector3(pos.x, pos.y + speed, transform.position.z);


        if (goingToUp)
            transform.position = new Vector3(pos.x, pos.y - speed, transform.position.z);

        if (goingToLeft)
            transform.position = new Vector3(pos.x - speed, pos.y, transform.position.z);

        if (pos.x <= startPosition.x)
            goingToLeft = false;

        if (pos.y <= startPosition.y)
            goingToUp = false;

        if (pos.x >= startPosition.x + MoveRightDistance)
            goingToLeft = true;

        if (pos.y >= startPosition.y + MoveDownDistance)
            goingToUp = true;
    }
}