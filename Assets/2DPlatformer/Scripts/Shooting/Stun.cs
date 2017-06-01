using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public class Stun : Bullet
    {
        [SerializeField]
        private float actingTime = 5F;
        private float sizeChange;
        private Vector3 rotate = new Vector3(0, 0, 15);
        System.Random randomFactor = new System.Random();
        private float sizeFactor;

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

            if (unit && unit!= Parent)
            {
                Destroy(gameObject);
                unit.Froze();
                unit.Invoke("Unfroze", actingTime);
            }
        }
    }

