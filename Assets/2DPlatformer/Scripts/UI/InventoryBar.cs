using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryBar : MonoBehaviour
{
    private static Transform[] weapons = new Transform[Inventory.MaxItems];

    private void Awake()
    {
        for (int i = 0; i < Inventory.cartridge.Count; i++)
        {
            weapons[i] = transform.GetChild(i);
        }
    }

    public static void Refresh()
    {
        Debug.Log("inventorybar refresh");
        for (int i = 0; i < Inventory.availableWeapon.Count; i++)
        {
            if (Inventory.cartridge.ContainsKey(Inventory.availableWeapon[i]))
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
       // Refresh();
    }

}

