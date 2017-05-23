using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCommand : MonoBehaviour, ICommand
{
    Transform unitTransform;
    Unit unit;
    Weapon weapon;

    private void Start()
    {
        Controller.Instance.AddCommand(this);
        unit = FindObjectOfType<Player>();
        weapon = gameObject.AddComponent<Weapon>() as Weapon;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ChangeShooting"))
        {
            Inventory.ChangeActiveWeapon();
        }
        else
            if (Input.GetButtonDown("Fire1"))
                
                if (Inventory.cartridge.ContainsKey(Inventory.activeWeapon) 
                && Inventory.cartridge[Inventory.activeWeapon]>0)
                {
                    Inventory.cartridge[Inventory.activeWeapon]--;
                    weapon.Shoot(unit, Inventory.activeWeapon);
                    Debug.Log(Inventory.activeWeapon.ToString()+Inventory.cartridge[Inventory.activeWeapon]);
                }
    }

    public void TakeUnit(Unit unit)
    {
        this.unit = unit;
    }

}
