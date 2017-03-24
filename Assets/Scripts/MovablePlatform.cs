using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector2 positionA;

    [SerializeField]
    private Vector2 positionB;

    private bool movingToPointB;
    private Vector2 direction;
    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        direction = positionB - positionA;
        movingToPointB = true;
    }

    private void FixedUpdate()
    {
        var magnitude = ((movingToPointB ? positionB : positionA) - (Vector2)transform.position).magnitude;
        if (magnitude < 0.5f)
        {
            movingToPointB = !movingToPointB;
            direction *= -1;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3)direction, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
