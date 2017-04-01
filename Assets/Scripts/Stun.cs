using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BreakRules
{
    public class Stun : Bullet
    {
        private float sizeChange;
        private Vector3 rotate = new Vector3(0, 0, 15);
        System.Random randomFactor = new System.Random();
        private float sizeFactor;
        private float actingTime = 5F;

        protected override void Update()
        {
            base.Update();
            transform.Rotate(rotate);
        }

        protected override void Start()
        {
            Destroy(gameObject, 3F);
            sizeFactor = randomFactor.Next(1, 20) * 0.02F;
            base.transform.localScale += new Vector3(sizeFactor, sizeFactor, 0);
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            Unit unit = collider.GetComponent<Unit>();

            if (unit && unit.gameObject != base.Parent)
            {
                Destroy(gameObject);
                unit.frozen = true;
                unit.Invoke("Unfroze", actingTime);
            }
        }
    }
}
