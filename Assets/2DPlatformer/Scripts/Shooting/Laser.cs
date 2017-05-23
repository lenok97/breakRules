using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public class Laser : Bullet
    {
        private float speed = 0.3F;
        private float sizeChange;
        Vector3 startPosition;

        protected override void Start()
        {
            Destroy(gameObject, 1.3F);
            startPosition = Parent.transform.position;
        }

        protected override void Update()
        {
            if (base.Parent != null)
            {
                startPosition = Parent.transform.position;
            }

            base.transform.localScale += new Vector3(speed, 0, 0);
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            sizeChange = boxCollider.size.x * transform.localScale.x * (base.Direction.x > 0 ? 1.0F : -1.0F);
            transform.position = new Vector3(startPosition.x + sizeChange / 2, startPosition.y, startPosition.z);
            Sprite.flipX = base.Direction.x < 0;

        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            Unit unit = collider.GetComponent<Unit>();

            if (unit && unit!= Parent)
            {
                Destroy(gameObject);
                unit.ReceiveDamage();
            }
        }
    }
