using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    public class MovablePlatform :MonoBehaviour
    {

        [SerializeField]
        private Vector2 positionA;

        [SerializeField]
        private Vector2 positionB;

        [SerializeField]
        private float maxSpeed = 1;

        private bool movingToPointB;
        private Vector2 direction;
        private Rigidbody2D rb2d;
        private Rigidbody2D rb2dUnit;
        private new Transform transform;

        private void Awake()
        {
            transform = GetComponent<Transform>();
            direction = positionB - positionA;
            movingToPointB = true;
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var magnitude = ((movingToPointB ? positionB : positionA) - (Vector2)transform.position).magnitude;
            if (magnitude < (direction.normalized*Time.fixedDeltaTime*maxSpeed).magnitude)
            {
                movingToPointB = !movingToPointB;
                direction *= -1;
            }
            rb2d.MovePosition(rb2d.position + direction.normalized * Time.fixedDeltaTime*maxSpeed);
        }
    }
}
