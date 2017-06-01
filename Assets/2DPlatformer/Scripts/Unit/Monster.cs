using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster :Unit
{

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        Player character = coll.collider.GetComponent<Player>();
        if (character)
        {
            character.ReceiveDamage();
        }
    }


}

