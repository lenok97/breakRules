using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace BreakRules
//{
    public class Monster :Unit
    {

        void OnEnable()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Bullet bullet = collider.GetComponent<Bullet>();

            if (bullet && bullet.Parent != this)
            {
                ReceiveDamage();
            }

            Player character = collider.GetComponent<Player>();
            if (character)
            {
                character.ReceiveDamage();
            }
        }
    }
//}
