using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeapon : MonoBehaviour
{
    [SerializeField]
    private int number = 45;

    [SerializeField]
    BulletType type;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            if (Weapon.cartridge.ContainsKey(type))
                Weapon.cartridge[type] += number;
            else
            {
                Weapon.cartridge.Add(type, number);
            }
            Weapon.activeWeapon = type;
            Destroy(gameObject);
        }
    }
}
