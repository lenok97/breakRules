using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : Bullet
    {
        private Vector3 deviation = new Vector3(3, 0, 0);
        private float sizeChange;
        private Animator animator;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            Invoke("Boom", 0.7F);
            Destroy(gameObject, 0.8F);

        }

        protected override void Update()
        {
            base.Update();
            Sprite.flipX = Direction.x > 0;
        }

        private void Boom()
        {
            animator.SetBool("Boom", true);
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            Unit unit = collider.GetComponent<Unit>();

            if (unit && unit!= Parent)
            {
                unit.ReceiveDamage();
                unit.Rb2D.velocity *= 0.5f;
                Invoke("Boom", 0.4F);
                Destroy(gameObject, 0.5F);
            }
        }
    }
