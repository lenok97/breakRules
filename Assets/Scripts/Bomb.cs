using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BreakRules
{
    public class Bomb : Bullet
    {
        private Vector3 deviation = new Vector3(3, 0, 0);
        private float sizeChange;

        private Animator animator;

        private UnitState.CharState State
        {
            get { return (UnitState.CharState)animator.GetInteger("State"); }
            set { animator.SetInteger("State", (int)value); }
        }

        private SpriteRenderer sprite;

        protected override void Awake()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            Destroy(gameObject, 0.6F);
            Invoke("Boom", 0.4F);
        }

        protected override void Update()
        {
            base.Update();
            sprite.flipX = base.Direction.x > 0;
        }

        private void Boom()
        {
            State = UnitState.CharState.Run;
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            Unit unit = collider.GetComponent<Unit>();

            if (unit && unit.gameObject != base.Parent)
            {
                unit.transform.position += deviation * (base.Direction.x > 0 ? 1.0F : -1.0F);
                Invoke("Boom", 0.2F);
                Destroy(gameObject, 0.3F);

            }
        }
    }
}
