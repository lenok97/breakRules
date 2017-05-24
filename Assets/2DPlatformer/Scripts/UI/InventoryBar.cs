using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryBar : MonoBehaviour
{
    private static Transform[] weapons = new Transform[Enum.GetValues(typeof(BulletType)).Length];

    [SerializeField]
    private Transform activeWeapon;


    private void Awake()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = transform.GetChild(i);
        }
        
    }

    public static void Refresh()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Inventory.cartridge.ContainsKey((BulletType)i) && 
                Inventory.cartridge[(BulletType)i] > 0 && (i == (int)Inventory.activeWeapon))
            {
               if (i == (int)Inventory.activeWeapon)
                weapons[i].gameObject.SetActive(true);
            }
            else
                weapons[i].gameObject.SetActive(false);
     
        }
    }

}

