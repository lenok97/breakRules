using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponBar : MonoBehaviour
{
    private static Transform[] weapons = new Transform[Enum.GetValues(typeof(BulletType)).Length];

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
            if (Weapon.cartridge.ContainsKey((BulletType)i) && 
                Weapon.cartridge[(BulletType)i] > 0 && (i == (int)Weapon.activeWeapon))
            {
               if (i == (int)Weapon.activeWeapon)
                weapons[i].gameObject.SetActive(true);
            }
            else
                weapons[i].gameObject.SetActive(false);
     
        }
    }

}

