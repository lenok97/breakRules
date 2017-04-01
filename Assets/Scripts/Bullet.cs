using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    public class Bullet : MonoBehaviour
    {
        private float speed = 10.0F;

        private Vector3 direction;
        public Vector3 Direction { set { direction = value; } get { return direction; } }

        private GameObject parent;
        public GameObject Parent { set { parent = value; } get { return parent; } }

        private SpriteRenderer sprite;

        protected virtual void Awake()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            Destroy(gameObject, 1.4F);
        }

        protected virtual void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Unit unit = collider.GetComponent<Unit>();

            if (unit && unit.gameObject != parent)
            {
                Destroy(gameObject);
            }
        }
    }
}
