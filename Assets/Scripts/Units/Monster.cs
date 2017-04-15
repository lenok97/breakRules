﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    public class Monster : Unit
    {
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void Update() { }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Bullet bullet = collider.GetComponent<Bullet>();

            if (bullet && bullet.Parent != gameObject)
            {
                ReceiveDamage();
            }

            Character character = collider.GetComponent<Character>();
            if (character)
            {
                character.ReceiveDamage();
            }
        }
    }
}
