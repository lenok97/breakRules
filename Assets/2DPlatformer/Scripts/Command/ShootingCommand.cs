using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCommand : MonoBehaviour, ICommand
{
    Transform unitTransform;
    Unit unit;
    Shooting shooting;

    private void Start()
    {
        Controller.Instance.AddCommand(this);
        unit = FindObjectOfType<Player>();
        shooting = gameObject.AddComponent<Shooting>() as Shooting;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ChangeShooting"))
        {
            Weapon.ChangeActiveWeapon();
        }
        else
            if (Input.GetButtonDown("Fire1"))
                
                if (Weapon.cartridge.ContainsKey(Weapon.activeWeapon) 
                && Weapon.cartridge[Weapon.activeWeapon]>0)
                {
                    shooting.Shoot(unit, Weapon.activeWeapon);
                    Weapon.cartridge[Weapon.activeWeapon]--;
                }
    }

    public void TakeUnit(Unit unit)
    {
        this.unit = unit;
    }

}
