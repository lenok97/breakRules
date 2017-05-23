using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeapon : MonoBehaviour
{
    [SerializeField]
    private int number = 45;

    [SerializeField]
    BulletType type = BulletType.Bomb;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            if (player && Inventory.cartridge.ContainsKey(type))
                Inventory.cartridge[type] = +number;
            else
                Inventory.cartridge.Add(type, number);
            Inventory.activeWeapon = type;
            Destroy(gameObject);
        }
    }
}
