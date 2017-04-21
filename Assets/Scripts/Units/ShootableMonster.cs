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
        private Bullet bullet;
        [SerializeField]
        private GameObject screen;

        protected override void Awake()
        {
            bullet = Resources.Load<Laser>("Laser");
        }

        protected override void Start()
        {
            if (screen.GetComponent<Camera>().cullingMask == -1)
                InvokeRepeating("Shoot", rate, rate);

        }

        private void Shoot()
        {
            if (FindObjectOfType<Character>().rules.visibly == true)
            {
                Vector3 position = transform.position;
                position.y += 0.3F;

                Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
                newBullet.Parent = gameObject;
                newBullet.Direction = -newBullet.transform.right;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            Unit unit = collider.GetComponent<Unit>();
            if (unit && unit is Character)
            {
                if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F)
                    ReceiveDamage();
                else
                    unit.ReceiveDamage();
            }
        }
    }
}
