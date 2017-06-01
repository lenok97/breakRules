using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MovableMonster : Monster
{

    private Vector3 direction;

    void OnEnable()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
       Move();
    }

    protected void Start()
    {
        direction = transform.right;
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (transform.position + 0.8F * (transform.up + transform.right * direction.x), 0.1F);

        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Player>()))
            direction *= -1.0F;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, MaxSpeed * Time.deltaTime);
        spriteRenderer.flipX = direction.x > 0;
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
