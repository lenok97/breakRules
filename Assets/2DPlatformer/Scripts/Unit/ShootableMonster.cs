using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster
{
    [SerializeField]
    private float rate = 2.0F;
        
    [SerializeField]
    BulletType bulletType = BulletType.Laser;

    Shooting weapon;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        weapon = gameObject.AddComponent<Shooting>() as Shooting;
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        weapon.Shoot(this, bulletType);
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        Unit unit = coll.collider.GetComponent<Unit>();

        if (unit && unit is Player)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F)
                ReceiveDamage();
            else
                unit.ReceiveDamage();
        }
    }
}

