using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    public class ShootableMonster : Monster
    {
        [SerializeField]
        private float rate = 2.0F;
        
        [SerializeField]
        BulletType bulletType = BulletType.Laser;

        Weapon weapon;

        void OnEnable()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected void Start()
        {
            weapon = gameObject.AddComponent<Weapon>() as Weapon;
            InvokeRepeating("Shoot", rate, rate);
        }

        private void Shoot()
        {
            weapon.Shoot(this, bulletType);
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            Unit unit = collider.GetComponent<Unit>();
            if (unit && unit is Player)
            {
                if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F)
                    ReceiveDamage();
                else
                    unit.ReceiveDamage();
            }
        }
    }
}
