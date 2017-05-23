using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    private Bullet bullet;

    public void Shoot(Unit parent, BulletType type)
    {
        if (type == BulletType.Bomb)
            bullet = Resources.Load<Bomb>("Bomb");
        else

            if (type == BulletType.Laser)
                bullet = Resources.Load<Laser>("Laser");
            else

                if (type == BulletType.Stun)
                    bullet = Resources.Load<Stun>("Stun");
        
        var position = parent.transform.position;
        position.y += 0.3F;
        position.x += 0.2F;

        var newBullet = Instantiate(bullet, position, bullet.transform.rotation);
        newBullet.Parent = parent;
        newBullet.Direction = newBullet.transform.right * (parent.SpriteRenderer.flipX ? -1.0F : 1.0F);
    }

}
