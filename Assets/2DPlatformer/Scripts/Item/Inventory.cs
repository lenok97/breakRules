using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    //доступное оружие: тип, количество снарядов
    public static Dictionary<BulletType, int> cartridge = new Dictionary<BulletType, int>();

    [SerializeField]
    private InventoryBar inventoryBar;

    [SerializeField]
    private static int maxItems = 20;

    public static int MaxItems { get { return maxItems; } }

    public static BulletType activeWeapon;
    public static List<BulletType> availableWeapon=new List<BulletType>();
    public static int selected = -1;
    
    public static void ChangeActiveWeapon()
    {
        selected++;
        if (selected == availableWeapon.Count)
            selected = 0;

        foreach (KeyValuePair<BulletType, int> cart in cartridge)
        {
            availableWeapon.Add(cart.Key);
        }

        if (availableWeapon.Count>0)
            activeWeapon = availableWeapon[selected];

    }

    private void Update()
    {
        if (cartridge.ContainsKey(activeWeapon)&&cartridge[activeWeapon]==0)
            ChangeActiveWeapon();
    }

    void OnGUI()
    {
        if (cartridge.ContainsKey(activeWeapon))
        {
            GUI.Label(new Rect(30, 40, 100, 50), cartridge[activeWeapon].ToString());
        }
        InventoryBar.Refresh();
    }
}

