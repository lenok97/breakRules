using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{
    private Bullet bullet;

    public void Shoot(Unit parent, BulletType type)
    {
        switch (type)
        {
            case BulletType.Bomb:
                bullet = Resources.Load<Bomb>("Bomb");
                break;

            case BulletType.Laser:
                bullet = Resources.Load<Laser>("Laser");
                break;

            case BulletType.Stun:
                bullet = Resources.Load<Stun>("Stun");
                break;

        }
        
        var position = parent.transform.position;
        position.y += 0.3F;
        position.x += 0.2F;

        var newBullet = Instantiate(bullet, position, bullet.transform.rotation);
        newBullet.Parent = parent;
        newBullet.Direction = newBullet.transform.right * (parent.SpriteRenderer.flipX ? -1.0F : 1.0F);
    }

}
